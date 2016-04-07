using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlanetBase {
    public string Name;
    public TerrainType[] regions;
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}