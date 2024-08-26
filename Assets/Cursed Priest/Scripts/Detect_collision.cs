using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BossManager bossManager;

    void Start()
    {
        bossManager = FindObjectOfType<BossManager>(); // Encontrar el BossManager en la escena
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que chocamos tiene el tag "Enemy" (demonios)
        if (other.CompareTag("Enemy"))
        {
            // Destruir al enemigo
            Destroy(other.gameObject);

            // Destruir el proyectil
            Destroy(gameObject);

            // Notificar al BossManager que un demonio fue derrotado
            if (bossManager != null)
            {
                bossManager.DemonDefeated();
            }
        }
        // Verificar si el objeto impactado es el FinalBoss
        else if (other.CompareTag("FinalBoss"))
        {
            // Reducir la vida del FinalBoss
            BossManager finalBoss = other.GetComponent<BossManager>();
            if (finalBoss != null)
            {
                finalBoss.TakeHit();
            }

            // Destruir el proyectil
            Destroy(gameObject);
        }
    }
}
