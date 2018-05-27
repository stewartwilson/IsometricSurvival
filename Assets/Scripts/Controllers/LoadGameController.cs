using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameController : MonoBehaviour {

    public SaveContainer saveData;

    public PowerUpData getPowerUpDataFromEnum(PowerUp powerUp)
    {
        PowerUpContainer container = GameObject.Find("Power Up Container").GetComponent<PowerUpContainer>();
        foreach (PowerUpData pud in container.powerUpData)
        {
            if (pud.powerUp.Equals(powerUp))
            {
                return pud;
            }
        }
        return null;
    }

}
