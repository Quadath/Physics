using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public readonly Substance Substance = new Substance(0, Physics.Materials[1], Aggregation.Gas);
    public Vector2Int pos;
    public void Init(Vector2Int p)
    {
        pos = p;
    }
    

    public void OnMouseOver()
    {
        FindObjectOfType<MaterialPanel>().SetTile(this);
    }

    public void OnMouseDown()
    {
        Substance.Mass++;
    }
}
