using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Melee : Action
{
    public int range;
    public int damage;

    public Melee()
    {
        range = 1;
        damage = 1;
        actionName = "Melee";
    }
}

