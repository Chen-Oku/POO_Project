using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VidaUI : MonoBehaviour
{
    [SerializeField] private Image barraVida;
    [SerializeField] private TextMeshProUGUI textoVida;
    
    private SistemaVida _sistemaVida;
    
    private void Start()
    {
        // Buscar el portador jugable
        PortadorJugable portadorJugable = FindAnyObjectByType<PortadorJugable>();
        if (portadorJugable != null && portadorJugable.sistemaVida != null)
        {
            // Obtener el sistema de vida y suscribirse a cambios
            _sistemaVida = portadorJugable.sistemaVida;
            _sistemaVida.OnVidaCambiada += ActualizarUIVida;
            ActualizarUIVida(); // Actualizar UI inicialmente
        }
        else
        {
            Debug.LogError("No se encontró el portador jugable o su sistema de vida");
        }
    }
    
    private void OnDestroy()
    {
        // Desuscribirse para evitar memory leaks
        if (_sistemaVida != null)
        {
            _sistemaVida.OnVidaCambiada -= ActualizarUIVida;
        }
    }
    
    // Este método es para suscripción al evento OnVidaCambiada (sin parámetros)
    private void ActualizarUIVida()
    {
        if (_sistemaVida != null)
        {
            // Accede a las propiedades de vida desde la instancia de _sistemaVida
            ActualizarUIVida(_sistemaVida.VidaActual, _sistemaVida.VidaMaxima);
        }
    }
    
    // Esta sobrecarga es para llamadas directas con valores específicos
    public void ActualizarUIVida(float vidaActual, float vidaMaxima)
    {
        // Actualizar la barra de vida
        if (barraVida != null)
        {
            barraVida.fillAmount = vidaActual / vidaMaxima;
        }

        // Actualizar el texto
        if (textoVida != null)
        {
            textoVida.text = $"{vidaActual}/{vidaMaxima}";
        }
    }
}