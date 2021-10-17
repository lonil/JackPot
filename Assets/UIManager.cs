using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject blackBackground;

    [SerializeField] Transform investmentPanel;

    [SerializeField] float investmentPanelDelay;

    bool investmentPanelAnimating;
    bool investmentPanelOn;

    private void Awake()
    {
        instance = this;
    }

    public void TurnInvestment()
    {
        if (investmentPanelAnimating)
            return;

        StartCoroutine(TurnInvestmentCoroutine(investmentPanelOn = !investmentPanelOn));
    }

    IEnumerator TurnInvestmentCoroutine(bool b)
    {
        investmentPanelAnimating = true;

        Camera cam = Camera.main;
        Rect r = cam.rect;

        if (b)
        {
            investmentPanel.gameObject.SetActive(true);

            r.y = 0.4f;
            r.height = 0.45f;
            investmentPanel.DOScaleY(1, investmentPanelDelay);
        }
        else
        {
            r.y = 0;
            r.height = 0.85f;
            investmentPanel.DOScaleY(0, investmentPanelDelay);
        }

        cam.DORect(r, investmentPanelDelay);

        yield return new WaitForSeconds(investmentPanelDelay);

        if (!b)
            investmentPanel.gameObject.SetActive(false);

        investmentPanelAnimating = false;
    }
}
