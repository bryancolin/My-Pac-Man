using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;
    private GameObject[,] levelMap = new GameObject[14, 15];

    private void Awake()
    {
        LeftQuadrant();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LeftQuadrant()
    {
        Line8();
        Line9();
        Line10();
        Line11();
        Line12();
        Line13();
        Line14();
        Line15();
    }

    private void Line8()
    {
        levelMap[0, 7] = Instantiate(gameObjects[2], new Vector3(-13.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[1, 7] = Instantiate(gameObjects[5], new Vector3(-13.0f, 8.0f, 0.0f), Quaternion.identity);

        levelMap[2, 7] = Instantiate(gameObjects[3], new Vector3(-11.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[3, 7] = Instantiate(gameObjects[4], new Vector3(-11f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[4, 7] = Instantiate(gameObjects[4], new Vector3(-10f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[5, 7] = Instantiate(gameObjects[3], new Vector3(-9.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        levelMap[6, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 8.0f, 0.0f), Quaternion.identity);
        levelMap[7, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[9, 7] = Instantiate(gameObjects[5], new Vector3(-5.0f, 8.0f, 0.0f), Quaternion.identity);

        levelMap[10, 7] = Instantiate(gameObjects[3], new Vector3(-3.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[11, 7] = Instantiate(gameObjects[4], new Vector3(-3.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[12, 7] = Instantiate(gameObjects[4], new Vector3(-2.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[13, 7] = Instantiate(gameObjects[3], new Vector3(-1.5f, 7.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line9()
    {
        levelMap[0, 8] = Instantiate(gameObjects[2], new Vector3(-13.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for(int i = 1; i<7; i++)
        {
            levelMap[i, 8] = Instantiate(gameObjects[5], new Vector3(-i - 7.0f, 7.0f, 0.0f), Quaternion.identity);
        }

        levelMap[7, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        for (int i = 9; i < 13; i++)
        {
            levelMap[i, 8] = Instantiate(gameObjects[5], new Vector3(-i + 7.0f, 7.0f, 0.0f), Quaternion.identity);
        }

        levelMap[13, 8] = Instantiate(gameObjects[4], new Vector3(-0.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line10()
    {
        levelMap[0, 9] = Instantiate(gameObjects[1], new Vector3(-13.5f, 6.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 5; i++)
        {
            levelMap[i, 9] = Instantiate(gameObjects[2], new Vector3(-i - 9.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        levelMap[5, 9] = Instantiate(gameObjects[1], new Vector3(-9.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[6, 9] = Instantiate(gameObjects[5], new Vector3(-8.0f, 6.0f, 0.0f), Quaternion.identity);
        levelMap[7, 9] = Instantiate(gameObjects[4], new Vector3(-6.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 9] = Instantiate(gameObjects[3], new Vector3(-5.5f, 6.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));

        levelMap[9, 9] = Instantiate(gameObjects[4], new Vector3(-5.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[10, 9] = Instantiate(gameObjects[4], new Vector3(-4.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[11, 9] = Instantiate(gameObjects[3], new Vector3(-3.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[13, 9] = Instantiate(gameObjects[4], new Vector3(-0.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line11()
    {
        levelMap[5, 10] = Instantiate(gameObjects[2], new Vector3(-9.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[6, 10] = Instantiate(gameObjects[5], new Vector3(-8.0f, 5.0f, 0.0f), Quaternion.identity);
        levelMap[7, 10] = Instantiate(gameObjects[4], new Vector3(-6.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 10] = Instantiate(gameObjects[3], new Vector3(-5.5f, 4.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

        levelMap[9, 10] = Instantiate(gameObjects[4], new Vector3(-5.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[10, 10] = Instantiate(gameObjects[4], new Vector3(-4.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[11, 10] = Instantiate(gameObjects[3], new Vector3(-3.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        levelMap[13, 10] = Instantiate(gameObjects[3], new Vector3(-0.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line12()
    {
        levelMap[5, 11] = Instantiate(gameObjects[2], new Vector3(-9.5f, 4.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[6, 11] = Instantiate(gameObjects[5], new Vector3(-8.0f, 4.0f, 0.0f), Quaternion.identity);
        levelMap[7, 11] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 11] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line13()
    {
        levelMap[5, 12] = Instantiate(gameObjects[2], new Vector3(-9.5f, 3.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[6, 12] = Instantiate(gameObjects[5], new Vector3(-8.0f, 3.0f, 0.0f), Quaternion.identity);
        levelMap[7, 12] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 12] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[10, 12] = Instantiate(gameObjects[3], new Vector3(-3.5f, 2.5f, 0.0f), Quaternion.identity);
        levelMap[11, 12] = Instantiate(gameObjects[4], new Vector3(-3f, 2.5f, 0.0f), Quaternion.identity);
        levelMap[12, 12] = Instantiate(gameObjects[4], new Vector3(-2f, 2.5f, 0.0f), Quaternion.identity);
    }

    private void Line14()
    {
        for(int i=0; i<5; i++)
        {
            levelMap[i, 13] = Instantiate(gameObjects[2], new Vector3(-i-10, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        }
        levelMap[5, 13] = Instantiate(gameObjects[1], new Vector3(-9.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[6, 13] = Instantiate(gameObjects[5], new Vector3(-8.0f, 2.0f, 0.0f), Quaternion.identity);
        levelMap[7, 13] = Instantiate(gameObjects[3], new Vector3(-6.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 13] = Instantiate(gameObjects[3], new Vector3(-6.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[10, 13] = Instantiate(gameObjects[4], new Vector3(-3.5f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line15()
    {
        levelMap[6,14] = Instantiate(gameObjects[5], new Vector3(-8.0f, 1.0f, 0.0f), Quaternion.identity);
        levelMap[10,14] = Instantiate(gameObjects[4], new Vector3(-3.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }
}
