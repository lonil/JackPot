using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public Dictionary<Tag, List<Curve>> curves = new Dictionary<Tag, List<Curve>>();

    List<EventCard> events = new List<EventCard>();

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

        var r = Random.Range(1.08f, 1.23f);
        curves.Add(Tag.Entertaiment, new List<Curve>(new Curve[] { new Curve() { addictive = true, curve = defaultCurve,
             duration = 100, multiplier = r, unit = r/100 } }));

        r = Random.Range(1.08f, 1.23f);
        curves.Add(Tag.Sales, new List<Curve>(new Curve[] { new Curve() { addictive = true, curve = defaultCurve,
             duration = 100, multiplier = r , unit = r/100 } }));

        r = Random.Range(1.08f, 1.23f);
        curves.Add(Tag.Tecnology, new List<Curve>(new Curve[] { new Curve() { addictive = true, curve = defaultCurve,
             duration = 100, multiplier = r , unit = r/100} }));

        r = Random.Range(1.08f, 1.23f);
        curves.Add(Tag.Transport, new List<Curve>(new Curve[] { new Curve() { addictive = true, curve = defaultCurve,
             duration = 100, multiplier = r, unit = r/100 } }));

        TimeController.instance.OnFinishDay += delegate ()
        {
            AddRandomEvent();
        };
    }

    public void AddRandomEvent()
    {
        var r = Resources.Load<Event>(@"Assets/Resources/ScriptableObjects/Events");

        events.Add(Instantiate(cardPrefab, newCardRef.position, new Quaternion()));
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

            v._Value = a;// + Random.Range(-range, range);
        }
    }

    float GetValueByCurves(List<Curve> c)
    {
        return c[0]._Value;
    }
}
