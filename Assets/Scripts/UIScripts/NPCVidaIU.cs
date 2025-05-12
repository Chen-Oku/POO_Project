using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCVidaUI : MonoBehaviour
{
    [SerializeField] private Image barraVida;
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider Slider;
    
    private PortadorNoJugable portador;
    
    private Camera camara;
    
    private void Awake()
    {
        // Asegurarse de que el canvas existe
        if (canvas == null)
            canvas = GetComponentInChildren<Canvas>();
        
        if (canvas != null)
            canvas.renderMode = RenderMode.WorldSpace;
        
        // Obtenemos la referencia a la cámara principal
        camara = Camera.main;
    }
    
    // Método para vincular el portador con esta UI
    public void ConfigurarConPortador(PortadorNoJugable _portador)
    {
        portador = _portador;
        
        if (portador != null && portador.sistemaVida != null)
        {
            // Suscribirse al evento de cambio de vida
            portador.sistemaVida.OnVidaCambiada += ActualizarVida;
            // Actualizar UI inicialmente
            ActualizarVida();
        }
    }
    
    // Actualizar la UI sin parámetros (llamado por eventos)
    public void ActualizarVida()
    {
        if (portador != null && portador.sistemaVida != null)
        {
            ActualizarVida(portador.sistemaVida.VidaActual, portador.sistemaVida.VidaMaxima);
        }
    }
    
    // Actualizar la UI con valores específicos
    public void ActualizarVida(float vidaActual, float vidaMaxima)
    {
        if (barraVida != null)
        {
            float ratio = Mathf.Clamp01(vidaActual / vidaMaxima);
            barraVida.fillAmount = ratio;
            Slider.value = barraVida.fillAmount;
        }
        
        if (textoVida != null)
        {
            textoVida.text = $"{vidaActual}/{vidaMaxima}";
        }
    }
    
    private void LateUpdate()
    {
        // Hacer que la barra de vida siempre mire a la cámara
        if (camara != null && canvas != null)
        {
            canvas.transform.LookAt(camara.transform);
        }
    }
    
    private void OnDestroy()
    {
        // Desuscribirse de eventos
        if (portador != null && portador.sistemaVida != null)
        {
            portador.sistemaVida.OnVidaCambiada -= ActualizarVida;
        }
    }
}