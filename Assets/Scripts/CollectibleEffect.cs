using UnityEngine;

public class CollectibleEffect : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;
    public float rotateSpeed = 120f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(
            startPos.x,
            startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight,
            transform.position.z
        );

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
