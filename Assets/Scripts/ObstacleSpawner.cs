using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public float spawnDistance = 60f;
    public float obstacleScale = 3f;
    public float spawnHeight = 0f;

    private Transform player;

    // Tiempo desde el último spawn
    private float spawnTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null || obstaclePrefabs.Length == 0)
            return;

        // Velocidad actual del jugador
        float speed = 16f;

        PlayerMovement playerMovement =
            player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            speed = playerMovement.speed;
        }

        // ==========================================
        // TIEMPO ENTRE OBSTÁCULOS
        // ==========================================

        float spawnInterval = 2f;

        // 16 - 19
        // EXACTAMENTE como estaba antes
        if (speed < 20f)
        {
            spawnInterval = 2f;
        }

        // 20 - 23
        // Empieza a aumentar la dificultad
        else if (speed < 24f)
        {
            spawnInterval = 1.7f;
        }

        // 24 - 26
        else if (speed < 27f)
        {
            spawnInterval = 1.4f;
        }

        // 27 - 30
        else
        {
            spawnInterval = 1.1f;
        }

        // ==========================================
        // TEMPORIZADOR
        // ==========================================

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            SpawnObstacle(speed);
        }
    }

    void SpawnObstacle(float speed)
    {
        if (obstaclePrefabs.Length == 0)
            return;

        // ==========================================
        // CARRILES
        // ==========================================

        float[] lanes = { -3f, 0f, 3f };

        // ==========================================
        // CANTIDAD DE OBSTÁCULOS
        // ==========================================

        int amountToSpawn = 1;

        // 16 - 23
        // Un solo obstáculo, igual que antes
        if (speed < 24f)
        {
            amountToSpawn = 1;
        }

        // 24 - 26
        // A veces aparecen 2
        else if (speed < 27f)
        {
            amountToSpawn =
                Random.value < 0.35f ? 2 : 1;
        }

        // 27 - 30
        // Más posibilidades de 2
        else
        {
            amountToSpawn =
                Random.value < 0.60f ? 2 : 1;
        }

        // ==========================================
        // ELEGIR CARRIL
        // ==========================================

        int firstLane =
            Random.Range(0, lanes.Length);

        for (int i = 0; i < amountToSpawn; i++)
        {
            int laneIndex;

            if (i == 0)
            {
                laneIndex = firstLane;
            }
            else
            {
                // Elegir uno de los otros dos carriles
                int secondLane;

                do
                {
                    secondLane =
                        Random.Range(0, lanes.Length);

                } while (secondLane == firstLane);

                laneIndex = secondLane;
            }

            float xPos = lanes[laneIndex];

            // ==========================================
            // OBSTÁCULO ALEATORIO
            // ==========================================

            GameObject prefab =
                obstaclePrefabs[
                    Random.Range(
                        0,
                        obstaclePrefabs.Length
                    )
                ];

            Vector3 spawnPosition =
                new Vector3(
                    xPos,
                    spawnHeight,
                    player.position.z + spawnDistance
                );

            GameObject obj =
                Instantiate(
                    prefab,
                    spawnPosition,
                    Quaternion.identity
                );

            // Mantener tu escala actual
            obj.transform.localScale *= obstacleScale;

            // ==========================================
            // COLLIDER
            // ==========================================

            if (obj.GetComponent<Collider>() == null)
            {
                obj.AddComponent<BoxCollider>();
            }

            // ==========================================
            // DESTRUCCIÓN
            // ==========================================

            DestroyBehindPlayer destroy =
                obj.GetComponent<DestroyBehindPlayer>();

            if (destroy == null)
            {
                destroy =
                    obj.AddComponent<DestroyBehindPlayer>();
            }

            destroy.player = player;
            destroy.destroyDistance = 40f;
        }
    }
}