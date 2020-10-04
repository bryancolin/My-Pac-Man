using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject[] gameObjects;

    private void Awake()
    {
        SetUpCamera();

        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        //audioSource.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Time.timeScale = 1;
        }
        SetAnimator();
    }

    void SetUpCamera()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 15.0f;
    }

    void SetAnimator()
    {
        for(int i=0; i<gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Animator>().SetFloat("Speed", 1);
        }
    }
}
