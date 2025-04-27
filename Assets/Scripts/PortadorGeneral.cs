using UnityEngine;

public class PortadorGeneral
{
    private float vidaActual;
    private float vidaMaxima;

    public PortadorGeneral(float vidaActual, float vidaMaxima)
    {
        this.VidaActual = vidaActual;
        this.VidaMaxima = vidaMaxima;
    }

    protected float VidaActual { get => vidaActual; set => vidaActual = value; }
    protected float VidaMaxima { get => vidaMaxima; set => vidaMaxima = value; }

    public void DamageTaken(int amount)
    {
        VidaActual -= amount;
        if (VidaActual < 0)
        {
            VidaActual = 0;
        }
    }

    public void Heal(int amount)
    {
        VidaActual += amount;
        if (VidaActual > VidaMaxima)
        {
            VidaActual = VidaMaxima;
        }
    }
}
