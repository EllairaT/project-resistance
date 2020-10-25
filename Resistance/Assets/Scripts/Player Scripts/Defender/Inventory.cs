using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class DefaultKeyBinds : SerializableDictionaryBase<KeyCode, GameObject> { }

public class Inventory : BaseMonobehaviour
{
    #region variables
    [Header("Structure Images")]
    public DefaultKeyBinds keybinds;
    public GameObject foundationPreview;
    public GameObject foundationSlot;
    public GameObject materialSlot;
    public Sprite highlight;
    public Sprite normalBorder;

    public GameObject ItemToBuild { get; set; } = null;
    public List<GameObject> CurrentList { get; set; } = new List<GameObject>();
    public GameObject CurrentlyActive { get; set; } = null;
    public MaterialPurchases MatPurchases { get; set; }
    public StructurePurchases StrucPurchases { get; set; }

    public BuildSystem buildsys;

    private int index;
    private int lastIndex;

    #endregion

    private void Start()
    {
        StrucPurchases = GetComponent<PlayerPurchase>().StructurePurchase;
        MatPurchases = GetComponent<PlayerPurchase>().MaterialPurchase;
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
    }

    private void AddToMaterialsList()
    {
        foreach (GameObject _m in MatPurchases.Keys)
        {
            if (!CurrentList.Contains(_m))
            {
                CurrentList.Add(_m);
            }
        }
    }

    private void AddToStructuresList()
    {
        if (StrucPurchases.Keys != null)
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

    #region highlight the slot
    private void EnableSlot(GameObject _s)
    {
        //make sure there are no objects in the currentlist
        ResetList(); 

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
            AddToMaterialsList();
        }
        else
        {
            AddToStructuresList();
        }
    }
    #endregion 

    public void ResetList()
    {
        CurrentList.Clear();
        index = 0;
    }

    private void ShowStructureInSlot(GameObject _img, GameObject _o)
    {
        _img.GetComponent<InventorySlot>().sprite.sprite = ConvertTextureToSprite.Convert(RuntimePreviewGenerator.GenerateModelPreview(_o.transform));
    }

    private void ShowTextureInSlot(GameObject _img, Materials _m)
    {
        _img.GetComponent<InventorySlot>().sprite.sprite = ConvertTextureToSprite.Convert(_m.texture);
    }

    public void ScrollThroughInventory()
    {
        #region commented out
        //if (_currentList != null)
        //{
        //    float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        //    if (scroll != 0)
        //    {
        //        if (scroll > 0) //+1
        //        {
        //            currentIndex++;
        //        }
        //        else //-1
        //        {
        //            currentIndex--;
        //        }

        //        if (currentIndex == _currentList.Count)
        //        {
        //            currentIndex = 0;
        //        }
        //        if (currentIndex < 0)
        //        {
        //            currentIndex = _currentList.Count - 1;
        //        }


        //        if (_currentlyActive.GetComponent<InventorySlot>().type == StructureType.MATERIAL)
        //        {
        //            ShowTextureInSlot(_currentlyActive, _currentList[currentIndex].GetComponent<Materials>());
        //        }
        //        else
        //        {
        //            ShowStructureInSlot(_currentlyActive, _currentList[currentIndex]);
        //        }
        //    }
        //}
        #endregion     
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        StartCoroutine(Scroll(scroll));
    }

    IEnumerator Scroll(float i)
    {
        while (i == 0)
        {
            yield return null;
        }

        if (CurrentList != null && CurrentlyActive != null)
        {
            if (i != 0)
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index++;
                }

                if (index > CurrentList.Count - 1)
                {
                    index = 0;
                }
                if (index < 0)
                {
                    index = CurrentList.Count - 1;
                }
            }
        }

        Debug.Log(index);
        ShowStructureInSlot(CurrentlyActive, CurrentList[index]);
        buildsys.NewBuild(CurrentList[index]);
    }

    public static class ConvertTextureToSprite
    {
        public static Sprite Convert(Texture2D _t)
        {
            return Sprite.Create(_t, new Rect(0, 0, _t.width, _t.height), Vector2.zero);
        }
    }
}