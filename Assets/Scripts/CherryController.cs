using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField]
    private GameObject cherry;
    private Tweener tweenRef;

    private int lastTime, lastMoveTime;
    private float timer;
    private const float spawnTime = 30.0f;

    private GameObject cherryRef;
    private Vector3 startPosition, finalPosition;

    private List<Vector3> listOfZones = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        tweenRef = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        // Random Range every 15 seconds
        Invoke("SetZones", 15.0f);

        timer += Time.deltaTime;
        if ((int)timer > lastTime)
        {
            lastTime = (int)timer;
        }
        if (timer > lastMoveTime + spawnTime)
        {
            SpawnCherry();
            lastMoveTime = (int)timer;
        }

        DestroyCherry();
    }

    // Set Random Range (Left, Right, Up, Down)
    private void SetZones()
    {
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(0.0f, Random.Range(-1.0f, 1.0f), 0.0f)));
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(1.0f, Random.Range(-1.0f, 1.0f), 0.0f)));
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-1.0f, 1.0f), 1.0f, 0.0f)));
        listOfZones.Add(Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, 0.0f)));
    }

    // Spawning Cherry 
    private void SpawnCherry()
    {
        startPosition = listOfZones[Random.Range(0, listOfZones.Count)];
        finalPosition = -startPosition;

        cherryRef = Instantiate(cherry, startPosition, Quaternion.identity);

        if (!tweenRef.TweenExists(cherryRef.transform))
        {
            tweenRef.AddTween(cherryRef.transform, cherryRef.transform.position, -cherryRef.transform.position, 10.0f);
        }
    }

    // Destroying Cherry
    private void DestroyCherry()
    {
        if (cherryRef != null)
        {
            if (cherryRef.transform.position == finalPosition)
            {
                Destroy(cherryRef);
            }
        }
    }
}
