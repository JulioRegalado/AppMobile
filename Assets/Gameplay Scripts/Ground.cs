using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Configuración")]
    public float speed = 8f;
    private float spriteWidth;

    void Start()
    {
        // Calcula automáticamente el ancho exacto del sprite para evitar huecos
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            spriteWidth = sr.bounds.size.x;
        }
        else
        {
            spriteWidth = 30f; // Valor de respaldo por si acaso
        }
    }

    void Update()
    {
        // Mueve el objeto hacia la izquierda constantemente
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Cuando la plataforma sale por completo de la pantalla, se reubica al frente sin dejar espacios
        if (transform.position.x <= -spriteWidth)
        {
            transform.position = new Vector3(transform.position.x + (spriteWidth * 2f), transform.position.y, transform.position.z);
        }
    }
}