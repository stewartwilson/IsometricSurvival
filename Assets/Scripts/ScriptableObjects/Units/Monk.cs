using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Units/Monk")]
public class Monk : Unit {

    public override UnitAction returnAction(Unit target)
    {
        MeleeAttack action = new MeleeAttack();
        return action;
    }

    public override int takeDamage(int damage)
    {
        changeCurrentHealth(-damage);
        return damage;
    }

    public override int takeHealing(int healing)
    {
        changeCurrentHealth(healing);
        return healing;
    }
}
