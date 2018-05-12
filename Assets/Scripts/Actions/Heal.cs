using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Heal : Action
{

    public Heal()
    {
        range = 1;
        actionName = "Heal";
    }

    public override void act()
    {
        base.act();
    }
}

