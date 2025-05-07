using UnityEngine;

public enum TipoDeHabilidad {
    Proyectil, Curacion, AOE
}

[CreateAssetMenu(fileName = "HabilidadBase", menuName = "Scriptable Objects/HabilidadBase")]

public abstract class HabilidadBase : ScriptableObject
{
    public string nombre;
    public Sprite icono;
    public float cooldown;
    protected float ultimoUso;
    public TipoDeHabilidad tipoDeHabilidad;

   public abstract void Usar();
}
