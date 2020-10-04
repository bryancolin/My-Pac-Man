using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public Animator animator;
    private Tweener tweener;
    private KeyCode lastInput;

    private Vector3 movement;
    private float movementSqrtMagnitude;

    public float walkSpeed = 1.5f;
    public float timeDuration = 4.5f;

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
            CharacterPosition();
        //else
            //movementSqrtMagnitude = 0.0f;
 
        CharacterPosition();
        WalkingAnimation();
    }

    void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrtMagnitude = movement.sqrMagnitude;

        if (Input.GetAxis("Horizontal") > 0)
            lastInput = KeyCode.D;
        if (Input.GetAxis("Horizontal") < 0)
            lastInput = KeyCode.A;
        if (Input.GetAxis("Vertical") > 0)
            lastInput = KeyCode.W;
        if (Input.GetAxis("Vertical") < 0)
            lastInput = KeyCode.S; 
    }

    void CharacterPosition()
    {
        transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World); 
    }

    void WalkingAnimation()
    {
        if (movement != Vector3.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movementSqrtMagnitude);
    }

    void Tweening()
    {
        tweener.AddTween(transform, transform.position, transform.position + movement, timeDuration);
    }

    bool RayCastCheck()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hitInfo, 0.5f);
        if (hit)
        {
            Debug.Log("Raycast Hit: " + hitInfo.collider.name);
            if (hitInfo.collider.CompareTag("Freeze"))
                return true;
        }
        return false;
    }
}
