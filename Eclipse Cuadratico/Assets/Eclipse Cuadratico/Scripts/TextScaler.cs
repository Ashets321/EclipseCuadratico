using UnityEngine;
using TMPro;

public class TextScaler : MonoBehaviour
{
    public TextMeshProUGUI buttonText;       // Referencia al texto del botón
    public Vector3 normalScale = Vector3.one;  // Escala original del texto
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Escala al pasar el cursor

    void Start()
    {
        if (buttonText != null)
        {
            buttonText.rectTransform.localScale = normalScale; // Establece la escala inicial
        }
    }

    // Método para agrandar el texto al pasar el cursor
    public void ScaleUp()
    {
        if (buttonText != null)
        {
            buttonText.rectTransform.localScale = hoverScale;
        }
    }

    // Método para restaurar el tamaño original al salir
    public void ScaleDown()
    {
        if (buttonText != null)
        {
            buttonText.rectTransform.localScale = normalScale;
        }
    }
}
