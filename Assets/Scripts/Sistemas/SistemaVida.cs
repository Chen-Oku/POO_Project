using UnityEngine;

public class SistemaVida : SistemaEstadisticas
{
    public int VidaActual => vidaActual;
    public int VidaMaxima => vidaMaxima;
    public int VidaMinima => vidaMinima;

 // Evento para notificar cambios en la vida
    public event System.Action OnVidaCambiada;
    
    // Constructor opcional
    public SistemaVida(int vidaMaxima = 100, int vidaActual = 100)
    {
        base.vidaMaxima = vidaMaxima;
        base.vidaActual = vidaActual;
        base.vidaMinima = 0;
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