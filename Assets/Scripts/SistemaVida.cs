using UnityEngine;

public class SistemaVida
{
    private float vidaActual;
    private float vidaMaxima;


    public SistemaVida(float vidaActual, float vidaMaxima)
    {
        this.VidaActual = vidaActual;
        this.VidaMaxima = vidaMaxima;
    }

     protected float VidaActual { get => vidaActual; set => vidaActual = value; }
    protected float VidaMaxima { get => vidaMaxima; set => vidaMaxima = value; }

    public void recibirDanio(float cantidad)
    {
        VidaActual = Mathf.Max(VidaActual - cantidad, 0);
    }

    public void curar(float cantidad)
    {
        VidaActual = Mathf.Min(VidaActual + cantidad, VidaMaxima);
    }
}
