using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernController : MonoBehaviour {

    public string currentSceneName;
    public LoadGameController gameGontroller;
    public int numberOfPowerChoices;
    public List<PowerUp> allPowerups;
    public List<PowerUp> currentPowerUps;
    public List<PowerUp> powerUpChoices;
    public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;
    

    // Use this for initialization
    void Start () {
        if (GameObject.Find("Load Game Controller") != null) {
            gameGontroller = GameObject.Find("Load Game Controller").GetComponent<LoadGameController>();
            currentPowerUps = gameGontroller.saveData.powerUps;
        }
        foreach (PowerUp power in Enum.GetValues(typeof(PowerUp)))
        {
            allPowerups.Add(power);
        }
        List<PowerUp> possibleChoices = allPowerups;
        possibleChoices.RemoveAll(power => currentPowerUps.Contains(power));

        System.Random random = new System.Random();
        for (int i = 0; i < numberOfPowerChoices; i++)
        {
            int index = random.Next(0, possibleChoices.Count);
            Debug.Log(index);
            powerUpChoices.Add(possibleChoices[index]);
            possibleChoices.RemoveAt(index);
        }
        populatePowerUpUIElements();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadNextScene()
    {
        SceneManager.LoadScene("Main Game", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentSceneName);
    }

    public void populatePowerUpUIElements()
    {
        foreach(PowerUp powerUp in powerUpChoices)
        {
            string description = PowerUpHelper.powerUpDescriptionMap[powerUp];
        }
    }


    public void choosePowerUp1()
    {
        choosePowerUp(0);
    }

    public void choosePowerUp2()
    {
        choosePowerUp(1);
    }

    public void choosePowerUp3()
    {
        choosePowerUp(2);
    }

    public void choosePowerUp(int index)
    {
        gameGontroller.saveData.powerUps.Add(powerUpChoices[index]);
        loadNextScene();
    }
}
