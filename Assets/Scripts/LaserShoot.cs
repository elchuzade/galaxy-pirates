using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShoot : MonoBehaviour
{
    [SerializeField] Material laserRed;
    GameObject shootTip;

    const int Infinity = 9999;

    int maxReflections = 100;
    int currentReflections = 0;

    Vector2 startPoint;
    Vector2 direction;

    List<Vector3> Points;
    int defaultRayDistance = 10000;
    LineRenderer lr;

    float laserWidth;
    Ship ship;

    void Awake()
    {
        shootTip = transform.parent.Find("ShootTip").gameObject;
        ship = transform.parent.GetComponent<Ship>();
    }

    // Use this for initialization
    void Start()
    {
        startPoint = new Vector2(shootTip.transform.position.x, shootTip.transform.position.y);

        direction = new Vector2(startPoint.x - 1000, startPoint.y);

        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();

        laserWidth = ship.GetWidthFromDamage();
    }

    void FixedUpdate()
    {
        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance);

        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData)
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (direction - startPoint).normalized * Infinity);
        }
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(hitData.point + newDirection * defaultRayDistance);
        }
    }
}
