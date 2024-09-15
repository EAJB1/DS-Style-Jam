using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject mud, grass;

    [Space]

    [SerializeField] int size;
    [SerializeField] float padding;

    [Space]

    public Transform selectedTile;

    void Start()
    {
        GenerateGridLayer(mud);
        GenerateGridLayer(grass);
    }

    void GenerateGridLayer(GameObject tile)
    {
        Vector2 offset = (((Vector2.one * size) / 2f) + (Vector2.one * (size * padding)) / 2f) - new Vector2(0.5f, 0.5f);
        Debug.Log(offset);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 position = new Vector2(x, y) * (1f + padding) - offset;

                Tile currentTile = Instantiate(tile, position, Quaternion.identity).GetComponent<Tile>();
                currentTile.transform.parent = transform;
                currentTile.coordinates = new Vector2(x, y);
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
