using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialPanel : MonoBehaviour
{
    private Tile Tile;
    
    public Text Name;
    public Text Mass;

    public void SetTile(Tile tile)
    {
        Tile = tile;
        Name.text = Tile.Substance.Material.Name;
        Mass.text = $"{Tile.Substance.Mass}kg";
    }

}
