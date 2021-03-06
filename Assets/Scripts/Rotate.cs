using UnityEngine;
using static GlobalVariables;

public class Rotate : MonoBehaviour
{
    // Set angular speed with which to rotate
    [SerializeField] float rotateSpeed;
    // Choose a direction clockwise or counter clockwise
    [SerializeField] Direction direction;

    // rotate direction in integer form
    private int angularDirection;

    void Start()
    {
        // Change direction from enum to int
        if (direction == Direction.Clockwise)
        {
            angularDirection = -1;
        }
        else if (direction == Direction.CounterClockwise)
        {
            angularDirection = 1;
        }
    }

    void Update()
    {
        // Rotate along Z axis based on direction and speed
        transform.Rotate(new Vector3(0, 0, rotateSpeed * angularDirection * Time.deltaTime));
    }
}
