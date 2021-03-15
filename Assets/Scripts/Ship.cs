using UnityEngine;

public class Ship : MonoBehaviour
{
    Player player;

    [SerializeField] float damage;

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();
    }

    public float GetWidthFromDamage()
    {
        return damage / 2;
    }

    public float GetDamage()
    {
        return damage;
    }
}
