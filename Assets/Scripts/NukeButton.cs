using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NukeButton : MonoBehaviour
{

    bool alliesNuke;
    bool axisNuke;

    public Sprite alliesBox;
    public Sprite alliesBoxTick;
    public Sprite axisBox;
    public Sprite axisBoxTick;


    private void Awake()
    {
        alliesNuke = false;
        axisNuke = false;
    }

    

    public void nukeButtonPressed()
    {

        GameObject pressedButton = EventSystem.current.currentSelectedGameObject;

        switch (pressedButton.name)
        {
            case "AlliesNukeButton":
                if (GameManager.Instance.turn == GameManager.Turn.Allies)
                {
                    if (alliesNuke)
                    {
                        pressedButton.GetComponent<Image>().sprite = alliesBox;
                    }
                    else
                    {
                        pressedButton.GetComponent<Image>().sprite = alliesBoxTick;
                    }
                    alliesNuke = !alliesNuke;
                }
                break;
            case "AxisNukeButton":
                if (GameManager.Instance.turn == GameManager.Turn.Axis)
                {
                    if (axisNuke)
                    {
                        pressedButton.GetComponent<Image>().sprite = axisBox;
                    }
                    else
                    {
                        pressedButton.GetComponent<Image>().sprite = axisBoxTick;
                    }
                    axisNuke = !axisNuke;
                }
                break;
        }

        Debug.Log(alliesNuke);
        Debug.Log(axisNuke);

        if (axisNuke && alliesNuke)
        {
            Debug.Log("BOOOOOOOOOOM!!!");
            BombManager.Instance.NukeBomb();
            GameManager.Instance.UpdateGameState(GameManager.GameState.EndByNuke);

        }

    }

}
