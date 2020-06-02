using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Core
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] float speedHorizontal = 2.0f;
        [SerializeField] float speedVertical = 2.0f;
        [SerializeField] float zoomAmount = 5.0f;

        float yaw = 0.0f;
        float pitch = 0.0f;
        CinemachineVirtualCamera virtualCamera;

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                //prevent moving and rotating at once
                if (Input.GetMouseButton(0)) return;


                yaw += speedHorizontal * Input.GetAxis("Mouse X");

                pitch -= speedVertical * Input.GetAxis("Mouse Y");
                pitch = Mathf.Clamp(pitch, 10, 30);

                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }

            //Zoom on scrolling middle mouse button - removed for now

            if (Input.GetMouseButton(2))
            {
                //want to zoom in and out with mousewheel
                //print("***middle mouse button down***");
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //print("***zoom out***");
                //virtualCamera.m_Lens.FieldOfView += zoomAmount;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //print("***zoom in***");
                //virtualCamera.m_Lens.FieldOfView -= zoomAmount;
            }
        }
    }
}
