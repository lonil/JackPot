using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investiments : MonoBehaviour
{
    public static Investiments instance;

    public List<InvestmentCard> cards = new List<InvestmentCard>();

    [SerializeField] Transform cardsParent;

    private void Awake()
    {
        instance = this;
    }

    public float GetTotalValueInvested() 
    {
        float f = 0;

        foreach (var inv in cards)   
        {
            f += inv._Value;
        }

        return f;
    }

    public float GetTotalInvested()
    {
        float f = 0;

        foreach (var inv in cards)
        {
            f += inv.invAmount;
        }

        return f;
    }
}
