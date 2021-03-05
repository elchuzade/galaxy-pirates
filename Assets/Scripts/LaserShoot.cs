using System.Collections;
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

    // Keep track of previous collect scrap material to compare to current to stop particle effects on previous
    GameObject lastCollectScrapMaterial;

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

    void FixedUpdate()
    {
        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance);

        Points.Clear();
        Points.Add(startPoint);

        // First hit point
        if (hitData)
        {
            if (hitData.collider.tag == "Barrier")
            {
                ReflectFurther(startPoint, hitData);
            }
            else if (hitData.collider.tag == "ScrapMaterial")
            {
                hitData.collider.GetComponent<ScrapMaterial>().CollectScrapMaterial(hitData.point, damage);
                Points.Add(hitData.point);

                // The tip of the laser is on the scrap material
                if (lastCollectScrapMaterial && lastCollectScrapMaterial.GetHashCode() != hitData.collider.gameObject.GetHashCode())
                {
                    // Stop the previous one and reassign it to the current one
                    lastCollectScrapMaterial.GetComponent<ScrapMaterial>().StopCollectScrapMaterial();
                    lastCollectScrapMaterial = hitData.collider.gameObject;
                }

                lastCollectScrapMaterial = hitData.collider.gameObject;
            } else if (hitData.collider.tag == "Breakable")
            {
                hitData.collider.GetComponent<ScrapMaterial>().CollectScrapMaterial(hitData.point, damage);
                Points.Add(hitData.point);

                hitData.collider.GetComponent<Breakable>().DamageBreakableObject(damage);
            }
        }

        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
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
            if (nextHitData.collider.tag == "Barrier")
            {
                ReflectFurther(hitData.point, nextHitData);
            }
            else if (nextHitData.collider.tag == "ScrapMaterial")
            {
                nextHitData.collider.GetComponent<ScrapMaterial>().CollectScrapMaterial(nextHitData.point, damage);
                Points.Add(nextHitData.point);

                // The tip of the laser is on the scrap material
                if (lastCollectScrapMaterial && lastCollectScrapMaterial.GetHashCode() != nextHitData.collider.gameObject.GetHashCode())
                {
                    // Stop the previous one and reassign it to the current one
                    lastCollectScrapMaterial.GetComponent<ScrapMaterial>().StopCollectScrapMaterial();
                    lastCollectScrapMaterial = nextHitData.collider.gameObject;
                }

                lastCollectScrapMaterial = nextHitData.collider.gameObject;
            }
            else if (hitData.collider.tag == "Breakable")
            {
                nextHitData.collider.GetComponent<ScrapMaterial>().CollectScrapMaterial(nextHitData.point, damage);
                Points.Add(nextHitData.point);

                hitData.collider.GetComponent<Breakable>().DamageBreakableObject(damage);
            }
        } else
        {
            inDirection = (hitData.point - origin).normalized;
            newDirection = Vector2.Reflect(inDirection, hitData.normal);

            Points.Add(hitData.point + newDirection * defaultRayDistance);

            // No more reflections, laser to the void
            if (lastCollectScrapMaterial)
            {
                lastCollectScrapMaterial.GetComponent<ScrapMaterial>().StopCollectScrapMaterial();
                lastCollectScrapMaterial = null;
            }
        }
    }
}
