using UnityEngine;
using TMPro;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas; // Canvas del menú principal
    public GameObject scoreCanvas;    // Canvas del juego
    public GameManager gameManager;   // Referencia al GameManager
    public TextMeshProUGUI infoText;  // Texto para mostrar puntajes

    private Coroutine infoCoroutine;

    void Start()
    {
        // Ocultar el InfoText al inicio
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
        }

        // Mostrar el menú principal al inicio
        ShowMenu();
    }

    public void StartGame()
    {
        Debug.Log("Iniciando juego...");

        // Ocultar el menú principal
        mainMenuCanvas.SetActive(false);

        // Mostrar el Canvas de puntaje
        scoreCanvas.SetActive(true);

        // Reiniciar el GameManager y activar jugador y Spawner
        if (gameManager != null)
        {
            gameManager.ResetGame();
        }
    }

    public void ShowMenu()
    {
        Debug.Log("Mostrando el menú principal...");

        // Detener cualquier mensaje de texto activo
        if (infoCoroutine != null)
        {
            StopCoroutine(infoCoroutine);
        }

        // Ocultar el InfoText
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
        }

        // Mostrar el menú principal
        mainMenuCanvas.SetActive(true);

        // Ocultar el Canvas de puntaje
        scoreCanvas.SetActive(false);

        // Detener el movimiento del jugador y Spawner
        if (gameManager != null)
        {
            if (gameManager.player != null) gameManager.player.canMove = false;
            if (gameManager.spawner != null) gameManager.spawner.canSpawn = false;
        }
    }

    public void ShowLastScore()
    {
        int lastScore = GameManager.GetLastScore();
        ShowInfo($"Última Puntuación: {lastScore}");
    }

    public void ShowBestScore()
    {
        int bestScore = GameManager.GetBestScore();
        ShowInfo($"Mejor Puntuación: {bestScore}");
    }

    private void ShowInfo(string message)
    {
        if (infoCoroutine != null)
        {
            StopCoroutine(infoCoroutine);
        }

        infoCoroutine = StartCoroutine(ShowInfoCoroutine(message));
    }

    private IEnumerator ShowInfoCoroutine(string message)
    {
        infoText.text = message;
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        infoText.gameObject.SetActive(false);
    }
}
