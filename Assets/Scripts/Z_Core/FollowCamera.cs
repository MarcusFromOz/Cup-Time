using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        //** Variables
        [SerializeField] Transform target;


        //** Start and Update


        //** Public Methods


        //** Private Methods
        void LateUpdate()
        {
            transform.position = target.position;    
        }
    }
}
