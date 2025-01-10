using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOpacity : MonoBehaviour
{
    public Material material; // �ҡ Material ������ Inspector
    public float opacity = 0.5f; // ��� Opacity (0.0 - 1.0)

    void Update()
    {
        if (material != null)
        {
            Color color = material.color;
            color.a = opacity; // ��駤�Ҥ��������
            material.color = color;
        }
    }
}