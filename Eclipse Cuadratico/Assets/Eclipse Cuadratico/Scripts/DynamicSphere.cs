using System.Collections;
using UnityEngine;

public class DynamicSphere : MonoBehaviour
{
    public enum SphereState { Green, Red } // Tipos de estados
    public SphereState currentState;      // Estado actual de la esfera

    public Material greenMaterial; // Material verde
    public Material redMaterial;   // Material rojo

    private Renderer sphereRenderer; // Renderer de la esfera para cambiar su color

    void Start()
    {
        // Obtener el Renderer de la esfera
        sphereRenderer = GetComponent<Renderer>();

        // Inicializar el estado al azar
        ChangeState(Random.value > 0.5f ? SphereState.Green : SphereState.Red);

        // Iniciar el ciclo de cambio de color cada 5 segundos
        StartCoroutine(ChangeColorEvery5Seconds());
    }

    // Cambiar el estado de la esfera y actualizar su color
    public void ChangeState(SphereState newState)
    {
        currentState = newState;

        if (currentState == SphereState.Green)
        {
            sphereRenderer.material = greenMaterial;
        }
        else if (currentState == SphereState.Red)
        {
            sphereRenderer.material = redMaterial;
        }
    }

    // Corutina para cambiar el estado cada 5 segundos
    private IEnumerator ChangeColorEvery5Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            // Cambiar al estado opuesto
            if (currentState == SphereState.Green)
            {
                ChangeState(SphereState.Red);
            }
            else
            {
                ChangeState(SphereState.Green);
            }
        }
    }
}
