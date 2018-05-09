using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernController : MonoBehaviour {

    public string currentSceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadNextScene()
    {
        SceneManager.LoadScene("Main Game", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentSceneName);
    }
}
