using Unity.VisualScripting;
using UnityEngine;

public class MovimientoLerp : MonoBehaviour
{
    public Transform puntoInicio; // Punto inicial del movimiento
    public Transform puntoFinal; // Punto final del movimiento
    public float duracion = 2.0f; // Duración del movimiento en segundos

    private float tiempoTranscurrido = 0.0f;
    private bool moviendo = true;

    void Update()
    {
        if (moviendo)
        {
            // Incrementa el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime / duracion;
            float t = Mathf.Clamp01(tiempoTranscurrido); // Asegura que t esté entre 0 y 1

            
            // Interpola entre el punto inicial y el punto final
            transform.position = Vector3.Lerp(puntoInicio.position, puntoFinal.position, t);

            // Detiene el movimiento si se alcanza el destino
            if (t >= 1.0f)
            {
                moviendo = false;
            }
        }
    }

    // Método para iniciar el movimiento
    public void IniciarMovimiento()
    {
        tiempoTranscurrido = 0.0f;
        moviendo = true;
    }
}
