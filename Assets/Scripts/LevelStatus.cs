using UnityEngine;
using static GlobalVariables;

public class LevelStatus : MonoBehaviour
{
    // Scoreboard gold scrap material icon
    GameObject goldIcon;
    GameObject silverIcon;
    GameObject bronzeIcon;
    GameObject brassIcon;
    GameObject titaniumIcon;
    int gold;
    int silver;
    int bronze;
    int brass;
    int titanium;

    void Awake()
    {
        // materials inside scoreboard inside canvas
        Transform materials = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Materials");
        goldIcon = materials.Find("Gold").gameObject;
        silverIcon = materials.Find("Silver").gameObject;
        bronzeIcon = materials.Find("Bronze").gameObject;
        brassIcon = materials.Find("Brass").gameObject;
        titaniumIcon = materials.Find("Titanium").gameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // @Access from scrap material script
    // Call when scrap material unit is being destroyed
    public void CollectScrapMaterial(ScrapMaterialName scrapMaterialName)
    {
        // Run the zoom animation on the appropriate scrap material
        // Increase the local appropriate material count
        switch (scrapMaterialName)
        {
            case ScrapMaterialName.Gold:
                goldIcon.GetComponent<TriggerAnimation>().Trigger();
                gold++;
                break;
            case ScrapMaterialName.Silver:
                silverIcon.GetComponent<TriggerAnimation>().Trigger();
                silver++;
                break;
            case ScrapMaterialName.Bronze:
                bronzeIcon.GetComponent<TriggerAnimation>().Trigger();
                bronze++;
                break;
            case ScrapMaterialName.Brass:
                brassIcon.GetComponent<TriggerAnimation>().Trigger();
                brass++;
                break;
            case ScrapMaterialName.Titanium:
                titaniumIcon.GetComponent<TriggerAnimation>().Trigger();
                titanium++;
                break;
            default:
                break;
        }
    }
}
