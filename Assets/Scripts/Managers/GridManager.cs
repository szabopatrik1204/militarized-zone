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

    [SerializeField] private Bomb bombPrefab;

    [SerializeField] private GameObject highlightPrefab;

    [SerializeField] private Camera cam;

    public Dictionary<Vector2, Tile> tiles;

    public Dictionary<Vector2, Soldier> soldiers;

    public Dictionary<Vector2, GameObject> highlights;


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
                    /*if (GameManager.Instance.State == GameManager.GameState.ShootBomb)
                    {
                        BombManager.Instance.ShootBomb(BombManager.Instance.choosenOption,hit);
                    }*/

                    if (GameManager.Instance.State == GameManager.GameState.ShootBomb)
                    {
                        if (BombManager.Instance.chosenOption != null)
                        {
                            BombManager.Instance.ShootBomb(BombManager.Instance.chosenOption, hit);
                        }
                    }

                    if (GameManager.Instance.State == GameManager.GameState.FlagCarrierSpawningAllies
                        || GameManager.Instance.State == GameManager.GameState.FlagCarrierSpawningAxis)
                    {
                        Debug.Log("Hit " + hit.transform.gameObject.name);
                        Spawner.Instance.FlagCarrierSpawner(hit.transform.gameObject.name);
                    }
                }
        }
    }

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();

        soldiers = new Dictionary<Vector2, Soldier>();

        highlights = new Dictionary<Vector2, GameObject>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var spawnedSoldier = Instantiate(soldierPrefab, new Vector3(x, y), Quaternion.identity);
                spawnedSoldier.Init(200, new Vector2(x, y), Soldier.Side.None);
                spawnedSoldier.name = $"Soldier {x} {y}";

                var highlight = Instantiate(highlightPrefab, new Vector3(x, y), Quaternion.identity);
                highlight.name = $"Highlight {x} {y}";

                var isOffset = (x + y) % 2 == 1;

                spawnedTile.Init(isOffset);

                highlights[new Vector2(x, y)] = highlight;

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

    public void ClearHighlights()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject highlighted = GameObject.Find($"Highlight {x} {y}");
                var highlightColor = Color.red;
                highlightColor.a = 0f;
                highlighted.GetComponent<SpriteRenderer>().color = highlightColor;
            }
        }
    }

    public static void ClearZones()
    {
        for (int x = 0; x < GridManager.Instance.width; x++)
        {
            for (int y = 0; y < GridManager.Instance.height; y++)
            {
                GameObject zoneObject = GameObject.Find($"Soldier {x} {y}");
                Soldier zone = zoneObject.GetComponent<Soldier>();
                if ((zone.playerSide == Soldier.Side.HealedZone) || (zone.playerSide == Soldier.Side.BombedZone))
                {
                    zone.ChangeSide(Soldier.Side.None);
                }
            }
        }
    }

}
