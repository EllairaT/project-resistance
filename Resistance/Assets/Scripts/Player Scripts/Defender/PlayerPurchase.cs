using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[System.Serializable] public class StructurePurchases : SerializableDictionaryBase<GameObject, int> { }
[System.Serializable] public class MaterialPurchases : SerializableDictionaryBase<GameObject, int> { }

public class PlayerPurchase : BaseMonobehaviour
{
    //--all the structures and materials that the user has bought.
    public StructurePurchases StructurePurchase;
    public MaterialPurchases MaterialPurchase;

    public void AddToStructurePurchases(GameObject _o)
    {
        if (!StructurePurchase.ContainsKey(_o))
        {
            StructurePurchase.Add(_o, 0);
        }
        else
        {
            ++StructurePurchase[_o];
        }
    }  

    public void AddToMaterialPurchases(GameObject _m)
    {
        if (!MaterialPurchase.ContainsKey(_m))
        {
            MaterialPurchase.Add(_m, 0);
        }
        else
        {
            ++MaterialPurchase[_m];
        }
    }
}
