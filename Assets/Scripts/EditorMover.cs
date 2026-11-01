using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;

        //todo comment: Что произойдёт, если _delay > _duration?
        // Если _delay (интервал между записями) будет больше, чем _duration (общее время записи),
        // то за всё время работы кода (скрипта) успеет записаться 0 или 1 точка.
        // По итогу Records будет пустым или возможно почти пустым,
        // и реплей (если его потом запускать через ReplayMover) не покажет никакого движения.

        [SerializeField]
        [Range(0.2f, 1.0f)]
        private float _delay = 0.5f;

        [Range(0.2f, float.MaxValue)]
        private float _duration = 5f;

		private void Start()
		{
            //todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
            // Если делать это допустим в Update,то будем вызывать GetComponent каждый кадр (60+ раз в секунду),
            // что создаст нагрузку на сборщик мусора и снизит производительность.
            // Кроме того, если компонента PositionSaver нет, ошибка проявится сразу в Start, а не будет повторяться каждый кадр в Update.
            _save = GetComponent<PositionSaver>();
			_save.Records.Clear();

            if (_duration <= _delay)
            {
                _duration = _delay * 5f;
                Debug.Log($"<b>{name}</b>: _duration was too small. Automatically installed in {_duration:F2} sec (5 times more _delay).", this);
            }

            _currentDelay = _delay;
        }

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}

            //todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
            // Потому что _delay это фиксированный интервал между записями (например, 0.5 секунды),
            // а _currentDelay это получается как таймер обратного отсчёта до следующей записи.
            // Если уменьшать сам _delay, то и интервал будет постоянно сокращаться,
            // и точки начнут записываться всё чаще и чаще, и как бы ломая равномерность траектории.
            // _currentDelay сбрасывается на значение _delay после каждой записи, сохраняя постоянный шаг между точками.
            _currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
                    //todo comment: Для чего сохраняется значение игрового времени?
                    // Time.time это получается игровое время в секундах с начала сцены.
                    // Оно нужно чтобы при воспроизведении в ReplayMover нам точно знать
                    // в какой момент времени объект должен находиться в данной точке.
                    // И без него невозможно сделать плавную интерполяцию между точками
                    // и синхронизировать движение с игровым временем.
                    Time = Time.time,
				});
			}
		}
	}
}