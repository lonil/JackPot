using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public Action OnFinishDay;
    public Action<int> OnReachAnHour;

    [Header("UI")]
    [SerializeField] RectTransform timeBar;
    [SerializeField] RectTransform hourIconParent;
    [SerializeField] Image hourMarkPrefab;

    [Header("Time Control")]
    [SerializeField] float realTimeForGameDay;

    Image[] hourMarks;

    float hourByTimeLimit;

    int currHour;
    float time;

    bool couting;

    Coroutine countingCoroutine;

    private void Awake()
    {
        instance = this;

        hourByTimeLimit = realTimeForGameDay / 24;
    }

    private void Start()
    {
        float space = (hourIconParent.rect.max.x - hourIconParent.rect.min.x) / 24;
        float iniX = (hourIconParent.rect.min.x - hourIconParent.rect.max.x) * 0.5f;

        hourMarks = new Image[24];
        for (int i = 0; i < 24; i++)
        {
            hourMarks[i] = Instantiate(hourMarkPrefab, hourIconParent);
            hourMarks[i].transform.localPosition = new Vector3(iniX + space * i, 0);

            hourMarks[i].color *= 0.5f;
        }

        StartDay();
    }

    public void StartDay()
    {
        time = 0;
        currHour = 0;

        couting = true;

        countingCoroutine = StartCoroutine(CountTime());
    }

    public void PauseTime(bool v)
    {
        if (v != couting)
        {
            couting = v;

            if (couting)
            {
                countingCoroutine = StartCoroutine(CountTime());
            }
            else
            {
                StopCoroutine(countingCoroutine);
            }
        }
    }

    IEnumerator CountTime()
    {
        timeBar.localScale = Vector3.up;

        float nextLimit;
        Vector3 aux;

        nextLimit = hourByTimeLimit;

        hourMarks[currHour].DOColor(hourMarks[currHour].color * 2, 0.38f);

        while (true)
        {
            time += Time.deltaTime;

            aux = timeBar.localScale;
            aux.x = time / realTimeForGameDay;
            timeBar.localScale = aux;

            if (time >= nextLimit)
            {
                if (++currHour >= 24)
                {
                    OnFinishDay?.Invoke();

                    break;
                }
                else
                {
                    OnReachAnHour?.Invoke(currHour);
                }

                hourMarks[currHour].DOColor(hourMarks[currHour].color * 2, 0.38f);

                nextLimit = (currHour + 1) * hourByTimeLimit;
            }

            yield return null;
        }
    }
}
