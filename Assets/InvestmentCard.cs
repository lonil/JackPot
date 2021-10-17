using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InvestmentCard : MonoBehaviour
{
    delegate bool Test(int i);

    const float doubleClickCheckDelay = 0.23f;
    const float freq = 0.13f;

    public Action<float, float> OnValueChanged;

    Test[] conditions;

    [SerializeField] TMP_Text estimateValue;
    [SerializeField] TMP_Text max;
    [SerializeField] TMP_Text min;

    public Investment_Obj inv;

    float value = 1;
    float boughtFromPlayer_Value;

    int boughtFromPlayer;

    Coroutine cor;

    public int BoughtFromPlayer => boughtFromPlayer;

    public float _Value
    {
        get => value;
        set
        {
            OnValueChanged?.Invoke(this.value, value);

            this.value = value;
        }
    }

    public Investment_Obj _Investment
    {
        get => inv;
        set
        {
            inv = value;

            estimateValue.text = inv.estimateValue.ToString();
            max.text = inv.max.ToString();
            min.text = inv.min.ToString();
        }
    }

    public void OnClickButtonDown(int i)
    {
        cor = StartCoroutine(CheckDoubleClick(i));
    }

    public void OnExitButton()
    {
        if (cor != null)
            StopCoroutine(cor);
    }

    private void Start()
    {
        value = 1;
    }

    bool Transiction(bool isGreen, int v)
    {
        if (isGreen)
        {
            if (PlayerStatus.instance.TryTransiction(v))
            {
                boughtFromPlayer++;

                return true;
            }
            else
                return false;
        }
        else
        {
            if (boughtFromPlayer + v >= 0)
            {
                PlayerStatus.instance.Transiction(value);

                boughtFromPlayer--;

                return true;
            }
            else
                return false;
        }
    }

    IEnumerator CheckDoubleClick(int d)
    {
        float time = 0;
        bool isGreen = d > 0;
        bool b = false;

        while (time < doubleClickCheckDelay)
        {
            time += Time.deltaTime;

            if (Input.GetMouseButtonUp(0))
            {
                Transiction(isGreen, d);

                b = true;

                break;
            }

            yield return null;
        }

        if (!b)
        {
            time = 0;
            while (true)
            {
                time += Time.deltaTime;

                if (time >= freq)
                {
                    time = 0;

                    if (!Transiction(isGreen, d))
                        break;
                }

                if (Input.GetMouseButtonUp(0))
                    break;

                yield return null;
            }
        }
    }
}

