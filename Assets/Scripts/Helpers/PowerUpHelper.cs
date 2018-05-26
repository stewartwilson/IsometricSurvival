using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class PowerUpHelper
{
    public static Dictionary<PowerUp, string> powerUpDescriptionMap = new Dictionary<PowerUp, string>() {
        { PowerUp.Heal1, "Heal Power +1"},
        { PowerUp.Hook1, "Hook Power +1"},
        { PowerUp.JumpNurse1, "Nurse jump distance +1"},
        { PowerUp.MovementHunter1, "Hunter movement distance +1"},
        { PowerUp.Shoot1, "Shoot Power +1"},
        { PowerUp.SpeedFisherman1, "Fisherman speed +1"},
        { PowerUp.Trap1, "Trap Power +1"},
    };

    public static Dictionary<PowerUp, UnitType> powerUpUnitTypeMap = new Dictionary<PowerUp, UnitType>()
    {
        { PowerUp.Heal1, UnitType.Nurse},
        { PowerUp.Hook1, UnitType.Fisherman},
        { PowerUp.JumpNurse1, UnitType.Nurse},
        { PowerUp.MovementHunter1, UnitType.Hunter},
        { PowerUp.Shoot1, UnitType.Hunter},
        { PowerUp.SpeedFisherman1, UnitType.Hunter},
        { PowerUp.Trap1, UnitType.Contractor},
    };

    public static Dictionary<PowerUp, string> powerUpTitle = new Dictionary<PowerUp, string>()
    {
        { PowerUp.Heal1, "Heal Power +1"},
        { PowerUp.Hook1, "Hook Power +1"},
        { PowerUp.JumpNurse1, "Nurse jump distance +1"},
        { PowerUp.MovementHunter1, "Hunter movement distance +1"},
        { PowerUp.Shoot1, "Shoot Power +1"},
        { PowerUp.SpeedFisherman1, "Fisherman speed +1"},
        { PowerUp.Trap1, "Trap Power +1"},
    };
}