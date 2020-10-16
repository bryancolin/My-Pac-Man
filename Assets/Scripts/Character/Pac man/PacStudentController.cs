using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PacStudentController : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem particleSystem;

    public LevelGenerator levelGenerator;

    public AudioSource movementSource;
    public AudioClip[] movementClips;

    private Tweener tweener;
    private Vector3 movement;
    private float movementSqrtMagnitude;

    private Vector3 lastInput = Vector3.zero;
    private Vector3 currentInput = Vector3.zero;

    private Vector3 destination = Vector3.zero;

    private int xPosition, yPosition;

    public int playerScore;
    private GhostMovement ghostMovement;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        ghostMovement = GameObject.FindWithTag("Ghost").GetComponent<GhostMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();

        //if (!RayCastCheck())
        //    CharacterPosition();
        //else
        //    movementSqrtMagnitude = 0.0f;

        CharacterPosition();
        CharacterRotation();
        WalkingAnimation();
        ParticlePlay();
        MovementAudio();
    }

    void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude(movement, 1.0f);
        //movementSqrtMagnitude = movement.sqrMagnitude;

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

            if (levelGenerator.levelMap[yPosition, xPosition] == 0)
            {
                return true;
            }
            else if (levelGenerator.levelMap[yPosition, xPosition] == 5 || levelGenerator.levelMap[yPosition, xPosition] == 6)
            {
                levelGenerator.levelMap[yPosition, xPosition] = 0;
                return true;
            }
        }

        // Top Right
        if (transform.position.x + inputDirection.x >= 0 && transform.position.y + inputDirection.y >= 0)
        {
            xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);
            yPosition = (int)(Mathf.Abs(transform.position.y - 14) + -inputDirection.y);

            //Debug.Log("X : " + xPosition + " Y : " + yPosition);

            if (levelGenerator.levelMapTopRight[yPosition, xPosition] == 0)
            {
                return true;
            }
            else if (levelGenerator.levelMapTopRight[yPosition, xPosition] == 5 || levelGenerator.levelMapTopRight[yPosition, xPosition] == 6)
            {
                levelGenerator.levelMapTopRight[yPosition, xPosition] = 0;
                return true;
            }
        }

        // Bottom Left
        if (transform.position.x + inputDirection.x < 0 && transform.position.y + inputDirection.y < 0)
        {
            xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            //Debug.Log("X : " + xPosition + " Y : " + yPosition);
            //Debug.Log(levelGenerator.levelMapBottomLeft[yPosition, xPosition]);

            if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 0)
            {
                return true;
            }
            else if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 6)
            {
                levelGenerator.levelMapBottomLeft[yPosition, xPosition] = 0;
                return true;
            }
        }

        // Bottom Right
        if (transform.position.x + inputDirection.x > 0 && transform.position.y + inputDirection.y < 0)
        {
            xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            //Debug.Log("X : " + xPosition + " Y : " + yPosition);
            //Debug.Log(levelGenerator.levelMapBottomRight[yPosition, xPosition]);

            if (levelGenerator.levelMapBottomRight[yPosition, xPosition] == 0)
            {
                return true;
            }
            else if (levelGenerator.levelMapBottomRight[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomRight[yPosition, xPosition] == 6)
            {
                levelGenerator.levelMapBottomRight[yPosition, xPosition] = 0;
                return true;
            }
        }

        return false;
    }

    // Using RayCast to check if Grid is Walkable
    bool RayCastCheck(Vector3 inputDirection)
    {
        Vector3 pos = transform.position;
        inputDirection += new Vector3(inputDirection.x * 0.45f, inputDirection.y * 0.45f);

        RaycastHit2D hit = Physics2D.Linecast(pos + inputDirection, pos);
        Debug.DrawLine(pos + inputDirection, pos, Color.yellow);

        if (hit)
        {
            if (hit.collider.CompareTag("NormalPellet") || hit.collider.CompareTag("PowerPellet") || hit.collider.CompareTag("Portal") || (hit.collider == GetComponent<Collider2D>()))
            {
                Debug.Log("Normal Pellet");
                return true;
            }
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("Wall");
                return false;
            }
        }
        return false;
    }

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
        //animator.SetFloat("Horizontal", direction.x);
        //animator.SetFloat("Vertical", direction.y);
    }

    void ParticlePlay()
    {
        if (movementSqrtMagnitude != 1.0f)
        {
            particleSystem.Stop();
        }
        else
        {
            particleSystem.Play();
        }
    }

    void MovementAudio()
    {
        //Debug.Log(movementSqrtMagnitude);
        if (movementSqrtMagnitude == 1.0f)
        {
            if (!movementSource.isPlaying)
            {
                if(levelGenerator.levelMap[yPosition,xPosition] == 0)
                {
                    Debug.Log("Move");
                    movementSource.clip = movementClips[0];
                    movementSource.volume = 0.5f;
                    movementSource.Play();
                }
                else
                {
                    Debug.Log("Eat");
                    movementSource.clip = movementClips[1];
                    movementSource.volume = 0.3f;
                    movementSource.Play();
                }
            }
        }
        else if (movementSqrtMagnitude < 1.0f)
        {
            if (movementSource.isPlaying)
            {
                movementSource.Stop();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NormalPellet"))
        {
            //if (movementSource.isPlaying)
            //{
            //    Debug.Log("Playing eating");
            //    //movementSource.clip = movementClips[1];
            //    movementSource.volume = 0.1f;
            //    movementSource.Play();
            //}
            playerScore += 10;
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("Cherry"))
        {
            playerScore += 100;
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("PowerPellet"))
        {
            ghostMovement.SetScared();
            Destroy(collision.gameObject);
        }

        Debug.Log(playerScore);

        //if (collision.CompareTag("Portal"))
        //{
        //    if (this.transform.position.x < 0)
        //    {
        //        Debug.Log("Left");
        //        this.transform.position = new Vector3(13.0f, 0.0f, 0.0f);
        //    }
        //    else if (this.transform.position.x > 0)
        //    {
        //        Debug.Log("Right");
        //        this.transform.position = new Vector3(-13.0f, 0.0f, 0.0f);
        //    }
        //}
    }
}
