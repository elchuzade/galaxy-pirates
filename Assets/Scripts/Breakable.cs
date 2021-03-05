using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public enum BreakableObjectName { Moon, Satellite, Meteor, Astronaut };
    [SerializeField] BreakableObjectName breakableObjectName;
    [SerializeField] float healthPoints;

    GameObject breakParticles;

    void Awake()
    {
        // Find break particle that is a child of the breakable object
        breakParticles = transform.Find("BreakParticles").gameObject;
    }

    void Start()
    {
        breakParticles.SetActive(false);
    }

    void Update()
    {
        
    }

    // @Access from LaserShoot
    // Call when laser is pointing at this breakable object
    public void DamageBreakableObject(float damage)
    {
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
            breakParticles.SetActive(true);
            StartCoroutine(DestroyBreakableObject());
        }
    }

    private IEnumerator DestroyBreakableObject()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
