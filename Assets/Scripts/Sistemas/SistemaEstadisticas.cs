using UnityEngine;

public class SistemaEstadisticas 
{
    public int vidaActual;
    public int vidaMaxima;
    public int vidaMinima;

    public int manaActual;
    public int manaMaximo;
    public int manaMinimo;

    public virtual void ModificarValor(int amount) 
    {
        vidaActual = Mathf.Clamp(vidaActual + amount, vidaMinima, vidaMaxima);
    }

    public virtual void RecibirDaño(int cantidad)
    {
        // Base implementation of damage logic
        Debug.Log($"Recibiendo {cantidad} de daño en SistemaEstadisticas.");
    }
}
