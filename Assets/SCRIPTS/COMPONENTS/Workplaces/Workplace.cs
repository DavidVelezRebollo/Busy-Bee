using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Workplace", menuName = "Workplace")]
public class Workplace : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite workplaceSprite;

    public int honeyProduction;
}