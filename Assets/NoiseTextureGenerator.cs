// Example C# script to generate a noise texture in Unity
using UnityEngine;

public class NoiseTextureGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20.0f;

    void Start()
    {
        Texture2D noiseTex = new Texture2D(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                noiseTex.SetPixel(x, y, new Color(sample, sample, sample));
            }
        }
        noiseTex.Apply();
        GetComponent<Renderer>().material.SetTexture("_NoiseTex", noiseTex);
    }
}

