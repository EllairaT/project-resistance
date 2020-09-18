using UnityEngine;
using TMPro;

public class AmmoScript : MonoBehaviour
{
    public TextMeshProUGUI currentClipAmmo;
    public TextMeshProUGUI totalAmmo;

    //Set the current amount of ammo in the player's clip
    public void SetClipAmmo(int ammo)
    {
        currentClipAmmo.text = ammo.ToString();
    }

    //Set the total ammo the player has
    public void SetTotalAmmo(int ammo)
    {
        totalAmmo.text = ammo.ToString();
    }
}
