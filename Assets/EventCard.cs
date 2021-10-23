using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EventCard : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] TextMesh title;
    [SerializeField] TextMesh desc;

    [SerializeField] Texture2D cardBackground;

    [SerializeField] Event _event;

    public Event _Event
    {
        get => _event;
        set
        {
            _event = value;

            Activate();
        }
    }

    public void Activate() 
    {
        foreach (var v in _event.curves)
        {
            EventManager.instance.AddCurve(v.tag, v.curve);
        }

        TimeController.instance.OnReachAnHour += CheckDelay;
    }   

    public void CheckDelay(int d)
    {
        if (d >= _event.displayDelay)
        {
            StartCoroutine(Display());

            TimeController.instance.OnReachAnHour -= CheckDelay;
        }
    }

    IEnumerator Display()
    {
        _renderer.transform.DOScaleX(-_renderer.transform.localScale.x, 0.6f);
        
        yield return new WaitForSeconds(0.3f);

        _renderer.material.mainTexture = cardBackground;
        
        desc.transform.DOScaleX(-desc.transform.localScale.x, 0.3f);
        title.transform.DOScaleX(-title.transform.localScale.x, 0.3f);

        yield return new WaitForSeconds(0.3f);

        desc.text = _event.description;
        title.text = _event._name;
    }
}
