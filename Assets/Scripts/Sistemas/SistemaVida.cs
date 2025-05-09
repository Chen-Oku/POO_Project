using UnityEngine;

public class SistemaVida : SistemaEstadisticas
{
    public float VidaActual => vidaActual;
    public float VidaMaxima => vidaMaxima;
    public float VidaMinima => vidaMinima;

    public override void RecibirDaño(float cantidad)
    {
        // Custom damage logic for SistemaVida
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            Debug.Log("El personaje ha muerto.");
        }
    }
    public void Curar(float cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima; // Prevent overhealing
        }
        Debug.Log($"Curado por {cantidad}. Vida actual: {vidaActual}");
    }

}
