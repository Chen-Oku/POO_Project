using UnityEngine;

public class SistemaEstadisticas 
{
    public int vidaActual;
    public int vidaMaxima;
    public int vidaMinima;

    public int manaActual;
    public int manaMaximo;
    public int manaMinimo;

    public virtual void ModificarValor(int cantidad) 
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, vidaMinima, vidaMaxima);
    }

    public virtual void RecibirDaño(int cantidad)
    {
        // Base implementation of damage logic
        Debug.Log($"Recibiendo {cantidad} de daño en SistemaEstadisticas.");
    }
}
