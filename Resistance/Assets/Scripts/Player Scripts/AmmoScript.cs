using UnityEngine;
using TMPro;

public class AmmoScript : MonoBehaviour
{
    public TextMeshProUGUI currentClipAmmo;
    public TextMeshProUGUI totalAmmo;
    public void SetClipAmmo(int ammo)
    {
        currentClipAmmo.text = ammo.ToString();
    }

    public void SetTotalAmmo(int ammo)
    {
        totalAmmo.text = ammo.ToString();
    }
}
