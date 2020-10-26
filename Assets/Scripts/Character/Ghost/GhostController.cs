using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostController : MonoBehaviour
{
    public GhostState currentGhostState = GhostState.Normal;

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
        if (!isDeath)
            tweener.AddTween(transform, transform.position, transform.position + inputDirection, 0.3f);
        else
            tweener.AddTween(transform, transform.position, initialPosition, 5.0f);
    }

    void WalkingAnimation()
    {
        Vector3 direction = destination - transform.position;
        movementSqrtMagnitude = direction.sqrMagnitude;

        animator.SetFloat("Speed", movementSqrtMagnitude);

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        animator.SetBool("Scared", isScared);
        animator.SetBool("Transition", isRecovering);
        animator.SetBool("Death", isDeath);
    }

    public void SetNormal()
    {
        isNormal = true;
        currentGhostState = GhostState.Normal;
        backgroundMusic.StopPlaying();
    }

    public void SetScared()
    {
        isNormal = false;
        isScared = true;
        currentGhostState = GhostState.Scared;

        if (startScared != null)
        {
            StopCoroutine(startScared);
        }

        startScared = StartCoroutine(StartScared());
    }

    IEnumerator StartScared()
    {
        if (isRecovering)
        {
            isRecovering = false;
        }

        backgroundMusic.StopPlaying();

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
        currentGhostState = GhostState.Recovering;
    }


    public void SetDeath()
    {
        isDeath = true;
        currentGhostState = GhostState.Death;

        if(isNormal)
        {
            isNormal = false;
            isScared = true;
        }

        if (isRecovering)
        {
            isRecovering = false;
        }

        if (startDeath == null)
        {
            startDeath = StartCoroutine(StartDeath());
        }
    }

    IEnumerator StartDeath()
    {
        backgroundMusic.StopPlaying();

        while (transform.position != initialPosition)
        {
            yield return new WaitForSeconds(1f);
        }

        if (isScared)
        {
            isScared = false;
        }

        isDeath = false;

        SetNormal();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isNormal)
            {
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

        if (collision.CompareTag("Fire"))
        {
            if (isNormal || isScared || isRecovering)
            {
                if (!isDeath)
                {
                    Destroy(collision.gameObject);
                    UiManager.Instance.UpdateScore(100);
                    SetDeath();
                }
            }
        }
    }
}
