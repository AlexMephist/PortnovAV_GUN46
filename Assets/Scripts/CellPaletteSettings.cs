using UnityEngine;

[CreateAssetMenu(fileName = "CellPaletteSettings", menuName = "Game/Cell Palette Settings")]
public class CellPaletteSettings : ScriptableObject
{
    [Header("Highlight Materials")]
    [Tooltip("Материал, когда клетка ВЫБРАНА (например, клик по юниту)")]
    public Material selectedMaterial;

    [Tooltip("Материал, когда на клетку МОЖНО ПОЙТИ (доступный ход)")]
    public Material moveableMaterial;

    [Tooltip("Материал, когда на клетку МОЖНО ПОЙТИ И УДАРИТЬ (вражеская клетка)")]
    public Material attackableMaterial;
}