using System;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public event Action<Cell> OnCellClicked;

    private readonly List<Cell> _cells = new List<Cell>();
    private Unit _unit;

    private void Start()
    {
        _cells.AddRange(FindObjectsOfType<Cell>());
        Debug.Log($"CellManager запущен, найдено клеток: {_cells.Count}");

        AssignCoordinates();
        SetupNeighbours();
        SetupUnits();
        SubscribeToCells();
    }

    private void OnDestroy()
    {
        foreach (Cell cell in _cells)
        {
            cell.OnPointerClickEvent -= OnCellPointerClick;
        }
    }

    private void AssignCoordinates()
    {
        float minX = float.MaxValue;
        float minZ = float.MaxValue;

        foreach (Cell cell in _cells)
        {
            if (cell.transform.position.x < minX) minX = cell.transform.position.x;
            if (cell.transform.position.z < minZ) minZ = cell.transform.position.z;
        }

        foreach (Cell cell in _cells)
        {
            cell.X = Mathf.RoundToInt(cell.transform.position.x - minX);
            cell.Z = Mathf.RoundToInt(cell.transform.position.z - minZ);
        }

        Debug.Log("Координаты клеток расставлены (0..7)");
    }

    private void SetupNeighbours()
    {
        foreach (Cell cell in _cells)
        {
            cell.Neighbours.Clear();

            foreach (Cell other in _cells)
            {
                if (other == cell) continue;

                int dx = other.X - cell.X;
                int dz = other.Z - cell.Z;

                if (Mathf.Abs(dx) > 1 || Mathf.Abs(dz) > 1) continue;

                cell.Neighbours[GetNeighbourType(dx, dz)] = other;
            }
        }
    }

    private NeighbourType GetNeighbourType(int dx, int dz)
    {
        if (dx == -1 && dz == 0) return NeighbourType.Left;
        if (dx == 1 && dz == 0) return NeighbourType.Right;
        if (dx == 0 && dz == 1) return NeighbourType.Top;
        if (dx == 0 && dz == -1) return NeighbourType.Bottom;
        if (dx == -1 && dz == 1) return NeighbourType.LeftTop;
        if (dx == -1 && dz == -1) return NeighbourType.LeftBottom;
        if (dx == 1 && dz == 1) return NeighbourType.RightTop;
        return NeighbourType.RightBottom;
    }

    private void SetupUnits()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        Debug.Log($"Найдено юнитов: {units.Length}");

        foreach (Unit unit in units)
        {
            Cell cell = FindCellUnderUnit(unit);

            if (cell != null)
            {
                unit.Cell = cell;
                cell.Unit = unit;
                _unit = unit;
                Debug.Log($"Юнит связан с клеткой: x={cell.X}, z={cell.Z}, соседей: {cell.Neighbours.Count}");
            }
        }
    }

    private Cell FindCellUnderUnit(Unit unit)
    {
        Vector3 unitPos = unit.transform.position;

        foreach (Cell cell in _cells)
        {
            Vector3 cellPos = cell.transform.position;

            if (Mathf.Abs(unitPos.x - cellPos.x) < 0.1f &&
                Mathf.Abs(unitPos.z - cellPos.z) < 0.1f)
            {
                return cell;
            }
        }

        return null;
    }

    private void SubscribeToCells()
    {
        foreach (Cell cell in _cells)
        {
            cell.OnPointerClickEvent += OnCellPointerClick;
        }
    }

    private void OnCellPointerClick(Cell cell)
    {
        OnCellClicked?.Invoke(cell);
        TryMoveUnitTo(cell);
    }

    // Временная проверка движения: юнит идёт на соседнюю клетку по клику.
    // Когда дойдём до GameController (п. 14), эта логика переедет туда.
    public void TryMoveUnitTo(Cell cell)
    {
        if (_unit == null || _unit.Cell == null) return;
        if (_unit.Cell == cell) return;
        if (!_unit.Cell.Neighbours.ContainsValue(cell)) return;

        Cell oldCell = _unit.Cell;
        oldCell.Unit = null;

        void OnMoveEnd()
        {
            _unit.Cell = cell;
            cell.Unit = _unit;
            _unit.OnMoveEndCallback -= OnMoveEnd;
            Debug.Log($"Юнит дошёл до клетки: x={cell.X}, z={cell.Z}");
        }

        _unit.OnMoveEndCallback += OnMoveEnd;
        _unit.Move(cell);
    }
}
