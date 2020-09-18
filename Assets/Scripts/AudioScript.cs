using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        audioSource.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Time.timeScale = 1;
        }
    }
}
