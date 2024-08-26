using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    public float slowMoveSpeed = 4f; // Velocidad reducida al presionar Shift
    public Vector2 xLimits = new Vector2(-10f, 10f); // Límites en el eje X
    public Vector2 zLimits = new Vector2(-10f, 10f); // Límites en el eje Z

    void Update()
    {
        // Capturar la entrada del usuario en los ejes horizontal y vertical
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Verificar si la tecla Shift izquierda está siendo presionada
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowMoveSpeed : moveSpeed;

        // Crear un vector de movimiento
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Aplicar el movimiento al personaje con la velocidad adecuada
        transform.position += move * currentSpeed * Time.deltaTime;

        // Limitar el movimiento del personaje dentro de los límites especificados
        float clampedX = Mathf.Clamp(transform.position.x, xLimits.x, xLimits.y);
        float clampedZ = Mathf.Clamp(transform.position.z, zLimits.x, zLimits.y);

        // Asignar la nueva posición con los límites aplicados
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}


