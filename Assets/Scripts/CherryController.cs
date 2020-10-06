using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField]
    private GameObject cherry;
    private Tweener tweenerRef;

    private float timer=0, lastTime=0;
    private List<Vector3> listOfZones = new List<Vector3>();

    private GameObject gameObject1;
    // Start is called before the first frame update
    void Start()
    {
        tweenerRef = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        SetZones();

        timer += Time.deltaTime;
        if (timer >= lastTime && timer % 2 == 0)
        {
            //gameObject1 = Instantiate(cherry, listOfZones[Random.Range(0, listOfZones.Count)], Quaternion.identity);
            //if (cherry.transform.position != new Vector3(0.0f, 0.0f, 0.0f))
            //{
            //    tweenerRef.AddTween(gameObject1.transform, gameObject1.transform.position, Vector3.zero, 10.0f);
            //}
            Instantiate(cherry, Vector3.zero, Quaternion.identity);
            Debug.Log(lastTime);
            lastTime += 1;
        }
        else if (timer >= lastTime)
        {
            Debug.Log(lastTime);
            lastTime += 1;
        }

        //bool spawned = false;
        //if (spawned == false)
        //{
        //    spawned = true;
        //    GameObject gameObject = Instantiate(cherry, randPos, Quaternion.identity);
        //}
    }

    // Set Random Range (Left, Right, Up, Down)
    private void SetZones()
    {
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(0.0f, Random.Range(-1.0f, 1.0f), 0.0f)));
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(1.0f, Random.Range(-1.0f, 1.0f), 0.0f)));
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-1.0f, 1.0f), 1.0f, 0.0f)));
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, 0.0f)));
    }
}
