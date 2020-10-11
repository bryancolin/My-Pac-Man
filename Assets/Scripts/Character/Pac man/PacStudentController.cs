using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem particleSystem;

    public AudioSource movementSource;
    public AudioClip[] movementClips;
    private bool isEating = false;

    private Tweener tweener;
    private Vector3 movement;
    private float movementSqrtMagnitude;

    private float rotation;

    private Vector3 lastInput = Vector3.zero;
    private Vector3 currentInput = Vector3.zero;

    private Vector3 destination = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
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
        if (RayCastCheckPellet(lastInput))
        {
            destination = lastInput + transform.position;
            currentInput = lastInput;
            Tweening(lastInput);
        }
        else
        {
            if (RayCastCheckPellet(currentInput))
            {
                destination = currentInput + transform.position;
                Tweening(currentInput);
            }
        }
    }

    // Using RayCast to check if Grid is Walkable
    bool RayCastCheckPellet(Vector3 inputDirection)
    {
        Vector3 pos = transform.position;
        inputDirection += new Vector3(inputDirection.x * 0.3f, inputDirection.y * 0.3f);

        RaycastHit2D hit = Physics2D.Linecast(pos + inputDirection, pos);
        Debug.DrawLine(pos + inputDirection, pos, Color.yellow);

        if (hit)
        {
            if (hit.collider.CompareTag("NormalPellet") || hit.collider.CompareTag("PowerPellet") || (hit.collider == GetComponent<Collider2D>()))
            {
                return true;
            }
            if (hit.collider.CompareTag("Wall"))
            {
                return false;
            }
        }
        return false;
    }

    void Tweening(Vector3 inputDirection)
    {
        tweener.AddTween(transform, transform.position, transform.position + inputDirection, 0.25f);
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
        Vector3 direction = destination - transform.position;

        animator.SetFloat("Speed", direction.sqrMagnitude);
        //animator.SetFloat("Horizontal", direction.x);
        //animator.SetFloat("Vertical", direction.y);
    }

    void ParticlePlay()
    {
        Vector3 direction = destination - transform.position;        

        if (direction.sqrMagnitude < 0.3f)
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
        if (movementSqrtMagnitude >= 0.3f && !movementSource.isPlaying)
        {
            Debug.Log("Playing movement");
            movementSource.clip = movementClips[1];
            movementSource.volume = 0.5f;
            movementSource.Play();
        }
        else if (movementSqrtMagnitude <= 0.3f && movementSource.isPlaying)
        {
            movementSource.Stop();
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
        if (collision.CompareTag("NormalPellet") || collision.CompareTag("PowerPellet") || collision.CompareTag("Cherry"))
        {
            Debug.Log("Playing eating");
            movementSource.volume = 0.1f;
            movementSource.PlayOneShot(movementClips[0]);

            Destroy(collision.gameObject);
            Debug.Log("eat pellet");
        }
    }
}
