using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject optionsPanel;
    public GameObject loadGamePanel;
    public GameObject saveFilePanel;

    private void Start()
    {
        SceneManager.LoadScene("Persistence", LoadSceneMode.Additive);
    }

    public void StartNewGame()
    {
        Debug.Log("Creating new game");
        SceneManager.LoadScene("Opening Cutscene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Start Menu");

    }

    public void ContinueGame()
    {
        if (SaveDataHelper.saveFileExists())
        {
            GameObject.Find("Load Game Controller").GetComponent<LoadGameController>().saveData = SaveDataHelper.loadSaveFile();
        } else
        {
            Debug.Log("No save data found");
        }
        
        //SceneManager.UnloadSceneAsync("Start Menu");
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
