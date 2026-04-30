using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public TextMeshProUGUI stage01_02;
    public TextMeshProUGUI stage03;
    public TextMeshProUGUI stage04;
    public TextMeshProUGUI stage05;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stage01_02.text = "Stage 1 & 2 : " + HighScore.Load(1).ToString();
        stage03.text = "Stage 3 : " + HighScore.Load(2).ToString();
        stage04.text = "Stage 4 : " + HighScore.Load(3).ToString();
        stage05.text = "Stage 5 : " + HighScore.Load(4).ToString();
    }
}
