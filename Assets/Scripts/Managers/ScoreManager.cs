using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //public int score;
    private int totalPellets;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTotalPellet(int pellet)
    {
        totalPellets += pellet;
    }

    public int GetTotalPellets()
    {
        return totalPellets;
    }
}
