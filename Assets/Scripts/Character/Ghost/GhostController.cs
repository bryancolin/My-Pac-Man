using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Animator animator;
    private LevelGenerator levelGenerator;
    private Tweener tweener;

    private Vector3 movement, lastInput, currentInput, destination;
    private int xPosition, yPosition;
    private float movementSqrtMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        levelGenerator = GameObject.FindWithTag("Maze").GetComponent<LevelGenerator>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tweener.TweenExists(transform))
        {
            GetMovementInput();
            CharacterPosition();
            WalkingAnimation();
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

            if (levelGenerator.levelMap[yPosition, xPosition] == 0 || levelGenerator.levelMap[yPosition, xPosition] == 5 || levelGenerator.levelMap[yPosition, xPosition] == 6)
                return true;
        }

        // Top Right
        else if (transform.position.x + inputDirection.x >= 0 && transform.position.y + inputDirection.y >= 0)
        {
            xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);
            yPosition = (int)(Mathf.Abs(transform.position.y - 14) + -inputDirection.y);

            if (levelGenerator.levelMapTopRight[yPosition, xPosition] == 0 || levelGenerator.levelMapTopRight[yPosition, xPosition] == 5 || levelGenerator.levelMapTopRight[yPosition, xPosition] == 6)
                return true;
        }

        // Bottom Left
        else if (transform.position.x + inputDirection.x < 0 && transform.position.y + inputDirection.y < 0)
        {
            xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 0 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 6)
                return true;
        }

        // Bottom Right
        else if (transform.position.x + inputDirection.x > 0 && transform.position.y + inputDirection.y < 0)
        {
            xPosition = (int)((transform.position.x - 0.5f) + inputDirection.x);
            yPosition = (int)Mathf.Abs((transform.position.y + 1) + inputDirection.y);

            if (levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 0 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 5 || levelGenerator.levelMapBottomLeft[yPosition, xPosition] == 6)
                return true;
        }

        return false;
    }

    void Tweening(Vector3 inputDirection)
    {
        tweener.AddTween(transform, transform.position, transform.position + inputDirection, 0.3f);
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
}
