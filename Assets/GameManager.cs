using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float salary;

    int daysCounter;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TimeController.instance.OnFinishDay += CountDays;
    }

    void CountDays() 
    {
        if (++daysCounter >= 5) 
        {
            daysCounter = 0;

            PlayerStatus.instance.Transiction(salary);
        }
    }
}
