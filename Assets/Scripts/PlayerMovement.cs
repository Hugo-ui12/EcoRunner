using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;
    public float jumpForce = 7f;

    public float slideDuration = 0.8f;

    private Rigidbody rb;
    private int desiredLane = 1;
    private bool isGrounded = true;
    private bool isSliding = false;

    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            AudioManager.instance.PlayJump();

            isGrounded = false;
        }

        // Agacharse
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isSliding)
        {
            StartCoroutine(Slide());
        }

        // Aumentar velocidad según puntos
        if (ScoreManager.score >= 800)
            speed = 30f;
        else if (ScoreManager.score >= 500)
            speed = 27f;
        else if (ScoreManager.score >= 250)
            speed = 24f;
        else if (ScoreManager.score >= 100)
            speed = 20f;
        else
            speed = 16f;

        // Correr
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Cambiar carril
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            desiredLane--;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            desiredLane++;

        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

        Vector3 targetPosition = transform.position;

        if (desiredLane == 0)
            targetPosition.x = -laneDistance;

        if (desiredLane == 1)
            targetPosition.x = 0;

        if (desiredLane == 2)
            targetPosition.x = laneDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            laneChangeSpeed * Time.deltaTime
        );
    }

    IEnumerator Slide()
    {
        isSliding = true;

        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * 0.5f,
            originalScale.z
        );

        yield return new WaitForSeconds(slideDuration);

        transform.localScale = originalScale;

        isSliding = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}