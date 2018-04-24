using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject optionsPanel;

    public void StartSoloGame()
    {
        Debug.Log("Creating new solo game");
        //SceneManager.LoadScene("Character Creator");

    }

    public void StartMultiplayerGame()
    {
        Debug.Log("Creating new multiplayer game");
        //SceneManager.LoadScene("Character Creator");

    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
