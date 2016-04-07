using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

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

    public float r;

    public void DrawMap()
    {
        tex = new Texture2D(dimension,dimension,TextureFormat.RGBA32,false);
        tex.name = "Procedural Texture";

        GetComponent<Renderer>().material.mainTexture = tex;

        float[,] noiseMap = Noise.GenerateNoiseMap(dimension, dimension, seed, noiseScale,octaves,persistence,lacunarity,offset,r);
        Color[] colourMap = new Color[dimension * dimension];

        int cnt = tex.height / 2;
        for(int i = 0; i<tex.width;i++)
        {
            //int js = (int)Mathf.Sqrt(r * 2 - i * 2f);
            for(int j = 0; j<tex.height;j++)
            {

                if ((i - cnt) * (i - cnt) + (j - cnt) * (j - cnt) < r * r)
                {
                    colourMap[i * dimension + j] = Color.Lerp(Color.black, Color.white, noiseMap[i, j]);
                }
                else
                {
                    colourMap[i * dimension + j] = new Color(0, 0, 0, 0);

                    //tex.SetPixel(i,j,new Color(0,0,0,0));
                }
            }
        }
        tex.SetPixels(colourMap);
        tex.Apply();


    }

    void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;

        if (octaves < 0)
            octaves = 0;

        if (r < 64)
            r = 64;

        if (r > dimension / 2f)
            r = dimension / 2f;

    }

}
