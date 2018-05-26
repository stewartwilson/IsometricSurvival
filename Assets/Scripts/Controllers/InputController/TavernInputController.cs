using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernInputController : MonoBehaviour {
	
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            GameObject.Find("Tavern Controller").GetComponent<TavernController>().loadNextScene();
        }
	}


}
