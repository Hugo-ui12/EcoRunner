
using UnityEngine;

public class ForestEdgeSpawner : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public GameObject edgePrefab;

    [Header("Configuracion")]
    public float groundWidth = 70f;
    public float edgeWidth = 2f;
    public float pieceLength = 100f;
    public float edgeY = -0.02f;
    public float spawnAhead = 300f;
    public float keepBehind = 150f;

    private float nextSpawnZ;

    void Start()
    {
        if (player == null || edgePrefab == null)
        {
            Debug.LogError(
                "ForestEdgeSpawner: asigna Player y Edge Prefab."
            );
            enabled = false;
            return;
        }

        nextSpawnZ = player.position.z;

        while (nextSpawnZ < player.position.z + spawnAhead)
        {
            SpawnEdges();
        }
    }

    void Update()
    {
        while (nextSpawnZ < player.position.z + spawnAhead)
        {
            SpawnEdges();
        }
    }

    void SpawnEdges()
    {
        float sideX = groundWidth / 2f - edgeWidth / 2f;

        CreateEdge(-sideX);
        CreateEdge(sideX);

        nextSpawnZ += pieceLength;
    }

    void CreateEdge(float x)
    {
        GameObject edge = Instantiate(
            edgePrefab,
            new Vector3(x, edgeY, nextSpawnZ + pieceLength / 2f),
            Quaternion.identity
        );

        edge.name = "ForestEdge";

        edge.transform.localScale = new Vector3(
            edgeWidth,
            0.05f,
            pieceLength
        );

        ForestEdgeCleanup cleanup =
            edge.GetComponent<ForestEdgeCleanup>();

        if (cleanup == null)
        {
            cleanup = edge.AddComponent<ForestEdgeCleanup>();
        }

        cleanup.player = player;
        cleanup.keepBehind = keepBehind;
    }
}

public class ForestEdgeCleanup : MonoBehaviour
{
    public Transform player;
    public float keepBehind = 150f;

    void Update()
    {
        if (player == null) return;

        if (transform.position.z < player.position.z - keepBehind)
        {
            Destroy(gameObject);
        }
    }
}
