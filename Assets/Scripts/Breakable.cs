using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using static GlobalVariables;

[RequireComponent(typeof(Animator))]
public class Breakable : MonoBehaviour
{
    LevelStatus levelStatus;

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
    [SerializeField] GameObject redKey;
    [SerializeField] GameObject purpleKey;
    [SerializeField] GameObject blueKey;

    // Base is used to multiply the reward by factor that effects the chest price
    [Header("Diamond reward")]
    [SerializeField] int diamondChance;
    [SerializeField] int diamondBaseMin;
    [SerializeField] int diamondBaseMax;
    [Header("Coin reward")]
    [SerializeField] int coinChance;
    [SerializeField] int coinBaseMin;
    [SerializeField] int coinBaseMax;
    [Header("Keys reward")]
    [SerializeField] int redKeyChance;
    [SerializeField] int redKeyBaseMin;
    [SerializeField] int redKeyBaseMax;
    [SerializeField] int purpleKeyChance;
    [SerializeField] int purpleKeyBaseMin;
    [SerializeField] int purpleKeyBaseMax;
    [SerializeField] int blueKeyChance;
    [SerializeField] int blueKeyBaseMin;
    [SerializeField] int blueKeyBaseMax;

    void Awake()
    {
        levelStatus = FindObjectOfType<LevelStatus>();
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
        // Make a logic of chance dropping items
        List<Rewards> allRewards = new List<Rewards>();
        // STEP 1: Pick one reward type
        allRewards = BuildAllRewards(allRewards);
        int rewardIndex = Random.Range(0, allRewards.Count);
        Rewards reward = allRewards[rewardIndex];
        // STEP 2: Pick how much of the reward to give
        int rewardCount = GetRewardCount(reward);
        // STEP 3: Instantiate all those rewards and move them to canvas
        DropAllRewards(reward, rewardCount);

        yield return new WaitForSeconds(2f);

        if (breakableObjectName == BreakableObjectName.EnemyShip)
        {
            levelStatus.PassLevel();
        }

        Destroy(gameObject);
    }

    private void DropAllRewards(Rewards reward, int rewardCount)
    {
        // Drop rewards in -5 to 5 pixels away from the breakable object
        switch (reward)
        {
            case Rewards.RedKey:
                for (int i = 0; i < rewardCount; i++)
                {
                    // Drop a diamond as a reward for destroying the breakable object
                    GameObject redKeyInstance = Instantiate(redKey, transform.position, Quaternion.identity);
                    redKeyInstance.GetComponent<Droppable>().InitializeDroppable(
                        new Vector2(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-20, 20)), 80);
                }
                break;
            case Rewards.PurpleKey:
                for (int i = 0; i < rewardCount; i++)
                {
                    // Drop a diamond as a reward for destroying the breakable object
                    GameObject purpleKeyInstance = Instantiate(purpleKey, transform.position, Quaternion.identity);
                    purpleKeyInstance.GetComponent<Droppable>().InitializeDroppable(
                        new Vector2(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-20, 20)), 80);
                }
                break;
            case Rewards.BlueKey:
                for (int i = 0; i < rewardCount; i++)
                {
                    // Drop a diamond as a reward for destroying the breakable object
                    GameObject blueKeyInstance = Instantiate(blueKey, transform.position, Quaternion.identity);
                    blueKeyInstance.GetComponent<Droppable>().InitializeDroppable(
                        new Vector2(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-20, 20)), 80);
                }
                break;
            case Rewards.Diamond:
                for (int i = 0; i < rewardCount; i++)
                {
                    // Drop a diamond as a reward for destroying the breakable object
                    GameObject diamondInstance = Instantiate(diamond, transform.position, Quaternion.identity);
                    diamondInstance.GetComponent<Droppable>().InitializeDroppable(
                        new Vector2(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-20, 20)), 80);
                }
                break;
            case Rewards.Coin:
            default:
                for (int i = 0; i < rewardCount; i++)
                {
                    // Drop a coin as a reward for destroying the breakable object
                    GameObject coinInstance = Instantiate(coin, transform.position, Quaternion.identity);
                    coinInstance.GetComponent<Droppable>().InitializeDroppable(
                        new Vector2(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-20, 20)), 80);
                }
                break;
        }
    }

    private List<Rewards> BuildAllRewards(List<Rewards> allRewards)
    {
        // Add all the possible rewards into the basket of all rewards
        for (int i = 0; i < diamondChance; i++)
        {
            allRewards.Add(Rewards.Diamond);
        }
        for (int i = 0; i < coinChance; i++)
        {
            allRewards.Add(Rewards.Coin);
        }
        for (int i = 0; i < redKeyChance; i++)
        {
            allRewards.Add(Rewards.RedKey);
        }
        for (int i = 0; i < purpleKeyChance; i++)
        {
            allRewards.Add(Rewards.PurpleKey);
        }
        for (int i = 0; i < blueKeyChance; i++)
        {
            allRewards.Add(Rewards.BlueKey);
        }
        return allRewards;
    }

    private int GetRewardCount(Rewards reward)
    {
        int result = 0;
        switch (reward)
        {
            case Rewards.Diamond:
                result = Random.Range(diamondBaseMin, diamondBaseMax);
                break;
            case Rewards.Coin:
                result = Random.Range(coinBaseMin, coinBaseMax);
                break;
            case Rewards.RedKey:
                result = Random.Range(redKeyBaseMin, redKeyBaseMax);
                break;
            case Rewards.PurpleKey:
                result = Random.Range(purpleKeyBaseMin, purpleKeyBaseMax);
                break;
            case Rewards.BlueKey:
                result = Random.Range(blueKeyBaseMin, blueKeyBaseMax);
                break;
        }
        return result;
    }
}
