using UnityEngine;

public enum TipoDeHabilidad {
    Proyectil, Curacion, AOE
}

public abstract class HabilidadBase : ScriptableObject
{
    public string nombre;
    public Sprite icono;
    public float cooldown;
    protected float ultimoUso = -999f; // Inicializado a un valor bajo para permitir el uso inmediato
    public TipoDeHabilidad tipoDeHabilidad;

    public virtual int costoMana { get; set; }

   public abstract void Usar(PortadorJugable portador);


//ultimos cambios
   public bool EstaDisponible()
    {
        return Time.time >= ultimoUso + cooldown;
    }
    
    public float TiempoRestante()
    {
        float tiempoRestante = (ultimoUso + cooldown) - Time.time;
        return Mathf.Max(0, tiempoRestante); // Nunca devolver tiempo negativo
    }
    
    protected void RegistrarUso()
    {
        ultimoUso = Time.time;
    }
    
    // Método para reiniciar el tiempo (útil al reiniciar el juego)
    public void Reiniciar()
    {
        ultimoUso = -999f; // O cualquier valor que permita uso inmediato
    }

   
}
