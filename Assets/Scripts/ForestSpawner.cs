
using UnityEngine;

public class ForestSpawner : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Prefabs de arboles")]
    public GameObject[] treePrefabs;

    [Header("Prefabs de plantas y arbustos")]
    public GameObject[] plantPrefabs;

    [Header("Prefabs de piedras")]
    public GameObject[] stonePrefabs;

    [Header("Prefabs de pasto y helechos")]
    public GameObject[] grassPrefabs;

    [Header("Generacion infinita")]
    public float spawnDistance = 120f;
    public float forestSpacing = 12f;

    [Header("Distancia lateral de los arboles")]
    public float treeMinX = 10f;
    public float treeMaxX = 18f;

    [Header("Zona de vegetacion")]
    public float forestMinX = 7f;
    public float forestMaxX = 30f;

    [Header("Zona de piedras")]
    public float stoneMinX = 11f;
    public float stoneMaxX = 17f;

    [Header("Cantidad por segmento y por lado")]
    public int treesPerSide = 4;
    public int plantsPerSide = 8;
    public int stonesPerSide = 3;
    public int grassPerSide = 30;

    [Header("Tamano de los objetos")]
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    [Header("Tamano de las piedras")]
    public float stoneMinScale = 0.25f;
    public float stoneMaxScale = 0.45f;

    [Header("Limpieza de objetos")]
    public float destroyDistance = 150f;

    private float nextSpawnZ;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError(
                "ForestSpawner: asigna el jugador en el Inspector."
            );

            enabled = false;
            return;
        }

        nextSpawnZ = player.position.z + 20f;

        // Generar los primeros segmentos.
        for (int i = 0; i < 15; i++)
        {
            SpawnForestBlock();
            nextSpawnZ += forestSpacing;
        }
    }

    void Update()
    {
        // Mantener vegetacion delante del jugador.
        while (player.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnForestBlock();
            nextSpawnZ += forestSpacing;
        }
    }

    void SpawnForestBlock()
    {
        SpawnSide(-1);
        SpawnSide(1);
    }

    void SpawnSide(int side)
    {
        // Arboles en la zona exterior.
        SpawnObjects(
            treePrefabs,
            treesPerSide,
            side,
            treeMinX,
            treeMaxX,
            false
        );

        // Arbustos y plantas entre los arboles.
        SpawnObjects(
            plantPrefabs,
            plantsPerSide,
            side,
            forestMinX,
            forestMaxX,
            false
        );

        // Piedras pequenas en una zona mas controlada.
        SpawnObjects(
            stonePrefabs,
            stonesPerSide,
            side,
            stoneMinX,
            stoneMaxX,
            true
        );

        // Pasto para cubrir los espacios libres.
        SpawnObjects(
            grassPrefabs,
            grassPerSide,
            side,
            forestMinX,
            forestMaxX,
            false
        );
    }

    void SpawnObjects(
        GameObject[] prefabs,
        int amount,
        int side,
        float minX,
        float maxX,
        bool isStone
    )
    {
        if (!HasPrefabs(prefabs) || amount <= 0)
            return;

        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = GetRandomPrefab(prefabs);

            if (prefab == null)
                continue;

            // Separacion lateral aleatoria.
            float x = side * Random.Range(minX, maxX);

            // Distribucion a lo largo del segmento.
            float segmentPart = forestSpacing / amount;
            float z = nextSpawnZ
                + (i * segmentPart)
                + Random.Range(0f, segmentPart);

            GameObject obj = Instantiate(
                prefab,
                new Vector3(x, 0f, z),
                Quaternion.Euler(
                    0f,
                    Random.Range(0f, 360f),
                    0f
                )
            );

            // Escala independiente para las piedras.
            float scale;

            if (isStone)
            {
                scale = Random.Range(
                    stoneMinScale,
                    stoneMaxScale
                );
            }
            else
            {
                scale = Random.Range(minScale, maxScale);
            }

            obj.transform.localScale *= scale;

            AddDestroyScript(obj);
        }
    }

    bool HasPrefabs(GameObject[] prefabs)
    {
        if (prefabs == null)
            return false;

        foreach (GameObject prefab in prefabs)
        {
            if (prefab != null)
                return true;
        }

        return false;
    }

    GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        if (prefabs == null)
            return null;

        int validCount = 0;

        foreach (GameObject prefab in prefabs)
        {
            if (prefab != null)
                validCount++;
        }

        if (validCount == 0)
            return null;

        int target = Random.Range(0, validCount);

        foreach (GameObject prefab in prefabs)
        {
            if (prefab == null)
                continue;

            if (target == 0)
                return prefab;

            target--;
        }

        return null;
    }

    void AddDestroyScript(GameObject obj)
    {
        DestroyBehindPlayer destroy =
            obj.GetComponent<DestroyBehindPlayer>();

        if (destroy == null)
            destroy = obj.AddComponent<DestroyBehindPlayer>();

        destroy.player = player;
        destroy.destroyDistance = destroyDistance;
    }
}
