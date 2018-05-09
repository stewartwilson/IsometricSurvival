﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterUnitController : PlayerUnitController
{

    public override void doAction1()
    {
        action1.act();
    }

    public override void doAction2()
    {
        action2.act();
    }

    protected override void initiateUnitChacteristics()
    {
        action1 = new Shoot();
        action2 = new Melee();
    }

}