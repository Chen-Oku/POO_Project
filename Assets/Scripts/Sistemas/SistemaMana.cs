using UnityEngine;

public class SistemaMana : SistemaEstadisticas
{
    public int ManaActual => base.manaActual;  // Use parent's field
    public int ManaMaximo => base.manaMaximo;  // Use parent's field
    public int ManaMinimo => base.manaMinimo;  // Use parent's field
    private int regeneracionMana = 1;
    private float tiempoUltimaRegeneracion;
    private float intervaloRegeneracion = 1f; // regenerar cada 1 segundo
    
    private void Start()
    {
        InicializarMana();
        tiempoUltimaRegeneracion = Time.time;
    }
    
    private void Update()
    {
        // Regenerar mana automáticamente a intervalos
        if (Time.time >= tiempoUltimaRegeneracion + intervaloRegeneracion)
        {
            RecuperarMana(regeneracionMana);
            tiempoUltimaRegeneracion = Time.time;
        }
    }
    
    public void InicializarMana(int max = 100, int actual = 100, int min = 0)
    {
        manaMaximo = max;
        manaActual = actual;
        manaMinimo = min;
    }
    
    public void RecuperarMana(int cantidad)
    {
        if (manaActual < manaMaximo)
        {
            manaActual = Mathf.Min(manaActual + cantidad, manaMaximo);
            // Notificar cambio para actualizar la UI
            OnEstadisticasCambiadas?.Invoke();
        }
    }
    
    public void ConsumirMana(int cantidad)
    {
        if (manaActual > manaMinimo)
        {
            manaActual = Mathf.Max(manaActual - cantidad, manaMinimo);
            // Notificar cambio para actualizar la UI
            OnEstadisticasCambiadas?.Invoke();

            Debug.Log($"Mana consumido: {cantidad}, Mana actual: {manaActual}");
        }
    }
    
    public bool TieneSuficienteMana(int cantidad)
    {
        bool resultado = manaActual >= cantidad;
        Debug.Log($"Comprobando mana: actual={manaActual}, requerido={cantidad}, suficiente={resultado}");
        return resultado;
        //return manaActual >= cantidad;
    }
    
    // Este evento debe estar definido en SistemaEstadisticas o implementarlo aquí
    public delegate void EstadisticasCambiadas();
    public event EstadisticasCambiadas OnEstadisticasCambiadas;
}