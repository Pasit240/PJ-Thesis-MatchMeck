using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOpacity : MonoBehaviour
{
    public Material material; // ลาก Material มาใส่ใน Inspector
    public float opacity = 0.5f; // ค่า Opacity (0.0 - 1.0)

    void Update()
    {
        if (material != null)
        {
            Color color = material.color;
            color.a = opacity; // ตั้งค่าความโปร่งใส
            material.color = color;
        }
    }
}