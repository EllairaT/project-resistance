using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class DefaultKeyBinds : SerializableDictionaryBase<KeyCode, GameObject> { }

public class Inventory : BaseMonobehaviour
{
    //structures
    [Header("Structure Images")]
    public DefaultKeyBinds keybinds;
    public GameObject foundationPreview;
    public GameObject foundationSlot;
    public GameObject materialSlot;
    public Sprite highlight;
    public Sprite normalBorder;
    private int currentIndex = 0;

    public GameObject ItemToBuild { get; set; } = null;
    public List<GameObject> CurrentList { get; set; } = new List<GameObject>();
    public GameObject CurrentlyActive { get; set; } = null;
    public MaterialPurchases MatPurchases { get; set; }
    public StructurePurchases StrucPurchases { get; set; }

    private BuildSystem buildsys;
    private GameObject preview;

    private void Start()
    {
        buildsys = transform.root.GetComponent<BuildSystem>();
        ShowStructureInSlot(foundationSlot, foundationPreview);
        ShowTextureInSlot(materialSlot, buildsys.defaultMaterial);

        if (StrucPurchases != null)
        {

        }
        if(MatPurchases != null)
        {

        }
    }

    public void ListenForInput()
    {
        foreach (KeyCode k in keybinds.Keys)
        {
            if (Input.GetKey(k))
            {
                CurrentlyActive = keybinds[k];
                EnableSlot(CurrentlyActive);
                break;
            }
        }
        ScrollThroughInventory(CurrentlyActive, CurrentList);
    }

    private void EnableSlot(GameObject _s)
    {
        ResetList(); //make sure there are no objects in the currentlist

        //highlight the slot
        foreach (GameObject _o in keybinds.Values)
        {
            if (_o.Equals(_s))
            {
                _s.GetComponent<Image>().sprite = highlight;
            }
            else
            {
                _o.GetComponent<Image>().sprite = normalBorder;
            }
        }

        //add all the objects in the current list
        if (CurrentlyActive.GetComponent<InventorySlot>().type == StructureType.MATERIAL)
        {
            foreach (GameObject _m in MatPurchases.Keys)
            {
                if (!CurrentList.Contains(_m))
                {
                    CurrentList.Add(_m);
                }
            }
        }
        else
        {
            //add all the objects with the same type as the slot into the currentlist
            foreach (GameObject _o in StrucPurchases.Keys)
            {
                if (_o.GetComponent<Preview>().type.Equals(CurrentlyActive.GetComponent<InventorySlot>().type))
                {
                    if (!CurrentList.Contains(_o))
                    {                     
                        CurrentList.Add(_o);
                    }
                }
            }
        }
    }

    public void ResetList()
    {
        CurrentList.Clear();
        currentIndex = 0;
    }

    private void ShowStructureInSlot(GameObject _img, GameObject _o)
    {
        _img.transform.Find("Image").GetComponent<Image>().sprite = ConvertTextureToSprite.Convert(RuntimePreviewGenerator.GenerateModelPreview(_o.transform));
    }

    private void ShowTextureInSlot(GameObject _img, Materials _m)
    {
        _img.transform.Find("Image").GetComponent<Image>().sprite = ConvertTextureToSprite.Convert(_m.texture);
    }

    public void ScrollThroughInventory(GameObject _currentlyActive, List<GameObject> _currentList)
    {
        if (_currentList != null)
        {
            float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

            if (scroll != 0)
            {
                if (scroll > 0) //+1
                {
                    currentIndex++;
                }
                else //-1
                {
                    currentIndex--;
                }

                if (currentIndex == _currentList.Count)
                {
                    currentIndex = 0;
                }
                if (currentIndex < 0)
                {
                    currentIndex = _currentList.Count - 1;
                }
                

                if (_currentlyActive.GetComponent<InventorySlot>().type == StructureType.MATERIAL) 
                {
                    ShowTextureInSlot(_currentlyActive, _currentList[currentIndex].GetComponent<Materials>());
                }
                else
                {
                    ShowStructureInSlot(_currentlyActive, _currentList[currentIndex]);
                }
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
}