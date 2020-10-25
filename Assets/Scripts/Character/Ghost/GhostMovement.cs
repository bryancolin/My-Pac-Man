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
    private AudioManager backgroundMusic;

    private Coroutine startScared, startDeath;

    private bool isNormal = true, isScared = false, isRecovering = false, isDeath = false;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        backgroundMusic = GameObject.FindWithTag("Managers").GetComponent<AudioManager>();

        ghostTimer = GameObject.FindWithTag("GhostTimer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.currentGameState)
        {
            case GameState.GameScene:
                //MovementBGM();
                break;

            case GameState.GameOverScene:
                //Destroy(this.gameObject.GetComponent<GhostController>());
                break;
        }
    }

    // Set Ghost back to Normal State
    public void SetNormal()
    {
        isNormal = true;

        transform.GetComponent<Animator>().SetBool("Scared", false);
        transform.GetComponent<Animator>().SetBool("Transition", false);
        transform.GetComponent<Animator>().SetBool("Death", false);

        backgroundMusic.ChangeBackgroundMusic(1);
    }

    // Set Ghost to Scared State
    public void SetScared()
    {
        isNormal = false;
        isScared = true;
        // Start timer now
        //backgroundMusic.StopPlaying();

        if (startScared != null)
        {
            StopCoroutine(startScared);
        }

        startScared = StartCoroutine(StartScared());
    }

    IEnumerator StartScared()
    {
        if (isRecovering == true)
        {
            SetNormal();
        }

        transform.GetComponent<Animator>().SetBool("Scared", true);

        backgroundMusic.ChangeBackgroundMusic(2);

        ghostTimer.gameObject.SetActive(true);

        int timer = 10;
        ghostTimer.color = Color.green;

        while (timer > 0)
        {
            if(isDeath)
            {
                yield break;
            }

            if (timer == 3)
            {
                ghostTimer.color = Color.red;
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

        startScared = null;
    }

    // Set Ghost to Recovering State
    public void SetTransition()
    {
        isRecovering = true;

        transform.GetComponent<Animator>().SetBool("Transition", isRecovering);
    }

    // Set Ghost to Death State
    public void SetDeath()
    {
        isDeath = true;
        //backgroundMusic.StopPlaying();

        transform.GetComponent<Animator>().SetBool("Death", true);

        if (isRecovering)
        {
            isRecovering = false;
            transform.GetComponent<Animator>().SetBool("Transition", false);
        }

        if (startDeath == null)
        {
            startDeath = StartCoroutine(StartDeath());
        }
    }

    IEnumerator StartDeath()
    {
        int timer = 5;

        backgroundMusic.ChangeBackgroundMusic(3);

        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }

        isDeath = false;

        if (isScared)
        {
            isScared = false;
            transform.GetComponent<Animator>().SetBool("Scared", false);
        }

        SetNormal();

    }

    private void MovementBGM()
    {
        if (!backgroundMusic.Playing())
        {
            if (isNormal && !isScared && !isRecovering && !isDeath)
            {
                backgroundMusic.ChangeBackgroundMusic(1);
            }
            else
            {
                if ((isScared || isRecovering) && !isDeath)
                {
                    backgroundMusic.ChangeBackgroundMusic(2);
                }
                else if (isDeath)
                {
                    backgroundMusic.ChangeBackgroundMusic(3);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PacStudentController pacStudent = collision.GetComponent<PacStudentController>();

            if(isNormal)
            {
                StartCoroutine(pacStudent.DeadTrigger());
                gameManager.LoseLife();
            }
            else if(isScared || isRecovering)
            {
                if (!isDeath)
                {
                    UiManager.Instance.UpdateScore(300);
                    SetDeath();
                }
            }
        }
    }


}
