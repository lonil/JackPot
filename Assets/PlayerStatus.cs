using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    const string DefaultCapitalText = @"$ ";
    const string DefaultLossText = @"Losses: ";
    const string DefaultGainText = @"Gain: ";

    float capital;

    int loss;
    int gain;

    [SerializeField] TMP_Text capital_Label;
    [SerializeField] TMP_Text loss_Label;
    [SerializeField] TMP_Text gain_Label; 

    public float Capital 
    {
        get => capital;
        set
        {
            capital = value;
            capital_Label.text = DefaultCapitalText + capital.ToString();
        }
    }

    public int Loss
    {
        get => loss;
        set
        {
            loss = value;
            loss_Label.text = DefaultLossText + loss.ToString();
        }
    }

    public int Gain
    {
        get => gain;
        set
        {
            gain = value;
            gain_Label.text = DefaultGainText + gain.ToString();
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Loss = 0;
        Gain = 0;
        Capital = GameManager.instance.salary;
    }

    public bool TryTransiction(float v)
    {
        if (capital - v < 0)
            return false;
        else 
        {
            Capital -= v;

            return true;
        }
    }

    public void Transiction(float v)
    {
        Capital += v;
    }
}
