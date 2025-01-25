using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed = 1f;
    public float waypointThreshold = .1f;

    public Transform toMove;
    public List<Transform> waypoints;

    private int curWaypoint = 0;

    // Update is called once per frame
    void Update()
    {
        var waypoint = waypoints[curWaypoint];
        var targetPos = -waypoint.position;
        //targetPos.x = -targetPos.x;

        var curPos = Vector2.MoveTowards(toMove.transform.position, targetPos, speed * Time.deltaTime);

        toMove.transform.position = curPos;

        if (Vector2.Distance(curPos, targetPos) < waypointThreshold)
        {
            ++curWaypoint;

            if (curWaypoint >= waypoints.Count)
            {
                curWaypoint = waypoints.Count - 1;
            }
        }
    }
}

