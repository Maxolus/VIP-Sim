using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float pos = (cam.nearClipPlane + 10.0f);

        transform.position = cam.transform.position + cam.transform.forward * pos;
        transform.LookAt(cam.transform);
        float h = (Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f) * cam.aspect / 10.0f;
        float w = h * Screen.height / Screen.width;
        transform.localScale = new Vector3(h, w, 1);
    }
}
