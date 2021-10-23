using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public Dictionary<Tag, List<Curve>> curves = new Dictionary<Tag, List<Curve>>();

    List<Event> events = new List<Event>();

    [SerializeField] EventCard cardPrefab;
    [SerializeField] Transform newCardRef;
    [SerializeField] AnimationCurve defaultCurve;

    [SerializeField] float range;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TimeController.instance.OnReachAnHour += CalculateNewValueForAll;

        var eve = Resources.Load<Event>("");

        curves.Add(Tag.Entertaiment, new List<Curve>(new Curve[] { new Curve() { constant = 1, curve = defaultCurve,
             duration = 100, multiplier = Random.Range(0.01f, 0.12f) } }));

        curves.Add(Tag.Sales, new List<Curve>(new Curve[] { new Curve() { constant = 1, curve = defaultCurve,
             duration = 100, multiplier = Random.Range(0.01f, 0.12f) } }));

        curves.Add(Tag.Tecnology, new List<Curve>(new Curve[] { new Curve() { constant = 1, curve = defaultCurve,
             duration = 100, multiplier = Random.Range(0.01f, 0.12f) } }));

        curves.Add(Tag.Transport, new List<Curve>(new Curve[] { new Curve() { constant = 1, curve = defaultCurve,
             duration = 100, multiplier = Random.Range(0.01f, 0.12f) } }));
    }

    public void AddRandomEvent()
    {
        Instantiate(cardPrefab, newCardRef.position, new Quaternion());
    }

    public void AddCurve(Tag tag, Curve curve)
    {
        if (curves.ContainsKey(tag))
            curves[tag].Add(curve);
        else
            curves.Add(tag, new List<Curve>(new Curve[] { curve }));
    }

    void CalculateNewValueForAll(int h)
    {
        float a = 1;
        float b;
        foreach (var v in Investiments.instance.cards)
        {
            if (curves.ContainsKey(v.inv.tag))
                a = GetValueByCurves(curves[v.inv.tag]);

            //b = 0.5f / v.inv.subTags.Length;

            //for (int i = 0; i < v.inv.subTags.Length; i++)
            //{
            //    if (curves.ContainsKey(v.inv.subTags[i]))
            //        a += GetValueByCurves(curves[v.inv.subTags[i]]) * b;
            //}

            v._Value = v._Value * a;// + Random.Range(-range, range);
        }
    }

    float GetValueByCurves(List<Curve> c)
    {
        return c[0].GetValue();
    }
}
