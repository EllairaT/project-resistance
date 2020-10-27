using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOns : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite _icon;

    [SerializeField] private string _description;

    [SerializeField] private int _cost;

    public string Name => _name;
    public Sprite Icon => _icon;

    public string Description => _description;

    public int Cost { get => _cost; set => _cost = value; }
}
