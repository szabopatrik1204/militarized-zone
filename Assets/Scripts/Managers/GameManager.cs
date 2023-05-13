using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public Turn turn;

    public static int numberOfSpawnedSoldiers = 13;

    public int currentRound = 0 ;

    public TextMeshProUGUI Console;

    public TextMeshProUGUI DeathCounter;

    public GameObject EndGameCanvas;

    public TextMeshProUGUI WinnerText;

    public TextMeshProUGUI AlliesAlive;

    public TextMeshProUGUI AxisAlive;

    public GameObject RestartButton;

    public GameObject AxisTurnIndicator;
    public GameObject AlliesTurnIndicator;

    public GameObject clickToContinue;

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

        RestartButton.GetComponent<Button>().onClick.AddListener(restartOnClick);
        clickToContinue.SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.ClickToContinue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (GameState.ClickToContinue == GameManager.Instance.State))
        {
            clickToContinue.gameObject.SetActive(false);
            UpdateGameState(GameState.MapGenerate);
        }
    }

    private void FixedUpdate()
    {
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.ClickToContinue:
                break;
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
                canRestartGame();
                break;
            case GameState.EndByNuke:
                nukeEndGame();
                canRestartGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

    }

    public void canRestartGame()
    {
        RestartButton.SetActive(true);
        
    }

    public void restartOnClick()
    {
        SceneManager.LoadScene("BombZone");
    }

    public void nextTurn()
    {
        RefreshAliveText();
        if (Soldier.listAllies().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Sárga nyert";
        }
        else if(Soldier.listAxis().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Kék nyert";
        }

        if (turn == Turn.Axis)
        {
            UpdateGameState(GameState.FlagTurn);
            turn = Turn.Allies;
            Option1Image.color = Soldier.AlliesColor;
            Option2Image.color = Soldier.AlliesColor;
            Option3Image.color = Soldier.AlliesColor;
            AnimationManager.Instance.IdleSide(Soldier.Side.Allies);
            GameObject.Find("Map Background").GetComponent<SpriteRenderer>().color = Soldier.AlliesColor;

        }
        else
        {
            turn = Turn.Axis;
            Option1Image.color = Soldier.AxisColor;
            Option2Image.color = Soldier.AxisColor;
            Option3Image.color = Soldier.AxisColor;
            AnimationManager.Instance.IdleSide(Soldier.Side.Axis);
            GameObject.Find("Map Background").GetComponent<SpriteRenderer>().color = Soldier.AxisColor;       

        }
    }

    public void nukeEndGame()
    {

        int alliesHealth = 0;
        int axisHealth = 0;

        RefreshAliveText();
        if ((Soldier.listAllies().Count() == 0) && (Soldier.listAxis().Count() == 0))
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Döntetlen";
        }
        else if (Soldier.listAllies().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Sárga nyert";
        }
        else if (Soldier.listAxis().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Kék nyert";
        }

        foreach (Soldier soldier in Soldier.listAllies())
        {
            alliesHealth += soldier.health;
        }

        foreach (Soldier soldier in Soldier.listAxis())
        {
            axisHealth += soldier.health;
        }

        if (axisHealth > alliesHealth)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Sárga nyert";
        }
        else if (alliesHealth > axisHealth)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Kék nyert";
        }
        else if (alliesHealth == axisHealth)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Döntetlen";
        }

    }

    public void RefreshAliveText()
    {
        AlliesAlive.text = Soldier.listAllies().Count().ToString();
        AxisAlive.text = Soldier.listAxis().Count().ToString();
    }

    public enum GameState
    {
        ClickToContinue = -1,
        MapGenerate = 0,
        BombOptionGenerate = 1,
        SoldierSpawning = 2,
        FlagCarrierSpawningAllies = 3,
        FlagCarrierSpawningAxis = 4,
        ShootBomb = 5,
        FlagTurn = 6,
        EndGame = 7,
        EndByNuke = 8,
    }

    public enum Turn
    {
        Allies = 0,
        Axis = 1,
    }


}
