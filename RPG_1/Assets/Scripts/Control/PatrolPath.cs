using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
       
            for (int i = 0; i < transform.childCount; i++)
            {
                
                int nextIndex = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(nextIndex));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount) return 0;
            return i+1;
        }

        //patrolpath
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}