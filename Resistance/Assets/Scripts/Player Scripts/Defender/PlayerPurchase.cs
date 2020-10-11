
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[System.Serializable]
public class Purchases : SerializableDictionaryBase<GameObject, int>{}

public class PlayerPurchase : MonoBehaviour
{
    //--all the structures that the user has bought.
    public bool hi;

    public Purchases blocks;
    public Purchases fences;

}
