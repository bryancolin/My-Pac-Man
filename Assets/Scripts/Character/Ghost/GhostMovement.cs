using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    //[SerializeField]
    //public enum GhostState { Normal, Scared, Recovering, Death }

    private GameManager gameManager;

    private float timer;
    private int lastTime;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
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

    public void SetGhost()
    {
        transform.GetComponent<Animator>().SetFloat("Speed", 1);
    }

    // Set All Ghost back to Normal State
    public void SetNormal()
    {
        transform.GetComponent<Animator>().SetBool("Transition", false);
        //backgroundMusic.ChangeBackgroundMusic(1);
    }

    // Set All Ghost to Scared State
    public void SetScared()
    {
        transform.GetComponent<Animator>().SetTrigger("Scared");
        //backgroundMusic.ChangeBackgroundMusic(2);

        // Start timer now
        //timer += 0;
    }

    // Set All Ghost to Recovering State
    public void SetTransition()
    {
        transform.GetComponent<Animator>().SetBool("Transition", true);
    }
}
