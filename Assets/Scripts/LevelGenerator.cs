using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;

    public int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    public int[,] levelMapTopRight = new int[15, 14];
    public int[,] levelMapBottomLeft = new int[15 - 1, 14];
    public int[,] levelMapBottomRight = new int[15 - 1, 14];

    private GameObject[,] topLeft = new GameObject[15, 15];
    private GameObject[,] topRight = new GameObject[15, 15];
    private GameObject[,] bottomLeft = new GameObject[15, 15];
    private GameObject[,] bottomRight = new GameObject[15, 15];

    private readonly GameObject[] q = new GameObject[4];

    private int rows, columns;

    private void Awake()
    {
        rows = topLeft.GetLength(0);
        columns = topLeft.GetLength(1);

        MirrorLevelMap();

        EmptyGameObject();
        Quadrants();
    }

    private void MirrorLevelMap()
    {
        MirrorHorizontal();
        MirrorVertical();
        MirrorHorizontalVertical();
    }
     
    private void MirrorHorizontal()
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                levelMapTopRight[i, j] = levelMap[i, 13 - j];
            }
        }
    }

    private void MirrorVertical()
    {
        for (int i = 0; i < levelMap.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                levelMapBottomLeft[i, j] = levelMap[13 - i, j];
            }
        }
    }

    private void MirrorHorizontalVertical()
    {
        for (int i = 0; i < levelMap.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                levelMapBottomRight[i, j] = levelMap[13 - i, 13 - j];
            }
        }
    }

    private void EmptyGameObject()
    {
        for (int i = 0; i < 4; i++)
        {
            q[i] = new GameObject();
            q[i].transform.SetParent(this.transform);
            q[i].name = $"Quandrant {i + 1}";
        }

        q[0].transform.position = new Vector3(6.75f, 7.5f, 0.0f);
        q[1].transform.position = new Vector3(-6.75f, 7.5f, 0.0f);
        q[2].transform.position = new Vector3(-6.75f, -7.5f, 0.0f);
        q[3].transform.position = new Vector3(6.75f, -7.5f, 0.0f);
    }

    private void Quadrants()
    {
        Quadrant2();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (topLeft[i, j] != null)
                {
                    if (j > 0)
                    {
                        Quadrant1(i, j);
                        Quadrant3(i, j);
                        Quadrant4(i, j);
                    }
                    else
                    {
                        Quadrant1(i, j);
                    }
                }
            }
        }
    }

    private void Quadrant1(int i, int j)
    {
        topRight[i, j] = Instantiate(topLeft[i, j], new Vector3(-topLeft[i, j].transform.position.x, topLeft[i, j].transform.position.y, 0), Quaternion.Euler(topLeft[i, j].transform.rotation.x, 180.0f, topLeft[i, j].transform.rotation.eulerAngles.z));
        topRight[i, j].transform.SetParent(q[0].transform);
    }

    private void Quadrant3(int i, int j)
    {
        bottomLeft[i, j] = Instantiate(topLeft[i, j], new Vector3(topLeft[i, j].transform.position.x, -topLeft[i, j].transform.position.y, 0), Quaternion.Euler(180.0f, topLeft[i, j].transform.rotation.y, topLeft[i, j].transform.rotation.eulerAngles.z));
        bottomLeft[i, j].transform.SetParent(q[2].transform);
    }

    private void Quadrant4(int i, int j)
    {
        bottomRight[i, j] = Instantiate(topLeft[i, j], new Vector3(-topLeft[i, j].transform.position.x, -topLeft[i, j].transform.position.y, 0), Quaternion.Euler(180.0f, 180.0f, topLeft[i, j].transform.rotation.eulerAngles.z));
        bottomRight[i, j].transform.SetParent(q[3].transform);
    }

    private void Quadrant2()
    {
        Line0();
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

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (topLeft[i, j] != null)
                {
                    topLeft[i, j].transform.SetParent(q[1].transform);
                }
            }
        }
    }

    private void Line14()
    {
        topLeft[14, 14] = Instantiate(gameObjects[1], new Vector3(-13.5f, 14.0f, 0.0f), Quaternion.identity);

        for (int i = 2; i < 14; i++)
        {
            topLeft[i, 14] = Instantiate(gameObjects[2], new Vector3(-i + 0.5f, 14.0f, 0.0f), Quaternion.identity);
        }
        topLeft[1, 14] = Instantiate(gameObjects[7], new Vector3(-0.5f, 14.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void Line13()
    {
        topLeft[14, 13] = Instantiate(gameObjects[2], new Vector3(-13.5f, 13.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));

        for (int i = 2; i < 14; i++)
        {
            topLeft[i, 13] = Instantiate(gameObjects[5], new Vector3(-i + 0.5f, 13.0f, 0.0f), Quaternion.identity);
        }

        topLeft[1, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 13.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line12()
    {
        topLeft[14, 12] = Instantiate(gameObjects[2], new Vector3(-13.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[13, 12] = Instantiate(gameObjects[5], new Vector3(-12.5f, 12.0f, 0.0f), Quaternion.identity);

        topLeft[12, 12] = Instantiate(gameObjects[3], new Vector3(-11.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[11, 12] = Instantiate(gameObjects[4], new Vector3(-10.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[10, 12] = Instantiate(gameObjects[4], new Vector3(-9.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[9, 12] = Instantiate(gameObjects[3], new Vector3(-8.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[8, 12] = Instantiate(gameObjects[5], new Vector3(-7.5f, 12.0f, 0.0f), Quaternion.identity);

        topLeft[7, 12] = Instantiate(gameObjects[3], new Vector3(-6.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 12] = Instantiate(gameObjects[4], new Vector3(-5.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[5, 12] = Instantiate(gameObjects[4], new Vector3(-4.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[4, 12] = Instantiate(gameObjects[4], new Vector3(-3.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[3, 12] = Instantiate(gameObjects[3], new Vector3(-2.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[2, 12] = Instantiate(gameObjects[5], new Vector3(-1.5f, 12.0f, 0.0f), Quaternion.identity);
        topLeft[1, 12] = Instantiate(gameObjects[4], new Vector3(-0.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line11()
    {
        topLeft[14, 11] = Instantiate(gameObjects[2], new Vector3(-13.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[13, 11] = Instantiate(gameObjects[6], new Vector3(-12.5f, 11.0f, 0.0f), Quaternion.identity);

        topLeft[12, 11] = Instantiate(gameObjects[4], new Vector3(-11.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[9, 11] = Instantiate(gameObjects[4], new Vector3(-8.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[8, 11] = Instantiate(gameObjects[5], new Vector3(-7.5f, 11.0f, 0.0f), Quaternion.identity);

        topLeft[7, 11] = Instantiate(gameObjects[4], new Vector3(-6.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[3, 11] = Instantiate(gameObjects[4], new Vector3(-2.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[2, 11] = Instantiate(gameObjects[5], new Vector3(-1.5f, 11.0f, 0.0f), Quaternion.identity);
        topLeft[1, 11] = Instantiate(gameObjects[4], new Vector3(-0.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line10()
    {
        topLeft[14, 10] = Instantiate(gameObjects[2], new Vector3(-13.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[13, 10] = Instantiate(gameObjects[5], new Vector3(-12.5f, 10.0f, 0.0f), Quaternion.identity);

        topLeft[12, 10] = Instantiate(gameObjects[3], new Vector3(-11.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[11, 10] = Instantiate(gameObjects[4], new Vector3(-10.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[10, 10] = Instantiate(gameObjects[4], new Vector3(-9.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[9, 10] = Instantiate(gameObjects[3], new Vector3(-8.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[8, 10] = Instantiate(gameObjects[5], new Vector3(-7.5f, 10.0f, 0.0f), Quaternion.identity);

        topLeft[7, 10] = Instantiate(gameObjects[3], new Vector3(-6.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 10] = Instantiate(gameObjects[4], new Vector3(-5.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[5, 10] = Instantiate(gameObjects[4], new Vector3(-4.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 10] = Instantiate(gameObjects[4], new Vector3(-3.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[3, 10] = Instantiate(gameObjects[3], new Vector3(-2.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[2, 10] = Instantiate(gameObjects[5], new Vector3(-1.5f, 10.0f, 0.0f), Quaternion.identity);

        topLeft[1, 10] = Instantiate(gameObjects[3], new Vector3(-0.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line9()
    {
        topLeft[14, 9] = Instantiate(gameObjects[2], new Vector3(-13.5f, 9.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 14; i++)
        {
            topLeft[i, 9] = Instantiate(gameObjects[5], new Vector3(-i + 0.5f, 9.0f, 0.0f), Quaternion.identity);
        }
    }

    private void Line8()
    {
        topLeft[14, 8] = Instantiate(gameObjects[2], new Vector3(-13.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[13, 8] = Instantiate(gameObjects[5], new Vector3(-12.5f, 8.0f, 0.0f), Quaternion.identity);

        topLeft[12, 8] = Instantiate(gameObjects[3], new Vector3(-11.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[11, 8] = Instantiate(gameObjects[4], new Vector3(-10.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[10, 8] = Instantiate(gameObjects[4], new Vector3(-9.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[9, 8] = Instantiate(gameObjects[3], new Vector3(-8.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[8, 8] = Instantiate(gameObjects[5], new Vector3(-7.5f, 8.0f, 0.0f), Quaternion.identity);

        topLeft[7, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[6, 8] = Instantiate(gameObjects[3], new Vector3(-5.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[5, 8] = Instantiate(gameObjects[5], new Vector3(-4.5f, 8.0f, 0.0f), Quaternion.identity);

        topLeft[4, 8] = Instantiate(gameObjects[3], new Vector3(-3.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[3, 8] = Instantiate(gameObjects[4], new Vector3(-2.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[2, 8] = Instantiate(gameObjects[4], new Vector3(-1.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[1, 8] = Instantiate(gameObjects[4], new Vector3(-0.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void Line7()
    {
        topLeft[14, 7] = Instantiate(gameObjects[2], new Vector3(-13.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[13, 7] = Instantiate(gameObjects[5], new Vector3(-12.5f, 7.0f, 0.0f), Quaternion.identity);

        topLeft[12, 7] = Instantiate(gameObjects[3], new Vector3(-11.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[11, 7] = Instantiate(gameObjects[4], new Vector3(-10.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[10, 7] = Instantiate(gameObjects[4], new Vector3(-9.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[9, 7] = Instantiate(gameObjects[3], new Vector3(-8.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[8, 7] = Instantiate(gameObjects[5], new Vector3(-7.5f, 7.0f, 0.0f), Quaternion.identity);
        topLeft[7, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 7] = Instantiate(gameObjects[4], new Vector3(-5.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[5, 7] = Instantiate(gameObjects[5], new Vector3(-4.5f, 7.0f, 0.0f), Quaternion.identity);

        topLeft[4, 7] = Instantiate(gameObjects[3], new Vector3(-3.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[3, 7] = Instantiate(gameObjects[4], new Vector3(-2.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[2, 7] = Instantiate(gameObjects[4], new Vector3(-1.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[1, 7] = Instantiate(gameObjects[3], new Vector3(-0.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line6()
    {
        topLeft[14, 6] = Instantiate(gameObjects[2], new Vector3(-13.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));

        for (int i = 8; i < 14; i++)
        {
            topLeft[i, 6] = Instantiate(gameObjects[5], new Vector3(-i + 0.5f, 6.0f, 0.0f), Quaternion.identity);
        }

        topLeft[7, 6] = Instantiate(gameObjects[4], new Vector3(-6.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 6] = Instantiate(gameObjects[4], new Vector3(-5.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        for (int i = 2; i < 6; i++)
        {
            topLeft[i, 6] = Instantiate(gameObjects[5], new Vector3(-i + 0.5f, 6.0f, 0.0f), Quaternion.identity);
        }

        topLeft[1, 6] = Instantiate(gameObjects[4], new Vector3(-0.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line5()
    {
        topLeft[14, 5] = Instantiate(gameObjects[1], new Vector3(-13.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 10; i < 14; i++)
        {
            topLeft[i, 5] = Instantiate(gameObjects[2], new Vector3(-i + 0.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        topLeft[9, 5] = Instantiate(gameObjects[1], new Vector3(-8.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[8, 5] = Instantiate(gameObjects[5], new Vector3(-7.5f, 5.0f, 0.0f), Quaternion.identity);
        topLeft[7, 5] = Instantiate(gameObjects[4], new Vector3(-6.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 5] = Instantiate(gameObjects[3], new Vector3(-5.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));

        topLeft[5, 5] = Instantiate(gameObjects[4], new Vector3(-4.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[4, 5] = Instantiate(gameObjects[4], new Vector3(-3.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        topLeft[3, 5] = Instantiate(gameObjects[3], new Vector3(-2.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        topLeft[1, 5] = Instantiate(gameObjects[4], new Vector3(-0.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line4()
    {
        topLeft[9, 4] = Instantiate(gameObjects[2], new Vector3(-8.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[8, 4] = Instantiate(gameObjects[5], new Vector3(-7.5f, 4.0f, 0.0f), Quaternion.identity);
        topLeft[7, 4] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 4] = Instantiate(gameObjects[3], new Vector3(-5.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
          
        topLeft[5, 4] = Instantiate(gameObjects[4], new Vector3(-4.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 4] = Instantiate(gameObjects[4], new Vector3(-3.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[3, 4] = Instantiate(gameObjects[3], new Vector3(-2.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        topLeft[1, 4] = Instantiate(gameObjects[3], new Vector3(-0.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line3()
    {
        topLeft[9, 3] = Instantiate(gameObjects[2], new Vector3(-8.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[8, 3] = Instantiate(gameObjects[5], new Vector3(-7.5f, 3.0f, 0.0f), Quaternion.identity);
        topLeft[7, 3] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 3] = Instantiate(gameObjects[4], new Vector3(-5.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line2()
    {
        topLeft[9, 2] = Instantiate(gameObjects[2], new Vector3(-8.5f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[8, 2] = Instantiate(gameObjects[5], new Vector3(-7.5f, 2.0f, 0.0f), Quaternion.identity);
        topLeft[7, 2] = Instantiate(gameObjects[4], new Vector3(-6.5f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 2] = Instantiate(gameObjects[4], new Vector3(-5.5f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        topLeft[4, 2] = Instantiate(gameObjects[3], new Vector3(-3.5f, 2.0f, 0.0f), Quaternion.identity);
        topLeft[3, 2] = Instantiate(gameObjects[4], new Vector3(-2.5f, 2.0f, 0.0f), Quaternion.identity);
        topLeft[2, 2] = Instantiate(gameObjects[4], new Vector3(-1.5f, 2.0f, 0.0f), Quaternion.identity);
    }

    private void Line1()
    {
        for (int i = 10; i < 15; i++)
        {
            topLeft[i, 1] = Instantiate(gameObjects[2], new Vector3(-i + 0.5f, 1f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        }
        topLeft[9, 1] = Instantiate(gameObjects[1], new Vector3(-8.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[8, 1] = Instantiate(gameObjects[5], new Vector3(-7.5f, 1.0f, 0.0f), Quaternion.identity);
        topLeft[7, 1] = Instantiate(gameObjects[3], new Vector3(-6.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        topLeft[6, 1] = Instantiate(gameObjects[3], new Vector3(-5.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        topLeft[4, 1] = Instantiate(gameObjects[4], new Vector3(-3.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line0()
    {
        topLeft[8, 0] = Instantiate(gameObjects[5], new Vector3(-7.5f, 0.0f, 0.0f), Quaternion.identity);
        topLeft[4, 0] = Instantiate(gameObjects[4], new Vector3(-3.5f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }
}
