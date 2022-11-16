using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    public Soldier soldier;

    public void Init(bool isOffset)
    { 
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }
    void OnMouseEnter()
    { 
        highlight.SetActive(true);
    }
    private void OnMouseDown()
    {
        highlight.SetActive(false);
    }


    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

}
