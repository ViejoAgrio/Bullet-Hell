using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public float changeDirectionTime = 2f; // Tiempo antes de cambiar de dirección
    public Vector2 xLimits = new Vector2(-10f, 10f); // Límites en el eje X
    public Vector2 zLimits = new Vector2(-10f, 10f); // Límites en el eje Z

    private Vector3 moveDirection; // Dirección de movimiento
    private float timer;

    void Start()
    {
        // Inicializar la dirección de movimiento aleatoria
        // ChangeDirection();
    }

    void Update()
    {
        // Mover al enemigo en la dirección actual
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Limitar el movimiento del enemigo dentro de los límites especificados
        float clampedX = Mathf.Clamp(transform.position.x, xLimits.x, xLimits.y);
        float clampedZ = Mathf.Clamp(transform.position.z, zLimits.x, zLimits.y);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

        // Verificar si el enemigo ha alcanzado los límites y cambiar de dirección si es necesario
        if (transform.position.x == xLimits.x || transform.position.x == xLimits.y ||
            transform.position.z == zLimits.x || transform.position.z == zLimits.y)
        {
            ChangeDirection();
        }

        // Temporizador para cambiar de dirección
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            ChangeDirection();
            timer = 0f;
        }
    }

    void ChangeDirection()
    {
        // Generar una nueva dirección de movimiento aleatoria
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0f, randomZ).normalized;
    }
}

