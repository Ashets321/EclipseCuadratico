using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spherePrefab;  // Prefab de la esfera
    public Transform player;         // Referencia al jugador
    public float spawnRadius = 20f;  // Radio de generación de esferas
    public float spawnInterval = 3f; // Tiempo entre generaciones
    public float impulseForce = 5f;  // Fuerza de impulso para las esferas

    public bool canSpawn = false;    // Controla si se pueden generar esferas

    void Start()
    {
        InvokeRepeating("SpawnSphere", 1f, spawnInterval);
    }

    void SpawnSphere()
    {
        if (!canSpawn) return;

        Vector3 randomPosition = player.position + Random.insideUnitSphere * spawnRadius;
        randomPosition.y = player.position.y;

        GameObject newSphere = Instantiate(spherePrefab, randomPosition, Quaternion.identity);

        Rigidbody rb = newSphere.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            rb.AddForce(randomDirection * impulseForce, ForceMode.Impulse);
        }
    }
}
