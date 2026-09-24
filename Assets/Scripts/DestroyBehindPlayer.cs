using UnityEngine;

public class DestroyBehindPlayer : MonoBehaviour
{
    public Transform player;
    public float destroyDistance = 40f;

    void Update()
    {
        if (transform.position.z < player.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}