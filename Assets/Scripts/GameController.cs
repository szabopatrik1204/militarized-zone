using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{

    public TileBase highlightTile;
    public Tilemap highlightMap;

    public TileBase shadowTile;
    public Tilemap shadowMap;
    
    public Grid grid;

    public Vector3Int previousMousePos = new Vector3Int();


    private void Start()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
        Color color = new Color(1.0f, 1.0f, 1.0f, 0.1f); shadowMap.SetColor(coordinate, color);
    }

    void Update()
    {

        Vector3Int mousePos = GetMousePosition();

        if (!mousePos.Equals(previousMousePos))
        {

            shadowMap.SetTile(previousMousePos, null); // Remove old hoverTile

            shadowMap.SetTile(mousePos, shadowTile);

            previousMousePos = mousePos;

        }

        if (Input.GetMouseButtonDown(0))
        {
            highlightMap.SetTile(mousePos, highlightTile);
        }

        if (Input.GetMouseButtonDown(1))
        {
            highlightMap.SetTile(mousePos, null);
        }

        shadowMap.SetTile(mousePos, shadowTile);

    }
    Vector3Int GetMousePosition()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return grid.WorldToCell(mouseWorldPos);

    }
}
