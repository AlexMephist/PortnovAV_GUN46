using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace DefaultNamespace
{
	public class PositionSaver : MonoBehaviour
	{
		[System.Serializable]
		public struct Data
		{
			public Vector3 Position;
			public float Time;
		}

		[SerializeField]
		private TextAsset _json;

		[SerializeField]
		[HideInInspector]
		public List<Data> Records { get; private set; }

		private void Awake()
		{
			//todo comment: Что будет, если в теле этого условия не сделать выход из метода?
			// Если убрать return, код продолжит выполнение и сразу вызовет JsonUtility.FromJsonOverwrite(_json.text, this).
			// Поскольку _json == null, то обращение к _json.text выбросит NullReferenceException — игра упадёт с ошибкой.
			// return здесь важен - он безопасно прервет инициализацию, отключит объект и выведит четкое сообщение.
			if (_json == null)
			{
				gameObject.SetActive(false);
				Debug.LogError("Please, create TextAsset and add in field _json");
				return;
			}

			JsonUtility.FromJsonOverwrite(_json.text, this);
			//todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
			// JsonUtility может оставить Records == null, если в JSON нет данных или структура не совпала.
			// Эта проверка скажем так гарантирует, что у нас всегда есть валидный список.
			// new List<Data>(10) — резервируем ёмкость на 10 элементов.
			if (Records == null)
				Records = new List<Data>(10);

		}

		private void OnDrawGizmos()
		{
			//todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
			// Records == null это защита от NullReferenceException, если список ещё не инициализирован.
			// Records.Count == 0 это защита от IndexOutOfRangeException при попытке извлечь data[0].
			// Эти проверки позволяют делать отрисовку Gizmos как бы безопасной.
			if (Records == null || Records.Count == 0) return;
			var data = Records;
			var prev = data[0].Position;
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(prev, 0.3f);
			//todo comment: Почему итерация начинается не с нулевого элемента?
			// Ноль уже обработан до цикла - взяли data[0] как за стартовую точку prev и нарисовали первую сферу.
			// Цикл рисует линии и следующие сферы между точками: он берёт текущую точку i, соединяет её с prev, затем обновляет prev = curr.
			for (int i = 1; i < data.Count; i++)
			{
				var curr = data[i].Position;
				Gizmos.DrawWireSphere(curr, 0.3f);
				Gizmos.DrawLine(prev, curr);
				prev = curr;
			}
		}

#if UNITY_EDITOR
		[Tooltip("To fill in the _json field, use the component’s context menu and the command \"Create File\".")]
		[ContextMenu("Create File")]
		private void CreateFile()
		{
			//todo comment: Что происходит в этой строке?
			// Создаётся (или перезаписывается) файл Path.txt в папке Assets проекта.
            // File.Create возвращает FileStream, который остаётся открытым, пока его не закроют.
			var stream = File.Create(Path.Combine(Application.dataPath, "Path.txt"));
			//todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав)
			// stream.Dispose() получается закрывает и освобождает файловый поток.
            // Если закомментировать эту строку, файл останется заблокированным ОС до завершения редактора Unity.
            // Это помешает корректному импорту ассетов, может вызвать ошибки в AssetDatabase.Refresh() и привести к тому,
            // что созданный файл не появится в проекте или будет помечен как «недоступный».
			stream.Dispose();
			UnityEditor.AssetDatabase.Refresh();
			//В Unity можно искать объекты по их типу, для этого используется префикс "t:"
			//После нахождения, Unity возвращает массив гуидов 
			var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
			foreach (var guid in guids)
			{
				//Этой командой можно получить путь к ассету через его гуид
				var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
				//Этой командой можно загрузить сам ассет
				var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
				//todo comment: Для чего нужны эти проверки?
				// asset != null это получается как бы защита на случай, если LoadAssetAtPath по какой-то причине вернул null.
                // asset.name == "Path" это чтобы присвоить только нужный ассет с именем Path, а не первый попавшийся TextAsset.
                // Без этой проверки можно случайно подцепить другой текстовый файл и сломать логику загрузки.
				if(asset != null && asset.name == "Path")
				{
					_json = asset;
					UnityEditor.EditorUtility.SetDirty(this);
					UnityEditor.AssetDatabase.SaveAssets();
					UnityEditor.AssetDatabase.Refresh();
					//todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
					// Мы уже нашли нужный ассет (Path) и присвоили его в _json. Дальше искать нет смысла - получили чего хотели.
                    // return здесь могу предположить сэкономит на лишних итерациях и предотвратит случайную перезапись _json другим ассетом,
                    // если вдруг в проекте появится ещё один TextAsset с таким же именем.
					return;
				}
			}
		}

		private void OnDestroy()
		{			
			//todo logic...
			if (_json != null)
				{
				string json = JsonUtility.ToJson(this, true);
            
				string relativePath = AssetDatabase.GetAssetPath(_json);
				string absolutePath = Path.Combine(Application.dataPath, relativePath);
            
				File.WriteAllText(absolutePath, json);
            
				AssetDatabase.Refresh();
            
				Debug.Log("The trajectory is saved in: " + _json.name, this);
        }
		}
#endif
    }

}