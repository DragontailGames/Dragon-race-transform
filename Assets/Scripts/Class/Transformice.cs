using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transformice
{
    public string name;
    public EnumDT.TransformiceType transformiceType;
    public float speed;
    public Material material;
    public int level = 0;
    public float levelMultiplier = 0.20f;
}
