using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    private AudioManager backgroundMusic;

    [SerializeField]
    private GameObject[] ghost;

    private void Awake()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].GetComponent<Animator>().SetFloat("Speed", 1);
        }

        backgroundMusic = GameObject.FindWithTag("Managers").GetComponent<AudioManager>();
        backgroundMusic.ChangeBackgroundMusic(1);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScared()
    {
        for(int i=0; i<ghost.Length; i++)
        {
            ghost[i].GetComponent<Animator>().SetTrigger("Scared");
        }
        backgroundMusic.ChangeBackgroundMusic(2);
    }
}
