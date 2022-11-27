using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public Turn turn;

    public static event Action<GameState> OnGameStateChanged;

    public static int numberOfSpawnedSoldiers = 13;

    public int currentRound = 0 ;

    public TextMeshProUGUI Console;

    public TextMeshProUGUI DeathCounter;

    public GameObject EndGameCanvas;

    public TextMeshProUGUI WinnerText;

    public TextMeshProUGUI AlliesAlive;

    public TextMeshProUGUI AxisAlive;

    public Image Option1Image;
    public Image Option2Image;
    public Image Option3Image;

    private void Awake()
    {
        Instance = this;

        Option1Image.color = Soldier.AlliesColor;
        Option2Image.color = Soldier.AlliesColor;
        Option3Image.color = Soldier.AlliesColor;

        turn = Turn.Allies;
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
                RefreshAliveText();
                break;
            case GameState.FlagCarrierSpawningAllies:
                AnimationManager.Instance.IdleSide(Soldier.Side.Allies);
                Highlight.Instance.FlagHelperHighlight();
                break;
            case GameState.FlagCarrierSpawningAxis:
                AnimationManager.Instance.IdleSide(Soldier.Side.Axis);
                Highlight.Instance.FlagHelperHighlight();
                break;
            case GameState.ShootBomb:
                break;
            case GameState.FlagTurn:
                FlagManager.Instance.FlagStep();
                UpdateGameState(GameState.ShootBomb);
                break;
            case GameState.EndGame:
                EndGameCanvas.SetActive(true);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void nextTurn()
    {
        RefreshAliveText();
        if (turn == Turn.Axis)
        {
            UpdateGameState(GameState.FlagTurn);
            turn = Turn.Allies;
            Option1Image.color = Soldier.AlliesColor;
            Option2Image.color = Soldier.AlliesColor;
            Option3Image.color = Soldier.AlliesColor;
            AnimationManager.Instance.IdleSide(Soldier.Side.Allies);
        }
        else
        {
            turn = Turn.Axis;
            Option1Image.color = Soldier.AxisColor;
            Option2Image.color = Soldier.AxisColor;
            Option3Image.color = Soldier.AxisColor;
            AnimationManager.Instance.IdleSide(Soldier.Side.Axis);
        }
    }

    public void RefreshAliveText()
    {
        AlliesAlive.text = Soldier.listAllies().Count().ToString();
        AxisAlive.text = Soldier.listAxis().Count().ToString();
    }

    public enum GameState
    {
        MapGenerate = 0,
        BombOptionGenerate = 1,
        SoldierSpawning = 2,
        FlagCarrierSpawningAllies = 3,
        FlagCarrierSpawningAxis = 4,
        ShootBomb = 5,
        FlagTurn = 6,
        EndGame = 7,
    }

    public enum Turn
    {
        Allies = 0,
        Axis = 1,
    }


}
