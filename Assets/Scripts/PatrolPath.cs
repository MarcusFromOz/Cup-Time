using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{ 
    public class PatrolPath : MonoBehaviour
    {
        //** Variables
        const float gizmoWaypointRadius = 0.3f;


        //** Start and Update


        //** Public methods

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
        
        //** Private methods
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), gizmoWaypointRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

      
    }
}
