using System.Collections.Generic;
using UnityEngine;


public class PowerUpContainer : MonoBehaviour
{
    public List<PowerUpData> powerUpData;

    public PowerUpData getPowerUpDataFromEnum(PowerUp powerUp)
    {
        foreach(PowerUpData pud in powerUpData)
        {
            if(pud.powerUp.Equals(powerUp))
            {
                return pud;
            }
        }
        return null;
    }
}

