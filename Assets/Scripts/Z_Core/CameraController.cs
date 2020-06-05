using UnityEngine;
using Cinemachine;
using RPG.Control;

namespace RPG.Core
{

    public class CameraController : MonoBehaviour
    {
        [SerializeField] GameObject freeLookCamera;
        CinemachineFreeLook freeLookComponent;
        PlayerController playerControllerScript;

        // Start is called before the first frame update
        private void Awake()
        {
            freeLookComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
            playerControllerScript = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                //if (playerControllerScript.isDraggingUI) return;

                freeLookComponent.m_XAxis.m_MaxSpeed = 500;
            }

            if (Input.GetMouseButtonUp(1))
            {
                freeLookComponent.m_XAxis.m_MaxSpeed = 0;
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                freeLookComponent.m_YAxis.m_MaxSpeed = 10;
            }

        }
    }
}

