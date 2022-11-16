using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static GridManager Instance;

    [SerializeField] public int width, height;

    [SerializeField] public Tile tilePrefab;

    [SerializeField] private Soldier soldierPrefab;

    [SerializeField] private Camera cam;

    public Dictionary<Vector2, Tile> tiles;

    public Dictionary<Vector2, Soldier> soldiers;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null)
                {
                    Debug.Log("Hit " + hit.transform.gameObject.name);
                    Spawner.Instance.FlagCarrierSpawner(hit.transform.gameObject.name);
                }

        }
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();

        soldiers = new Dictionary<Vector2, Soldier>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var spawnedSoldier = Instantiate(soldierPrefab, new Vector3(x, y), Quaternion.identity);
                spawnedSoldier.Init(200, new Vector2(x, y), Soldier.Side.None);
                spawnedSoldier.name = $"Soldier {x} {y}";


                var isOffset = (x + y) % 2 == 1;

                spawnedTile.Init(isOffset);

                soldiers[new Vector2(x, y)] = spawnedSoldier;

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -1);
        cam.orthographicSize = 5;

        GameManager.Instance.UpdateGameState(GameManager.GameState.SoldierSpawning);

    }

    public Vector2 SpawnPosition()
    {
        float spawnPositionX = Random.Range(0, width);
        float spawnPositionY = Random.Range(0, height);
        //return (Tile) tiles.Where(t => t.Key == new Vector2(spawnPositionX, spawnPositionY));

        return new Vector2(spawnPositionX, spawnPositionY);
    }

    public Soldier GetSoldierAtPosition(Vector2 pos)
    {
        if (soldiers.TryGetValue(pos, out var soldier))
        {
            return soldier;
        }
        return null;
    }

    public Soldier GetSoldierAtCoordinate(Vector2 a)
    {
        return null;
    }

}
