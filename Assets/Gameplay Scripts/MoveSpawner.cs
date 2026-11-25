
using UnityEngine;

public class MoveSpawner : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 7f;

    [Header("Spawn Gap")]
    public float minGap = 5f;
    public float maxGap = 10f;

    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private Rigidbody2D body;
    private float objectWidth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        body = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.size.x;
        }
    }

    void Update()
    {
        if (body == null)
        {
            MoveAndWrap(Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (body != null)
        {
            MoveAndWrap(Time.fixedDeltaTime);
        }
    }

    private void MoveAndWrap(float deltaTime)
    {
        Vector3 movement = Vector3.left * speed * deltaTime;
        if (body != null)
        {
            body.MovePosition(body.position + (Vector2)movement);
        }
        else
        {
            transform.Translate(movement, Space.World);
        }

        if (mainCamera == null || spriteRenderer == null) return;

        float cameraLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, Mathf.Abs(mainCamera.transform.position.z - transform.position.z))
        ).x;

        float cameraRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 0f, Mathf.Abs(mainCamera.transform.position.z - transform.position.z))
        ).x;

        float objectRight = spriteRenderer.bounds.max.x;

        if (objectRight < cameraLeft)
        {
            float gap = Random.Range(minGap, maxGap);

            Vector3 spawnPosition = new Vector3(
                cameraRight + objectWidth / 2f + gap,
                transform.position.y,
                transform.position.z
            );
            if (body != null)
            {
                body.position = spawnPosition;
            }
            else
            {
                transform.position = spawnPosition;
            }
        }
    }
}
