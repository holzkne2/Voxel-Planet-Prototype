using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VoxelDataType {

    public float m_density;

    public VoxelDataType(float density)
    {
        m_density = density;
    }
}
