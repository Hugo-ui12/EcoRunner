using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public GameObject[] cityPrefabs;
    public Transform player;

    [Header("Configuración")]
    public float spawnDistance = 120f;

    // Distancia entre cada fila de edificios
    public float citySpacing = 10f;

    // Distancia lateral de los edificios
    public float buildingMinX = 15f;
    public float buildingMaxX = 22f;

    private float nextSpawnZ;


    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistance;

        // Generar bastante ciudad desde el inicio
        for (int i = 0; i < 15; i++)
        {
            SpawnCityBlock();
            nextSpawnZ += citySpacing;
        }
    }


    void Update()
    {
        // Mantener ciudad suficiente adelante
        while (player.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnCityBlock();

            nextSpawnZ += citySpacing;
        }
    }


    void SpawnCityBlock()
    {
        SpawnSide(true);
        SpawnSide(false);
    }


    void SpawnSide(bool rightSide)
    {
        float baseX;

        if (rightSide)
        {
            baseX = Random.Range(buildingMinX, buildingMaxX);
        }
        else
        {
            baseX = Random.Range(-buildingMaxX, -buildingMinX);
        }


        float baseZ = nextSpawnZ;


        // =====================================================
        // EDIFICIO PRINCIPAL
        // =====================================================

        CreateRandomBuilding(
            baseX,
            baseZ
        );


        // =====================================================
        // SEGUNDO EDIFICIO
        // =====================================================

        CreateRandomBuilding(
            baseX + Random.Range(-2f, 2f),
            baseZ + 3.5f
        );


        // =====================================================
        // TERCER EDIFICIO
        // =====================================================

        CreateRandomBuilding(
            baseX + Random.Range(-2f, 2f),
            baseZ + 7f
        );


        // =====================================================
        // CUARTO EDIFICIO
        // =====================================================

        CreateRandomBuilding(
            baseX + Random.Range(-2f, 2f),
            baseZ + 10.5f
        );


        // =====================================================
        // QUINTO EDIFICIO
        // =====================================================

        CreateRandomBuilding(
            baseX + Random.Range(-2f, 2f),
            baseZ + 14f
        );


        // =====================================================
        // POSTE
        // =====================================================

        if (Random.value < 0.85f)
        {
            CreateCity(
                cityPrefabs.Length - 2,
                rightSide ? Random.Range(7f, 8f) : Random.Range(-8f, -7f),
                baseZ + Random.Range(0f, 14f),
                1f
            );
        }


        // =====================================================
        // BOTE
        // =====================================================

        if (Random.value < 0.65f)
        {
            CreateCity(
                cityPrefabs.Length - 1,
                rightSide ? 6f : -6f,
                baseZ + Random.Range(0f, 14f),
                2f
            );
        }
    }


    void CreateRandomBuilding(float x, float z)
    {
        int totalBuildings = cityPrefabs.Length - 2;


        if (totalBuildings <= 0)
            return;


        int randomBuilding = Random.Range(
            0,
            totalBuildings
        );


        GameObject obj = Instantiate(
            cityPrefabs[randomBuilding],
            new Vector3(x, 0f, z),
            Quaternion.identity
        );


        // Tamaño de los edificios
        float randomScale = Random.Range(
            0.65f,
            0.90f
        );


        obj.transform.localScale =
            Vector3.one * randomScale;


        // Girar los edificios hacia la carretera
        if (x > 0)
        {
            obj.transform.rotation =
                Quaternion.Euler(0, -90, 0);
        }
        else
        {
            obj.transform.rotation =
                Quaternion.Euler(0, 90, 0);
        }


        AddDestroyScript(obj);
    }


    void CreateCity(
        int index,
        float x,
        float z,
        float scale
    )
    {
        if (index < 0 ||
            index >= cityPrefabs.Length)
            return;


        GameObject obj = Instantiate(
            cityPrefabs[index],
            new Vector3(x, 0f, z),
            Quaternion.identity
        );


        obj.transform.localScale =
            Vector3.one * scale;


        AddDestroyScript(obj);
    }


    void AddDestroyScript(GameObject obj)
    {
        DestroyBehindPlayer destroy =
            obj.GetComponent<DestroyBehindPlayer>();


        if (destroy == null)
        {
            destroy =
                obj.AddComponent<DestroyBehindPlayer>();
        }


        destroy.player = player;

        // No eliminar los edificios demasiado pronto
        destroy.destroyDistance = 150f;
    }
}