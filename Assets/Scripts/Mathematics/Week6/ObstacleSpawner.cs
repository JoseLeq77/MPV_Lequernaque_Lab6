using UnityEngine;

    public class ObstacleSpawner : MonoBehaviour
    {
        [Header("Obstacle Prefabs")]
        [SerializeField] private GameObject[] obstaclePrefabs = new GameObject[3];

        [Header("Spawn Zone")]
        [SerializeField] private Vector2 spawnAreaMin = new Vector2(-5f, -2f);
        [SerializeField] private Vector2 spawnAreaMax = new Vector2(5f, 2f);

        [SerializeField] private float spawnInterval = 5f;

        private float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnObstacle();
                timer = 0f;
            }
        }

        private void SpawnObstacle()
        {
            int rand = Random.Range(1, 101); 
            int index = 0;

            if (rand <= 50)
            {
                index = 0;
            }
            else if (rand <= 80)
            {
                index = 1;
            }
            else
            {
                index = 2;
            }

            if (obstaclePrefabs[index] != null)
            {
                Vector3 spawnPos = new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y), transform.position.z);
                Instantiate(obstaclePrefabs[index], spawnPos, Quaternion.identity);
            }
        }
    }
