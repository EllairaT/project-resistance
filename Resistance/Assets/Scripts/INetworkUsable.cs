using Mirror;
public interface INetworkUsable
{
    void SetId(int value);
    int GetId();
    void Use(float healthDeduct);
    NetworkIdentity GetNetworkIdentity();
}
