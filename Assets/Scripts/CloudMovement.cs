using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.Translate(
            Vector3.right * speed * Time.deltaTime,
            Space.World
        );
    }
}