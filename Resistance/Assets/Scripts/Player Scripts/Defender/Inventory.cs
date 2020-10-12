using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class DefaultKeyBinds : SerializableDictionaryBase<KeyCode, GameObject> { }

public class Inventory : MonoBehaviour
{
    //structures
    [Header("Structure Images")]
    public DefaultKeyBinds keybinds;

    private List<GameObject> currentList;
    private GameObject itemToBuild = null;
    private GameObject currentlyActive = null;
    private int currentIndex = 0;
    private int lastIndex = 0;

    public GameObject ItemToBuild { get => itemToBuild; set => itemToBuild = value; }
    public List<GameObject> CurrentList { get => currentList; set => currentList = value; }
    public GameObject CurrentlyActive { get => currentlyActive; set => currentlyActive = value; }
    public int LastIndex { get => lastIndex; set => lastIndex = value; }

    public void ListenForInput()
    {
        foreach (KeyCode k in keybinds.Keys)
        {
            if (Input.GetKey(k))
            {
                currentlyActive = keybinds[k];
                break;
            }
        }
    }

    private void Update()
    {
        if (currentList != null && currentlyActive != null)
        {
            lastIndex = currentList.Count - 1;

            if (lastIndex < 0)
            {
                lastIndex = 0;
            }
            //previewItem = currentList[0];
            // Debug.Log("currently active: " + currentlyActive.transform.parent.name);
        }
    }

    public void ResetAll()
    {
        CurrentList = null;
        CurrentlyActive = null;
        currentIndex = 0;
        lastIndex = 0;
        //previewItem = null;
    }

    private void ShowInSlot(GameObject _img, GameObject _o)
    {
        _img.GetComponent<Image>().sprite = ConvertTextureToSprite.Convert(RuntimePreviewGenerator.GenerateModelPreview(_o.transform));
       // previewItem = _o;
    }

    public void ScrollThroughInventory(List<GameObject> _current)
    {
        try
        {

            // Raw will only return 1, -1 or 0
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                // If currentindex is at the last index
                if (currentIndex == CurrentList.Count - 1)
                {
                    // Set back to start
                    currentIndex = 0;
                }
                else
                {
                    // Increment it
                    currentIndex++;
                }
            }
            // Show
            ShowInSlot(CurrentlyActive, _current[currentIndex]);

        }
        catch (System.ArgumentOutOfRangeException)
        {
            currentIndex = 0;
        }
    }
}


public static class ConvertTextureToSprite
{
    public static Sprite Convert(Texture2D _t)
    {
        return Sprite.Create(_t, new Rect(0, 0, _t.width, _t.height), Vector2.zero);
    }
}