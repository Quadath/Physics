using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTilemap : MonoBehaviour
{
    public int height, width;
    public static Tile[,] Tiles;

    public Tile vacuum;
    private void Start()
    {
        Tiles = new Tile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tiles[x, y] = Instantiate(vacuum, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Tile>();
                Tiles[x, y].substance.Material = Physics.Materials[1];
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) Tiles[x, y].substance.Material = Physics.Materials[0];
                if (x == 2 && y == 2)
                {
                    Tiles[x, y].substance.Material = Physics.Materials[2];
                    Tiles[x, y].substance.Aggregation = Aggregation.Gas;
                    Tiles[x, y].substance.Mass = 5;
                }
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tiles[x, y].Init(new Vector2Int(x, y));
            }
        }

        StartCoroutine(Tick());
    }

    public IEnumerator Tick()
    {
        while (true)
        {
            SubstanceTick();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SubstanceTick()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Tiles[x, y].substance.Aggregation == Aggregation.Gas)
                {
                    Tile[] neighbors = Neighbors(new Vector2Int(x, y));
                    for (int a = 0; a < 4; a++)
                    {
                        if (!neighbors[a]) continue;
                        Substance current = Tiles[x, y].substance;
                        Substance n = neighbors[a].substance;
                        if (current.Material != n.Material && n.Material.Name != "Vacuum") continue;
                        if (current.Mass < n.Mass) continue;

                        float diff = current.Mass - n.Mass;
                        current.AddMass(-diff / 16);
                        n.AddMass(diff / 16);
                        if (n.Material.Name == "Vacuum")
                        {
                            n.Material = current.Material;
                            n.Aggregation = current.Aggregation;
                        }
                        if (current.Material.Name == "Hydrogen")
                        {
                            Tiles[x,y].GetComponent<SpriteRenderer>().color = Color.magenta;
                        }

                    }
                }
            }
        }
    }
    public Tile[] Neighbors(Vector2Int cords)
    {
        Tile[] tiles = new[]
        {
            GetTile(new Vector2Int(cords.x, cords.y + 1)),
            GetTile(new Vector2Int(cords.x + 1, cords.y)),
            GetTile(new Vector2Int(cords.x, cords.y - 1)),
            GetTile(new Vector2Int(cords.x - 1, cords.y))
        };
        return tiles;
    }

    private Tile GetTile(Vector2Int cords)
    {
        if (cords.x <= -1) return null;
        if (cords.x >= width) return null;
        if (cords.y <= -1) return null;
        if (cords.y >= height) return null;
        return Tiles[cords.x, cords.y];
    }
 }
