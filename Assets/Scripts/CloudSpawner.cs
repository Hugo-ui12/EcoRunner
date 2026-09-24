using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Transform player;

    [Header("Generación")]
    public float spawnDistance = 250f;
    public float spawnInterval = 35f;

    [Header("Altura")]
    public float minHeight = 30f;
    public float maxHeight = 50f;

    [Header("Distancia lateral")]
    public float minX = -60f;
    public float maxX = 60f;

    private float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = player.position.z + 50f;

        // Crear varias nubes desde el inicio
        for (int i = 0; i < 8; i++)
        {
            SpawnCloud();
            nextSpawnZ += spawnInterval;
        }
    }

    void Update()
    {
        while (player.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnCloud();
            nextSpawnZ += spawnInterval;
        }
    }

    void SpawnCloud()
    {
        if (cloudPrefab == null || player == null)
            return;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minHeight, maxHeight);

        Vector3 spawnPosition = new Vector3(
            x,
            y,
            nextSpawnZ
        );

        GameObject cloud = Instantiate(
            cloudPrefab,
            spawnPosition,
            Quaternion.identity
        );

        // Tamaño diferente para que no parezcan copias
        float randomScale = Random.Range(0.7f, 1.5f);

        cloud.transform.localScale =
            Vector3.one * randomScale;

        // Rotación aleatoria
        cloud.transform.rotation =
            Quaternion.Euler(
                0f,
                Random.Range(0f, 360f),
                0f
            );

        // Destruir cuando quede muy atrás
        DestroyBehindPlayer destroy =
            cloud.GetComponent<DestroyBehindPlayer>();

        if (destroy == null)
        {
            destroy =
                cloud.AddComponent<DestroyBehindPlayer>();
        }

        destroy.player = player;
        destroy.destroyDistance = 200f;
    }
}