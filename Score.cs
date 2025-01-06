using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    private UI ui;

    void Start()
    {
        ui = FindObjectOfType<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        ui.scoreTxt.text = score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
