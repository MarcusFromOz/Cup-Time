using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] float speedHorizontal = 2.0f;
    [SerializeField] float speedVertical = 2.0f;

    float yaw = 0.0f;
    float pitch = 0.0f;


    void Update()
    {

        if (Input.GetMouseButton(1))
        {

            if (Input.GetMouseButton(0)) return;
            
            yaw += speedHorizontal * Input.GetAxis("Mouse X");
            pitch -= speedVertical * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    


}
