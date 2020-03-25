using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float h = 4f * Input.GetAxis("Mouse X");
        float v = 4f * Input.GetAxis("Mouse Y");

        this.transform.Rotate(v, h, 0);


    }
}
