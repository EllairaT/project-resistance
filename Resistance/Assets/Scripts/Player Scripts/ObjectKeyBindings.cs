using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "objKB", menuName = "Bind object to key")]
public class ObjectKeyBindings : ScriptableObject
{
    public GameObject obj;
    public KeyCode keyBind;
}
