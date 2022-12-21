using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material Material = Physics.Materials[1];

    private Vector2Int cords;

    private Substance substance;


    private MaterialTilemap materialTilemap;

    private void Awake()
    {
        materialTilemap = FindObjectOfType<MaterialTilemap>();
    }

    public void Init(Vector2Int pos)
    {
        cords = pos;
        gameObject.name = Material.Name;
        // if (Material.Name != "???") neighbors = materialTilemap.Neighbors(cords);
        StartCoroutine(Tick());
    }
    
    private IEnumerator Tick()
    {
        while (true)
        {
            if (Aggregation == Aggregation.Gas)
            {
                GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            if (Aggregation == Aggregation.Gas)
            {
                GasTick();
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void OnMouseOver()
    {
        FindObjectOfType<MaterialPanel>().SetTile(this);
    }

    private void GasTick()
    {
        for (int t = 0; t < 4; t++)
        {
            // if (!neighbors[t]) continue;
            // Tile n = neighbors[t];
            // if (n.Material.Name != Material.Name && n.Material.Name != "Vacuum") continue;
            // if (n.mass > mass) continue;
            // if (mass < 0.01) continue;
            // float diff = mass -= n.mass;
            // AddMass(-(diff / 16));
            // if (n.Material.Name == "Vacuum")
            // {
            //     n.Material = Material;
            //     n.Aggregation = Aggregation;
            // }
            // n.AddMass(diff / 16);
        }
    }

    public void AddMass(float m)
    {
        mass += m;
    }
}
