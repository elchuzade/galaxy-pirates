using UnityEngine;

public class Absorber : MonoBehaviour
{
    GameObject damageParticles;

    void Awake()
    {
        // Find damage particle that is a child of the absorber object
        damageParticles = transform.Find("DamageParticles").gameObject;
    }

    void Start()
    {
        damageParticles.SetActive(false);
    }

    // @Access from LaserShoot
    // Call when laser is pointing at this breakable object
    public void DamageAbsorberObject(Vector2 position)
    {
        // Move collect particles to the point where laser touches the scrap material
        damageParticles.SetActive(true);

        // Black hole does not get damaged
        damageParticles.transform.position = position;
    }

    // @Access from LaserShoot
    // Stop hit particle when laser is not focusing this breakable object anymore
    public void StopDamageAbsorberObject()
    {
        damageParticles.SetActive(false);
    }
}
