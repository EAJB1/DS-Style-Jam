using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject mud, nut, grass;

    [Space]

    [SerializeField] int size;
    [SerializeField] float padding;
    
    [SerializeField] float spawnPercent;
    int spawnCount;

    [Space]

    [HideInInspector] public Transform selectedTile;

    bool[] canSpawn;

    void Start()
    {
        InitGrid();
    }

    void InitGrid()
    {
        GenerateGridLayer(mud, false);

        GenerateRandomPositions();
        GenerateGridLayer(nut, true);

        GenerateGridLayer(grass, false);
    }

    void GenerateGridLayer(GameObject tile, bool isNut)
    {
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

                index++;
            }
        }
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

    Tile ReturnTile(int xPos, int yPos)
    {
        Tile tile = new Tile();
        int count = 0;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (yPos == y && xPos == x)
                {


                    return tile;
                }

                count++;
            }
        }

        return null;
    }
}
