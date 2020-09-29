using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    Transform target;
    Vector3 startPos, endPos;
    float startTime, duration;

    public Tween(Transform target, Vector3 startPos, Vector3 endPos, float startTime, float duration)
    {
        this.target = target;
        this.startPos = startPos;
        this.endPos = endPos;
        this.startTime = startTime;
        this.duration = duration;
    }

    public Transform Target
    {
        get { return target; }
        private set { target = value; }
    }

    public Vector3 StartPos
    {
        get { return startPos; }
        private set { startPos = value; }
    }

    public Vector3 EndPos
    {
        get { return endPos; }
        private set { endPos = value; }
    }

    public float StartTime
    {
        get { return startTime; }
        private set { startTime = value; }
    }

    public float Duration
    {
        get { return duration; }
        private set { duration = value; }
    }
}