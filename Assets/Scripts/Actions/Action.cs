using UnityEngine;

public class Action : MonoBehaviour
{
    public string actionName;
    public GameObject actor;
    public GameObject target;

    public void act()
    {
        Debug.Log(actionName + "," + actor.name + "," + target.name);
    }

}

