    using UnityEngine;

public class SistemaMana
{
    private float manaActual;
    private float manaMaximo;

     public SistemaMana(float manaActual, float manaMaximo)
    {
        this.ManaActual = manaActual;
        this.ManaMaximo = manaMaximo;
    }

    protected float ManaActual { get => manaActual; set => manaActual = value; }
    protected float ManaMaximo { get => manaMaximo; set => manaMaximo = value; }

    public void consumirMana(float cantidad)
    {
        ManaActual = Mathf.Max(ManaActual - cantidad, 0);
    }

    public void regenerarMana(float cantidad)
    {
        ManaActual = Mathf.Min(ManaActual + cantidad, ManaMaximo);
    }
    
}
