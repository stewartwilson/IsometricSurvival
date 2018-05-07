using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public abstract class UnitAction
{
    public string actionName;
    public string description;
    public Unit actor;
    public Unit target;
    public bool targetsEnemy;
    public bool targetsAlly;
    public int range;

}
