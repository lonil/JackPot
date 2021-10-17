using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EndOfTheDay_PopUp : MonoBehaviour
{
    const string DefaultInvestment = @"Investiment: ";
    const string DefaultProfit = @"Profit: ";
    const string DefaultProfitPerCent = @"Profit %: ";

    [SerializeField] Transform panel;

    [Header("Form")]
    [SerializeField] TMP_Text inv;
    [SerializeField] TMP_Text profit;
    [SerializeField] TMP_Text profitCent;

    private void Start()
    {
        TimeController.instance.OnFinishDay += Display;
    }

    public void Display()
    {
        UIManager.instance.blackBackground.SetActive(true);
        panel.gameObject.SetActive(true);

        panel.localScale = Vector3.zero;
        panel.DOScale(Vector3.one, 0.618f);

        float totalValue = Investiments.instance.GetTotalValueInvested();
        int total = Investiments.instance.GetTotalInvested();

        inv.text = DefaultInvestment + totalValue.ToString();
        profit.text = DefaultProfit + (totalValue - total).ToString();

        if (totalValue != 0)
            profitCent.text = DefaultProfitPerCent + Mathf.FloorToInt((totalValue - total) / totalValue * 100).ToString();
        else
            profitCent.text = DefaultProfitPerCent + "0";
    }

    public void GoToNextDay()
    {
        TimeController.instance.StartDay();

        UIManager.instance.blackBackground.SetActive(false);
        panel.gameObject.SetActive(false);
    }
}
