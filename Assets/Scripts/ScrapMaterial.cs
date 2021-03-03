using System.Collections;
using UnityEngine;

public class ScrapMaterial : MonoBehaviour
{
    public enum ScrapMaterialName { Gold, Silver, Bronze, Titanium, Iron, Copper, Aluminum };
    [SerializeField] ScrapMaterialName scrapMaterialName;

    [SerializeField] float scrapMaterialLife;
    GameObject collectParticles;

    void Awake()
    {
        collectParticles = transform.Find("CollectParticles").gameObject;
    }

    void Start()
    {
        collectParticles.SetActive(false);
    }

    public void CollectScrapMaterial(Vector2 position, float damage)
    {
        collectParticles.SetActive(true);
        collectParticles.transform.position = position;
        scrapMaterialLife -= damage;

        if (scrapMaterialLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void StopCollectScrapMaterial()
    {
        collectParticles.SetActive(false);
    }
}
