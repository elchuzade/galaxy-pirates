using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    // To indicate player touching the barrier
    bool touching;

    // These are to make rotation always proportional to the moved angle of player fingers
    // Angle at which the rotation has started,
    float initWallAngle;
    // Coordinates at which the rotation has started.
    Vector2 initMouseDirection;
    // Distance from center of wall that is touchable by player to rotate wall
    int touchRadius = 80;

    void Start()
    {

    }

    void Update()
    {
        if (touching)
        {
            // If player is touching the wall, rotate it based on his finger moves
            RotateWall();
        }

        if (Input.GetMouseButtonDown(0) && !touching)
        {
            if (Vector2.Distance(transform.position, Input.mousePosition) < touchRadius)
            {
                touching = true;
                transform.Find("WallReflect").GetComponent<SpriteRenderer>().color = new Color32(200, 200, 200, 255);
                initMouseDirection = GetDirection();
                initWallAngle = transform.rotation.eulerAngles.z;
            }
        } else if (Input.GetMouseButtonUp(0) && touching)
        {
            touching = false;
            transform.Find("WallReflect").GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
        }
    }

    private void RotateWall()
    {
        // When rotating the wall find direction of the finger at every frame
        Vector2 currentMouseDirection = GetDirection();

        // Find an angle between initial direction and current direction, to get the difference in rotation
        float angle = Vector2.SignedAngle(initMouseDirection, initMouseDirection + currentMouseDirection);

        // Rotate the wall based on that snapped angle
        transform.rotation = Quaternion.Euler(0, 0, angle + initWallAngle);
    }

    private Vector2 GetDirection()
    {
        // Get current mouse or finger tap coordinates
        Vector3 mouseScreen = Input.mousePosition;
        // Change them to world unit coordinates, since the wall is not on canvas but on the world space
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        // Return the resultant distance from object's position to mouse position
        // This is to determine the spot at which the finger tap has taken a place for rotation
        return new Vector2(
            mouse.x - transform.position.x,
            mouse.y - transform.position.y);
    }
}
