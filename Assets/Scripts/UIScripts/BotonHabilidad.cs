using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BotonHabilidad : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Button boton;
    [SerializeField] private Image imagenHabilidad;
    [SerializeField] private Image overlayEnfriamiento;
    [SerializeField] private TextMeshProUGUI textoEnfriamiento;
    
    [Header("Configuración")]
    [SerializeField] private Color colorInactivo = new Color(0.5f, 0.5f, 0.5f, 0.8f);
    [SerializeField] private HabilidadBase habilidad;
    
    private bool enCooldown = false;
    private float cooldownRestante = 0f;
    
    private void Awake()
    {
        // Asegúrate de que tengamos todas las referencias
        if (boton == null) boton = GetComponent<Button>();
        if (imagenHabilidad == null) imagenHabilidad = GetComponent<Image>();
        
        // Configura el overlay inicialmente invisible
        if (overlayEnfriamiento != null)
        {
            overlayEnfriamiento.type = Image.Type.Filled;
            overlayEnfriamiento.fillMethod = Image.FillMethod.Radial360;
            overlayEnfriamiento.fillOrigin = (int)Image.Origin360.Top;
            overlayEnfriamiento.fillClockwise = false;
            overlayEnfriamiento.fillAmount = 0f;
        }
        
        // Ocultar texto de cooldown inicialmente
        if (textoEnfriamiento != null)
        {
            textoEnfriamiento.gameObject.SetActive(false);
        }
    }
    
    public void ConfigurarHabilidad(HabilidadBase nuevaHabilidad)
    {
        habilidad = nuevaHabilidad;
        
        // Si la habilidad tiene un icono, asignarlo
        if (habilidad != null && habilidad.icono != null && imagenHabilidad != null)
        {
            imagenHabilidad.sprite = habilidad.icono;
        }
    }
    
    public void IniciarCooldown(float duracion)
    {
        if (duracion <= 0) return;
        
        enCooldown = true;
        cooldownRestante = duracion;
        
        // Desactivar el botón
        if (boton != null) boton.interactable = false;
        
        // Oscurecer la imagen
        if (imagenHabilidad != null) imagenHabilidad.color = colorInactivo;
        
        // Mostrar overlay y texto
        if (overlayEnfriamiento != null) overlayEnfriamiento.fillAmount = 1f;
        if (textoEnfriamiento != null)
        {
            textoEnfriamiento.gameObject.SetActive(true);
            textoEnfriamiento.text = Mathf.Ceil(duracion).ToString();
        }
        
        // Iniciar la corrutina para actualizar visualmente el cooldown
        StartCoroutine(ActualizarCooldown());
    }
    
    private IEnumerator ActualizarCooldown()
    {
        float duracionTotal = cooldownRestante;
        
        while (cooldownRestante > 0)
        {
            cooldownRestante -= Time.deltaTime;
            
            // Actualizar el fill amount del overlay
            if (overlayEnfriamiento != null)
            {
                overlayEnfriamiento.fillAmount = cooldownRestante / duracionTotal;
            }
            
            // Actualizar el texto
            if (textoEnfriamiento != null)
            {
                textoEnfriamiento.text = Mathf.Ceil(cooldownRestante).ToString();
            }
            
            yield return null;
        }
        
        // Restablecer todo cuando termina el cooldown
        enCooldown = false;
        cooldownRestante = 0f;
        
        if (boton != null) boton.interactable = true;
        if (imagenHabilidad != null) imagenHabilidad.color = Color.white;
        if (overlayEnfriamiento != null) overlayEnfriamiento.fillAmount = 0f;
        if (textoEnfriamiento != null) textoEnfriamiento.gameObject.SetActive(false);
    }
    
    // Método público para verificar si la habilidad está en cooldown
    public bool EstaEnCooldown()
    {
        return enCooldown;
    }
}
