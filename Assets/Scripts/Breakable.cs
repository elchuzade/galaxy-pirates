using System.Collections;
using UnityEngine;
using static GlobalVariables;

[RequireComponent(typeof(Animator))]
public class Breakable : MonoBehaviour
{
    
    [SerializeField] BreakableObjectName breakableObjectName;
    [SerializeField] float healthPoints;

    // When the breakable object is destroyed
    GameObject breakParticles;
    // When the laser is hitting the breakable object
    GameObject damageParticles;
    Animator damageAnimator;

    // Drop items to reward player for destroying the breakable object
    [SerializeField] GameObject coin;
    [SerializeField] GameObject diamond;

    void Awake()
    {
        // Find break and damage particle that is a child of the breakable object
        breakParticles = transform.Find("BreakParticles").gameObject;
        damageParticles = transform.Find("DamageParticles").gameObject;
        damageAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        breakParticles.SetActive(false);
        damageParticles.SetActive(false);
        damageAnimator.enabled = false;
    }

    // @Access from LaserShoot
    // Call when laser is pointing at this breakable object
    public void DamageBreakableObject(Vector2 position, float damage)
    {
        // Move collect particles to the point where laser touches the scrap material
        damageParticles.SetActive(true);

        // Black hole does not get damaged
        damageParticles.transform.position = position;

        damageAnimator.enabled = true;

        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            AttemptDestroyBreakableObject();
        }
    }

    private void AttemptDestroyBreakableObject()
    {
        if (breakableObjectName == BreakableObjectName.Meteor)
        {
            // Meteor has 1 circle collider
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else if (breakableObjectName == BreakableObjectName.Moon)
        {
            // Moon has 1 circle collider
            GetComponent<CircleCollider2D>().enabled = false;
        } else if (breakableObjectName == BreakableObjectName.Satellite)
        {
            // Satellite has 2 box colliders
            BoxCollider2D[] satelliteColliders = GetComponents<BoxCollider2D>();
            satelliteColliders[0].enabled = false;
            satelliteColliders[1].enabled = false;
        } else if (breakableObjectName == BreakableObjectName.Astronaut)
        {
            // Astronaut has 1 capsule collider
            GetComponent<CapsuleCollider2D>().enabled = false;
        } else if (breakableObjectName == BreakableObjectName.EnemyShip)
        {
            // Enemy Ship has 1 capsule collider
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        damageParticles.SetActive(false);
        breakParticles.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(DestroyBreakableObject());
    }

    // @Access from LaserShoot
    // Stop hit particle when laser is not focusing this breakable object anymore
    public void StopDamageBreakableObject()
    {
        damageAnimator.enabled = false;
        damageParticles.SetActive(false);
    }

    private IEnumerator DestroyBreakableObject()
    {
        // Drop a diamond as a reward for destroying the breakable object
        GameObject diamondInstance = Instantiate(diamond, transform.position, Quaternion.identity);
        diamondInstance.GetComponent<Droppable>().InitializeDroppable(
            new Vector2(transform.position.x, transform.position.y - 1), 2);

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
