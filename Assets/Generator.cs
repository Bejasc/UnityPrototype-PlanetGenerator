using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

    public AnimationCurve clip;
    public enum DrawMode { NoiseMap, ColorMap };
    public DrawMode drawMode;
    int dimension = 512;
    [Tooltip("Best < 1")]
    public int seed;
    public Vector2 offset;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistence;
    public float lacunarity;
    public Texture2D tex;

    public float PlanetRadius;

    public enum TypePlanets { Earth, GreyMoon };
    public TypePlanets PlanetType;

    PlanetType MyPlanet;


    public void GenerateMap()
    {

        MyPlanet = PlanetTypes.GetPlanetType(PlanetType);

        tex = new Texture2D(dimension,dimension,TextureFormat.RGBA32,false);
        tex.name = "Procedural Texture";

        GetComponent<Renderer>().material.mainTexture = tex;

        float[,] noiseMap = Noise.GenerateNoiseMap(dimension, dimension, seed, noiseScale,octaves,persistence,lacunarity,offset,PlanetRadius);

        Color[] mapColors = new Color[dimension*dimension];
        if(drawMode == DrawMode.ColorMap)
        {
            mapColors = DrawColourMap(noiseMap);
        }
        if(drawMode == DrawMode.NoiseMap)
        {
            mapColors = DrawNoiseMap(noiseMap);
        }


        //this block sets anything outside the circle as blank.


        mapColors = AtmosphereDarkener(mapColors);

        int cnt = tex.height / 2;
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {

                if (((i - cnt) * (i - cnt) + (j - cnt) * (j - cnt) > PlanetRadius * PlanetRadius))
                {
                    mapColors[i * dimension + j] = new Color(0, 0, 0, 0);
                }

            }
        }

        tex.SetPixels(mapColors);
        tex.Apply();



    }

    Color[] AtmosphereDarkener(Color[] colorMap)
    {
        Color[] AtmosphericColourMap = colorMap;
        int cnt = tex.height / 2;

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                Color c = colorMap[i * dimension + j];
                for(float v = 0.95f; v>0.75f;v-=0.05f)
                {
                    if ((i - cnt) * (i - cnt) + (j - cnt) * (j - cnt) > v * (PlanetRadius * PlanetRadius))
                    {
                        AtmosphericColourMap[i * dimension + j] = new Color(c.r *= v, c.g *= v, c.b *=v, 255);
                    }
                }




            }
        }

        return AtmosphericColourMap;
    }

    Color[] DrawNoiseMap(float[,] noiseMap)
    {
        Color[] noiseColors = new Color[dimension * dimension];

        int cnt = tex.height / 2;
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                if ((i - cnt) * (i - cnt) + (j - cnt) * (j - cnt) < PlanetRadius * PlanetRadius)
                {
                    noiseColors[i * dimension + j] = Color.Lerp(Color.black, Color.white, noiseMap[i, j]);
                }
            }
        }

        return noiseColors;
    }

    Color[] DrawColourMap(float[,] noiseMap)
    {
        Color[] colorMap = new Color[dimension * dimension];

        for (int y = 0; y < dimension; y++)
        {
            for (int x = 0; x < dimension; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < MyPlanet.regions.Length; i++)
                {
                    if (currentHeight <= MyPlanet.regions[i].height)
                    {
                        colorMap[y * dimension + x] = MyPlanet.regions[i].color;
                        break;
                    }
                }
            }
        }

        return colorMap;
    }


    void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;

        if (octaves < 0)
            octaves = 0;

        if (PlanetRadius < 64)
            PlanetRadius = 64;

        if (PlanetRadius > dimension / 2f)
            PlanetRadius = dimension / 2f;

    }

    public void SetUpPlanetTypes()
    {

    }

}


