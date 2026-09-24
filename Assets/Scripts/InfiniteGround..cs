using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public Transform player;

    private float groundLength = 1500f;

    // Ahora se mueve con muchísimo margen
    private float triggerDistance = 800f;

    void Update()
    {
        if (player == null)
            return;

        if (player.position.z > transform.position.z + triggerDistance)
        {
            transform.position += Vector3.forward * groundLength;
        }
    }
}