
using UnityEngine;
using System.Collections;

public class PlayerRunner : MonoBehaviour
{
    [Header("Movimiento")]
    public Rigidbody2D rb;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask platformLayer;

    private bool isGrounded;
    private bool isOnPlatform;
    private bool isDead;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (isDead || groundCheck == null) return;

        // Detectar solamente el suelo principal
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
        );
        { }
    }

    // BOTON JUMP
    public void Jump()
    {
        if (isGrounded && !isDead)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }
    }

    // BOTON DOWN
    public void DropDown()
    {
        if (isOnPlatform && !isDead)
        {
            StartCoroutine(DisablePlatformCollision());
        }
    }

    private IEnumerator DisablePlatformCollision()
    {
        int platform = LayerMask.NameToLayer("Platform");

        if (platform == -1)
        {
            Debug.LogError("No existe la Layer Platform");
            yield break;
        }

        // Desactivar temporalmente la colision
        Physics2D.IgnoreLayerCollision(
            gameObject.layer,
            platform,
            true
        );

        yield return new WaitForSeconds(0.4f);

        // Volver a activar la colision
        Physics2D.IgnoreLayerCollision(
            gameObject.layer,
            platform,
            false
        );
    }

    // COLISION CON ENEMIGOS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TriggerGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        if (isDead) return;

        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }
        else
        {
            Debug.LogError("No se encontro GameManager");
        }
    }


}
