using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distant;

    GameObject[] background;
    Material[] mat;
    float[] backspeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backspeed = new float[backCount];
        background = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            background[i] = transform.GetChild(i).gameObject;
            mat[i] = background[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    private void BackSpeedCalculate(int backCount)
    {
        for (int i = 0;i < backCount; i++)
        {
            if ((background[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = background[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount ; i++)
        {
            backspeed[i] = 1 - (background[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distant = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x,transform.position.y, 0);

        for (int i = 0; i < background.Length; i++)
        {
            float speed = backspeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex",new Vector2(distant, 0) * speed);
        }
    }
}
