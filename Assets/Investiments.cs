using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investiments : MonoBehaviour
{
    public static Investiments instance;

    public List<InvestmentCard> cards = new List<InvestmentCard>();

    [SerializeField] Transform cardsParent;
    [SerializeField] Transform prefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++) 
        {
            Instantiate(prefab, cardsParent);
        }
    }

    public float GetTotalValueInvested() 
    {
        float f = 0;

        foreach (var inv in cards) 
        {
            f += inv.BoughtFromPlayer * inv._Value;
        }

        return f;
    }

    public int GetTotalInvested()
    {
        int f = 0;

        foreach (var inv in cards)
        {
            f += inv.BoughtFromPlayer;
        }

        return f;
    }
}
