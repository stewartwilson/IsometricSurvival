using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Shoot : Action
{
    public int range;
    public int damage;

    public Shoot()
    {
        range = 3;
        damage = 1;
        actionName = "Shoot";
    }
}

