using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider; //slider used for health bar
    
    //Set the current health of the player
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    //Set the max/starting health of the player
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}