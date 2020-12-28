using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        const float gizmoRadius = 0.3f;

        private void OnDrawGizmos() {
            for (int i = 0; i < transform.childCount; i++)
            {

                // int j = GetNextWayPoint(i);
                Gizmos.DrawSphere(GetWayPoint(i), gizmoRadius);
                Gizmos.DrawLine(GetWayPoint(i),GetWayPoint(GetNextWayPoint(i)));
            }
        }

        public  int GetNextWayPoint(int i)
        {
            if(i + 1 == transform.childCount) return 0;

            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
    
}
