using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    private AudioManager backgroundMusic;

    [SerializeField]
    private GameObject[] ghost;
    public enum GhostState { Normal, Scared, Recovering, Death }

    public static GhostState currentRedGhost = GhostState.Normal;
    public static GhostState currentBlueGhost = GhostState.Normal;
    public static GhostState currentYellowGhost = GhostState.Normal;
    public static GhostState currentPinkGhost = GhostState.Normal;

    private float timer;
    private int lastTime;

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
        //    timer += Time.deltaTime;
        //    if(currentRedGhost == GhostState.Scared && timer <= 10)
        //    {
        //        lastTime = (int)timer;
        //        //Debug.Log(lastTime);
        //        if (timer >= 7)
        //        {
        //            SetTransition();
        //        }
        //    }
        //    else
        //    {
        //        SetNormal();
        //    }
    }

    public void SetNormal()
    {
        // Set All Ghost back to Normal State
        for(int i=0; i< ghost.Length; i++)
        {
            ghost[i].GetComponent<Animator>().SetBool("Transition", false);
        }
        backgroundMusic.ChangeBackgroundMusic(0);
    }

    public void SetScared()
    {
        // Set All Ghost to Scared State
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].GetComponent<Animator>().SetTrigger("Scared");
        }
        backgroundMusic.ChangeBackgroundMusic(2);

        currentRedGhost = GhostState.Scared;
        currentBlueGhost = GhostState.Scared;
        currentYellowGhost = GhostState.Scared;
        currentPinkGhost = GhostState.Scared;

        // Start timer now
        timer += 0;
    }

    public void SetTransition()
    {
        // Set All Ghost to Recovering State
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].GetComponent<Animator>().SetBool("Transition", true);
            backgroundMusic.ChangeBackgroundMusic(3);
        }
    }
}
