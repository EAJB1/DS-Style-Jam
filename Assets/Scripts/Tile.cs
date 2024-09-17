using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Sprite image;
    public Vector2 coordinates;

    SpriteRenderer sR;
    GridManager grid;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();

        if (image != null)
        {
            sR.sprite = image;
        }

        grid = GetComponentInParent<GridManager>();
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        
        grid.CheckGameState(transform.GetComponent<Tile>());
    }
}
