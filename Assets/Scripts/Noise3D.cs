using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise3D {

	public static float[, ,] GenerateNoiseMap(int size, float scale)
    {
        float[, ,] noiseMap = new float[size, size, size];

        if (scale <= 0)
            scale = 0.0001f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;
                    float sampleZ = z / scale;

                    float perlinValue = Perlin3D(sampleX, sampleY, sampleZ);
                    noiseMap[x,y,z] = perlinValue;
                }
            }
        }

        return noiseMap;
    }

    public static float GetNoise(int x, int y, int z, float scale)
    {
        float sampleX = x / scale;
        float sampleY = y / scale;
        float sampleZ = z / scale;

        float perlinValue = Perlin3D(sampleX, sampleY, sampleZ);
        return perlinValue;
    }

    private static float Perlin3D(float x, float y, float z)
    {
        float ab = Mathf.PerlinNoise(x,y);
        float bc = Mathf.PerlinNoise(x,y);
        float ac = Mathf.PerlinNoise(x,z);

        float ba = Mathf.PerlinNoise(y,x);
        float cb = Mathf.PerlinNoise(z,y);
        float ca = Mathf.PerlinNoise(z,x);

        float abc = ab + bc + ac + ba + cb + ca;
        return abc / 6f;
    }

}
