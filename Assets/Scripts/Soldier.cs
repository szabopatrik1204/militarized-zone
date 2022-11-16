using System.Collections;
using System.Collections.Generic;
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

    public GameObject HpBar;

    public enum Side 
    { 
        Allies,
        Axis,
        FlagCarrierAllies,
        FlagCarrierAxis,
        None,
    }

    public void Init(int health, Vector2 position, Side playerSide)
    {
        this.health = health;
        this.position = position;
        this.playerSide = playerSide;

        if (playerSide == Side.Allies)
        {
            spriteRenderer.sprite = Allies;
        }
        else if (playerSide == Side.Axis)
        {
            spriteRenderer.sprite = Axis;
        }
        else if (playerSide == Side.FlagCarrierAllies)
        {
            spriteRenderer.sprite = FlagBlue;
        }
        else if (playerSide == Side.FlagCarrierAxis)
        {
            spriteRenderer.sprite = FlagRed;
        }
    }

    


}
