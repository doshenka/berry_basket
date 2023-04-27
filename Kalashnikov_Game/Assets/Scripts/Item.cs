using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public string description;
}
