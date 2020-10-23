using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    private Animator animator;
    private Tweener tweener;

    private Vector3[] directions = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    private Vector3 currentInput, newInput, lastDirection, destination;
    private int xPosition, yPosition, directionIndex = 1;
    private float movementSqrtMagnitude;

    public bool isDeath = false;

    // Start is called before the first frame update
    void Start()
    {
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
            tweener.AddTween(transform, transform.position, new Vector3(0.5f, 0.0f, 0.0f), 5.0f);
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
