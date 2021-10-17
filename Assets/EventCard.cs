using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCard : MonoBehaviour
{
    public TextMesh time;

    Event _event;

    public void Activate() 
    {
        foreach (var v in _event.curves)
        {
            EventManager.instance.AddCurve(v.tag, v.curve);
        }
    }

    public void Display()
    {
        
    }
}
