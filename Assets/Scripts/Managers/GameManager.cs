using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public static int numberOfSpawnedSoldiers = 13;

    public Bomb bombPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.MapGenerate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MapGenerate:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.BombOptionGenerate:
                BombManager.Instance.GenerateOptions();
                break;
            case GameState.SoldierSpawning:
                Spawner.Instance.SoldierSpawner();
                break;
            case GameState.FlagCarrierSpawningAllies:
                break;
            case GameState.FlagCarrierSpawningAxis:
                break;
            case GameState.Player1Turn:
                break;
            case GameState.Player2Turn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        MapGenerate = 0,
        BombOptionGenerate = 1,
        SoldierSpawning = 2,
        FlagCarrierSpawningAllies = 3,
        FlagCarrierSpawningAxis = 4,
        Player1Turn = 5,
        Player2Turn = 6,
    }

}
