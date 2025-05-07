using UnityEngine;

public class SistemaEstadisticas
{
    public float valorActual;
    public float valorMaximo;
    public float valorMinimo;

    public virtual void ModificarValor(int amount) 
    {
        valorActual = Mathf.Clamp(valorActual + amount, valorMinimo, valorMaximo);
    }
}
