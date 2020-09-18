using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    //Add spawn point
    private void Awake() => PlayerSpawnSystem.AddSpawnPoint(transform);

    //Remove spawn point
    private void OnDestroy() => PlayerSpawnSystem.RemoveSpawnPoint(transform);

    //Draw spawn points in scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }
}