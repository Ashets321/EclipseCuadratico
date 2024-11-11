using UnityEngine;

public class GradientColorChanger : MonoBehaviour
{
    public Renderer playerRenderer; // Renderer del Player
    public float colorChangeSpeed = 0.2f; // Velocidad lenta del cambio de color

    private Material playerMaterial;
    private float colorTime = 0f;

    void Start()
    {
        // Obtener el material del Renderer
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;
        }
        else
        {
            Debug.LogError("No se ha asignado el Renderer del Player.");
        }
    }

    void Update()
    {
        if (playerMaterial != null)
        {
            // Incrementar el tiempo para el gradiente
            colorTime += Time.deltaTime * colorChangeSpeed;

            // Crear colores pastel más intensos mezclando con menos blanco
            Color color1 = Color.Lerp(Color.red, Color.white, 0.5f); // Rojo pastel más fuerte
            Color color2 = Color.Lerp(Color.blue, Color.white, 0.5f); // Azul pastel más fuerte
            Color color3 = Color.Lerp(Color.green, Color.white, 0.5f); // Verde pastel más fuerte

            // Transición suave entre los colores pastel
            Color gradientColor = Color.Lerp(
                Color.Lerp(color1, color2, Mathf.PingPong(colorTime, 1)),
                color3,
                Mathf.PingPong(colorTime * 0.5f, 1)
            );

            // Aplicar el color al material
            playerMaterial.color = gradientColor;
        }
    }
}
