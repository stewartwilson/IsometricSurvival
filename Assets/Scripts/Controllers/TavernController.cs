using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TavernController : MonoBehaviour {

    public string currentSceneName;
    public LoadGameController gameGontroller;
    public GameObject powerUpContainer;
    public int numberOfPowerChoices;
    public List<PowerUp> allPowerups;
    public List<PowerUp> currentPowerUps;
    public List<PowerUp> powerUpChoices;
    public List<GameObject> powerUpUIElements;
    

    // Use this for initialization
    void Start () {
        if (GameObject.Find("Load Game Controller") != null) {
            gameGontroller = GameObject.Find("Load Game Controller").GetComponent<LoadGameController>();
            powerUpContainer = GameObject.Find("Power Up Container");
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
        int index = 0;
        foreach(PowerUp powerUp in powerUpChoices)
        {
            PowerUpData pud = powerUpContainer.GetComponent<PowerUpContainer>().getPowerUpDataFromEnum(powerUp);
            GameObject uiElement = powerUpUIElements[index];
            Text[] children = uiElement.GetComponentsInChildren<Text>();
            children[0].text = pud.title;
            children[1].text = pud.description;
            children[2].text = pud.unitType.ToString();
            index++;
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
        Debug.Log(powerUpChoices[index].ToString());
        loadNextScene();
    }
}
