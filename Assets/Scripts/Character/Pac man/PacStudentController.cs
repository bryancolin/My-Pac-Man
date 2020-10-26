using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PacStudentController : MonoBehaviour
{
    private GameManager gameManager;
    private ScoreManager scoreManager;
    private ParticleSystem movingParticle, collideParticle, deathParticle;
    private LevelGenerator levelGenerator;
    private Animator animator;

    public AudioSource movementSource;
    public AudioClip[] movementClips;
    private bool isEating = true, isDeath = false;

    private Tweener tweener;
    private float movementSqrtMagnitude;

    private Vector3 movement, lastInput, currentInput, destination, initialPosition;

    private int xPosition, yPosition;

    public int totalPellets;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        scoreManager = GameObject.FindWithTag("Managers").GetComponent<ScoreManager>();

        // Maze
        levelGenerator = GameObject.FindWithTag("Maze").GetComponent<LevelGenerator>();

        // Particle System
        movingParticle = GameObject.FindWithTag("MovingParticle").GetComponent<ParticleSystem>();
        collideParticle = GameObject.FindWithTag("CollisionParticle").GetComponent<ParticleSystem>();
        deathParticle = GameObject.FindWithTag("DeathParticle").GetComponent<ParticleSystem>();

        // PacStudent Animator and Tweener
        animator = GetComponent<Animator>();
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.currentGameState)
        {
            case GameState.GameScene:
                if (!tweener.TweenExists(transform) && !isDeath)
                {
                    GetMovementInput();
                    CharacterPosition();
                    CharacterRotation();
                    WalkingAnimation();
                    ParticlePlay();
                    MovementAudio();
                }
                break;

            case GameState.GameOverScene:
                
                break;
        }
    }

    void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude(movement, 1.0f);

        if (movement.x > 0)
            lastInput = Vector3.right;
        else if (movement.x < 0)
            lastInput = Vector3.left;
        else if (movement.y > 0)
            lastInput = Vector3.up;
        else if (movement.y < 0)
            lastInput = Vector3.down;
    }

    void CharacterPosition()
    {
        if (GridCheck(lastInput))
        {
            destination = lastInput + transform.position;
            currentInput = lastInput;
            Tweening(lastInput);
        }
        else
        {
            if (GridCheck(currentInput))
            {
                destination = currentInput + transform.position;
                Tweening(currentInput);
            }
        }
    }

    // Using LevelMapGenerator to check if Grid is Walkable
    bool GridCheck(Vector3 inputDirection)
    {
        // Top Left
        if (transform.position.x + inputDirection.x <= 0 && transform.position.y + inputDirection.y >= 0)
        {
            xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);
            yPosition = (int)(Mathf.Abs(transform.position.y - 14) + -inputDirection.y);

            //Debug.Log("X : " + xPosition + " Y : " + yPosition);

            // No outside of Maze
            if (xPosition >= 0)
            {
                // Left Portal
                if (yPosition == 14 & xPosition == 0)
                {
                    transform.position = new Vector3(14.5f, 0.0f, 0.0f);
                    return true;
                }

                // Empty Grid
                if (levelGenerator.levelMap[yPosition, xPosition] == 0)
                {
                    isEating = false;
                    return true;
                }

                // Pellet Grid
                if (levelGenerator.levelMap[yPosition, xPosition] == 5 || levelGenerator.levelMap[yPosition, xPosition] == 6)
                {
                    isEating = true;
                    levelGenerator.levelMap[yPosition, xPosition] = 0;
                    return true;
                }
            }
        }

        // Top Right
        else if (transform.position.x + inputDirection.x >= 0 && transform.position.y + inputDirection.y >= 0)
        {
            xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);
            yPosition = (int)(Mathf.Abs(transform.position.y - 14) + -inputDirection.y);

            // No outside of Maze
            if (xPosition <= 13)
            {
                // Right Portal
                if (yPosition == 14 & xPosition == 13)
                {
                    transform.position = new Vector3(-14.5f, 0.0f, 0.0f);
                    return true;
                }

                // Empty Grid
                if (levelGenerator.levelMapTopRight[yPosition, xPosition] == 0)
                {
                    isEating = false;
                    return true;
                }

                // Pellet Grid
                if (levelGenerator.levelMapTopRight[yPosition, xPosition] == 5 || levelGenerator.levelMapTopRight[yPosition, xPosition] == 6)
                {
                    isEating = true;
                    levelGenerator.levelMapTopRight[yPosition, xPosition] = 0;
                    return true;
                }
            }
        }

        // Bottom Left
        else if (transform.position.x + inputDirection.x < 0 && transform.position.y + inputDirection.y < 0)
        {
            xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            // Empty Grid
            if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 0)
            {
                isEating = false;
                return true;
            }

            // Pellet Grid
            else if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 6)
            {
                isEating = true;
                levelGenerator.levelMapBottomLeft[yPosition, xPosition] = 0;
                return true;
            }
        }

        // Bottom Right
        else if (transform.position.x + inputDirection.x > 0 && transform.position.y + inputDirection.y < 0)
        {
            xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            // Empty Grid
            if (levelGenerator.levelMapBottomRight[yPosition, xPosition] == 0)
            {
                isEating = false;
                return true;
            }

            // Pellet Grid
            else if (levelGenerator.levelMapBottomRight[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomRight[yPosition, xPosition] == 6)
            {
                isEating = true;
                levelGenerator.levelMapBottomRight[yPosition, xPosition] = 0;
                return true;
            }
        }
        return false;
    }

    /*
    // Using RayCast to check if Grid is Walkable
    bool RayCastCheck(Vector3 inputDirection)
    {
        Vector3 pos = transform.position;
        inputDirection += new Vector3(inputDirection.x * 0.45f, inputDirection.y * 0.45f);

        RaycastHit2D hit = Physics2D.Linecast(pos + inputDirection, pos);
        Debug.DrawLine(pos + inputDirection, pos, Color.yellow);

        if (hit)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("Wall");
                return false;
            }
        }
        return false;
    }
    */

    void Tweening(Vector3 inputDirection)
    {
        tweener.AddTween(transform, transform.position, transform.position + inputDirection, 0.3f);
    }

    void CharacterRotation()
    {
        Vector3 direction = destination - transform.position;
        movementSqrtMagnitude = direction.sqrMagnitude;

        if (movementSqrtMagnitude != 0)
        {
            if (direction.x > 0)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            else if (direction.x < 0)
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            else if (direction.y > 0)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            else if (direction.y < 0)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
        }
    }

    void WalkingAnimation()
    {
        if (movementSqrtMagnitude != 1.0f)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", movementSqrtMagnitude);
        }
    }

    void ParticlePlay()
    {
        if (movementSqrtMagnitude != 1.0f)
        {
            movingParticle.Stop();
        }
        else
        {
            movingParticle.Play();
        }
    }

    void MovementAudio()
    {
        if (movementSqrtMagnitude == 1.0f)
        {
            movementSource.Stop();
            if (!movementSource.isPlaying)
            {
                if (isEating)
                {
                    // Play Eat
                    movementSource.clip = movementClips[1];
                    movementSource.volume = 0.2f;
                }
                else
                {
                    // Play Move
                    movementSource.clip = movementClips[0];
                    movementSource.volume = 0.5f;
                }
                movementSource.PlayDelayed(0.1f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 direction = destination - transform.position;

        if (collision.CompareTag("Wall") && direction.sqrMagnitude == 0.0f)
        {
            // Play Collision
            movementSource.PlayOneShot(movementClips[2]);
            collideParticle.Play();
            //Debug.Log("Trigger Enter: " + collision.name + " : " + collision.offset);
        }

        if (collision.CompareTag("Portal"))
        {
            collideParticle.Play();
        }

        if (collision.CompareTag("NormalPellet"))
        {
            UiManager.Instance.UpdateScore(10);
            totalPellets += 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Cherry"))
        {
            UiManager.Instance.UpdateScore(100);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("PowerPellet"))
        {
            gameManager.ScareGhost();
            totalPellets += 1;
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator DeadTrigger()
    {
        // Play Death Particle
        isDeath = true;
        deathParticle.Play();

        // Last Input & Current Input becomes null
        lastInput = Vector3.zero;
        currentInput = lastInput;

        yield return new WaitForSeconds(0.5f);

        transform.position = initialPosition;
        isDeath = false;
    }
}
