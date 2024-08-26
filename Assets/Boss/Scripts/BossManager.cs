using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public int maxHits = 10; // Número máximo de hits que puede recibir el FinalBoss
    private int currentHits; // Contador de hits recibidos
    public GameObject finalBoss; // Referencia al FinalBoss en la escena
    public float moveDistance = 10f; // Distancia que se moverá en el eje Z
    public float bossSpeed = 5f; // Velocidad a la que se moverá el FinalBoss
    public int demonsToDefeat = 8; // Número de Demons que deben ser eliminados antes de que el FinalBoss se mueva
    public DemonTreeShooting[] demonTrees; // Referencias a los DemonTrees para detener su disparo

    private int demonsDefeated = 0; // Contador de Demons eliminados
    private bool bossMoving = false; // Indica si el FinalBoss ya está en movimiento
    private bool boosInPlace = false;
    private Vector3 targetPosition; // Posición a la que se moverá el FinalBoss
    public Transform shootPoint; // Punto desde el que se disparan los proyectiles
    private float shootTimer; // Temporizador para los disparos
    public float spiralSpeed = 10f;
    public float shootInterval = 1f;
    public float projectileInterval = 0.1f; // Intervalo entre cada proyectil en la espiral
    public GameObject projectilePrefab;


    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public float changeDirectionTime = 2f; // Tiempo antes de cambiar de dirección
    public Vector2 xLimits = new Vector2(-10f, 10f); // Límites en el eje X
    public Vector2 zLimits = new Vector2(-10f, 10f); // Límites en el eje Z

    private Vector3 moveDirection; // Dirección de movimiento
    private float timer;

    void Start()
    {
        // Calcular la posición objetivo en el eje Z
        targetPosition = finalBoss.transform.position + new Vector3(0, 0, moveDistance);
        currentHits = maxHits; // Inicializar la vida del FinalBoss
    }

    // Este método se llamará cada vez que un Demon sea eliminado
    public void DemonDefeated()
    {
        demonsDefeated++;

        // Verificar si se han derrotado suficientes Demons
        if (demonsDefeated >= demonsToDefeat && !bossMoving)
        {
            StartBossMovement();
            StopDemonTrees();
        }
    }

    void StartBossMovement()
    {
        bossMoving = true;
    }

    void StopDemonTrees()
    {
        // Detener el disparo de todos los DemonTrees
        foreach (var demonTree in demonTrees)
        {
            demonTree.enabled = false; // Desactiva el script de disparo en los DemonTrees
        }
    }

    IEnumerator ShootSpiral()
    {
        float angleStep = 360f / 10; // Dividir el círculo en 10 partes
        float angle = 0f;

        for (int i = 0; i < 10; i++)
        {
            float projectileDirXPosition = shootPoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 1f;
            float projectileDirZPosition = shootPoint.position.z + Mathf.Cos((angle * Mathf.PI) / 180) * 1f;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, shootPoint.position.y, projectileDirZPosition);
            Vector3 projectileMoveDirection = (projectileVector - shootPoint.position).normalized;

            GameObject tempProjectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();

            // Asignar la velocidad inicial en la dirección del movimiento
            rb.velocity = projectileMoveDirection * spiralSpeed;

            angle += angleStep;

            Destroy(tempProjectile, 5f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void TakeHit()
    {
        currentHits--;

        if (currentHits <= 0)
        {
            Die(); // FinalBoss muere cuando se queda sin vida
        }
    }
    void Die()
    {
        // Añadir animaciones, efectos o lógica adicional aquí
        Destroy(gameObject);
        Time.timeScale = 0f;
    }

    void ChangeDirection()
    {
        // Generar una nueva dirección de movimiento aleatoria
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0f, randomZ).normalized;
    }
    void Update()
    {
        if (bossMoving)
        {
            // Mover al FinalBoss hacia la posición objetivo en el eje Z
            float step = bossSpeed * Time.deltaTime;
            finalBoss.transform.position = Vector3.MoveTowards(finalBoss.transform.position, targetPosition, step);

            // Verificar si ha llegado a la posición objetivo
            if (Vector3.Distance(finalBoss.transform.position, targetPosition) < 0.1f)
            {
                bossMoving = false; // Detener el movimiento cuando llegue
                boosInPlace = true;
            }
        }
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval && boosInPlace == true)
        {
            StartCoroutine(ShootSpiral());
            shootTimer = 0f;
        }

        if (boosInPlace == true){
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
    }
}
