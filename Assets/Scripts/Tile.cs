using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public readonly Substance substance = new Substance(0, Physics.Materials[1], Aggregation.Gas);
    public void Init(Vector2Int pos)
    {
    }
    

    public void OnMouseOver()
    {
        FindObjectOfType<MaterialPanel>().SetTile(this);
    }
}
