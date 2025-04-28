using System.Collections.Generic;
using UnityEngine;

public class MovimientoBezier : MonoBehaviour
{
    public List<Transform> puntosControl; // Lista de puntos de control (incluye inicio y final)
    public float duracion = 2.0f; // Duraci�n del movimiento en segundos

    private float tiempoTranscurrido = 0.0f;
    private bool moviendo = true;

    void Update()
    {
        if (moviendo)
        {
            // Incrementa el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime / duracion;
            float t = Mathf.Clamp01(tiempoTranscurrido); // Asegura que t est� entre 0 y 1

            // Calcula la posici�n en la curva de B�zier
            transform.position = CalcularBezierGeneral(puntosControl, t);

            // Detiene el movimiento si se alcanza el destino
            if (t >= 1.0f)
            {
                moviendo = false;
            }
        }
    }

    // M�todo para calcular la posici�n en una curva de B�zier general (n puntos)
    private Vector3 CalcularBezierGeneral(List<Transform> puntos, float t)
    {
        int n = puntos.Count - 1;
        Vector3 resultado = Vector3.zero;

        for (int i = 0; i <= n; i++)
        {
            float coeficienteBinomial = Factorial(n) / (Factorial(i) * Factorial(n - i));
            float termino = coeficienteBinomial * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            resultado += termino * puntos[i].position;
        }

        return resultado;
    }

    // M�todo para calcular el factorial de un n�mero
    private int Factorial(int numero)
    {
        if (numero <= 1) return 1;
        return numero * Factorial(numero - 1);
    }

    // M�todo para iniciar el movimiento
    public void IniciarMovimiento()
    {
        tiempoTranscurrido = 0.0f;
        moviendo = true;
    }
}