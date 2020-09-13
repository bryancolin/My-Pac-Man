using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (activeTween == null)
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween != null)
        {
            float distance = Vector3.Distance(activeTween.Target.transform.position, activeTween.EndPos);
<<<<<<< Updated upstream
            Debug.Log("Distance : " + distance);

            if (distance > 0.1f)
            {
                float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
                Debug.Log("Time : " + timeFraction);
                activeTween.Target.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
=======

            if (distance > 0.1f)
            {
                Linear(activeTween);
>>>>>>> Stashed changes
            }

            if (distance <= 0.1f)
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }
        }
    }

    public void Linear(Tween activeTween)
    {
        float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
        //Debug.Log("Time : " + timeFraction);
        activeTween.Target.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
    }

    public void Cubic(Tween activeTween)
    {
        float timeFraction = Mathf.Pow((Time.time - activeTween.StartTime) / activeTween.Duration, 3);
        //Debug.Log("Time : " + timeFraction);
        activeTween.Target.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
    }
}
