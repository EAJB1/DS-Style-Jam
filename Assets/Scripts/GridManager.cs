using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile mud, nut, grass;

    [Space]

    [SerializeField] float padding;
    [HideInInspector] public int size;
    int spawnCount;
    [SerializeField] float previewTime;
    [SerializeField] float transitionTime;

    [Space]

    public List<Tile> mudGrid, nutGrid, grassGrid;

    [Space]

    bool[] canSpawn;

    public int ReturnSpawnCount()
    {
        return spawnCount;
    }

    public void InitGrid()
    {
        GameMaster.I.lives = GameMaster.I.startLives;

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        GameMaster.I.tilesFound = 0;

        mudGrid = GenerateGridLayer(mud.gameObject, false);

        GenerateRandomPositions();
        nutGrid = GenerateGridLayer(nut.gameObject, true);

        grassGrid = GenerateGridLayer(grass.gameObject, false);

        ShowGrid(mudGrid);
        ShowGrid(nutGrid);
        ShowGrid(grassGrid);

        StartCoroutine(GridPreview(grassGrid));

        GameMaster.I.UpdateUI();
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

                Vector2 position = (Vector2)transform.position + new Vector2(x, y) * (1f + padding) - offset;

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
        spawnCount = (int)(canSpawn.Length * GameMaster.I.spawnPercent);
        int count = spawnCount;

        while (count > 0)
        {
            for (int i = 0; i < canSpawn.Length; i++)
            {
                if (count == 0) { break; }

                float rnd = Random.Range(0, 99) / 100f;

                if (rnd < GameMaster.I.spawnPercent && !canSpawn[i])
                {
                    canSpawn[i] = true;
                    count--;
                }
            }
        }
    }

    public IEnumerator GridTransition(List<Tile> grid)
    {
        HideGrid(grid);

        yield return new WaitForSeconds(transitionTime);

        ShowGrid(grid);

        yield return new WaitForSeconds(transitionTime);

        InitGrid();
    }

    IEnumerator GridPreview(List<Tile> grid)
    {
        HideGrid(grid);

        yield return new WaitForSeconds(previewTime);
        
        ShowGrid(grid);
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
}
