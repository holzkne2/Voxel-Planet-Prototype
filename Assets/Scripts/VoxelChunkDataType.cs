using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

[System.Serializable]
public class VoxelChunkDataType {

    public VoxelSystem m_system;
    public Vector3 m_postion;
    public int m_size;
    public VoxelDataType[, ,] m_voxels;

    public VoxelChunkDataType(VoxelSystem system, Vector3 position, int size)
    {
        m_system = system;
        m_postion = position;

        m_size = size;        
    }

    public void GenerateNew()
    {
        m_voxels = new VoxelDataType[m_size, m_size, m_size];
        float density;
        for (int x = 0; x < m_voxels.GetLength(0); x++)
        {
            for (int y = 0; y < m_voxels.GetLength(1); y++)
            {
                for (int z = 0; z < m_voxels.GetLength(2); z++)
                {
                    density = 100f - (new Vector3(x, y, z) + (m_postion * m_size) - Vector3.one * m_size * m_system.m_size / 2).magnitude;
                    density += Noise3D.GetNoise(x + (int)m_postion.x * m_size, y + (int)m_postion.y * m_size, z + (int)m_postion.z * m_size, m_system.m_noiseScale) * 10;
                    //density = Mathf.Clamp(density, -1, 1); //TODO: Remap, Estimate min, max
                    m_voxels[x, y, z] = new VoxelDataType(density);
                }
            }
        }
    }
}
