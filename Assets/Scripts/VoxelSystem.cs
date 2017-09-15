﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class VoxelSystem : MonoBehaviour {

    public enum DisplayMode {SimpleMesh, NoDensity, Density};
    public DisplayMode m_displayMode;

    public int m_size = 10;
    public int m_chunkSize = 16;
    public float m_scale = 0.1f;

    private VoxelChunkDataType[, ,] m_chunks;
    private Dictionary<Vector3, VoxelChunk> m_loadedChunks;

    public Mesh m_simpleMesh;
    public Material m_material;

    //public Texture3D noise3D;

    public bool m_drawChunkOutline = false;
    public bool m_autoUpdate = false;

    int m_chunksDataLoaded;
    Queue<ThreadInfo<VoxelChunkDataType>> m_chunkDataInfoQueue = new Queue<ThreadInfo<VoxelChunkDataType>>();
    int m_meshGeneratedCount;
    public Queue<ThreadInfo<MeshData>> m_meshDataInfoQueue = new Queue<ThreadInfo<MeshData>>();

    System.DateTime m_startTime;

    public void Generate()
    {
        // Init
        m_startTime = new System.DateTime(System.DateTime.Now.Ticks);
        m_meshGeneratedCount = 0;
        m_chunksDataLoaded = 0;
        m_chunks = new VoxelChunkDataType[m_size, m_size, m_size];
        m_loadedChunks = new Dictionary<Vector3, VoxelChunk>();
        for (int x = 0; x < m_size; x++)
        {
            for (int y = 0; y < m_size; y++)
            {
                for (int z = 0; z < m_size; z++)
                {
                    GenerateNewChunk(RecievedNewChunk, x, y, z);
                    //m_chunks[x, y, z] = new VoxelChunkDataType(this, new Vector3(x,y,z), m_chunkSize);
                    //m_chunks[x, y, z].GenerateNew();
                }
            }
        }
        //Debug.Log("Generate Time: " + (System.DateTime.Now - starttime));

        //ThreadDrawAll();

        //noise3D = new Texture3D(m_size * m_chunkSize, m_size * m_chunkSize, m_size * m_chunkSize, TextureFormat.ARGB32, false);
        //noise3D.filterMode = FilterMode.Point;

        //DrawAllChunks();
    }

    public bool NeigborsLoaded(Vector3 pos)
    {
        if (!m_loadedChunks.ContainsKey(pos + Vector3.left))
            return false;
        if (!m_loadedChunks.ContainsKey(pos + Vector3.right))
            return false;
        if (!m_loadedChunks.ContainsKey(pos + Vector3.up))
            return false;
        if (!m_loadedChunks.ContainsKey(pos + Vector3.down))
            return false;
        if (!m_loadedChunks.ContainsKey(pos + Vector3.forward))
            return false;
        if (!m_loadedChunks.ContainsKey(pos + Vector3.back))
            return false;

        return true;
    }

    public void GenerateNewChunk(Action<VoxelChunkDataType> callback, int x, int y, int z)
    {
        ThreadStart threadStart = delegate
        {
            GenerateNewChunkThread(callback, x, y, z);
        };

        new Thread(threadStart).Start();
    }

    void GenerateNewChunkThread(Action<VoxelChunkDataType> callback, int x, int y, int z)
    {
        VoxelChunkDataType chunk = new VoxelChunkDataType(this, new Vector3(x, y, z), m_chunkSize);
        chunk.GenerateNew();
        lock (m_chunks)
        {
            m_chunks[x, y, z] = chunk;
        }
        lock (m_chunkDataInfoQueue)
        {
            m_chunkDataInfoQueue.Enqueue(new ThreadInfo<VoxelChunkDataType>(callback, chunk));
        }
    }

    void RecievedNewChunk(VoxelChunkDataType chunk)
    {
        m_chunksDataLoaded++;
        if (m_chunksDataLoaded == m_size * m_size * m_size)
            ThreadDrawAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Generate();

        if (m_chunkDataInfoQueue.Count > 0)
        {
            for (int i = 0; i < m_chunkDataInfoQueue.Count; i++)
            {
                ThreadInfo<VoxelChunkDataType> threadInfo = m_chunkDataInfoQueue.Dequeue();
                threadInfo.m_callback(threadInfo.m_parameter);
            }
        }

        if (m_meshDataInfoQueue.Count > 0)
        {
            for (int i = 0; i < m_meshDataInfoQueue.Count; i++)
            {
                ThreadInfo<MeshData> threadInfo = m_meshDataInfoQueue.Dequeue();
                threadInfo.m_callback(threadInfo.m_parameter);
                m_meshGeneratedCount++;
                if (m_meshGeneratedCount == m_size * m_size * m_size)
                    Debug.Log("Generate Time: " + (System.DateTime.Now - m_startTime));
            }
        }
    }

    public void ThreadDrawAll()
    {
        for (int x = 0; x < m_chunks.GetLength(0); x++)
        {
            for (int y = 0; y < m_chunks.GetLength(1); y++)
            {
                for (int z = 0; z < m_chunks.GetLength(2); z++)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    if (!m_loadedChunks.ContainsKey(pos))
                    {
                        GameObject gobject = new GameObject("Chunk: " + pos, typeof(VoxelChunk));
                        gobject.transform.position = pos * m_chunkSize;
                        gobject.transform.SetParent(transform);
                        VoxelChunk chunk = gobject.GetComponent<VoxelChunk>();
                        chunk.m_data = m_chunks[x, y, z];
                        m_loadedChunks.Add(pos, chunk);
                    }
                    m_loadedChunks[pos].GenerateMesh();
                }
            }
        }
    }

    public void DrawAllChunks()
    {
        //float[, ,] noise = Noise3D.GenerateNoiseMap(m_size * m_chunkSize, m_scale);
        //int n = m_size * m_chunkSize;
        //Color[] colors = new Color[n * n * n];
        //for (int x = 0; x < noise.GetLength(0); x++)
        //{
        //    for (int y = 0; y < noise.GetLength(1); y++)
        //    {
        //        for (int z = 0; z < noise.GetLength(2); z++)
        //        {
        //            float t = noise[x, y, z];
        //            colors[x + y * n + z * n * n] = new Color(t, t, t, 1f);
        //        }
        //    }
        //}
        //noise3D.SetPixels(colors);
        //noise3D.Apply();

        System.DateTime starttime = new System.DateTime(System.DateTime.Now.Ticks);

        if (m_displayMode == DisplayMode.SimpleMesh && Application.isPlaying)
        {
            for (int x = 0; x < m_chunks.GetLength(0); x++)
            {
                for (int y = 0; y < m_chunks.GetLength(1); y++)
                {
                    for (int z = 0; z < m_chunks.GetLength(2); z++)
                    {
                        Vector3 pos = new Vector3(x, y, z);
                        if (!m_loadedChunks.ContainsKey(pos))
                        {
                            GameObject gobject = new GameObject("Chunk: " + pos, typeof(VoxelChunk));
                            gobject.transform.position = pos * m_chunkSize;
                            gobject.transform.SetParent(transform);
                            VoxelChunk chunk = gobject.GetComponent<VoxelChunk>();
                            chunk.m_data = m_chunks[x, y, z];
                            m_loadedChunks.Add(pos, chunk);
                        }
                        m_loadedChunks[pos].UpdateSimpleMesh();
                    }
                }
            }
        }
        else if (m_displayMode == DisplayMode.NoDensity && Application.isPlaying)
        {
            for (int x = 0; x < m_chunks.GetLength(0); x++)
            {
                for (int y = 0; y < m_chunks.GetLength(1); y++)
                {
                    for (int z = 0; z < m_chunks.GetLength(2); z++)
                    {
                        Vector3 pos = new Vector3(x, y, z);
                        if (!m_loadedChunks.ContainsKey(pos))
                        {
                            GameObject gobject = new GameObject("Chunk: " + pos, typeof(VoxelChunk));
                            gobject.transform.position = pos * m_chunkSize;
                            gobject.transform.SetParent(transform);
                            VoxelChunk chunk = gobject.GetComponent<VoxelChunk>();
                            chunk.m_data = m_chunks[x, y, z];
                            m_loadedChunks.Add(pos, chunk);
                        }
                        m_loadedChunks[pos].DrawNoDensity();
                    }
                }
            }
        }
        else if (m_displayMode == DisplayMode.Density && Application.isPlaying)
        {
            for (int x = 0; x < m_chunks.GetLength(0); x++)
            {
                for (int y = 0; y < m_chunks.GetLength(1); y++)
                {
                    for (int z = 0; z < m_chunks.GetLength(2); z++)
                    {
                        Vector3 pos = new Vector3(x, y, z);
                        if (!m_loadedChunks.ContainsKey(pos))
                        {
                            GameObject gobject = new GameObject("Chunk: " + pos, typeof(VoxelChunk));
                            gobject.transform.position = pos * m_chunkSize;
                            gobject.transform.SetParent(transform);
                            VoxelChunk chunk = gobject.GetComponent<VoxelChunk>();
                            chunk.m_data = m_chunks[x, y, z];
                            m_loadedChunks.Add(pos, chunk);
                        }
                        m_loadedChunks[pos].DrawDensity();
                    }
                }
            }
        }

        Debug.Log("Draw All Time: " + (System.DateTime.Now - starttime));
    }

    public byte GetCase(int wx, int wy, int wz)
    {
        VoxelDataType voxel;
        byte r = 0;
        
        //V0
        if (GetVoxel(wx, wy, wz).m_density > 0)
            r |= 1;
        
        //V1
        voxel = GetVoxel(wx + 1, wy, wz);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 2;

        //V2
        voxel = GetVoxel(wx + 1, wy, wz + 1);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 4;

        //V3
        voxel = GetVoxel(wx, wy, wz + 1);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 8;

        //V4
        voxel = GetVoxel(wx, wy + 1, wz);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 16;

        //V5
        voxel = GetVoxel(wx + 1, wy + 1, wz);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 32;

        //V6
        voxel = GetVoxel(wx + 1, wy + 1, wz + 1);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 64;

        //V7
        voxel = GetVoxel(wx, wy + 1, wz + 1);
        if (voxel == null)
            return 0;
        else if (voxel.m_density > 0)
            r |= 128;

        return r;
    }

    public float[] GetValues(int wx, int wy, int wz)
    {
        VoxelDataType voxel;
        float[] r = new float[8];

        //V0
        r[0] = GetVoxel(wx, wy, wz).m_density;

        //V1
        voxel = GetVoxel(wx + 1, wy, wz);
        if (voxel == null)
            r[1] = 0;
        else
            r[1] = voxel.m_density;

        //V2
        voxel = GetVoxel(wx + 1, wy, wz + 1);
        if (voxel == null)
            r[2] = 0;
        else
            r[2] = voxel.m_density;

        //V3
        voxel = GetVoxel(wx, wy, wz + 1);
        if (voxel == null)
            r[3] = 0;
        else
            r[3] = voxel.m_density;

        //V4
        voxel = GetVoxel(wx, wy + 1, wz);
        if (voxel == null)
            r[4] = 0;
        else
            r[4] = voxel.m_density;

        //V5
        voxel = GetVoxel(wx + 1, wy + 1, wz);
        if (voxel == null)
            r[5] = 0;
        else
            r[5] = voxel.m_density;

        //V6
        voxel = GetVoxel(wx + 1, wy + 1, wz + 1);
        if (voxel == null)
            r[6] = 0;
        else
            r[6] = voxel.m_density;

        //V7
        voxel = GetVoxel(wx, wy + 1, wz + 1);
        if (voxel == null)
            r[7] = 0;
        else
            r[7] = voxel.m_density;

        return r;
    }

    public VoxelDataType GetVoxel(int wx, int wy, int wz)
    {
        int max = m_size * m_chunkSize;
        if (wx < 0 || wy < 0 || wz < 0 || wx >= max || wy >= max || wz >= max)
            return null;

        return m_chunks[wx / m_chunkSize, wy / m_chunkSize, wz / m_chunkSize].m_voxels[wx % m_chunkSize, wy % m_chunkSize, wz % m_chunkSize];
    }

}
public struct ThreadInfo<T>
{
    public readonly Action<T> m_callback;
    public readonly T m_parameter;

    public ThreadInfo (Action<T> callback, T parameter)
    {
        m_callback = callback;
        m_parameter = parameter;
    }
}