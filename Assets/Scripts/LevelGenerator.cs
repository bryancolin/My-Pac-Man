﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;

    private GameObject[,] topLeft = new GameObject[14, 15];
    private GameObject[,] topRight = new GameObject[14, 15];
    private GameObject[,] bottomLeft = new GameObject[14, 15];
    private GameObject[,] bottomRight = new GameObject[14, 15]; 

    private int rows, columns;

    private void Awake()
    {
        rows = topLeft.GetLength(0);
        columns = topLeft.GetLength(1);

        Quadrant2();
        Quadrant1();
        Quadrant3();
        Quadrant4();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Quadrant1()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (topLeft[i, j] != null)
                {
                    topRight[i,j] = Instantiate(topLeft[i, j], new Vector3(-topLeft[i, j].transform.position.x, topLeft[i, j].transform.position.y, 0), Quaternion.Euler(topLeft[i, j].transform.rotation.x, 180.0f, topLeft[i, j].transform.rotation.eulerAngles.z));
                    topRight[i,j].transform.SetParent(this.transform);
                }
            }
        }
    }

    private void Quadrant3()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (topLeft[i, j] != null)
                {
                    bottomLeft[i,j] = Instantiate(topLeft[i, j], new Vector3(topLeft[i, j].transform.position.x, -topLeft[i, j].transform.position.y, 0), Quaternion.Euler(180.0f, topLeft[i, j].transform.rotation.y, topLeft[i, j].transform.rotation.eulerAngles.z));
                    bottomLeft[i,j].transform.SetParent(this.transform);
                }
            }
        }
    }

    private void Quadrant4()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (topLeft[i, j] != null)
                {
                    bottomRight[i,j] = Instantiate(topLeft[i, j], new Vector3(-topLeft[i, j].transform.position.x, -topLeft[i, j].transform.position.y, 0), Quaternion.Euler(180.0f, 180.0f, topLeft[i, j].transform.rotation.eulerAngles.z));
                    bottomRight[i,j].transform.SetParent(this.transform);
                }
            }
        }
    }

    private void Quadrant2()
    {
        Line1();
        Line2();
        Line3();
        Line4();
        Line5();
        Line6();
        Line7();
        Line8();
        Line9();
        Line10();
        Line11();
        Line12();
        Line13();
        Line14();
        //Line15();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (topLeft[i, j] != null)
                {
                    topLeft[i, j].transform.SetParent(this.transform);
                }
            }
        }
    }

    private void Line1()
    {
        topLeft[0, 0] = Instantiate(gameObjects[1], new Vector3(-13.5f, 14.5f, 0.0f), Quaternion.identity);

        for (int i = 1; i < 13; i++)
        {
            topLeft[0, i] = Instantiate(gameObjects[2], new Vector3(i - 14.0f, 14.5f, 0.0f), Quaternion.identity);
        }
        topLeft[0, 13] = Instantiate(gameObjects[7], new Vector3(-1.5f, 14.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void Line2()
    {
        topLeft[1, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 14.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 13; i++)
        {
            topLeft[1, i] = Instantiate(gameObjects[5], new Vector3(i - 14.0f, 14.0f, 0.0f), Quaternion.identity);
        }
        topLeft[1, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 14.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line3()
    {
        topLeft[2, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 13.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[2, 1] = Instantiate(gameObjects[5], new Vector3(-13.0f, 13.0f, 0.0f), Quaternion.identity);

        topLeft[2, 2] = Instantiate(gameObjects[3], new Vector3(-11.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 3] = Instantiate(gameObjects[4], new Vector3(-11f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 4] = Instantiate(gameObjects[4], new Vector3(-10f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 5] = Instantiate(gameObjects[3], new Vector3(-9.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[2, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 13.0f, 0.0f), Quaternion.identity);

        topLeft[2, 7] = Instantiate(gameObjects[3], new Vector3(-6.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 8] = Instantiate(gameObjects[4], new Vector3(-6f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 9] = Instantiate(gameObjects[4], new Vector3(-5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 10] = Instantiate(gameObjects[4], new Vector3(-4f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[2, 12] = Instantiate(gameObjects[5], new Vector3(-2.0f, 13.0f, 0.0f), Quaternion.identity);
        topLeft[2, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 13.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line4()
    {
        topLeft[3, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[3, 1] = Instantiate(gameObjects[6], new Vector3(-13.0f, 12.0f, 0.0f), Quaternion.identity);

        topLeft[3, 2] = Instantiate(gameObjects[4], new Vector3(-11.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[3, 5] = Instantiate(gameObjects[4], new Vector3(-9.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[3, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 12.0f, 0.0f), Quaternion.identity);

        topLeft[3, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[3, 11] = Instantiate(gameObjects[4], new Vector3(-3.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[3, 12] = Instantiate(gameObjects[5], new Vector3(-2.0f, 12.0f, 0.0f), Quaternion.identity);
        topLeft[3, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line5()
    {
        topLeft[4, 1] = Instantiate(gameObjects[2], new Vector3(-13.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[4, 2] = Instantiate(gameObjects[5], new Vector3(-13.0f, 11.0f, 0.0f), Quaternion.identity);

        topLeft[4, 3] = Instantiate(gameObjects[3], new Vector3(-11.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[4, 4] = Instantiate(gameObjects[4], new Vector3(-11f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 5] = Instantiate(gameObjects[4], new Vector3(-10f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 6] = Instantiate(gameObjects[3], new Vector3(-9.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[4, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 11.0f, 0.0f), Quaternion.identity);

        topLeft[4, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[4, 9] = Instantiate(gameObjects[4], new Vector3(-6f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 10] = Instantiate(gameObjects[4], new Vector3(-5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 11] = Instantiate(gameObjects[4], new Vector3(-4f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 12] = Instantiate(gameObjects[3], new Vector3(-3.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[4, 13] = Instantiate(gameObjects[5], new Vector3(-2.0f, 11.0f, 0.0f), Quaternion.identity);

        topLeft[4, 14] = Instantiate(gameObjects[3], new Vector3(-0.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line6()
    {
        topLeft[5, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 14; i++)
        {
            topLeft[5, i] = Instantiate(gameObjects[5], new Vector3(i - 14.0f, 10.0f, 0.0f), Quaternion.identity);
        }
    }

    private void Line7()
    {
        topLeft[6, 1] = Instantiate(gameObjects[2], new Vector3(-13.5f, 9.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 2] = Instantiate(gameObjects[5], new Vector3(-13.0f, 9.0f, 0.0f), Quaternion.identity);

        topLeft[6, 3] = Instantiate(gameObjects[3], new Vector3(-11.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 4] = Instantiate(gameObjects[4], new Vector3(-11f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 5] = Instantiate(gameObjects[4], new Vector3(-10f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 6] = Instantiate(gameObjects[3], new Vector3(-9.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[6, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 9.0f, 0.0f), Quaternion.identity);

        topLeft[6, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 9] = Instantiate(gameObjects[3], new Vector3(-6.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[6, 10] = Instantiate(gameObjects[5], new Vector3(-5.0f, 9.0f, 0.0f), Quaternion.identity);

        topLeft[6, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 12] = Instantiate(gameObjects[4], new Vector3(-3.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 13] = Instantiate(gameObjects[4], new Vector3(-2.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 14] = Instantiate(gameObjects[4], new Vector3(-1.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void Line8()
    {
        topLeft[7, 1] = Instantiate(gameObjects[2], new Vector3(-13.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[7, 2] = Instantiate(gameObjects[5], new Vector3(-13.0f, 8.0f, 0.0f), Quaternion.identity);

        topLeft[7, 3] = Instantiate(gameObjects[3], new Vector3(-11.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[7, 4] = Instantiate(gameObjects[4], new Vector3(-11f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[7, 5] = Instantiate(gameObjects[4], new Vector3(-10f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[7, 6] = Instantiate(gameObjects[3], new Vector3(-9.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[7, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 8.0f, 0.0f), Quaternion.identity);
        topLeft[7, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[7, 9] = Instantiate(gameObjects[4], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[7, 10] = Instantiate(gameObjects[5], new Vector3(-5.0f, 8.0f, 0.0f), Quaternion.identity);

        topLeft[7, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[7, 12] = Instantiate(gameObjects[4], new Vector3(-3.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[7, 13] = Instantiate(gameObjects[4], new Vector3(-2.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[7, 14] = Instantiate(gameObjects[3], new Vector3(-1.5f, 7.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line9()
    {
        topLeft[8, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 7; i++)
        {
            topLeft[8, i] = Instantiate(gameObjects[5], new Vector3(-i - 7.0f, 7.0f, 0.0f), Quaternion.identity);
        }

        topLeft[8, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[8, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        for (int i = 9; i < 13; i++)
        {
            topLeft[8, i] = Instantiate(gameObjects[5], new Vector3(-i + 7.0f, 7.0f, 0.0f), Quaternion.identity);
        }

        topLeft[8, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line10()
    {
        topLeft[9, 0] = Instantiate(gameObjects[1], new Vector3(-13.5f, 6.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 5; i++)
        {
            topLeft[9, i] = Instantiate(gameObjects[2], new Vector3(-i - 9.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        topLeft[9, 5] = Instantiate(gameObjects[1], new Vector3(-9.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[9, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 6.0f, 0.0f), Quaternion.identity);
        topLeft[9, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[9, 8] = Instantiate(gameObjects[3], new Vector3(-5.5f, 6.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));

        topLeft[9, 9] = Instantiate(gameObjects[4], new Vector3(-5.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[9, 10] = Instantiate(gameObjects[4], new Vector3(-4.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[9, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[9, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line11()
    {
        topLeft[10, 5] = Instantiate(gameObjects[2], new Vector3(-9.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[10, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 5.0f, 0.0f), Quaternion.identity);
        topLeft[10, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[10, 8] = Instantiate(gameObjects[3], new Vector3(-5.5f, 4.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

        topLeft[10, 9] = Instantiate(gameObjects[4], new Vector3(-5.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[10, 10] = Instantiate(gameObjects[4], new Vector3(-4.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[10, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[10, 12] = Instantiate(gameObjects[3], new Vector3(-0.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line12()
    {
        topLeft[11, 5] = Instantiate(gameObjects[2], new Vector3(-9.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[11, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 4.0f, 0.0f), Quaternion.identity);
        topLeft[11, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[11, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line13()
    {
        topLeft[12, 5] = Instantiate(gameObjects[2], new Vector3(-9.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[12, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 3.0f, 0.0f), Quaternion.identity);
        topLeft[12, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[12, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[12, 10] = Instantiate(gameObjects[3], new Vector3(-3.5f, 2.5f, 0.0f), Quaternion.identity);
        topLeft[12, 11] = Instantiate(gameObjects[4], new Vector3(-3f, 2.5f, 0.0f), Quaternion.identity);
        topLeft[12, 12] = Instantiate(gameObjects[4], new Vector3(-2f, 2.5f, 0.0f), Quaternion.identity);
    }

    private void Line14()
    {
        for (int i = 0; i < 5; i++)
        {
            topLeft[13, i] = Instantiate(gameObjects[2], new Vector3(-i - 10, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        }
        topLeft[13, 5] = Instantiate(gameObjects[1], new Vector3(-9.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[13, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 2.0f, 0.0f), Quaternion.identity);
        topLeft[13, 7] = Instantiate(gameObjects[3], new Vector3(-6.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[13, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[13, 10] = Instantiate(gameObjects[4], new Vector3(-3.5f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line15()
    {
        topLeft[14, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 1.0f, 0.0f), Quaternion.identity);
        topLeft[14, 10] = Instantiate(gameObjects[4], new Vector3(-3.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }
}
