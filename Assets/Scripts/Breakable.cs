using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public enum BreakableObjectName { Moon, Satellite, Meteor, Astronaut };
    [SerializeField] BreakableObjectName breakableObjectName;
    [SerializeField] float healthPoints;

    GameObject breakParticles;
    GameObject damageParticles;

    void Awake()
    {
        // Find break and damage particle that is a child of the breakable object
        breakParticles = transform.Find("BreakParticles").gameObject;
        damageParticles = transform.Find("DamageParticles").gameObject;
    }

    void Start()
    {
        breakParticles.SetActive(false);
        damageParticles.SetActive(false);
    }

    void Update()
    {
        
    }

    // @Access from LaserShoot
    // Call when laser is pointing at this breakable object
    public void DamageBreakableObject(Vector2 position, float damage)
    {
        // Move collect particles to the point where laser touches the scrap material
        damageParticles.SetActive(true);
        damageParticles.transform.position = position;

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

        } else
        {
            damageParticles.SetActive(false);
            breakParticles.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(DestroyBreakableObject());
        }
    }

    // @Access from LaserShoot
    // Stop hit particle when laser is not focusing this breakable object anymore
    public void StopDamageBreakableObject()
    {
        damageParticles.SetActive(false);
    }

    private IEnumerator DestroyBreakableObject()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
