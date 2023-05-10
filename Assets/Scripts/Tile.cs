using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private static Color baseColor = new Color32(86, 161, 96, 255), offsetColor = new Color32(86, 161, 96, 255);
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private Sprite tile1;
    [SerializeField] private Sprite tile2;
    public Bomb bombPrefab;

    private List<Tile> bombedTiles = new List<Tile>();

    public void Init(bool isOffset)
    {
        if (isOffset)
        {
            this.spriteRenderer.color = offsetColor;
            this.spriteRenderer.sprite = tile2;
        }
        else
        {
            this.spriteRenderer.color = baseColor;
            this.spriteRenderer.sprite = tile1;
        }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    private void OnMouseDown()
    {
        foreach (var tile in bombedTiles)
        {
            GameObject child = tile.transform.GetChild(0).gameObject;
            child.SetActive(false);
            bombedTiles.Remove(tile.GetComponent<Tile>());
        }

    }

    public static void ColorTile(Soldier soldier)
    {
        var tileObject = GameObject.Find($"Tile {(int)soldier.position.x} {(int)soldier.position.y}");
        Tile tile = tileObject.GetComponent<Tile>();
        if ((soldier.playerSide == Soldier.Side.Allies) ||
            (soldier.playerSide == Soldier.Side.FlagCarrierAllies))
        {
            tile.spriteRenderer.color = Soldier.AlliesColor;
        }
        else if ((soldier.playerSide == Soldier.Side.Axis) ||
                (soldier.playerSide == Soldier.Side.FlagCarrierAxis))
        {
            tile.spriteRenderer.color = Soldier.AxisColor;
        }
    }

    public static void ClearColorTile(int x, int y)
    {
        var tileObject = GameObject.Find($"Tile {x} {y}");
        Tile tile = tileObject.GetComponent<Tile>();
        tile.spriteRenderer.color = Tile.baseColor;
    }

}
