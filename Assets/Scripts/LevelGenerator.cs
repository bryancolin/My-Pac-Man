using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;
    private GameObject[,] levelMap = new GameObject[14, 15];
    private GameObject[,] flipVertical;
    private int rows, columns;

    private void Awake()
    {
        rows = levelMap.GetLength(0);
        columns = levelMap.GetLength(1);

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
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                if(levelMap[i, j] != null)
                {
                    GameObject.Instantiate(levelMap[i, j], new Vector3(-levelMap[i, j].transform.position.x - 1, levelMap[i, j].transform.position.y, 0), Quaternion.Euler(levelMap[i, j].transform.rotation.x, 180.0f, levelMap[i, j].transform.rotation.eulerAngles.z));
                }
            }
        }
    }

    private void Quadrant3()
    {
        for (int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                if (levelMap[i, j] != null)
                {
                    GameObject.Instantiate(levelMap[i, j], new Vector3(levelMap[i, j].transform.position.x, -levelMap[i, j].transform.position.y + 3, 0), Quaternion.Euler(180.0f, levelMap[i, j].transform.rotation.y, levelMap[i, j].transform.rotation.eulerAngles.z));
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
                if (levelMap[i, j] != null)
                {
                    GameObject.Instantiate(levelMap[i, j], new Vector3(-levelMap[i, j].transform.position.x - 1, -levelMap[i, j].transform.position.y + 3, 0), Quaternion.Euler(180.0f, 180.0f, levelMap[i, j].transform.rotation.eulerAngles.z));
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
    }

    private void Line1()
    {
        levelMap[0, 0] = Instantiate(gameObjects[1], new Vector3(-13.5f, 14.5f, 0.0f), Quaternion.identity);

        for (int i = 1; i < 13; i++)
        {
            levelMap[0, i] = Instantiate(gameObjects[2], new Vector3(i - 14.0f, 14.5f, 0.0f), Quaternion.identity);
        }
        levelMap[0, 13] = Instantiate(gameObjects[7], new Vector3(-1.5f, 14.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void Line2()
    {
        levelMap[1, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 14.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 13; i++)
        {
            levelMap[1, i] = Instantiate(gameObjects[5], new Vector3(i - 14.0f, 14.0f, 0.0f), Quaternion.identity);
        }
        levelMap[1, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 14.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line3()
    {
        levelMap[2, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 13.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[2, 1] = Instantiate(gameObjects[5], new Vector3(-13.0f, 13.0f, 0.0f), Quaternion.identity);

        levelMap[2, 2] = Instantiate(gameObjects[3], new Vector3(-11.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 3] = Instantiate(gameObjects[4], new Vector3(-11f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 4] = Instantiate(gameObjects[4], new Vector3(-10f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 5] = Instantiate(gameObjects[3], new Vector3(-9.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    
        levelMap[2, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 13.0f, 0.0f), Quaternion.identity);

        levelMap[2, 7] = Instantiate(gameObjects[3], new Vector3(-6.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 8] = Instantiate(gameObjects[4], new Vector3(-6f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 9] = Instantiate(gameObjects[4], new Vector3(-5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 10] = Instantiate(gameObjects[4], new Vector3(-4f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[2, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 12.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[2, 12] = Instantiate(gameObjects[5], new Vector3(-2.0f, 13.0f, 0.0f), Quaternion.identity);
        levelMap[2, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 13.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line4()
    {
        levelMap[3, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[3, 1] = Instantiate(gameObjects[6], new Vector3(-13.0f, 12.0f, 0.0f), Quaternion.identity);

        levelMap[3, 2] = Instantiate(gameObjects[4], new Vector3(-11.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[3, 5] = Instantiate(gameObjects[4], new Vector3(-9.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[3, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 12.0f, 0.0f), Quaternion.identity);

        levelMap[3, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[3,11] = Instantiate(gameObjects[4], new Vector3(-3.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[3,12] = Instantiate(gameObjects[5], new Vector3(-2.0f, 12.0f, 0.0f), Quaternion.identity);
        levelMap[3,13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 12.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line5()
    {
        levelMap[4, 1] = Instantiate(gameObjects[2], new Vector3(-13.5f, 11.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[4, 2] = Instantiate(gameObjects[5], new Vector3(-13.0f, 11.0f, 0.0f), Quaternion.identity);

        levelMap[4, 3] = Instantiate(gameObjects[3], new Vector3(-11.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[4, 4] = Instantiate(gameObjects[4], new Vector3(-11f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[4, 5] = Instantiate(gameObjects[4], new Vector3(-10f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[4, 6] = Instantiate(gameObjects[3], new Vector3(-9.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        levelMap[4, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 11.0f, 0.0f), Quaternion.identity);

        levelMap[4, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[4, 9] = Instantiate(gameObjects[4], new Vector3(-6f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[4, 10] = Instantiate(gameObjects[4], new Vector3(-5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[4, 11] = Instantiate(gameObjects[4], new Vector3(-4f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[4, 12] = Instantiate(gameObjects[3], new Vector3(-3.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        levelMap[4, 13] = Instantiate(gameObjects[5], new Vector3(-2.0f, 11.0f, 0.0f), Quaternion.identity);

        levelMap[4, 14] = Instantiate(gameObjects[3], new Vector3(-0.5f, 11.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line6()
    {
        levelMap[5, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 10.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 14; i++)
        {
            levelMap[5, i] = Instantiate(gameObjects[5], new Vector3(i - 14.0f, 10.0f, 0.0f), Quaternion.identity);
        }
    }

    private void Line7()
    {
        levelMap[6, 1] = Instantiate(gameObjects[2], new Vector3(-13.5f, 9.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[6, 2] = Instantiate(gameObjects[5], new Vector3(-13.0f, 9.0f, 0.0f), Quaternion.identity);

        levelMap[6, 3] = Instantiate(gameObjects[3], new Vector3(-11.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 4] = Instantiate(gameObjects[4], new Vector3(-11f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 5] = Instantiate(gameObjects[4], new Vector3(-10f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 6] = Instantiate(gameObjects[3], new Vector3(-9.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[6, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 9.0f, 0.0f), Quaternion.identity);

        levelMap[6, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 9] = Instantiate(gameObjects[3], new Vector3(-6.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[6, 10] = Instantiate(gameObjects[5], new Vector3(-5.0f, 9.0f, 0.0f), Quaternion.identity);

        levelMap[6, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 12] = Instantiate(gameObjects[4], new Vector3(-3.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 13] = Instantiate(gameObjects[4], new Vector3(-2.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[6, 14] = Instantiate(gameObjects[4], new Vector3(-1.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void Line8()
    {
        levelMap[7, 1] = Instantiate(gameObjects[2], new Vector3(-13.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[7, 2] = Instantiate(gameObjects[5], new Vector3(-13.0f, 8.0f, 0.0f), Quaternion.identity);

        levelMap[7, 3] = Instantiate(gameObjects[3], new Vector3(-11.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[7, 4] = Instantiate(gameObjects[4], new Vector3(-11f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[7, 5] = Instantiate(gameObjects[4], new Vector3(-10f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[7, 6] = Instantiate(gameObjects[3], new Vector3(-9.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        levelMap[7, 7] = Instantiate(gameObjects[5], new Vector3(-8.0f, 8.0f, 0.0f), Quaternion.identity);
        levelMap[7, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[7, 9] = Instantiate(gameObjects[4], new Vector3(-6.5f, 8.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[7, 10] = Instantiate(gameObjects[5], new Vector3(-5.0f, 8.0f, 0.0f), Quaternion.identity);

        levelMap[7, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[7, 12] = Instantiate(gameObjects[4], new Vector3(-3.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[7, 13] = Instantiate(gameObjects[4], new Vector3(-2.0f, 8.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[7, 14] = Instantiate(gameObjects[3], new Vector3(-1.5f, 7.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line9()
    {
        levelMap[8, 0] = Instantiate(gameObjects[2], new Vector3(-13.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 7; i++)
        {
            levelMap[8, i] = Instantiate(gameObjects[5], new Vector3(-i - 7.0f, 7.0f, 0.0f), Quaternion.identity);
        }

        levelMap[8, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[8, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        for (int i = 9; i < 13; i++)
        {
            levelMap[8, i] = Instantiate(gameObjects[5], new Vector3(-i + 7.0f, 7.0f, 0.0f), Quaternion.identity);
        }

        levelMap[8, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 7.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line10()
    {
        levelMap[9, 0] = Instantiate(gameObjects[1], new Vector3(-13.5f, 6.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        for (int i = 1; i < 5; i++)
        {
            levelMap[9, i] = Instantiate(gameObjects[2], new Vector3(-i - 9.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
        levelMap[9, 5] = Instantiate(gameObjects[1], new Vector3(-9.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[9, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 6.0f, 0.0f), Quaternion.identity);
        levelMap[9, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[9, 8] = Instantiate(gameObjects[3], new Vector3(-5.5f, 6.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));

        levelMap[9, 9] = Instantiate(gameObjects[4], new Vector3(-5.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[9, 10] = Instantiate(gameObjects[4], new Vector3(-4.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        levelMap[9, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));

        levelMap[9, 13] = Instantiate(gameObjects[4], new Vector3(-0.5f, 6.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line11()
    {
        levelMap[10, 5] = Instantiate(gameObjects[2], new Vector3(-9.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[10, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 5.0f, 0.0f), Quaternion.identity);
        levelMap[10, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 5.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[10, 8] = Instantiate(gameObjects[3], new Vector3(-5.5f, 4.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

        levelMap[10, 9] = Instantiate(gameObjects[4], new Vector3(-5.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[10, 10] = Instantiate(gameObjects[4], new Vector3(-4.0f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[10, 11] = Instantiate(gameObjects[3], new Vector3(-3.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));

        levelMap[10, 12] = Instantiate(gameObjects[3], new Vector3(-0.5f, 5.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line12()
    {
        levelMap[11, 5] = Instantiate(gameObjects[2], new Vector3(-9.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[11, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 4.0f, 0.0f), Quaternion.identity);
        levelMap[11, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[11, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 4.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }

    private void Line13()
    {
        levelMap[12, 5] = Instantiate(gameObjects[2], new Vector3(-9.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[12, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 3.0f, 0.0f), Quaternion.identity);
        levelMap[12, 7] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[12, 8] = Instantiate(gameObjects[4], new Vector3(-6.5f, 3.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f));
        levelMap[12, 10] = Instantiate(gameObjects[3], new Vector3(-3.5f, 2.5f, 0.0f), Quaternion.identity);
        levelMap[12, 11] = Instantiate(gameObjects[4], new Vector3(-3f, 2.5f, 0.0f), Quaternion.identity);
        levelMap[12, 12] = Instantiate(gameObjects[4], new Vector3(-2f, 2.5f, 0.0f), Quaternion.identity);
    }

    private void Line14()
    {
        for (int i = 0; i < 5; i++)
        {
            levelMap[13, i] = Instantiate(gameObjects[2], new Vector3(-i - 10, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        }
        levelMap[13, 5] = Instantiate(gameObjects[1], new Vector3(-9.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[13, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 2.0f, 0.0f), Quaternion.identity);
        levelMap[13, 7] = Instantiate(gameObjects[3], new Vector3(-6.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        levelMap[13, 8] = Instantiate(gameObjects[3], new Vector3(-6.5f, 2.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        levelMap[13, 10] = Instantiate(gameObjects[4], new Vector3(-3.5f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    private void Line15()
    {
        levelMap[14, 6] = Instantiate(gameObjects[5], new Vector3(-8.0f, 1.0f, 0.0f), Quaternion.identity);
        levelMap[14, 10] = Instantiate(gameObjects[4], new Vector3(-3.5f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }
}
