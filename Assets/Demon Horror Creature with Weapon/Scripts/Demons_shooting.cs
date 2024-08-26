using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil del enemigo
    public Transform firePoint; // Punto desde donde se disparará el proyectil
    public float projectileSpeed = 15f; // Velocidad del proyectil
    public float fireRate = 2f; // Intervalo entre disparos

    private Transform player; // Referencia al jugador
    private float nextFireTime = 0f;

    void Start()
    {
        // Encontrar al jugador (Priest) usando el tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verificar si el enemigo puede disparar (según el tiempo)
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil en la posición y rotación del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calcular la dirección hacia el jugador (Priest)
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Ajustar la rotación del proyectil para que mire hacia el jugador
        projectile.transform.forward = direction;

        // Añadir velocidad al proyectil en la dirección calculada
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = direction * projectileSpeed;

        // (Opcional) Destruir el proyectil después de cierto tiempo para evitar que se acumulen en la escena
        Destroy(projectile, 5f);
    }
}
