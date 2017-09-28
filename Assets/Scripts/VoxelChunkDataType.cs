using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

[System.Serializable]
public class VoxelChunkDataType {

    public VoxelSystem m_system;
    public Vector3 m_postion;
    public VoxelDataType[, ,] m_voxels;
    public int m_width { get { return m_system.m_chunkWidth; } }
    public int m_height { get { return m_system.m_chunkHeight; } }

    public VoxelChunkDataType(VoxelSystem system, Vector3 position)
    {
        m_system = system;
        m_postion = position;    
    }

    public void GenerateNew()
    {
        m_voxels = new VoxelDataType[m_width, m_height, m_width];
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                for (int z = 0; z < m_width; z++)
                {
                    int wsx = x + (int)m_postion.x * m_width;
                    int wsy = y + (int)m_postion.y * m_height;
                    int wsz = z + (int)m_postion.z * m_width;

                    float density = -wsy + 64;
                    density += Mathf.PerlinNoise(wsx / 75f, wsz / 75f) * 32f;
                    density += Mathf.PerlinNoise(wsx / 50f, wsz / 50f) * 12f;
                    density += Mathf.PerlinNoise(wsx / 10f, wsz / 10f) * 3f;

                    m_voxels[x, y, z] = new VoxelDataType(density);
                }
            }
        }
    }
}
