using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Spawner Instance;

    public List<Soldier> redSoldiers;

    public List<Soldier> blueSoldiers;

    public Soldier AxisFlag;

    public Soldier AlliesFlag;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 RandomTile(Dictionary<Vector2, Tile> dict)
    {
        List<Vector2> keys = Enumerable.ToList(dict.Select(x => x.Key));
        int size = keys.Count();
        if (size > 0)
        {
            return keys[Random.Range(0, size)];
        }
        else
        {
            Debug.Log("0 m�ret�");
            return new Vector2(0, 0);
        }
    }

    public void SpawnSoldier(Soldier.Side side)
    {

        var emptyTiles = GridManager.Instance.soldiers.Where(t => t.Value.playerSide == Soldier.Side.None).Select(t => t.Key);

        List<Vector2> pos = new List<Vector2>();
        foreach (var tile in emptyTiles)
        {
            pos.Add(tile);

        }
        if (pos.Count() > 0)
        {
            int index = Random.Range(0, pos.Count());
            var chosen = GameObject.Find($"Soldier {pos[index].x} {pos[index].y}");
            chosen.GetComponent<Soldier>().Init(300, pos[index], side);
            chosen.GetComponent<Soldier>().HpBar.SetActive(true);
            Tile.ColorTile(chosen.GetComponent<Soldier>());
        }
        else
        {
            Debug.Log("pos.Count = 0");
        }

    }

    public void SoldierSpawner()
    {

        for (int i = 0; i < GameManager.numberOfSpawnedSoldiers; i++)
        {
            SpawnSoldier(Soldier.Side.Allies);
        }

        for (int i = 0; i < GameManager.numberOfSpawnedSoldiers; i++)
        {
            SpawnSoldier(Soldier.Side.Axis);
        }

        GameManager.Instance.UpdateGameState(GameManager.GameState.BombOptionGenerate);
        
    }

    public void FlagCarrierSpawner(string soldierName)
    {
        var soldierObject = GameObject.Find(soldierName);

        Soldier clickedSoldier = soldierObject.GetComponent<Soldier>();

        if ((clickedSoldier is null) || (clickedSoldier.playerSide == Soldier.Side.Allies) || (clickedSoldier.playerSide == Soldier.Side.Axis))
        {
            return;
        }
        else
        {
            if (GameManager.Instance.State == GameManager.GameState.FlagCarrierSpawningAllies )
            {
                if (clickedSoldier.position.y == GridManager.Instance.height - 1)
                {
                    Highlight.Instance.ClearHighlight();
                    clickedSoldier.Init(200, clickedSoldier.position, Soldier.Side.FlagCarrierAllies);
                    AlliesFlag = clickedSoldier;
                    Tile.ColorTile(clickedSoldier);
                    GameManager.Instance.nextTurn();
                    GameManager.Instance.UpdateGameState(GameManager.GameState.FlagCarrierSpawningAxis);
                    GameManager.Instance.Console.text += $"Kek zaszlo elhelyezve!\n";
                }
            }
            else if (GameManager.Instance.State == GameManager.GameState.FlagCarrierSpawningAxis)
            {
                if (clickedSoldier.position.y == 0)
                {
                    Highlight.Instance.ClearHighlight();
                    clickedSoldier.Init(200, clickedSoldier.position, Soldier.Side.FlagCarrierAxis);
                    AxisFlag = clickedSoldier;
                    Tile.ColorTile(clickedSoldier);
                    GameManager.Instance.nextTurn();
                    GameManager.Instance.UpdateGameState(GameManager.GameState.ShootBomb);
                    GameManager.Instance.Console.text += $"Piros zaszlo elhelyezve!\n";
                    AnimationManager.Instance.IdleSide(Soldier.Side.Allies);
                }
            }

        }

    }


}
