using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{

    public int health;

    public Vector2 position;

    public Side playerSide;

    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private Sprite Allies;
    [SerializeField] private Sprite Axis;

    public enum Side 
    { 
        Allies,
        Axis,
    }

    public void Init(int health, Vector2 position, Side playerSide)
    {
        this.health = health;
        this.position = position;
        this.playerSide = playerSide;

        if (playerSide == Side.Allies)
        {
            _renderer.sprite = Allies;
        }
        else
        {
            _renderer.sprite = Axis;
        }
    }
}
