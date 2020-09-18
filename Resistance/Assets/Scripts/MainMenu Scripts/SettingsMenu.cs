using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    //Variable refer to audio mixer
    public AudioMixer audioMixer;

    //Volume slider to change master volume in game
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Fullscreen setting
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
