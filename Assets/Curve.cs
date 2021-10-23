using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Curve 
{
    public bool addictive;

    public AnimationCurve curve;
    public int duration;
    public float multiplier;
    public float constant;

    public float tax;

    [HideInInspector] public int hours;

    public float _Value => (addictive ? 1 : 0) + curve.Evaluate(hours++ / duration) * multiplier;

    public float GetValue() 
    {
        return constant + curve.Evaluate((float)hours++ / duration) * multiplier;
    }

    public float GetValue(int hour)
    {
        return constant + curve.Evaluate((float)hour / duration) * multiplier;
    }
}

[System.Serializable]
public class Curve_Tag 
{
    public Tag tag;
    public Curve curve;
}