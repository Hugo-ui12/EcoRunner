using UnityEngine;

public class SidewalkSpawner : MonoBehaviour
{
    public GameObject sidewalkPrefab;
    public Transform player;

    [Header("Configuración")]
    public float spawnDistance = 600f;
    public float pieceLength = 20f;

    private float lastSpawnZ;

    void Start()
    {
        lastSpawnZ = transform.position.z;

        // Generar una gran cantidad de sidewalk desde el inicio.
        // 30 piezas x 20 metros = 600 metros.
        for (int i = 0; i < 30; i++)
        {
            SpawnPiece();
        }
    }

    void Update()
    {
        // Mantener siempre 600 metros de sidewalk
        // preparados delante del jugador.
        while (player.position.z + spawnDistance > lastSpawnZ)
        {
            SpawnPiece();
        }
    }

    void SpawnPiece()
    {
        GameObject piece = Instantiate(
            sidewalkPrefab,
            new Vector3(
                transform.position.x,
                transform.position.y,
                lastSpawnZ
            ),
            Quaternion.identity
        );

        DestroyBehindPlayer destroyScript =
            piece.GetComponent<DestroyBehindPlayer>();

        if (destroyScript == null)
        {
            destroyScript = piece.AddComponent<DestroyBehindPlayer>();
        }

        destroyScript.player = player;

        // Mantener bastante sidewalk detrás
        // antes de eliminarlo.
        destroyScript.destroyDistance = 150f;

        lastSpawnZ += pieceLength;
    }
}