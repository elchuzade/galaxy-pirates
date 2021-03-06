using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShoot : MonoBehaviour
{
    [SerializeField] Material laserRed;
    GameObject shootTip;

    Vector2 startPoint;
    Vector2 direction;

    List<Vector3> Points;
    int defaultRayDistance = 10000;
    LineRenderer lr;

    float laserWidth;
    float damage;
    Ship ship;

    // List of objects that laser hits. To stop all other objects from particle effects
    // Run the tests for hitting objects only when laser has no place to reflect any more
    List<GameObject> laserHitObjects = new List<GameObject>();

    // List of objects that laser hit in the previous frame.
    // Compare the two lists and find the ones that are not in new list,
    // but in old list and stop their particle effects
    List<GameObject> previousLaserHitObjects = new List<GameObject>();

    void Awake()
    {
        shootTip = transform.parent.Find("ShootTip").gameObject;
        ship = transform.parent.GetComponent<Ship>();
    }

    // Use this for initialization
    void Start()
    {
        damage = ship.GetDamage();
        startPoint = new Vector2(shootTip.transform.position.x, shootTip.transform.position.y);

        direction = new Vector2(startPoint.x - 1000, startPoint.y);

        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();

        laserWidth = ship.GetWidthFromDamage();
    }

    void Update()
    {
        // Empty list of hitting objects, refill it all over again in each frame
        laserHitObjects.Clear();

        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance);

        Points.Clear();
        Points.Add(startPoint);

        // First hit point
        if (hitData)
        {
            // Add the game object that was hit by laser to list of objects
            laserHitObjects.Add(hitData.collider.gameObject);
            // The laser hit some collider which is not reflecting
            if (hitData.collider.tag == "Barrier")
            {
                ReflectFurther(startPoint, hitData);
            }
            else if (hitData.collider.tag == "ScrapMaterial")
            {
                hitData.collider.GetComponent<ScrapMaterial>().CollectScrapMaterial(hitData.point, damage);
                Points.Add(hitData.point);
            }
            else if (hitData.collider.tag == "Breakable")
            {
                hitData.collider.GetComponent<Breakable>().DamageBreakableObject(hitData.point, damage);
                Points.Add(hitData.point);
            }
            else if (hitData.collider.tag == "Absorber")
            {
                hitData.collider.GetComponent<Absorber>().DamageAbsorberObject(hitData.point);
                Points.Add(hitData.point);
            }
            else
            {
                Points.Add(hitData.point);
            }

            StopRedundantParticles();

            // Clear old list before repopulating it with laser new hit values
            previousLaserHitObjects.Clear();
            // Copy values from a new list of laser hit objects to old list of laser hit objects
            previousLaserHitObjects.AddRange(laserHitObjects);
        }

        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void StopRedundantParticles()
    {
        laserHitObjects.ForEach(hit =>
        {
            for (int i = 0; i < previousLaserHitObjects.Count; i++)
            {
                // Compare hash codes to see if the object exists in the previous list and new list
                // If so, remove it from the list of previous hits. This will leave only the items
                // that are not being targeted any more in the previous list. So stop particles in those
                if (hit.GetHashCode() == previousLaserHitObjects[i].GetHashCode())
                {
                    previousLaserHitObjects.Remove(previousLaserHitObjects[i]);
                }
            }
        });

        // When all the copy items are removed, stop particles from the objects that are left
        previousLaserHitObjects.ForEach(previousHit =>
        {
            if (previousHit != null)
            {
                StopLaserHitParticles(previousHit);
            }
        });
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        Points.Add(hitData.point);

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var nextHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);

        // Every other hit point
        if (nextHitData)
        {
            // Add the game object that was hit by laser to list of objects
            laserHitObjects.Add(nextHitData.collider.gameObject);
            // The laser hit some collider which is not reflecting
            if (nextHitData.collider.tag == "Barrier")
            {
                ReflectFurther(hitData.point, nextHitData);
            }
            else if (nextHitData.collider.tag == "ScrapMaterial")
            {
                nextHitData.collider.GetComponent<ScrapMaterial>().CollectScrapMaterial(nextHitData.point, damage);
                Points.Add(nextHitData.point);
            }
            else if (nextHitData.collider.tag == "Breakable")
            {
                nextHitData.collider.GetComponent<Breakable>().DamageBreakableObject(nextHitData.point, damage);
                Points.Add(nextHitData.point);
            }
            else if (nextHitData.collider.tag == "Absorber")
            {
                nextHitData.collider.GetComponent<Absorber>().DamageAbsorberObject(nextHitData.point);
                Points.Add(nextHitData.point);
            }
            else
            {
                Points.Add(nextHitData.point);
            }
        }
    }

    private void StopLaserHitParticles(GameObject hitObject)
    {
        switch (hitObject.tag)
        {
            // Moon, Satellite, Meteor, Astronaut 
            case "Breakable":
                hitObject.GetComponent<Breakable>().StopDamageBreakableObject();
                break;
            case "Absorber":
                hitObject.GetComponent<Absorber>().StopDamageAbsorberObject();
                break;
            case "ScrapMaterial":
                hitObject.GetComponent<ScrapMaterial>().StopCollectScrapMaterial();
                break;
            default:
                break;
        }
    }
}
