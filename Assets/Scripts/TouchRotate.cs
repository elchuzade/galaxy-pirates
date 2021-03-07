using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchRotate : MonoBehaviour, IPointerDownHandler
{
    // To indicate player touching the barrier
    bool touching;

    // These are to make rotation always proportional to the moved angle of player fingers
    // Angle at which the rotation has started,
    float initWallAngle;
    // Coordinates at which the rotation has started.
    Vector2 initMouseDirection;

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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " Was Clicked.");
    }

    public void OnMouseDown()
    {
        // If player has touhed the wall and the ball is not launched and the hint is not used
        // Make the color of the wall a little darker
        GetComponent<SpriteRenderer>().color = new Color32(200, 200, 200, 255);

        touching = true;
        //if (PlayerPrefs.GetInt("Haptics") == 1)
        //{
        //    MMVibrationManager.Haptic(HapticTypes.LightImpact);
        //}
        // Save current direction of the touch of the player fingers to rotate in proportion to the finger movement
        initMouseDirection = GetDirection();
        // Set the angle of the wall at the time of touching to add to the rotation, to keep it proportional
        initWallAngle = transform.rotation.eulerAngles.z;
    }

    public void OnMouseUp()
    {
        // When the finger is released and ball is not launched and hint is not used

        // Make the wall color back to light
        GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);

        touching = false;
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
