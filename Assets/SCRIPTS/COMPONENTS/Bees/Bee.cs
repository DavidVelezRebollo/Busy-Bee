using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bee", menuName = "Bee")]
public class Bee : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite beeSprite;

    public int workSpeed = 1; 
    public int effectiveWorkSpeed = 2;
}