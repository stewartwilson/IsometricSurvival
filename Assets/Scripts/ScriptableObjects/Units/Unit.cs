using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Unit : ScriptableObject {

    public Sprite sprite;
    public string unitName;
    public UnitClass unitClass;
    public int level;
    public int health;
    public int maxHealth;
    public UnitAction[] actionList = new UnitAction[3];
    public bool canAct;
    public int maxMovement;
    public GridPosition position;
    public bool isEnemy;

    public void changeCurrentHealth(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health < 0 )
        {
            health = 0;
        }
    }

    public void resetHealth()
    {
        health = maxHealth;
    }

    public abstract UnitAction returnAction(Unit target);

    public abstract int takeDamage(int damage);

    public abstract int takeHealing(int healing);


}
