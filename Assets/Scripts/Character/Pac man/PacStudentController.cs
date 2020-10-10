using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public Animator animator;

    public AudioSource movementSource;
    public AudioClip[] movementClips;
    private bool isEating = false;

    private Tweener tweener;
    private Vector3 movement;
    private float movementSqrtMagnitude;

    private Vector3 lastInput = Vector3.zero;
    private Vector3 currentInput = Vector3.zero;

    private Vector3 dest = Vector3.zero;
    private Vector3 dir = Vector3.zero;

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
        WalkingAnimation();
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
            dest = lastInput + transform.position;
            currentInput = lastInput;
            Tweening(lastInput);
        }
        else
        {
            if (RayCastCheckPellet(currentInput))
            {
                dest = currentInput + transform.position;
                Tweening(currentInput);
            }
        }
    }

    // Using RayCast to check if Grid is Walkable
    bool RayCastCheckPellet(Vector3 inputDirection)
    {
        Vector3 pos = transform.position;
        inputDirection += new Vector3(inputDirection.x * 0.45f, inputDirection.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + inputDirection, pos);

        if (hit)
        {
            if (hit.collider.CompareTag("NormalPellet") || hit.collider.CompareTag("PowerPellet") || (hit.collider == GetComponent<Collider2D>()))
                return true;
        }
        return false;
    }

    void Tweening(Vector3 inputDirection)
    {
        Vector3 destination = transform.position + inputDirection;
        tweener.AddTween(transform, transform.position, destination, 0.3f);
    }

    void WalkingAnimation()
    {
        Vector3 direction = dest - transform.position;
        movementSqrtMagnitude = direction.sqrMagnitude;

        animator.SetFloat("Speed", direction.sqrMagnitude);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    void MovementAudio()
    {
        if (movementSqrtMagnitude >= 0.3f && !movementSource.isPlaying)
        {
            if (isEating)
            {
                Debug.Log("Playing");
                movementSource.clip = movementClips[0];
                movementSource.Play();
            }
            else
            {
                Debug.Log("Playing other");
                movementSource.clip = movementClips[1];
                movementSource.Play();
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NormalPellet") || collision.CompareTag("PowerPellet"))
        {
            Destroy(collision.gameObject);
            isEating = true;
            Debug.Log("eat pellet");
        }
        else
        {
            isEating = false;
            Debug.Log("moving");
        }
    }
}
