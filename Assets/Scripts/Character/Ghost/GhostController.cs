using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostController : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager backgroundMusic;
    private TextMeshProUGUI ghostTimer;

    private LevelGenerator levelGenerator;
    private Animator animator;
    private Tweener tweener;

    private Vector3[] directions = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    private Vector3 currentInput, newInput, lastDirection, destination, initialPosition;
    private int xPosition, yPosition;
    private float movementSqrtMagnitude;

    private Coroutine startScared, startDeath;

    private bool isNormal = true, isScared = false, isRecovering = false, isDeath = false;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        backgroundMusic = GameObject.FindWithTag("Managers").GetComponent<AudioManager>();
        ghostTimer = GameObject.FindWithTag("GhostTimer").GetComponent<TextMeshProUGUI>();

        levelGenerator = GameObject.FindWithTag("Maze").GetComponent<LevelGenerator>();
        animator = GetComponent<Animator>();
        tweener = GetComponent<Tweener>();

        // Start with Random Current Direction
        currentInput = directions[Random.Range(0, 4)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!tweener.TweenExists(transform))
        {
            CharacterPosition();
            WalkingAnimation();
        }
    }

    void CharacterPosition()
    {
        if (GridCheck(currentInput))
        {
            if (GridCheck(newInput) && newInput != lastDirection)
            {
                destination = newInput + transform.position;
                lastDirection = -newInput;
                Tweening(newInput);
            }
            else
            {
                destination = currentInput + transform.position;
                lastDirection = -currentInput;
                newInput = directions[Random.Range(0, 4)];
                Tweening(currentInput);
            }
        }
        else
        {
            //Debug.Log("Change");
            RandomDirection();
        }
    }

    void RandomDirection()
    {
        Vector3 newDirection = directions[Random.Range(0, 4)];

        if (lastDirection != newDirection)
        {
            currentInput = newDirection;
        }
        else
        {
            RandomDirection();
        }
    }

    // Using LevelMapGenerator to check if Grid is Walkable
    bool GridCheck(Vector3 inputDirection)
    {
        if (transform.position.y + inputDirection.y >= 0) // Top
        {
            yPosition = (int)(Mathf.Abs(transform.position.y - 14) + -inputDirection.y);

            if (transform.position.x + inputDirection.x <= 0) // Top Left
            {
                if (transform.position.x + inputDirection.x < -7.5 && transform.position.y + inputDirection.y == 0)
                    return false;

                xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);

                if (levelGenerator.levelMap[yPosition, xPosition] == 0 || levelGenerator.levelMap[yPosition, xPosition] == 5 || levelGenerator.levelMap[yPosition, xPosition] == 6)
                    return true;
            }
            else if (transform.position.x + inputDirection.x >= 0) // Top Right
            {
                if (transform.position.x + inputDirection.x > 7.5 && transform.position.y + inputDirection.y == 0)
                    return false;

                xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);

                if (levelGenerator.levelMapTopRight[yPosition, xPosition] == 0 || levelGenerator.levelMapTopRight[yPosition, xPosition] == 5 || levelGenerator.levelMapTopRight[yPosition, xPosition] == 6)
                    return true;
            }
        }
        else if (transform.position.y + inputDirection.y < 0) // Bottom
        {
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            if (transform.position.x + inputDirection.x < 0) // Bottom Left
            {
                xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);

                if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 0 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 6)
                    return true;
            }
            else if (transform.position.x + inputDirection.x > 0) // Bottom Right
            {
                xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);

                if (levelGenerator.levelMapBottomRight[yPosition, xPosition] == 0 || levelGenerator.levelMapBottomRight[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomRight[yPosition, xPosition] == 6)
                    return true;
            }
        }
        return false;
    }

    void Tweening(Vector3 inputDirection)
    {
        if(!isDeath)
            tweener.AddTween(transform, transform.position, transform.position + inputDirection, 0.3f);
        else
            tweener.AddTween(transform, transform.position, initialPosition, 5.0f);
    }

    void WalkingAnimation()
    {
        Vector3 direction = destination - transform.position;
        movementSqrtMagnitude = direction.sqrMagnitude;

        if (movementSqrtMagnitude != 1.0f)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", movementSqrtMagnitude);
        }

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
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
            if (isDeath)
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

            if (isNormal)
            {
                StartCoroutine(pacStudent.DeadTrigger());
                gameManager.LoseLife();
            }
            else if (isScared || isRecovering)
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
