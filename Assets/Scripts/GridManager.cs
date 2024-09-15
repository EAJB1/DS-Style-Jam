using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject mud, nut, grass;

    [Space]

    [SerializeField] int size, spawnCount;
    [SerializeField] float padding;

    [Space]

    public Transform selectedTile;

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

        //GenerateGridLayer(grass, false);
    }

    void GenerateGridLayer(GameObject tile, bool isNut)
    {
        Vector2 offset = (((Vector2.one * size) / 2f) + (Vector2.one * (size * padding)) / 2f) - new Vector2(0.5f, 0.5f);
        Debug.Log(offset);

        int index = 0;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 position = new Vector2(x, y) * (1f + padding) - offset;

                if (isNut && !canSpawn[index])
                {
                    continue;
                }

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
        int count = spawnCount;

        for (int i = 0; i < canSpawn.Length; i++)
        {
            int rnd = Random.Range(0, 1);

            if (count > 0 /*&& rnd == 0*/)
            {
                canSpawn[i] = true;
                count--;
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
