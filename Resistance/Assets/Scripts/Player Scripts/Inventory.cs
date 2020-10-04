using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //structures

    [Header("Structure Images")]
    public GameObject blocks;
    public GameObject fences;
    public GameObject gates;
    public GameObject stairs;
    public GameObject walls;
    public GameObject foundation;
    public GameObject previewItem = null;

    public bool isInBuildMode = false;
    public PlayerPurchase purchases;

    private List<GameObject> currentList;
    private GameObject itemToBuild = null;
    private GameObject currentlyActive = null;
    private int currentIndex = 0;
    private int lastIndex;

    public GameObject ItemToBuild { get => itemToBuild; set => itemToBuild = value; }
    public List<GameObject> CurrentList { get => currentList; set => currentList = value; }
    public GameObject CurrentlyActive { get => currentlyActive; set => currentlyActive = value; }
    public int LastIndex { get => lastIndex; set => lastIndex = value; }

    void Start()
    {
        List<GameObject> allThumbnails = new List<GameObject> { blocks, fences, gates, stairs, walls, foundation };

        for (int i = 0; i < purchases.GetAllPurchases().Count; i++)
        {
            //only show first item from each list
            if (purchases.GetAllPurchases()[i].Count != 0)
            {
                Texture2D t = RuntimePreviewGenerator.GenerateModelPreview(purchases.GetAllPurchases()[i][0].transform);
                allThumbnails[i].GetComponent<Image>().sprite = ConvertTextureToSprite.Convert(t);
            }
        }
    }

    private void Update()
    {

    }

    public void ResetAll()
    {
        CurrentList = null;
        CurrentlyActive = null;
        currentIndex = 0;
        previewItem = null;
    }

    public void ListenForInput()
    {
        if (Input.GetKeyDown(KeyCode.B)) //user disables build mode
        {
            isInBuildMode = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("blocks enabled");
                CurrentList = purchases.BlockArr;
                CurrentlyActive = blocks;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("fences enabled");
                CurrentList = purchases.FenceArr;
                CurrentlyActive = fences;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("gates enabled");
                CurrentList = purchases.GatesArr;
                CurrentlyActive = gates;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("stairs enabled");
                CurrentList = purchases.StairsArr;
                CurrentlyActive = stairs;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("walls enabled");
                CurrentList = purchases.WallsArr;
                CurrentlyActive = walls;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("foundation enabled");
                CurrentList = purchases.FoundationArr;
                CurrentlyActive = foundation;
            }

            if (currentList != null)
            {
                lastIndex = currentList.Count - 1;

                if (lastIndex < 0)
                {
                    lastIndex = 0;
                }
            }

            // Debug.Log(currentlyActive.name + ": , currently at index: " + currentIndex + ", last index: " + lastIndex) ;
            isInBuildMode = true;
        }
    }

    private void ShowInSlot(GameObject _img, GameObject _o)
    {
        _img.GetComponent<Image>().sprite = ConvertTextureToSprite.Convert(RuntimePreviewGenerator.GenerateModelPreview(_o.transform));
        previewItem = _o;
    }

    public void ScrollThroughInventory(List<GameObject> _current)
    {
        if (isInBuildMode)
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
                    if (!(currentList.Count == 1))
                    {
                        currentIndex++;
                    }
                }

            }
            // Show
            ShowInSlot(CurrentlyActive, _current[currentIndex]);
        }
    }

    public void SetItemToBuild()
    {
        itemToBuild = CurrentList[currentIndex];
        ResetAll();
    }

    public void FinishBuild()
    {
        //get stuff from Buildsystem
    }
}


public static class ConvertTextureToSprite
{
    public static Sprite Convert(Texture2D _t)
    {
        return Sprite.Create(_t, new Rect(0, 0, _t.width, _t.height), Vector2.zero);
    }
}