using UnityEngine;

public class MoveSpawner : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 7f;             // Velocidad a la que se desplaza el mundo
    public float resetPositionX = -30f;  // Límite izquierdo donde se reinicia
    public float startPositionX = 30f;   // Posición a la que vuelve a la derecha

    void Update()
    {
        // Mueve el objeto continuamente hacia la izquierda
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Si el objeto sale de la pantalla por la izquierda, regresa de forma fluida a la derecha
        if (transform.position.x < resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}