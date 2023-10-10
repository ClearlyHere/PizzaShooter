using UnityEngine;

namespace Course_Library.Scripts
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject[] animalPrefabs;
        private const float SpawnRange = 20f;
        private const float StartDelay = 2f;
        private const float SpawnInterval = 1f;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnRandomAnimal), StartDelay, SpawnInterval);
        }

        private void SpawnRandomAnimal()
        {
            var transform1 = transform;
            var position = transform1.position;
            var spawnPos = new Vector3(position.x + Random.Range(-SpawnRange, SpawnRange), position.y, position.z);
            var animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[animalIndex], spawnPos, transform.rotation);
        }
    }
}