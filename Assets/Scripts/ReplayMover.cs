using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
            ////todo comment: зачем нужны эти проверки?
            // Первая часть условия (!TryGetComponent) проверяет есть ли на объекте компонент PositionSaver. Без него нечего воспроизводить.
            // Вторая часть условия (_save.Records.Count == 0) проверяет, что в списке есть хотя бы одна запись. Если список пуст, то реплей невозможен.
            if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
                //todo comment: Для чего выключается этот компонент?
                // enabled = false останавливает вызовы Update(). Это получается как бы защита - если данных нет, то не надо гонять логику и тратить ресурсы.
                enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
            //todo comment: Что проверяет это условие (с какой целью)?
            // Условие (Time.time > curr.Time) проверяет наступило ли время соответствующее следующей точке траектории.
            // Как только текущее время перешагнуло отметку curr.Time, нужно перейти к этой точке и двигать объект дальше.
            if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
                //todo comment: Для чего нужна эта проверка?
                // Эта проверка (_index >= _save.Records.Count) нужна, чтобы понять, что все точки по списку пройдены.
                // Если индекс стал равен или больше количества записей, то реплей завершён. Отключаем компонент и пишем лог.
                if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
            //todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
            // Вычисляется delta — от 0 до 1 между двумя соседними точками.
            // (Time.time - _prev.Time) — сколько времени прошло с предыдущей точки.
            // (curr.Time - _prev.Time) — сколько всего времени отведено на переход между ними.
            // Результат (delta) используется как параметр t для Vector3.Lerp, чтобы плавно двигать объект между позициями.
            var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
            //todo comment: Зачем нужна эта проверка?
            // Эта проверка float.IsNaN(delta) как бы защищает от деления на ноль. Это возможно возникнет, если curr.Time и _prev.Time совпадут (интервал окажется 0).
            // Деление на ноль даёт NaN, а Lerp с NaN сломает позицию объекта. Замена на 0f сделает скажем так поведение безопасным.
            if (float.IsNaN(delta)) delta = 0f;
            //todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
            // transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
            // Здесь мы ставим текущую позицию объекта через линейную интерполяцию:
            // - _prev.Position — откуда двигается объект (предыдущая сохранённая точка).
            // - curr.Position — куда двигается объект (следующая сохранённая точка).
            // - delta — где именно на отрезке между ними находится объект (от 0 до 1).
            // Vector3.Lerp(a, b, t) возвращает точку на прямой между a и b:
            //  - при t = 0 вернётся a,
            //  - при t = 1 вернётся b,
            //  - при 0 < t < 1 — точка между ними.
            // По итогу объект скажем так плавно перемещается по траектории, следуя времени из Records.
            transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}