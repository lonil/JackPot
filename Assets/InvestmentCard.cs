using System;
using System.Collections;
using TMPro;
using UnityEngine;

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
    [SerializeField] TMP_Text investment_Label;

    public Investment_Obj inv;

    float mont = 0;

    Coroutine cor;

    public float _Value
    {
        get => mont;
        set
        {
            OnValueChanged?.Invoke(this.mont, value);

            this.mont = value;

            investment_Label.text = mont.ToString("0.00");
        }
    }

    public float BoughtFromPlayer
    {
        get => mont;
        set
        {
            mont = value;

            investment_Label.text = mont.ToString();
        }
    }

    public Investment_Obj _Investment
    {
        get => inv;
        set
        {
            inv = value;

            UpdateLabels();
        }
    }

    void UpdateLabels()
    {
        estimateValue.text = inv.estimateValue.ToString();
        max.text = inv.max.ToString();
        min.text = inv.min.ToString();
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
            if (mont + v >= 0)
            {
                PlayerStatus.instance.Transiction(1);

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

