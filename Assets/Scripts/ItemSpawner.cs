using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Reciclables")]
    public GameObject[] recyclePrefabs;


    [Header("Configuración")]
    public float spawnDistance = 30f;
    public float spawnHeight = 0.8f;
    public float spawnInterval = 0.8f;


    [Header("Escala")]
    public float collectibleScale = 4f;


    private Transform player;

    private float[] lanes = { -3f, 0f, 3f };


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(SpawnPattern), 0.5f, spawnInterval);
    }



    void SpawnPattern()
    {
        if (recyclePrefabs.Length == 0)
            return;


        // Solo patrones con varios objetos
        int pattern = Random.Range(1, 4);


        switch (pattern)
        {
            case 1:
                SpawnStraight();
                break;

            case 2:
                SpawnZigZag();
                break;

            case 3:
                SpawnGroup();
                break;
        }
    }



    // Línea de reciclables
    void SpawnStraight()
    {
        float lane = lanes[Random.Range(0, lanes.Length)];

        int amount = Random.Range(4, 7);


        for (int i = 0; i < amount; i++)
        {
            SpawnObject(
                lane,
                spawnDistance + (i * 2f)
            );
        }
    }



    // Patrón zigzag
    void SpawnZigZag()
    {
        int lane = Random.Range(0, 3);

        int amount = Random.Range(5, 8);


        for (int i = 0; i < amount; i++)
        {
            SpawnObject(
                lanes[lane],
                spawnDistance + (i * 2f)
            );


            lane += Random.Range(0, 2) == 0 ? -1 : 1;

            lane = Mathf.Clamp(lane, 0, 2);
        }
    }



    // Grupo en los 3 carriles
    void SpawnGroup()
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            SpawnObject(
                lanes[i],
                spawnDistance + Random.Range(0f, 2f)
            );
        }
    }



    void SpawnObject(float x, float zOffset)
    {
        GameObject prefab = recyclePrefabs[
            Random.Range(0, recyclePrefabs.Length)
        ];


        Vector3 position = new Vector3(
            x,
            spawnHeight,
            player.position.z + zOffset
        );


        Quaternion rotation = Quaternion.Euler(
            0,
            Random.Range(0f, 360f),
            0
        );


        GameObject obj = Instantiate(
            prefab,
            position,
            rotation
        );



        // Escala igual para todos
        obj.transform.localScale = Vector3.one * collectibleScale;



        // Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }



        // Collider para recoger
        Collider col = obj.GetComponent<Collider>();

        if (col != null)
        {
            col.isTrigger = true;
        }



        // Destruir cuando queda atrás
        DestroyBehindPlayer destroy =
            obj.GetComponent<DestroyBehindPlayer>();


        if (destroy == null)
        {
            destroy = obj.AddComponent<DestroyBehindPlayer>();
        }


        destroy.player = player;
        destroy.destroyDistance = 40f;



        // Flotar y girar
        if (obj.GetComponent<CollectibleEffect>() == null)
        {
            obj.AddComponent<CollectibleEffect>();
        }

        // Recoger reciclable
        if (obj.GetComponent<CollectibleItem>() == null)
        {
            obj.AddComponent<CollectibleItem>();
        }
    }
}