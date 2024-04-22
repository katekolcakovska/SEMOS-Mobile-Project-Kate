using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // What to spawn
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject collectiblePrefab;

    // Where to spawn
    [SerializeField] private Transform spawnOrigin;

    [SerializeField] private Transform obstacleParent;

    // Interval of spawning
    [SerializeField] private float spawnInterval;

    [SerializeField] private float spawnOffset;

    // Speed of obstacles
    [SerializeField] private float obstacleSpeed;

    [SerializeField] private float destroyThreshold;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval;

        SpawnObstacle();

        SpawnCollectible();

    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawnObstacle())
        {
            
            SpawnObstacle();

            SpawnCollectible();

        }

        MoveObstacle();

        DestroyObstacles();
        
    }

    private bool ShouldSpawnObstacle()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            
            timer = spawnInterval;
            return true;

        }

        return false;
    }

    private void MoveObstacle()
    {
        //movement code

        foreach(Transform child in obstacleParent)
        {
            // code for each child
            child.position -= Vector3.right * Time.deltaTime * obstacleSpeed;
        }
    }

    private void SpawnObstacle()
    {
        Vector2 randPos = spawnOrigin.position + new Vector3(0, Random.Range(-spawnOffset, spawnOffset), 0);

        Instantiate(obstaclePrefab, randPos, spawnOrigin.rotation, obstacleParent);
    }

    private void SpawnCollectible()
    {
        Vector2 randPos2 = spawnOrigin.position + new Vector3((spawnInterval / 1.65f), Random.Range(-spawnOffset, spawnOffset), 0);
        // ova so deleno na 1.65 da pobaram bolje nacin?
        Instantiate(collectiblePrefab, randPos2, spawnOrigin.rotation, obstacleParent);
    }

    private void DestroyObstacles()
    {
        foreach(Transform obstacle in obstacleParent)
        {
            if(obstacle.position.x <= destroyThreshold)
            {
                Destroy(obstacle.gameObject);
            }
        }
    }    
}
