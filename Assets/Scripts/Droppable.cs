using UnityEngine;
using static GlobalVariables;

public class Droppable : MonoBehaviour
{
    LevelStatus levelStatus;

    [SerializeField] DroppableItemName droppableItemName;

    // Position of idle place when it is dropped
    Vector3 dropToPosition;
    // Speed for item to go from its drop object to its idle drop place
    float dropSpeed;
    // Speed for item to go from its drop place to canvas
    float collectSpeed = 2000;
    // Position in canvas where item goes, either diamond or coin
    Vector3 collectToPosition;
    // To indicate that collectable is ready to be moved to canvas
    bool reachedDropPosition; 

    void Awake()
    {
        levelStatus = FindObjectOfType<LevelStatus>();
    }

    void Start()
    {
        SetCollectToPosition();
    }

    void Update()
    {
        if (reachedDropPosition)
        {
            MoveTowardsCollectPosition();
        }
        else
        {
            MoveTowardsDropPosition();
        }
    }

    private void SetCollectToPosition()
    {
        if (droppableItemName == DroppableItemName.Coin)
        {
            // A coin icon on Top Right to move dropped coins toward
            Vector3 canvasCollectPosition = GameObject.Find("Coins").transform.Find("Coin").gameObject.transform.position;

            collectToPosition = new Vector3(
                canvasCollectPosition.x,
                canvasCollectPosition.y,
                canvasCollectPosition.z);
        }
        else if (droppableItemName == DroppableItemName.Key)
        {
            // handle key stuff
        }
        else if (droppableItemName == DroppableItemName.Diamond)
        {
            // A diamond icon on Top Center to move dropped diamonds toward
            collectToPosition = GameObject.Find("Diamonds").transform.Find("Diamond").gameObject.transform.position;
        }
    }

    private void MoveTowardsDropPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, dropToPosition, dropSpeed * Time.deltaTime);

        if (transform.position == dropToPosition)
        {
            reachedDropPosition = true;
        }
    }

    private void MoveTowardsCollectPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, collectToPosition, collectSpeed * Time.deltaTime);

        if (transform.position == collectToPosition)
        {
            if (droppableItemName == DroppableItemName.Coin)
            {
                levelStatus.CollectCoins(1);
            } else if (droppableItemName == DroppableItemName.Diamond)
            {
                levelStatus.CollectDiamonds(1);
            }
            Destroy(gameObject);
        }
    }

    // @Access from Breakable Object when it is destroyed
    // Use to set positions when the item is first initialized
    // to = coordinates of idle drop position that is where to go before heading to canvas
    // speed = speed with which to go there. NOTE: This is not the speed to canvas
    public void InitializeDroppable(Vector2 to, float speed)
    {
        dropToPosition = to;
        dropSpeed = speed;
    }
}
