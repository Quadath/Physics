using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTilemap : MonoBehaviour
{
    public int height, width;
    public static Tile[,] Tiles;

    public Tile tile;
    private void Start()
    {
        Tiles = new Tile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tiles[x, y] = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Tile>();
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) Tiles[x, y].Material = Physics.Materials[0];
                if (x == 12 && y == 12)
                {
                    Tiles[x, y].Material = Physics.Materials[2];
                    Tiles[x, y].Aggregation = Aggregation.Gas;
                    Tiles[x, y].mass = 625;
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
