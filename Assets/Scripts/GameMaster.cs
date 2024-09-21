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

    [Space]

    [SerializeField] int level;
    [SerializeField] int subLevel;
    [SerializeField] int maxSubLevel;
    public int startLives;
    [HideInInspector] public int lives;
    public int gridStartSize;
    public float spawnPercent;
    [HideInInspector] public int tilesFound;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        gridM.size = gridStartSize;
        gridM.InitGrid();
    }

    public void CheckGameState(Tile tile)
    {
        int bankedTilesFound = tilesFound;

        // Correct tile
        for (int i = 0; i < gridM.nutGrid.Count; i++)
        {
            if (tile.coordinates.x == gridM.nutGrid[i].coordinates.x &&
                tile.coordinates.y == gridM.nutGrid[i].coordinates.y)
            {
                tilesFound++;
                gameUI.SetAcornUI(tilesFound, gridM.ReturnSpawnCount());
            }
        }

        // Incorrect tile
        if (bankedTilesFound == tilesFound)
        {
            lives--;
            gameUI.SetLivesUI(lives);

            if (lives == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (tilesFound == (gridM.ReturnSpawnCount()))
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        level++;
        subLevel++;

        if (subLevel > maxSubLevel)
        {
            subLevel = 1;
            gridM.size++;
        }

        StartCoroutine(gridM.GridTransition(gridM.grassGrid));

    }

    public void UpdateUI()
    {
        tilesFound = 0;
        gameUI.SetAcornUI(tilesFound, gridM.ReturnSpawnCount());

        gameUI.SetLivesUI(lives);

        gameUI.SetLevelUI(level);

        gameUI.SetSubLevelUI(subLevel, maxSubLevel);
    }
}
