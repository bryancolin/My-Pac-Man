using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private float timeDuration;
    private Tweener tweener;

    public AudioSource clip;
    private float lastTime, timer;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = timer = 0;
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDuration != 0)
        {
            if (item.transform.position.x == -1.5f && item.transform.position.y == 0.0f)
            {
                tweener.AddTween(item.transform, item.transform.position, new Vector3(-12.5f, 0.0f, 0.0f), timeDuration);
                item.GetComponent<Animator>().SetTrigger("Left");
            }
            if (item.transform.position.x == -12.5f && item.transform.position.y == 0.0f)
            {
                tweener.AddTween(item.transform, item.transform.position, new Vector3(-12.5f, 13.0f, 0.0f), timeDuration);
                item.GetComponent<Animator>().SetTrigger("Up");
            }
            if (item.transform.position.x == -12.5f && item.transform.position.y == 13.0f)
            {
                tweener.AddTween(item.transform, item.transform.position, new Vector3(-1.5f, 13.0f, 0.0f), timeDuration);
                item.GetComponent<Animator>().SetTrigger("Right");
            }
            if (item.transform.position.x == -1.5f && item.transform.position.y == 13.0f)
            {
                tweener.AddTween(item.transform, item.transform.position, new Vector3(-1.5f, 0.0f, 0.0f), timeDuration);
                item.GetComponent<Animator>().SetTrigger("Down");
            }

            timer += Time.deltaTime;

            if (timer >= lastTime)
            {
                clip.PlayScheduled(lastTime);
                lastTime += 0.675f;
            }
        }
    }
}

