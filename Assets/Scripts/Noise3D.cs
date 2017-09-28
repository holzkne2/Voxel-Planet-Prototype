using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise3D {

    [System.Obsolete("Use GetNoise() instead of creating map")]
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

    public static float GetTerrainDensity(int x, int z)
    {
        const float elevationScale = 120f;
        const float roughnessScale = 70f;
        const float detailScale = 20f;

        float seed = 1502.023f;

        float elevation = Mathf.PerlinNoise(x / elevationScale + seed, z / elevationScale + seed);
        float roughness = Mathf.PerlinNoise(x / roughnessScale + seed, z / roughnessScale + seed);
        float detail = Mathf.PerlinNoise(x / detailScale + seed, z / detailScale + seed) * 0.1f;
        return (elevation + (roughness * detail)) * 64 + 64;
    }

    public static float GetTerrainDensity(int x, int z, float scale, int octaves, float persistance, float lacunarity)
    {
        if (scale <= 0)
            scale = 0.0001f;

        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX = x / scale * frequency;
            float sampleZ = z / scale * frequency;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ);
            noiseHeight = perlinValue * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }
        return noiseHeight * 64 + 64;
    }

    public static float GetNoise(int x, int y, int z, float scale, int octaves, float persistance, float lacunarity)
    {
        if (scale <= 0)
            scale = 0.0001f;

        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX = x / scale * frequency;
            float sampleY = y / scale * frequency;
            float sampleZ = z / scale * frequency;

            float perlinValue = Perlin3D(sampleX, sampleY, sampleZ) * 2 - 1;
            noiseHeight = perlinValue * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }
        return noiseHeight;
    }

    public static float Perlin3D(float x, float y, float z)
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
