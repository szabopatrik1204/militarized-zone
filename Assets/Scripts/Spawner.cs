using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Spawner Instance;

    [SerializeField] private Soldier soldier; 

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnSoldier()
    {

        for (int i = 0; i < GameManager.spawnedSoldiers; i++)
        {
            Vector2 position = GridManager.Instance.SpawnPosition();
            soldier.Init(200, position, Soldier.Side.Allies);
            Instantiate(soldier, new Vector3(position.x, position.y, 1), Quaternion.identity);

        }
        for (int i = 0; i < GameManager.spawnedSoldiers; i++)
        {
            Vector2 position = GridManager.Instance.SpawnPosition();
            soldier.Init(200, position, Soldier.Side.Axis);
            Instantiate(soldier, new Vector3(position.x, position.y, 1), Quaternion.identity);

        }

        GameManager.Instance.UpdateGameState(GameManager.GameState.FlagCarrierSpawning);
    }



}
