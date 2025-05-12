using UnityEngine;

public class SistemaMana : SistemaEstadisticas
{
    public int ManaActual
    {
        get => base.manaActual;
        set
        {
            base.manaActual = Mathf.Clamp(value, manaMinimo, manaMaximo);
            OnEstadisticasCambiadas?.Invoke();
        }
    }
   // public int ManaActual => base.manaActual;  // Use parent's field
    public int ManaMaximo => base.manaMaximo;  // Use parent's field
    public int ManaMinimo => base.manaMinimo;  // Use parent's field
    private int regeneracionMana = 1;
    private float tiempoUltimaRegeneracion;
    private float intervaloRegeneracion = 5f; // regenerar cada 1 segundo
    public delegate void EstadisticasCambiadas();
    public event EstadisticasCambiadas OnEstadisticasCambiadas;
    
    public int CostoHabilidadProyectil = 10;
    public int CostoHabilidadCuracion = 20;
    public int CostoHabilidadAOE = 30;

    public bool ConsumirMana(int cantidad) {
        if (ManaActual >= cantidad) 
        {
            ManaActual -= cantidad;
            return true;
        }
        return false;
    }
    public void InicializarMana(int max = 100, int actual = 100, int min = 0)
    {
        manaMaximo = max;
        manaActual = actual;
        manaMinimo = min;
        OnEstadisticasCambiadas?.Invoke(); // Notificar cambios iniciales
    }
    
    public void RecuperarMana(int cantidad)
    {
        if (manaActual < manaMaximo) // 
        {
            manaActual = Mathf.Min(manaActual + cantidad, manaMaximo);
            // Notificar cambio para actualizar la UI
            OnEstadisticasCambiadas?.Invoke();
        }
    }
    
    /* public void ConsumirMana(int cantidad)
    {
        if (manaActual > manaMinimo)
        {
            manaActual = Mathf.Max(manaActual - cantidad, manaMinimo);
            // Notificar cambio para actualizar la UI
            OnEstadisticasCambiadas?.Invoke();

            Debug.Log($"Mana consumido: {cantidad}, Mana actual: {manaActual}");
        }
    } */
    
    public bool TieneSuficienteMana(int cantidad)
    {
        bool resultado = manaActual >= cantidad;
        Debug.Log($"Comprobando mana: actual={manaActual}, requerido={cantidad}, suficiente={resultado}");
        return resultado;
        //return manaActual >= cantidad;
    }

    public void RestaurarManaCompleto()
    {
        // Internal implementation that changes the private backing field
        manaActual = manaMaximo;
        // Notify subscribers if necessary
        OnEstadisticasCambiadas?.Invoke();
    }

    public void ActualizarRegeneracion(float deltaTime)
    {
        tiempoUltimaRegeneracion += deltaTime;
        if (tiempoUltimaRegeneracion >= intervaloRegeneracion)
        {
            tiempoUltimaRegeneracion = 0;
            RecuperarMana(regeneracionMana);
        }
    }
    
    
}