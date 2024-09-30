using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster I = null;

    GridManager gridM;
    GameplayUI gameUI;
    Score score;

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

    bool canStartGame;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canStartGame = true;
        if (canStartGame)
        {
            gridM = FindObjectOfType<GridManager>();
            gameUI = FindObjectOfType<GameplayUI>();
            score = GetComponent<Score>();

            if (gameUI != null)
            {
                gameUI.InitText();
            }

            if (gridM != null)
            {
                gridM.size = gridStartSize;
                spawnPercent = spawnPercentMin;
                gridM.InitGrid();
            }
        }
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
                GameOver();
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

    void GameOver()
    {
        score.SaveScore(totalTilesFound, level, seconds, minutes);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
