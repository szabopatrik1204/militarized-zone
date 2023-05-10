using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soldier : MonoBehaviour
{

    public int health;

    public Vector2 position;

    public Side playerSide = Side.None;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite Allies;

    [SerializeField] private Sprite Axis;

    [SerializeField] private Sprite FlagBlue;

    [SerializeField] private Sprite FlagRed;

    public Sprite BombedZone;

    public Sprite HealedZone;

    public Sprite HealedZone2;

    public GameObject HpBar;

    [SerializeField] private GameObject healthSprite;

    public GameObject currentHp;

    //public Color AlliesColored;

    public static Color AxisColor = new Color32(217, 175, 0, 255);
    public static Color AxisHealthColor = new Color32(217, 91, 0, 255);

    public static Color AlliesHealthColor = new Color32(91, 110, 225, 255);
    public static Color AlliesColor = new Color32(134, 167, 202, 255);

    public static int diedAxisThisTurn { get; set; }

    public static int diedAlliesThisTurn { get; set; }

    public Animator animator;

    private void Awake()
    {
        //AlliesColor = AlliesColored;
        diedAlliesThisTurn = 0;
        diedAxisThisTurn = 0;
        GetComponent<Animator>().enabled = false;
    }

    public void UpdateHealthbar()
    {
        this.currentHp.transform.localScale = new Vector2 ((this.health / 300f)*0.8f, 0.15f);
    }

    public static void ResetDeathCounters()
    {
        diedAlliesThisTurn = 0;
        diedAxisThisTurn = 0;
    }

    public bool isDead()
    {
        if (this.health <= 0)
        {
            GameManager.Instance.Console.text += "Meghalt egy katona.\n";
            GridManager.Instance.soldiers.Remove(new Vector2(this.position.x, this.position.y));
            Tile.ClearColorTile((int) this.position.x,(int) this.position.y);
            if (this.playerSide == Side.Allies)
            {
                diedAlliesThisTurn++;
            }
            else
            {
                diedAxisThisTurn++;
            }
            this.ChangeSide(Side.None);
            GameManager.Instance.DeathCounter.text = $"ALlies died ={diedAlliesThisTurn} , Axis died = {diedAxisThisTurn}";
            return true;
        }
        return false;
    }

    public void ChangeSide(Side side)
    {
        playerSide = side;

        if (playerSide == Side.Allies)
        {
            spriteRenderer.sprite = Allies;
            this.healthSprite.GetComponent<SpriteRenderer>().color = AlliesHealthColor;
        }
        else if (playerSide == Side.Axis)
        {
            spriteRenderer.sprite = Axis;
            this.healthSprite.GetComponent<SpriteRenderer>().color = AxisHealthColor;
        }
        else if (playerSide == Side.FlagCarrierAllies)
        {
            spriteRenderer.sprite = FlagBlue;
        }
        else if (playerSide == Side.FlagCarrierAxis)
        {
            spriteRenderer.sprite = FlagRed;
        }
        else if (playerSide == Side.BombedZone)
        {
            spriteRenderer.sprite = BombedZone;
        }
        else if (playerSide == Side.HealedZone)
        {
            if (Random.Range(0, 2) == 0)
            {
                spriteRenderer.sprite = HealedZone;
            }
            else
            {
                spriteRenderer.sprite = HealedZone2;
            }
        }
        else if (playerSide == Side.None)
        {
            spriteRenderer.sprite = null;
            this.HpBar.SetActive(false);
            GetComponent<Animator>().enabled = false;
        }

    }

    public static List<Soldier> listSoldiers()
    {
        List<Soldier> soldiers = new List<Soldier>();
        var soldierVar = GridManager.Instance.soldiers.Where(t => t.Value.playerSide == Soldier.Side.Axis || t.Value.playerSide == Soldier.Side.Allies).Select(t => t.Value);
        foreach (var soldier in soldierVar)
        {
            soldiers.Add(soldier);
        }
        return soldiers;
    }

    public static List<Soldier> listAllies()
    {
        List<Soldier> soldiers = new List<Soldier>();
        var soldierVar = GridManager.Instance.soldiers.Where(t => t.Value.playerSide == Soldier.Side.Allies).Select(t => t.Value);
        foreach (var soldier in soldierVar)
        {
            soldiers.Add(soldier);
        }
        return soldiers;
    }

    public static List<Soldier> listAxis()
    {
        List<Soldier> soldiers = new List<Soldier>();
        var soldierVar = GridManager.Instance.soldiers.Where(t => t.Value.playerSide == Soldier.Side.Axis).Select(t => t.Value);
        foreach (var soldier in soldierVar)
        {
            soldiers.Add(soldier);
        }
        return soldiers;
    }

    public enum Side 
    { 
        Allies,
        Axis,
        FlagCarrierAllies,
        FlagCarrierAxis,
        BombedZone,
        HealedZone,
        None,
    }

    public void Init(int health, Vector2 position, Side playerSide)
    {
        this.health = health;
        this.position = position;
        this.playerSide = playerSide;
        ChangeSide(playerSide);

    }

    


}
