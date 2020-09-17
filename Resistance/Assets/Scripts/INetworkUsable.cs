using Mirror;

//Interface for objects that will be in the game that player's dont have authority over
public interface INetworkUsable
{
    void SetId(int value); //Set ID
    int GetId(); //Get ID
    void Use(float healthDeduct); //This 'Use' method is used to reduce an in-game object's health
    NetworkIdentity GetNetworkIdentity(); //The game object's network identity
}