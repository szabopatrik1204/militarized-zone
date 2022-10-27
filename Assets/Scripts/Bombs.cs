using UnityEngine;
using UnityEngine.Tilemaps;

public enum Bomb
{
    One,
    R,
    I,
    J,
    L,
    T,
    Plus,
    Z,
}
[System.Serializable]
public struct BombData
{
    public Bomb bomb;
    public Tile tile;
    public Vector2Int[] cells;
}
