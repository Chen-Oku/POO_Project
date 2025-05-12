using UnityEngine;

public class SistemaVida : SistemaEstadisticas
{
    public int VidaActual
    {
        get => vidaActual;
        set
        {
            vidaActual = Mathf.Clamp(value, vidaMinima, vidaMaxima);
            OnVidaCambiada?.Invoke();
        }
    }
    //public int VidaActual => vidaActual;
    public int VidaMaxima => vidaMaxima;
    public int VidaMinima => vidaMinima;

 // Evento para notificar cambios en la vida
    public event System.Action OnVidaCambiada;
    private float tiempoUltimaRegeneracion;
    private float intervaloRegeneracion = 5f; // regenerar cada 1 segundo
      private int regeneracionVida = 0;

    public int CostoHabilidadProyectil = 10;
    public int CostoHabilidadCuracion = 20;
    public int CostoHabilidadAOE = 30;
    
    // Constructor opcional
    public SistemaVida(int vidaMaxima = 100, int vidaActual = 100)
    {
        base.vidaMaxima = vidaMaxima;
        base.vidaActual = vidaActual;
        base.vidaMinima = 0;
    }

    public bool ConsumirVida(int cantidad) {
    if (VidaActual > cantidad) {
        VidaActual -= cantidad;
        return true;
    }
    return false;
}
    
    // Inicialización si no usas el constructor
    public void InicializarVida(int vidaMaxima = 100, int vidaActual = 100)
    {
        base.vidaMaxima = vidaMaxima;
        base.vidaActual = vidaActual;
        base.vidaMinima = 0;
        NotificarCambios();
    }
    
    // Método para recibir daño
    public override void RecibirDaño(int cantidad)
    {
        if (cantidad <= 0) return;
        
        base.vidaActual = Mathf.Max(base.vidaActual - cantidad, base.vidaMinima);
        NotificarCambios();
        
        // Verificar si ha muerto
        if (base.vidaActual <= base.vidaMinima)
        {
            // Trigger dead event or state
            Debug.Log("¡La entidad ha muerto!");
        }
    }
    
    // Método para curar
    public void Curar(int cantidad)
    {
        if (cantidad <= 0) return;
        
        base.vidaActual = Mathf.Min(base.vidaActual + cantidad, base.vidaMaxima);
        NotificarCambios();
    }
    
    // Para compatibilidad con código existente
    public override void ModificarValor(int cantidad)
    {
        if (cantidad > 0)
            Curar(cantidad);
        else if (cantidad < 0)
            RecibirDaño(-cantidad); // Convertir a positivo
    }
    public void RecuperarVida(int cantidad)
    {
        if (vidaActual < vidaMaxima) // 
        {
            vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);
            // Notificar cambio para actualizar la UI
            OnVidaCambiada?.Invoke();
        }
    }

    public void ActualizarRegeneracion(float deltaTime)
    {
        tiempoUltimaRegeneracion += deltaTime;
        if (tiempoUltimaRegeneracion >= intervaloRegeneracion)
        {
            tiempoUltimaRegeneracion = 0;
            RecuperarVida(regeneracionVida);
        }
    }
    public void RestaurarVidaCompleta()
    {
        // Internal implementation that changes the private backing field
        vidaActual = vidaMaxima;
        // Notify subscribers if necessary
        OnVidaCambiada?.Invoke();
    }
    
    private void NotificarCambios()
    {
        OnVidaCambiada?.Invoke();
    }
}