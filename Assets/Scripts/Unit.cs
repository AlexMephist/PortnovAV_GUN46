using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action OnMoveEndCallback;

    [SerializeField, Min(1)] private float _moveSpeed = 5f;

    public Cell Cell { get; set; }

    private const float CellHeightOffset = 1f;

    // Прокидываем события в клетку, на которой стоит юнит (п. 9 ТЗ)
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cell?.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cell?.OnPointerExit(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Cell?.OnPointerClick(eventData);
    }

    public void Move(Cell target)
    {
        if (target == null) return;

        StartCoroutine(MoveRoutine(target));
    }

    private IEnumerator MoveRoutine(Cell target)
    {
        Cell oldCell = Cell;

        Vector3 start = transform.position;
        Vector3 end = target.transform.position + Vector3.up * CellHeightOffset;
        float journey = 0f;

        while (journey < 1f)
        {
            journey = Mathf.Clamp01(journey + Time.deltaTime * _moveSpeed);
            transform.position = Vector3.Lerp(start, end, journey);
            yield return null;
        }

        transform.position = end;

        // Если курсор остался над старой клеткой, событие выхода могло «уехать» вместе с юнитом —
        // гасим подсветку наведения на ней вручную, чтобы Focus не залипал
        if (oldCell != null && oldCell != target)
        {
            oldCell.OnPointerExit(null);
        }

        Cell = target;
        OnMoveEndCallback?.Invoke();
    }
}
