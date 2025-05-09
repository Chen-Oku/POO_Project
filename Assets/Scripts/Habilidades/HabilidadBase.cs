using UnityEngine;

public enum TipoDeHabilidad {
    Proyectil, Curacion, AOE
}

public abstract class HabilidadBase : ScriptableObject
{
    public string nombre;
    public Sprite icono;
    public float cooldown;
    protected float ultimoUso;
    public TipoDeHabilidad tipoDeHabilidad;

    public virtual int costoMana { get; set; }

   public abstract void Usar(PortadorJugable portador);
}
