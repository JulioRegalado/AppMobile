
using UnityEngine;

public class MoveSpawner : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 7f;

    [Header("Spawn Settings")]
    public float resetPositionX = -30f;
    public float startPositionX = 30f;

    [Header("Object Settings")]
    public float objectWidth = 5f;

    void Update()
    {
        // Mover continuamente hacia la izquierda
        transform.Translate(
            Vector3.left * speed * Time.deltaTime,
            Space.World
        );

        // Calcular cuando el objeto sale completamente de pantalla
        float objectRightEdge = transform.position.x + (objectWidth / 2f);

        if (objectRightEdge < resetPositionX)
        {
            // Reaparecer fuera de la pantalla por la derecha
            transform.position = new Vector3(
                startPositionX,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
