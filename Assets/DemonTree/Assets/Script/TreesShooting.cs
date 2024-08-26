using UnityEngine;

public class DemonTreeShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán los proyectiles
    public int numberOfProjectiles = 8; // Número de proyectiles en el patrón
    public float projectileSpeed = 10f; // Velocidad de los proyectiles
    public float fireRate = 2f; // Tiempo entre cada patrón de disparos
    public float minAngle = 0f; // Ángulo mínimo de disparo
    public float maxAngle = 180f; // Ángulo máximo de disparo
    public float projectileLifetime = 5f; // Tiempo de vida del proyectil antes de ser destruido

    private float nextFireTime = 0f;

    void Update()
    {
        // Verificar si es hora de disparar el siguiente patrón
        if (Time.time >= nextFireTime)
        {
            FirePattern();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FirePattern()
    {
        float angleStep = (maxAngle - minAngle) / (numberOfProjectiles - 1);
        float angle = minAngle;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calcular la dirección de cada proyectil
            float projectileDirXPosition = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirZPosition = firePoint.position.z + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 projectileVector = new Vector3(projectileDirXPosition, firePoint.position.y, projectileDirZPosition);
            Vector3 projectileMoveDirection = (projectileVector - firePoint.position).normalized;

            // Instanciar el proyectil
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            proj.transform.forward = projectileMoveDirection;

            // Añadir velocidad al proyectil en la dirección calculada
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            rb.velocity = projectileMoveDirection * projectileSpeed;

            // Destruir el proyectil después de un tiempo
            Destroy(proj, projectileLifetime);

            angle += angleStep;
        }
    }
}
