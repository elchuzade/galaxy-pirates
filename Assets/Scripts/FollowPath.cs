using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
    // Moving values
    [SerializeField] Transform[] waypoints;
    // Whether the object will repeat its waypoints over and over until it dies or disappear after once moving along its path
    [SerializeField] bool loop;
    // When should the item start moving along its path
    [SerializeField] float delay;
    // Speed with which the breakable object will move
    [SerializeField] float speed;
    int currentWaypointIndex;
    bool moving;

    // Palce to store all waypoints globally
    GameObject globalWaypoints;

    void Awake()
    {
        globalWaypoints = GameObject.Find("GlobalWaypoints");
    }

    void Start()
    {
        if (waypoints.Length > 0)
        {
            // Move waypoints outside of theobject so they dont move along with the object
            // Their coordinates must be global. Not local to the object
            transform.Find("Waypoints").SetParent(globalWaypoints.transform);
            StartCoroutine(MoveAlongPath(delay));
        }
    }

    void Update()
    {
        if (moving)
        {
            FollowWaypoints();
        }
    }

    private IEnumerator MoveAlongPath(float delay)
    {
        yield return new WaitForSeconds(delay);

        moving = true;
    }

    private void FollowWaypoints()
    {
        // Not to walk outside of waypoints array boundaries
        if (currentWaypointIndex < waypoints.Length)
        {
            transform.parent.transform.position = Vector2.MoveTowards(transform.parent.transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            if (transform.parent.transform.position == waypoints[currentWaypointIndex].position)
            {
                currentWaypointIndex++;
                // If this is the last waypoint check loop param and reset waypoints if needed
                if (currentWaypointIndex == waypoints.Length && loop)
                {
                    currentWaypointIndex = 0;
                    transform.parent.transform.position = waypoints[currentWaypointIndex].position;
                }
            }
        }
    }
}
