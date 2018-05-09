using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicUnitController : PlayerUnitController
{

    public override void doAction1()
    {
        action1.act();
    }

    public override void doAction2()
    {
        action2.act();
    }

}
