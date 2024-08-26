using UnityEngine;
using UnityEngine.UI; // Si deseas mostrar la salud en la UI
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public TextMeshProUGUI healthText; // Opcional: para mostrar la salud en la pantalla

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Salud: " + currentHealth.ToString() + "%";
        }
    }

    void Die()
    {
        // Implementa la lógica de lo que sucede cuando el jugador muere, como reiniciar el nivel o mostrar una pantalla de Game Over.
        Debug.Log("¡El jugador ha muerto!");
        healthText.text = "¡El jugador ha muerto!";
        Destroy(gameObject);
        Time.timeScale = 0f;
    }
}

