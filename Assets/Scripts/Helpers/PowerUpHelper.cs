using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class PowerUpHelper
{
    public static void activatePowerUp(PowerUp powerUp, UnitController controller)
    {
        switch(powerUp)
        {
            case PowerUp.Heal1:
                foreach (ActionMap actionMap in controller.actionSet.actions)
                {
                    if (actionMap.action is Heal)
                    {
                        ((Heal)actionMap.action).healing++;
                    }
                }
                break;
            case PowerUp.Hook1:
                foreach (ActionMap actionMap in controller.actionSet.actions)
                {
                    if (actionMap.action is Hook)
                    {
                        ((Hook)actionMap.action).damage++;
                    }
                }
                break;
            case PowerUp.JumpNurse1:
                controller.maxJump++;
                break;
            case PowerUp.MovementHunter1:
                controller.maxMovement++;
                break;
            case PowerUp.Shoot1:
                foreach(ActionMap actionMap in controller.actionSet.actions)
                {
                    if(actionMap.action is Shoot)
                    {
                        ((Shoot)actionMap.action).damage++;
                    }
                }
                break;
            case PowerUp.SpeedFisherman1:
                controller.speed++;
                break;
            case PowerUp.Trap1:
                foreach (ActionMap actionMap in controller.actionSet.actions)
                {
                    if (actionMap.action is Trap)
                    {
                        ((Trap)actionMap.action).damage++;
                    }
                }
                break;
            default:
                break;
        }
    }
}