using Mirror;

public class UsableIdAssigner : NetworkBehaviour
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        INetworkUsable[] usables = GetComponents<INetworkUsable>();

        //Assign unique ID's to all components that player's don't have authority over
        //e.g. Target object
        for (int i = 0; i < usables.Length; i++)
        {
            usables[i].SetId(i);
        }
    }
}