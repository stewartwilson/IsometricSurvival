using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Power Up", menuName = "Power Ups")]
public class PowerUpData : ScriptableObject
{
    [SerializeField]
    public PowerUp powerUp;
    [SerializeField]
    public string title;
    [SerializeField]
    public string description;
    [SerializeField]
    public UnitType unitType;

}
