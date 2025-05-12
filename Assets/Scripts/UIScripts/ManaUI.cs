using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private Image barraMana;
    [SerializeField] private TextMeshProUGUI textoMana;
    [SerializeField] private Slider Slider;
    
    private SistemaMana _sistemaMana;

    // Propiedad pública para acceder al sistema
    public SistemaMana Sistema => _sistemaMana;
    
    private void Start()
    {
        if (FindAnyObjectByType<PortadorJugable>() != null)
        {
            Debug.Log($"ID del sistema de maná en UI: {FindAnyObjectByType<PortadorJugable>().sistemaMana.GetHashCode()}");
        }
        // Buscar el portador primero
        PortadorJugable portadorJugable = FindAnyObjectByType<PortadorJugable>();
        if (portadorJugable != null)
        {
            // Obtener el SistemaMana del portador
            _sistemaMana = portadorJugable.sistemaMana;
            
            if (_sistemaMana != null)
            {
                // Suscribirse al evento de cambio de estadísticas
                _sistemaMana.OnEstadisticasCambiadas += ActualizarUIMana;
                ActualizarUIMana(); // Actualizar UI al inicio
                Debug.Log("ManaUI conectado exitosamente con SistemaMana");
            }
            else
            {
                Debug.LogError("No se encontró el SistemaMana en el portador");
            }
        }
        else
        {
            Debug.LogError("No se encontró el PortadorJugable en la escena");
        }
    }
    
    private void OnDestroy()
    {
        // Desuscribirse para evitar memory leaks
        if (_sistemaMana != null)
        {
            _sistemaMana.OnEstadisticasCambiadas -= ActualizarUIMana;
        }
    }
    
    public void ActualizarUIMana()
    {
        if (_sistemaMana != null)
        {
            if (barraMana != null)
            {
                // Usar ManaActual y ManaMaximo (propiedades que ya existen en tu clase)
                float porcentaje = (float)_sistemaMana.ManaActual / _sistemaMana.ManaMaximo;
                barraMana.fillAmount = porcentaje;
                Slider.value = barraMana.fillAmount;
            }
            
            if (textoMana != null)
            {
                // Usar las mismas propiedades
                textoMana.text = $"{_sistemaMana.ManaActual}/{_sistemaMana.ManaMaximo}";
            }
        }
    }
}