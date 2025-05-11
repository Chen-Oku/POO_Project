using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCVidaIU : MonoBehaviour
{
   [SerializeField] private Image barraVida;
    [SerializeField] private Canvas canvas;
    
    private PortadorNoJugable enemigo;
    private Camera mainCamera;
    
    public void InicializarBarra(PortadorNoJugable enemigo)
    {
        this.enemigo = enemigo;
        mainCamera = Camera.main;
        
        // Asegurarse que el canvas mira siempre a la cámara
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
        }
        
        ActualizarVida();
    }
    
    private void Update()
    {
        // Hacer que la barra de vida siempre mire a la cámara
        if (mainCamera != null && canvas != null)
        {
            canvas.transform.LookAt(mainCamera.transform);
            // Invertir la rotación para que el texto sea legible
            canvas.transform.rotation = Quaternion.LookRotation(
                mainCamera.transform.position - canvas.transform.position);
        }
    }
    
    public void ActualizarVida()
    {
        if (enemigo != null && enemigo.sistemaVida != null && barraVida != null)
        {
            float porcentaje = (float)enemigo.sistemaVida.VidaActual / enemigo.sistemaVida.VidaMaxima;
            barraVida.fillAmount = porcentaje;
            
            // Cambiar color según la vida restante
            if (porcentaje > 0.6f)
                barraVida.color = Color.green;
            else if (porcentaje > 0.3f)
                barraVida.color = Color.yellow;
            else
                barraVida.color = Color.red;
        }
    }
}