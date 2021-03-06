using UnityEngine;
using static GlobalVariables;

public class ScrapMaterial : MonoBehaviour
{
    LevelStatus levelStatus;

    [SerializeField] ScrapMaterialName scrapMaterialName;

    [SerializeField] float scrapMaterialHealthPoints;
    GameObject collectParticles;

    void Awake()
    {
        levelStatus = FindObjectOfType<LevelStatus>();
        collectParticles = transform.Find("CollectParticles").gameObject;
    }

    void Start()
    {
        collectParticles.SetActive(false);
    }

    // @Access from LaserShoot
    // Call when laser is pointing at this scrap material 
    public void CollectScrapMaterial(Vector2 position, float damage)
    {
        // Move collect particles to the point where laser touches the scrap material
        collectParticles.SetActive(true);
        collectParticles.transform.position = position;
        // Decrease health of scrap material
        scrapMaterialHealthPoints -= damage;
        // Check if it is time to destroy it
        if (scrapMaterialHealthPoints <= 0)
        {
            Destroy(gameObject);
            levelStatus.CollectScrapMaterial(scrapMaterialName);
        }
    }

    // @Access from LaserShoot
    // Call when laser has moved out of this scrap material to stop collect particles
    public void StopCollectScrapMaterial()
    {
        collectParticles.SetActive(false);
    }
}
