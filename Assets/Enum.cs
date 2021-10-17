using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag 
{
    Entertaiment,
    Sales,
    Tecnology,
    Transport
}

[System.Flags]
public enum RequirementType
{
    None,
    ByTime,
    ByTagMinimum
}