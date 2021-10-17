using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Investment", menuName = "ScriptableObjects/Investment", order = 1)]
public class Investment_Obj : ScriptableObject
{
    public Tag tag;
    public Tag[] subTags;

    public float estimateValue;

    public float max;
    public float min;
}
