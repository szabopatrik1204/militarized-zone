using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{

    public Button restartButton;

    private void Awake()
    {
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(restartOnClick);
    }

    public void restartOnClick()
    {
        SceneManager.LoadScene("BombZone");
    }
}
