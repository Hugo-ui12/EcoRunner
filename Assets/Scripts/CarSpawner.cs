using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform player;

    public float spawnInterval = 4f;
    public float spawnDistance = 90f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCar(true);
            SpawnCar(false);

            timer = 0f;
        }
    }

    void SpawnCar(bool rightSide)
    {
        // Solo 40% de probabilidad de generar un carro
        if (Random.value > 0.4f)
            return;

        float x = rightSide ?
       Random.Range(8f, 9.5f) :
       Random.Range(-9.5f, -8f);
        float z = player.position.z + spawnDistance + Random.Range(-20f, 20f);

        GameObject car = Instantiate(
            carPrefab,
            new Vector3(x, 0f, z),
        Quaternion.Euler(0, 0, 0)
        );

        float randomScale = Random.Range(0.9f, 1.1f);
        car.transform.localScale *= randomScale;

        DestroyBehindPlayer destroyScript = car.AddComponent<DestroyBehindPlayer>();
        destroyScript.player = player;
        destroyScript.destroyDistance = 50f;
    }
}