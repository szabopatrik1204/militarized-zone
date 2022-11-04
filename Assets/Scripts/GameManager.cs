using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

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
            case GameState.SoldierSpawning:
                Debug.Log("Soldiers Spawning");
                break;
            case GameState.FlagCarrierSpawning:
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
        SoldierSpawning = 1,
        FlagCarrierSpawning = 2,
        Player1Turn = 3,
        Player2Turn = 4,
    }

}
