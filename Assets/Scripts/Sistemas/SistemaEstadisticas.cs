using UnityEngine;

public class SistemaEstadisticas 
{
    public float vidaActual;
    public float vidaMaxima;
    public float vidaMinima;

    public float manaActual;
    public float manaMaximo;
    public float manaMinimo;

    public virtual void ModificarValor(int amount) 
    {
        vidaActual = Mathf.Clamp(vidaActual + amount, vidaMinima, vidaMaxima);
    }

    public virtual void RecibirDaño(float cantidad)
    {
        // Base implementation of damage logic
        Debug.Log($"Recibiendo {cantidad} de daño en SistemaEstadisticas.");
    }
}
