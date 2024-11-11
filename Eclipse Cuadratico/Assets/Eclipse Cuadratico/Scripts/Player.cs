using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float Speed;

    public TextMeshProUGUI scoreText; // Referencia al texto del puntaje
    private int score = 0; // Puntaje del jugador

    public bool canMove = false; // Controla si el jugador puede moverse

    void Update()
    {
        if (!canMove) return; // Si no puede moverse, no hacer nada

        Vector3 movement = new Vector3();
        movement.z = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        rigidbody.AddForce(movement * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar colisiones con esferas dinámicas
        DynamicSphere dynamicSphere = collision.gameObject.GetComponent<DynamicSphere>();
        if (dynamicSphere != null)
        {
            // Actualizar puntaje dependiendo del estado de la esfera
            if (dynamicSphere.currentState == DynamicSphere.SphereState.Green)
            {
                score++;
            }
            else if (dynamicSphere.currentState == DynamicSphere.SphereState.Red)
            {
                score--;
            }

            UpdateScoreText();
            Destroy(collision.gameObject); // Eliminar esfera
        }
    }

    // Actualizar el texto del puntaje
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Obtener el puntaje actual
    public int GetScore()
    {
        return score;
    }

    // Reiniciar el puntaje
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
