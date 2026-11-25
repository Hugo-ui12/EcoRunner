
using UnityEngine;

public class ForestGroundSpawner : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public GameObject groundPrefab;
    public Material dirtMaterial;

    [Header("Dimensiones del suelo")]
    public float groundWidth = 70f;
    public float pieceLength = 100f;
    public float groundY = -0.1f;

    [Header("Generacion infinita")]
    public float spawnAhead = 300f;
    public float keepBehind = 150f;

    private float nextSpawnZ;

    void Start()
    {
        if (player == null || groundPrefab == null)
        {
            Debug.LogError(
                "ForestGroundSpawner: asigna Player y Ground Prefab."
            );
            enabled = false;
            return;
        }

        nextSpawnZ = player.position.z;

        // Crear suelo inicial y mantenerlo delante del jugador.
        while (nextSpawnZ < player.position.z + spawnAhead)
        {
            SpawnGroundPiece();
        }
    }

    void Update()
    {
        while (nextSpawnZ < player.position.z + spawnAhead)
        {
            SpawnGroundPiece();
        }
    }

    void SpawnGroundPiece()
    {
        GameObject piece = Instantiate(
            groundPrefab,
            new Vector3(0f, groundY, nextSpawnZ),
            Quaternion.identity
        );

        piece.name = "ForestGroundPiece";

        // Hacer el segmento ancho y largo.
        piece.transform.localScale = new Vector3(
            groundWidth,
            0.2f,
            pieceLength
        );

        if (dirtMaterial != null)
        {
            Renderer rend = piece.GetComponent<Renderer>();

            if (rend != null)
                rend.material = dirtMaterial;
        }

        // Evitar que el suelo recién creado se destruya
        // mediante un DestroyBehindPlayer heredado del prefab.
        DestroyBehindPlayer oldDestroy =
            piece.GetComponent<DestroyBehindPlayer>();

        if (oldDestroy != null)
            Destroy(oldDestroy);

        // Limpiar los segmentos que quedan muy atrás.
        ForestGroundPieceCleanup cleanup =
            piece.GetComponent<ForestGroundPieceCleanup>();

        if (cleanup == null)
            cleanup = piece.AddComponent<ForestGroundPieceCleanup>();

        cleanup.player = player;
        cleanup.destroyDistance = keepBehind;

        nextSpawnZ += pieceLength;
    }
}

public class ForestGroundPieceCleanup : MonoBehaviour
{
    public Transform player;
    public float destroyDistance = 150f;

    void Update()
    {
        if (player == null)
            return;

        if (transform.position.z + 50f <
            player.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
