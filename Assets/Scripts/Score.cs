using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public enum ScoreName
    {
        Nut,
        Level,
        Seconds,
        Minutes
    }

    public void SaveScore(int nut, int level, int seconds, int minutes)
    {
        PlayerPrefs.SetInt(ScoreName.Nut.ToString(), nut);
        PlayerPrefs.SetInt(ScoreName.Level.ToString(), level);
        PlayerPrefs.SetInt(ScoreName.Seconds.ToString(), seconds);
        PlayerPrefs.SetInt(ScoreName.Minutes.ToString(), minutes);
    }

    public int LoadIntScore(ScoreName name)
    {
        return PlayerPrefs.GetInt(name.ToString());
    }
}
