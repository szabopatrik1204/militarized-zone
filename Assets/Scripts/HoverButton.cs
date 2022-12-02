using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.turn == GameManager.Turn.Axis)
        {
            this.GetComponent<Image>().color = Soldier.AxisHealthColor;
        }
        else if (GameManager.Instance.turn == GameManager.Turn.Allies) 
        {
            this.GetComponent<Image>().color = Soldier.AlliesHealthColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.turn == GameManager.Turn.Axis)
        {
            this.GetComponent<Image>().color = Soldier.AxisColor;
        }
        else if (GameManager.Instance.turn == GameManager.Turn.Allies)
        {
            this.GetComponent<Image>().color = Soldier.AlliesColor;
        }
    }
}
