using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class Parallax2 : MonoBehaviour
{
    public Parallax2Camera parallaxCamera;
    List<Parallax2Layer> parallaxLayers = new List<Parallax2Layer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<Parallax2Camera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Parallax2Layer layer = transform.GetChild(i).GetComponent<Parallax2Layer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    void Move(float delta)
    {
        foreach (Parallax2Layer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
