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
    [SerializeField] TMP_Text v;
    [SerializeField] TMP_Text bou;

    public Investment_Obj inv;

    float value = 1;
    float boughtFromPlayer_Value;

    int boughtFromPlayer;

    Coroutine cor;

    public int BoughtFromPlayer
    {
        get => boughtFromPlayer;
        set 
        {
            boughtFromPlayer = value;

            bou.text = boughtFromPlayer.ToString();
        }
    }
    public float _Value
    {
        get => value;
        set
        {
            OnValueChanged?.Invoke(this.value, value);

            this.value = value;
            v.text = this.value.ToString();
        }
    }

    private void Update()
    {
        _Value = value;
    }

    public Investment_Obj _Investment
    {
        get => inv;
        set
        {
            inv = value;

            inv.estimateValue = (EventManager.instance.curves[inv.tag][0].multiplier);

            estimateValue.text = "1 : " + (inv.estimateValue).ToString("0.00");
            max.text = (inv.max = UnityEngine.Random.Range(2.3f, 8.2f)).ToString("0.00");
            min.text = (inv.min = UnityEngine.Random.Range(-5.1f, -0.7f)).ToString("0.00");
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
        _Investment = _Investment;

        value = 1;
    }

    bool Transiction(bool isGreen, int v)
    {
        if (isGreen)
        {
            if (PlayerStatus.instance.TryTransiction(v))
            {
                BoughtFromPlayer++;

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

                BoughtFromPlayer--;

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

