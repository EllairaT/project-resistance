using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public string characterName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCharacter()
    {
        Debug.Log("clicked: " + characterName);
        SpawnManager.Instance.SetCurrentCharacterType(characterName);
    }

    public void CreateCharacter()
    {
        SpawnManager.Instance.SetCurrentCharacterType(characterName); //use the +Prefab suffix to instantiate in next scene bc we're using idlePrefab suffix for characterselect
    }
}
