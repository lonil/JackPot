using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    Transform target;

    Coroutine coroutine;

    Camera cam;
    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 9))
            {
                target = hit.collider.transform.parent;

                coroutine = StartCoroutine(M_Update());
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
    }

    IEnumerator M_Update()
    {
        while (true)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 8))
            {
                target.position = hit.point;
            }

            yield return null;
        }
    }
}
