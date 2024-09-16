using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Image image;
    public Vector2 coordinates;

    GridManager grid;

    private void Start()
    {
        grid = GetComponentInParent<GridManager>();
    }

    private void OnMouseDown()
    {
        //grid.selectedTile = transform.GetComponent<Tile>();
        grid.tilesFound++;
        gameObject.SetActive(false);

        grid.CheckGameState();
    }
}
