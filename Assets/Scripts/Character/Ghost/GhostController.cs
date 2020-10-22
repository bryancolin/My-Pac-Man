using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Animator animator;
    private LevelGenerator levelGenerator;
    private Tweener tweener;

    private Vector3[] directions = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    private Vector3 movement, lastDirection, currentDirection, destination;
    private int xPosition, yPosition, directionIndex = 1;
    private float movementSqrtMagnitude;

    //[SerializeField]
    //private Vector2 currentDirection;

    [SerializeField]
    private float rayDistance;

    [SerializeField]
    private LayerMask rayLayer;

    // movement speed
    [SerializeField] float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        levelGenerator = GameObject.FindWithTag("Maze").GetComponent<LevelGenerator>();
        animator = GetComponent<Animator>();

        currentDirection = directions[1];
        Debug.Log(currentDirection);
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

    void RandomDirection()
    {
        // randomly select between -1 and 1;
        directionIndex += Random.Range(0, 2) * 2 - 1;

        // keeps index from exceeding 3
        int clampedIndex = directionIndex % directions.Length;

        // keep index positive
        if (clampedIndex < 0)
        {
            clampedIndex = directions.Length + clampedIndex;
        }

        Vector3 newDirection = directions[clampedIndex];

        if (lastDirection != newDirection)
        {
            currentDirection = newDirection;
        }
        else
        {
            RandomDirection();
        }
    }

    //void RandomInput()
    //{
    //    //movement.x = Input.GetAxis("Horizontal");
    //    //movement.y = Input.GetAxis("Vertical");

    //    //movement = Vector3.ClampMagnitude(movement, 1.0f);

    //    //if (movement.x > 0)
    //    //    lastDirection = Vector3.right;
    //    //else if (movement.x < 0)
    //    //    lastDirection = Vector3.left;
    //    //else if (movement.y > 0)
    //    //    lastDirection = Vector3.up;
    //    //else if (movement.y < 0)
    //    //    lastDirection = Vector3.down;
    //}

    void CharacterPosition()
    {
        if (GridCheck(currentDirection))
        {
            destination = currentDirection + transform.position;
            lastDirection = -currentDirection;
            Tweening(currentDirection);
        }
        else
        {
            RandomDirection();
        }
    }

    // Using LevelMapGenerator to check if Grid is Walkable
    bool GridCheck(Vector3 inputDirection)
    {
        // Top Left
        if (transform.position.x + inputDirection.x <= 0 && transform.position.y + inputDirection.y >= 0)
        {
            if (transform.position.x + inputDirection.x < -7.5 && transform.position.y + inputDirection.y == 0)
                return false;

            xPosition = (int)((transform.position.x + 13.5f) + inputDirection.x);
            yPosition = (int)(Mathf.Abs(transform.position.y - 14) + -inputDirection.y);

            if (levelGenerator.levelMap[yPosition, xPosition] == 0 || levelGenerator.levelMap[yPosition, xPosition] == 5 || levelGenerator.levelMap[yPosition, xPosition] == 6)
                return true;
        }

        // Top Right
        else if (transform.position.x + inputDirection.x >= 0 && transform.position.y + inputDirection.y >= 0)
        {
            if (transform.position.x + inputDirection.x > 7.5 && transform.position.y + inputDirection.y == 0)
                return false;

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
