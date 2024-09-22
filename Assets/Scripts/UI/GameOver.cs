using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI acornText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timeText;

    public void SetAcornUI(int count)
    {
        acornText.text = "x" + count.ToString();
    }

    public void SetLevelUI(int count)
    {
        levelText.text = count.ToString();
    }

    public void SetTimeUI(int sec, int min)
    {
        timeText.text = min.ToString() + ":" + sec.ToString();
    }
}
