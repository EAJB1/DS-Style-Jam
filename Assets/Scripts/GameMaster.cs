using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster I;

    [SerializeField] GridManager gridM;
    [SerializeField] GameplayUI gameUI;
    GameOver gameOver;

    [Space]

    [SerializeField] int level;
    [SerializeField] int subLevel;
    [SerializeField] int maxSubLevel;
    
    [Space]

    public int startLives;
    public int lives;
    
    [Space]
    
    public int gridStartSize;
    [SerializeField] int gridMaxSize;
    public float spawnPercent;
    [SerializeField] float spawnPercentIncrease;
    public float spawnPercentMin, spawnPercentMax;
    public int levelTilesFound;
    public int totalTilesFound;

    [Space]

    public int seconds;
    public int minutes;
    float timer;

    private void Awake()
    {
        I = this;
        DontDestroyOnLoad(I);
    }

    private void Start()
    {
        gridM.size = gridStartSize;
        spawnPercent = spawnPercentMin;
        gridM.InitGrid();
    }

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;
        minutes = (int)timer / 60;
    }

    public void CheckGameState(Tile tile)
    {
        int bankedTilesFound = levelTilesFound;

        // Correct tile
        for (int i = 0; i < gridM.nutGrid.Count; i++)
        {
            if (tile.coordinates.x == gridM.nutGrid[i].coordinates.x &&
                tile.coordinates.y == gridM.nutGrid[i].coordinates.y)
            {
                levelTilesFound++;
                gameUI.SetAcornUI(levelTilesFound, gridM.ReturnSpawnCount());
            }
        }

        // Incorrect tile
        if (bankedTilesFound == levelTilesFound)
        {
            lives--;
            gameUI.SetLivesUI(lives);

            if (lives == 0)
            {
                StartCoroutine(GameOver());
            }
        }

        if (levelTilesFound == (gridM.ReturnSpawnCount()))
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        level++;
        subLevel++;

        totalTilesFound += levelTilesFound;

        spawnPercent += spawnPercentIncrease;
        spawnPercent = Mathf.Clamp(spawnPercent, spawnPercentMin, spawnPercentMax);

        if (subLevel > maxSubLevel)
        {
            subLevel = 1;
            gridM.size++;
            gridM.size = Mathf.Clamp(gridM.size, gridStartSize, gridMaxSize);

            spawnPercent = spawnPercentMin;
        }

        StartCoroutine(gridM.GridTransition(gridM.grassGrid));

    }

    public void UpdateUI()
    {
        levelTilesFound = 0;
        gameUI.SetAcornUI(levelTilesFound, gridM.ReturnSpawnCount());

        gameUI.SetLivesUI(lives);

        gameUI.SetLevelUI(level);

        gameUI.SetSubLevelUI(subLevel, maxSubLevel);
    }

    IEnumerator GameOver()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(sceneIndex);

        yield return null;

        gameOver = FindObjectOfType<GameOver>();

        if (gameOver != null)
        {
            gameOver.SetAcornUI(totalTilesFound);
            gameOver.SetLevelUI(level);
            gameOver.SetTimeUI(seconds, minutes);
        }
    }
}
