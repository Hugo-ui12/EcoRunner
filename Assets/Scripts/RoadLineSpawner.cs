using UnityEngine;

public class RoadLineSpawner : MonoBehaviour
{
    public GameObject linePrefab;
    public Transform player;

    public float spawnDistance = 80f;
    public float spacing = 8f;

    private float nextSpawnZ = 0f;

    void Start()
    {
        nextSpawnZ = player.position.z;
    }

    void Update()
    {
        while (nextSpawnZ < player.position.z + spawnDistance)
        {
            SpawnLine();
            nextSpawnZ += spacing;
        }
    }

    void SpawnLine()
    {
        GameObject line = Instantiate(
            linePrefab,
            new Vector3(0f, 0.02f, nextSpawnZ),
            Quaternion.identity
        );

        DestroyBehindPlayer destroy = line.AddComponent<DestroyBehindPlayer>();
        destroy.player = player;
        destroy.destroyDistance = 30f;
    }
}