using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostMovement : MonoBehaviour
{
    //[SerializeField]
    //public enum GhostState { Normal, Scared, Recovering, Death }

    private GameManager gameManager;
    private TextMeshProUGUI ghostTimer;

    private Coroutine startTimer;

    private int timer;

    private bool isScared = false, isRecovering = false;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        ghostTimer = GameObject.FindWithTag("GhostTimer").GetComponent<TextMeshProUGUI>();
        //ghostTimer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGhost()
    {
        transform.GetComponent<Animator>().SetFloat("Speed", 1);
    }

    // Set Ghost back to Normal State
    public void SetNormal()
    {
        transform.GetComponent<Animator>().SetBool("Scared", false);
        transform.GetComponent<Animator>().SetBool("Transition", false);
        //backgroundMusic.ChangeBackgroundMusic(1);
    }

    // Set Ghost to Scared State
    public void SetScared()
    {
        isScared = true;
        // Start timer now
        if (startTimer != null)
        {
            StopCoroutine(startTimer);
        }

        startTimer = StartCoroutine(StartTimer());
    }

    // Set Ghost to Recovering State
    public void SetTransition()
    {
        transform.GetComponent<Animator>().SetBool("Transition", isRecovering);
    }

    IEnumerator StartTimer()
    {
        if (isRecovering == true)
        {
            SetNormal();
        }

        transform.GetComponent<Animator>().SetBool("Scared",isScared);

        ghostTimer.gameObject.SetActive(true);

        timer = 10;

        ghostTimer.color = Color.green;

        while (timer > 0)
        {
            if(timer == 3)
            {
                ghostTimer.color = Color.red;

                isRecovering = true;
                SetTransition();
            }

            ghostTimer.SetText(timer.ToString());
            yield return new WaitForSeconds(1f);
            timer--;
        }

        ghostTimer.gameObject.SetActive(false);

        isScared = false;
        isRecovering = false;

        SetNormal();

        startTimer = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PacStudentController>().deathParticle.Play();
            collision.transform.position = new Vector3(-12.5f, 13.0f, 0.0f);
        }
    }


}
