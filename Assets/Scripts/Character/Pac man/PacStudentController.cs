using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Vector3 movement;
    private Tweener tweener;

    private KeyCode lastInput;

    public float walkSpeed = 1.5f;
    public float timeDuration = 4.5f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        CharacterPosition();
        WalkingAnimation();
        Tweening();
    }

    void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude(movement, 1.0f);

        //if (Input.GetKey(Input.GetAxis("Horizontal"))
        //{
        //    lastInput = Event.current.keyCode;
        //    Debug.Log(lastInput);
        //}

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
        animator.SetFloat("Speed", movement.sqrMagnitude);
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
