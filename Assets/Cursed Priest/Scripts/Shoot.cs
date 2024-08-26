using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparará el proyectil
    public float projectileSpeed = 20f; // Velocidad del proyectil

    void Update()
    {
        // Detectar si el jugador presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil en la posición y rotación del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Añadir velocidad al proyectil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        // (Opcional) Destruir el proyectil después de cierto tiempo para evitar que se acumulen en la escena
        Destroy(projectile, 5f);
    }
}
