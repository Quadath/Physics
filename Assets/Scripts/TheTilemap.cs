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
                Tiles[x, y].pos = new Vector2Int(x, y);
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) Tiles[x, y].Substance.Material = Physics.Materials[0];
                if (x == 2 && y == 2)
                {
                    Tiles[x, y].Substance.Material = Physics.Materials[3];
                    Tiles[x, y].Substance.Aggregation = Aggregation.Gas;
                    Tiles[x, y].Substance.Mass = 1;
                }

                if (x == 14 && y == 14)
                {
                    Tiles[x, y].Substance.Material = Physics.Materials[2];
                    Tiles[x, y].Substance.Aggregation = Aggregation.Gas;
                    Tiles[x, y].Substance.Mass = 10;
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
            yield return new WaitForEndOfFrame();
        }
    }

    private void SubstanceTick()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile tile = Tiles[x, y];
                if (tile.Substance.Material == Physics.Materials[0]) continue;
                if (tile.Substance.Material == Physics.Materials[1]) continue;
                if (tile.Substance.Aggregation == Aggregation.Gas)
                {
                    
                    Tile[] neighbors = Neighbors(new Vector2Int(x, y));
                    float massReduction = 0;
                    for (int a = 0; a < 4; a++)
                    {
                        if (!neighbors[a]) continue;
                        Substance current = Tiles[x, y].Substance;
                        Substance n = neighbors[a].Substance;
                        {
                            if (current.Material.Name == "Hydrogen")
                                Tiles[x, y].GetComponent<SpriteRenderer>().color =
                                    new Color(1f, 0.5f, 0.8f, current.Mass / 2);
                            if (current.Material.Name == "Oxygen")
                                Tiles[x, y].GetComponent<SpriteRenderer>().color =
                                    new Color(0.6f, 0.8f, 0.8f, current.Mass / 2);
                            if (current.Material.Name == "???")
                                Tiles[x,y].GetComponent<SpriteRenderer>().color = Color.black;
                        }
                        if (n.Material == Physics.Materials[0]) continue;
                        if (current.Mass < n.Mass) continue;
                        if (n.Material.Name == "Vacuum")
                        {
                            float diff = current.Mass - n.Mass;
                            massReduction += (diff / 16);
                            n.AddMass(diff / 16);
                            n.Material = current.Material;
                            n.Aggregation = current.Aggregation;
                            continue;
                        }
                        if (current.Material == n.Material)
                        {
                            float diff = current.Mass - n.Mass;
                            massReduction += (diff / 16);
                            n.AddMass(diff / 16);
                            continue;
                        }
                        if (current.Mass * 2 < n.Mass) continue;
                        if (!SubstanceCanBePushed(neighbors[a].pos)) continue;
                        PushSubstance(neighbors[a].pos, new Substance(current.Mass / 16, current.Material, current.Aggregation));
                        massReduction += (current.Mass / 16);
                    }
                    tile.Substance.AddMass(-massReduction);
                }
            }
        }
    }

    #region neighbours
    private Tile[] Neighbors(Vector2Int cords)
    {
        Tile[] tiles = new[]
        {
            GetTile(new Vector2Int(cords.x, cords.y - 1)),
            GetTile(new Vector2Int(cords.x - 1, cords.y)),
            GetTile(new Vector2Int(cords.x, cords.y + 1)),
            GetTile(new Vector2Int(cords.x + 1, cords.y))
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

    #endregion

    #region substance
    
    private bool SubstanceCanBePushed(Vector2Int cords)
    {
        bool canBePushed = false;
        Tile tile = Tiles[cords.x, cords.y];
        Tile[] neighbors = Neighbors(cords);
        for (int t = 0; t < 4; t++)
        {
            if (!neighbors[t]) continue;
            if (tile.Substance.Material == neighbors[t].Substance.Material) canBePushed = true;
        }
        return canBePushed;
    }

    private void PushSubstance(Vector2Int cords, Substance substance)
    {
        Tile tile = Tiles[cords.x, cords.y];
        Tile[] neighbors = Neighbors(cords);
        for (int t = 0; t < 4; t++)
        {
            if (!neighbors[t]) continue;
            if (tile.Substance.Material == neighbors[t].Substance.Material)
            {
                neighbors[t].Substance.AddMass(tile.Substance.Mass);
                tile.Substance.Mass = substance.Mass;
                tile.Substance.Material = substance.Material;
                return;
            }
        }
    }

    #endregion
 }
