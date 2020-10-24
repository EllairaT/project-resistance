
using UnityEngine.EventSystems;

public interface ICustomUpdate : IEventSystemHandler
{
    void SendUpdate(int numberSpawned);
}
