using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Image image;
    public Vector2 coordinates;

    GridManager gridManager;

    private void Start()
    {
        gridManager = GetComponentInParent<GridManager>();
    }

    private void OnMouseDown()
    {
        gridManager.selectedTile = transform;
        gameObject.SetActive(false);
    }
}
