using UnityEngine;

public class Move : MonoBehaviour
{
    // Speed with which the movement will occur
    [SerializeField] float speed;
    // To save position of object to return to
    private Vector3 initPosition;
    // To save goal position to move towards
    private Vector3 movePosition;
    // Indicator of movement, back - forth
    bool towardsMovePosition;

    void Start()
    {
        // Save initial position from parent object to return to
        initPosition = transform.parent.transform.position;
        // Save its position as a goal to move towards
        movePosition = transform.position;
        // Return the movePosition object to parent game object
        transform.position = initPosition;
    }

    void Update()
    {
        // If movePosition was moved by level designed to indicate a move action
        if (initPosition != movePosition)
        {
            // Movement is from initPositon to movePosition, with given speed
            if (towardsMovePosition)
            {
                transform.parent.transform.position = Vector3.MoveTowards(
                    transform.position,
                    movePosition,
                    speed * Time.deltaTime);
            }
            // Movement is from movePosition to initPosition, with given speed
            else
            {
                transform.parent.transform.position = Vector3.MoveTowards(
                    transform.position,
                    initPosition,
                    speed * Time.deltaTime);
            }
        }
        if (transform.parent.transform.position == movePosition)
        {
            // Reached movePosition, reverse the movement to go back
            towardsMovePosition = false;
        }
        else if (transform.parent.transform.position == initPosition)
        {
            // Reached initPosition, reverse the movement to go forth
            towardsMovePosition = true;
        }
    }
}
