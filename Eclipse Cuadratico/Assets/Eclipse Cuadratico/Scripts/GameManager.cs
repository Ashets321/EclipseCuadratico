using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;                // Referencia al jugador
    public Spawner spawner;              // Referencia al Spawner
    public TextMeshProUGUI gameOverText; // Texto de Game Over
    public MenuController menuController; // Referencia al MenuController
    public float gameDuration = 90f;     // Duraci�n del juego en segundos

    public float timeRemaining;          // Tiempo restante
    private bool isGameOver = false;     // Controla si el juego termin�
    private Vector3 initialPlayerPosition; // Posici�n inicial del jugador

    void Start()
    {
        // Guardar la posici�n inicial del jugador
        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
            player.canMove = false; // Desactivar movimiento al inicio
        }

        if (spawner != null)
        {
            spawner.canSpawn = false; // Desactivar generaci�n al inicio
        }

        // Mostrar el men� principal
        if (menuController != null)
        {
            menuController.ShowMenu();
        }
    }

    public void ResetGame()
    {
        timeRemaining = gameDuration;
        isGameOver = false;

        // Ocultar el texto de "Game Over"
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        // Reiniciar el puntaje del jugador
        if (player != null)
        {
            player.ResetScore();
        }

        // Permitir el movimiento del jugador y Spawner
        player.canMove = true;
        spawner.canSpawn = true;
    }

    void Update()
    {
        if (isGameOver || !player.canMove) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isGameOver = true;

        // Detener el movimiento del jugador y Spawner
        player.canMove = false;
        spawner.canSpawn = false;

        // Guardar el puntaje de la partida
        int lastScore = player.GetScore();
        PlayerPrefs.SetInt("LastScore", lastScore);

        // Actualizar el mejor puntaje si es necesario
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (lastScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", lastScore);
        }

        PlayerPrefs.Save();

        // Mostrar el texto de Game Over
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = $"Game Over\nScore: {lastScore}";
        }

        // Eliminar todos los objetos collectables
        RemoveAllCollectables();

        // Reiniciar la posici�n del jugador
        ResetPlayerPosition();

        // Mostrar el men� principal despu�s de 5 segundos
        Invoke("ShowMainMenu", 5f);
    }

    void ShowMainMenu()
    {
        if (menuController != null)
        {
            menuController.ShowMenu();
        }
    }

    void RemoveAllCollectables()
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            Destroy(collectable);
        }
    }

    void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.position = initialPlayerPosition;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero; // Detener cualquier movimiento
        }
    }

    // Devuelve la �ltima puntuaci�n guardada
    public static int GetLastScore()
    {
        return PlayerPrefs.GetInt("LastScore", 0); // Devuelve 0 si no hay datos
    }

    // Devuelve la mejor puntuaci�n guardada
    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0); // Devuelve 0 si no hay datos
    }
}
