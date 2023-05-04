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

    public GameObject AxisTurnIndicator;
    public GameObject AlliesTurnIndicator;

    public GameObject clickToContinue;

    public Image Option1Image;
    public Image Option2Image;
    public Image Option3Image;

    //public GameObject Cannon;
    //Vector3 mousePos;
    //Rigidbody2D rb;

    private void Awake()
    {
        Instance = this;

        Option1Image.color = Soldier.AlliesColor;
        Option2Image.color = Soldier.AlliesColor;
        Option3Image.color = Soldier.AlliesColor;

        turn = Turn.Allies;

        //rb = Cannon.GetComponent<Rigidbody2D>();

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
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //Vector3 lookDir = mousePos - Cannon.transform.position;
        //Cannon.transform.Rotate(lookDir);

        /*Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;*/
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
                break;
            case GameState.EndByNuke:
                nukeEndGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void nextTurn()
    {
        RefreshAliveText();
        if (Soldier.listAllies().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Axis won";
        }
        else if(Soldier.listAxis().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Allies won";
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
            //AlliesTurnIndicator.SetActive(true);
            //AxisTurnIndicator.SetActive(false);

        }
        else
        {
            turn = Turn.Axis;
            Option1Image.color = Soldier.AxisColor;
            Option2Image.color = Soldier.AxisColor;
            Option3Image.color = Soldier.AxisColor;
            AnimationManager.Instance.IdleSide(Soldier.Side.Axis);
            GameObject.Find("Map Background").GetComponent<SpriteRenderer>().color = Soldier.AxisColor;
            //AxisTurnIndicator.SetActive(true);
            //AlliesTurnIndicator.SetActive(false);           

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
            WinnerText.text = "Tie";
        }
        else if (Soldier.listAllies().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Axis won";
        }
        else if (Soldier.listAxis().Count() == 0)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Allies won";
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
            WinnerText.text = "Axis won";
        }
        else if (alliesHealth > axisHealth)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Allies won";
        }
        else if (alliesHealth == axisHealth)
        {
            UpdateGameState(GameState.EndGame);
            WinnerText.text = "Tie";
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
