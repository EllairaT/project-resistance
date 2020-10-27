using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class DefaultKeyBinds : SerializableDictionaryBase<KeyCode, GameObject> { }

public class Inventory : BaseMonobehaviour
{
    #region variables
    [Header("Structure Images")]
    public DefaultKeyBinds keybinds;
    public Sprite highlight;
    public Sprite normalBorder;
    public TextMeshProUGUI matInfo;

    public List<GameObject> CurrentList { get; set; } = new List<GameObject>();

    public GameObject CurrentlyActive { get; set; } = null;
    public StructurePurchases StrucPurchases { get; set; }

    public BuildSystem buildsys;

    public int index;
    #endregion

    public void SetUp()
    {
        StrucPurchases = GetComponent<PlayerPurchase>().StructurePurchase;
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

    public GameObject GetObjectFromKey()
    {
        foreach (KeyCode k in keybinds.Keys)
        {
            if (Input.GetKey(k))
            {
                return keybinds[k];
            }
          //  break;
        }
        return null;
    }
    private void SetMaterial(Material m)
    {
        buildsys.SetMaterial(m);
    }

    private void AddToStructuresList()
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

    #region highlight the slot
    private void EnableSlot(GameObject _s)
    {
        //if (CurrentList != null)
        //{
        //}

        if (_s.GetComponent<Materials>() != null)
        {
            Debug.Log("materials!");
            SetMaterial(_s.GetComponent<Materials>().mat);
            matInfo.SetText("Structure material set to: " + _s.GetComponent<Materials>());
        }
        else
        {
            foreach (GameObject _o in keybinds.Values)
            {
                if (_o.Equals(_s))
                {
                    _s.GetComponent<InventorySlot>().sprite.sprite = highlight;
                }
                else
                {
                    _o.GetComponent<InventorySlot>().sprite.sprite = normalBorder;
                }
            }
            Debug.Log("add to structure list");
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
        _img.GetComponent<Image>().sprite = ConvertTextureToSprite.Convert(RuntimePreviewGenerator.GenerateModelPreview(_o.transform));
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
        StartCoroutine(Scroll(Input.GetAxisRaw("Mouse ScrollWheel")));
    }

    public IEnumerator Scroll(float i)
    {
        while (i == 0)
        {
            yield return null;
        }

        if ((CurrentList != null) && (CurrentlyActive != null))
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