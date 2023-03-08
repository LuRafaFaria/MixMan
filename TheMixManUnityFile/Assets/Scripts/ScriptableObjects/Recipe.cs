using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public new string name;

    public Ingredient ingredient1;
    public Ingredient ingredient2;

    public float barSpeed1;
    public float barSpeed2;

    public float percIngred1;
    //public float percIngred2;

    public float goalWidth;

}
