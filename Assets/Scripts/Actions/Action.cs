using UnityEngine;

[System.Serializable]
public class Action
{
    public string actionName;
    public GameObject actor;
    public GameObject target;
    public int range;
    public bool canTargetSelf;

    public virtual void act()
    {
        Debug.Log(this);
    }

    public override string ToString()
    {
        return actionName;
    }

}

