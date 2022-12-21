using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    public static Material[] Materials = new[]
    {
        new Material("???", 0),
        new Material("Vacuum", 0),
        new Material("Hydrogen", 0.083f),
        new Material("Oxygen", 16)
    };
}
public enum Aggregation
{
    Solid,
    Liquid,
    Gas
};

