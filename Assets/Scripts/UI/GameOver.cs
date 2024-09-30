using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] Score score;

    [Space]
    
    [SerializeField] TextMeshProUGUI acornText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timeText;

    private void Start()
    {
        SetAcornUI(score.LoadIntScore(Score.ScoreName.Nut));
        SetLevelUI(score.LoadIntScore(Score.ScoreName.Level));
        SetTimeUI(score.LoadIntScore(Score.ScoreName.Seconds), score.LoadIntScore(Score.ScoreName.Minutes));
    }

    void SetAcornUI(int count)
    {
        acornText.text = "x" + count.ToString();
    }

    void SetLevelUI(int count)
    {
        levelText.text = count.ToString();
    }

    void SetTimeUI(int sec, int min)
    {
        timeText.text = min.ToString() + ":" + sec.ToString();
    }
}
