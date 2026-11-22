using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event System.Action<Cell> OnPointerClickEvent;

    public int X { get; set; }
    public int Z { get; set; }
    public Unit Unit { get; set; }
    public Dictionary<NeighbourType, Cell> Neighbours { get; } = new Dictionary<NeighbourType, Cell>();

    [Header("References")]
    [SerializeField] private MeshRenderer _focusRenderer;
    [SerializeField] private MeshRenderer _selectRenderer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_focusRenderer != null) _focusRenderer.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_focusRenderer != null) _focusRenderer.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(this);
    }

    public void SetSelect(Material material)
    {
        if (_selectRenderer != null)
        {
            _selectRenderer.enabled = true;
            _selectRenderer.material = material;
        }
    }

    public void ResetSelect()
    {
        if (_selectRenderer != null) _selectRenderer.enabled = false;
    }
}
