using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile mud, nut, grass;

    [Space]

    [SerializeField] int size;
    [SerializeField] float padding;
    [SerializeField] float spawnPercent;
    int spawnCount;
    [SerializeField] float previewTime;
    [SerializeField] float transitionTime;

    [Space]

    [SerializeField] int startLives;
    int currentLives;

    [HideInInspector] public Tile selectedTile;
    
    bool[] canSpawn;
    List<Tile> mudGrid, nutGrid, grassGrid;
    int tilesFound;

    void Start()
    {
        InitGrid();
    }

    public void CheckGameState(Tile tile)
    {
        int bankedTilesFound = tilesFound;

        for (int i = 0; i < nutGrid.Count; i++)
        {
            if (tile.coordinates.x == nutGrid[i].coordinates.x &&
                tile.coordinates.y == nutGrid[i].coordinates.y)
            {
                tilesFound++;
            }
        }

        if (bankedTilesFound == tilesFound)
        {
            currentLives--;

            if (currentLives == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (tilesFound == (int)(canSpawn.Length * spawnPercent))
        {
            size++;
            StartCoroutine(GridTransition(grassGrid));
        }
    }

    void InitGrid()
    {
        currentLives = startLives;

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        tilesFound = 0;

        mudGrid = GenerateGridLayer(mud.gameObject, false);

        GenerateRandomPositions();
        nutGrid = GenerateGridLayer(nut.gameObject, true);

        grassGrid = GenerateGridLayer(grass.gameObject, false);

        ShowGrid(mudGrid);
        ShowGrid(nutGrid);
        ShowGrid(grassGrid);

        StartCoroutine(GridPreview(grassGrid));
    }

    List<Tile> GenerateGridLayer(GameObject tile, bool isNut)
    {
        List<Tile> tempGrid = new List<Tile>(size * size);

        Vector2 offset = (((Vector2.one * size) / 2f) + (Vector2.one * (size * padding)) / 2f) - new Vector2(0.5f, 0.5f);

        int index = 0;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (isNut && !canSpawn[index])
                {
                    index++;
                    continue;
                }

                Vector2 position = new Vector2(x, y) * (1f + padding) - offset;

                Tile currentTile = Instantiate(tile, position, Quaternion.identity).GetComponent<Tile>();
                currentTile.transform.parent = transform;
                currentTile.coordinates = new Vector2(x, y);

                tempGrid.Add(currentTile);

                index++;
            }
        }

        return tempGrid;
    }

    void GenerateRandomPositions()
    {
        canSpawn = new bool[size * size];
        spawnCount = (int)(canSpawn.Length * spawnPercent);

        while (spawnCount > 0)
        {
            for (int i = 0; i < canSpawn.Length; i++)
            {
                if (spawnCount == 0) { break; }

                float rnd = Random.Range(0, 99) / 100f;

                if (rnd < spawnPercent && !canSpawn[i])
                {
                    canSpawn[i] = true;
                    spawnCount--;
                }
            }
        }
    }

    IEnumerator GridPreview(List<Tile> grid)
    {
        HideGrid(grid);

        yield return new WaitForSeconds(previewTime);
        
        ShowGrid(grid);
    }

    IEnumerator GridTransition(List<Tile> grid)
    {
        ShowGrid(grid);

        yield return new WaitForSeconds(transitionTime);

        InitGrid();
    }


    void HideGrid(List<Tile> grid)
    {
        foreach (Tile tile in grid)
        {
            tile.gameObject.SetActive(false);
        }
    }

    void ShowGrid(List<Tile> grid)
    {
        foreach (Tile tile in grid)
        {
            tile.gameObject.SetActive(true);
        }
    }

    Tile ReturnSelectedTile(Tile tile)
    {
        return tile;
    }

    Tile ReturnTileAtPosition(List<Tile> grid, Vector2 position)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i].coordinates.x == position.x &&
                grid[i].coordinates.y == position.y)
            {
                return grid[i];
            }
        }

        return null;
    }
}
