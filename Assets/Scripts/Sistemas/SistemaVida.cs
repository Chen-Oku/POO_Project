using UnityEngine;

public class SistemaVida : SistemaEstadisticas
{
    public int VidaActual => vidaActual;
    public int VidaMaxima => vidaMaxima;
    public int VidaMinima => vidaMinima;

    public override void RecibirDaño(int cantidad)
    {
        // Custom damage logic for SistemaVida
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            Debug.Log("El personaje ha muerto.");
        }
    }
    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima; // Prevent overhealing
        }
        Debug.Log($"Curado por {cantidad}. Vida actual: {vidaActual}");
    }

}
