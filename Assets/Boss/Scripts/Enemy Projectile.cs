using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1; // Daño que hace el proyectil al jugador

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisionamos es el jugador
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destruir el proyectil después de hacer daño
            Destroy(gameObject);
        }
    }
}

