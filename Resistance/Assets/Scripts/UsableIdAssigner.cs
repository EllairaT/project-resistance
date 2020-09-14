using Mirror;

public class UsableIdAssigner : NetworkBehaviour
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        INetworkUsable[] usables = GetComponents<INetworkUsable>();

        for (int i = 0; i < usables.Length; i++)
        {
            usables[i].SetId(i);
        }
    }
}
