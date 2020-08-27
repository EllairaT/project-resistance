using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class PlayerManager : ScriptableObject
{
    public string objName = "Scriptable Object";
    private string username { get; set; }
    public string role { get; set; }
    public int gold { get; set; }
    public float goldMultiplier { get; set; }
    public Vector3[] spawnPoints;
}
