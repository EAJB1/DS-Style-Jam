using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 coordinates;

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        
        GameMaster.I.CheckGameState(transform.GetComponent<Tile>());
    }
}
