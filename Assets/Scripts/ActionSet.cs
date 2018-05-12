using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionSet {
    [SerializeField]
    public List<ActionMap> actions;
}

[System.Serializable]
public class ActionMap
{
    public Action action;
    public bool hasBeenDone;

    public ActionMap(Action action, bool hasBeenDone)
    {
        this.action = action;
        this.hasBeenDone = hasBeenDone;
    }
}
