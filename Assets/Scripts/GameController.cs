using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CellManager _cellManager;
    [SerializeField] private CellPaletteSettings _palette;

    private Unit _selectedUnit;

    private void Start()
    {
        if (_cellManager == null)
            _cellManager = FindObjectOfType<CellManager>();

        if (_palette == null)
        {
            Debug.LogWarning("[GameController] CellPaletteSettings не назначен! Подсветка работать не будет.");
        }

        foreach (Cell cell in FindObjectsOfType<Cell>())
        {
            cell.OnPointerClickEvent += OnCellClick;
        }
    }

    private void OnDestroy()
    {
        foreach (Cell cell in FindObjectsOfType<Cell>())
        {
            cell.OnPointerClickEvent -= OnCellClick;
        }
    }

    private void OnCellClick(Cell cell)
    {
        Debug.Log($"[GameController] Клик по клетке: x={cell.X}, z={cell.Z}");

        // Клик по клетке с юнитом
        if (cell.Unit != null)
        {
            // Повторный клик по уже выбранному юниту — снимаем выбор
            if (cell.Unit == _selectedUnit)
            {
                DeselectUnit();
                return;
            }

            SelectUnit(cell.Unit);
            return;
        }

        // Клик по пустой клетке
        if (_selectedUnit != null)
        {
            _cellManager.TryMoveUnitTo(cell);
            DeselectUnit();
        }
    }

    private void SelectUnit(Unit unit)
    {
        DeselectUnit();

        _selectedUnit = unit;

        if (unit.Cell != null)
        {
            // Клетка под юнитом — материал «выбрана»
            if (_palette != null && _palette.selectedMaterial != null)
            {
                unit.Cell.SetSelect(_palette.selectedMaterial);
            }

            foreach (var neighbour in unit.Cell.Neighbours.Values)
            {
                if (_palette != null && _palette.moveableMaterial != null)
                {
                    neighbour.SetSelect(_palette.moveableMaterial);
                }
            }

            Debug.Log($"[GameController] Выбран юнит на клетке: x={unit.Cell.X}, z={unit.Cell.Z}. Доступно ходов: {unit.Cell.Neighbours.Count}");
        }
    }

    private void DeselectUnit()
    {
        _selectedUnit = null;

        foreach (Cell cell in FindObjectsOfType<Cell>())
        {
            cell.ResetSelect();
        }
    }
}
