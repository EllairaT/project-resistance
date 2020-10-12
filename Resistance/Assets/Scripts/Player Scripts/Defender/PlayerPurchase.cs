
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[System.Serializable] public class StructurePurchases : SerializableDictionaryBase<GameObject, int> { }
[System.Serializable] public class MaterialPurchases : SerializableDictionaryBase<Materials, int> { }

public class PlayerPurchase : MonoBehaviour
{
    //--all the structures and materials that the user has bought.
    public StructurePurchases structurePurchase;
    public MaterialPurchases materialPurchase;

    public void AddToStructurePurchases(GameObject _o)
    {
        if (!structurePurchase.ContainsKey(_o))
        {
            structurePurchase.Add(_o, 0);
        }
        else
        {
            ++structurePurchase[_o];
        }
    }  

    public void AddToMaterialPurchases(Materials _m)
    {
        if (!materialPurchase.ContainsKey(_m))
        {
            materialPurchase.Add(_m, 0);
        }
        else
        {
            ++materialPurchase[_m];
        }
    }
}
