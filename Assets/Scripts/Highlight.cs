using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public static Highlight Instance;

    Animator animator;

    private void Awake()
    {
        Instance = this;
        GetComponent<Animator>().enabled = false;
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance.State == GameManager.GameState.ShootBomb)
        {
            if (BombManager.Instance.chosenOption != null)
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform != null)
                    {

                        HighlightHelper(BombManager.Instance.chosenOption, hit);
                    }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.State == GameManager.GameState.ShootBomb)
        {
            this.ClearHighlight();
        }
    }

    public void HighlightHelper(Bomb bomb, RaycastHit hit)
    {
        for (int i = 0; i < bomb.size; i++)
        {
            for (int j = 0; j < bomb.size; j++)
            {
                if (bomb.bombPattern[i, j] != 0)
                {
                    var target = GameObject.Find(hit.transform.gameObject.name);
                    var damageHighlight = GameObject.Find($"Highlight {target.transform.position.x + i - 1} {target.transform.position.y + j - 1}");
                    if (damageHighlight != null)
                    {
                        var highlightColor = Color.black;
                        highlightColor.a = 0.5f;
                        damageHighlight.GetComponent<SpriteRenderer>().color = highlightColor;
                    }
                }
            }
        }
    }

    public void ClearHighlight()
    {
        var highlightColor = Color.black;
        highlightColor.a = 0.0f;
        foreach (KeyValuePair<Vector2, GameObject> x in GridManager.Instance.highlights)
        {
            x.Value.transform.GetComponent<SpriteRenderer>().color = highlightColor;
        }
    }

    public void FlagHelperHighlight()
    {
        if (GameManager.Instance.State == GameManager.GameState.FlagCarrierSpawningAllies)
        {
            for (int x = 0; x < GridManager.Instance.width; x++)
            {
                var flagHighlight = GameObject.Find($"Highlight {x} {GridManager.Instance.height - 1}");
                var highlightColor = Color.white;
                highlightColor.a = 0.5f;
                flagHighlight.GetComponent<SpriteRenderer>().color = highlightColor;
            }
        }
        if (GameManager.Instance.State == GameManager.GameState.FlagCarrierSpawningAxis)
        {
            for (int x = 0; x < GridManager.Instance.width; x++)
            {
                var flagHighlight = GameObject.Find($"Highlight {x} {0}");
                var highlightColor = Color.white;
                highlightColor.a = 0.5f;
                flagHighlight.GetComponent<SpriteRenderer>().color = highlightColor;
            }
        }
    }

}
