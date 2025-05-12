using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class BotonHabilidad : MonoBehaviour
{
    public TextMeshProUGUI textoCosto;
    public TextMeshProUGUI textoCooldown; // Nuevo: para mostrar el tiempo restante
    public Image iconoHabilidad;
    private float cooldownTimer = 0f;
    private bool isCooldown = false;
    public Key teclaHabilidad1 = Key.Digit1; // Por defecto la tecla 1
    public HabilidadBase habilidad; // O HabilidadProyectil si solo usas proyectiles
    private PortadorJugable portadorJugable;

    void Start()
    {
        iconoHabilidad.fillAmount = 0;
        if (textoCooldown != null)
            textoCooldown.text = "";
        portadorJugable = FindFirstObjectByType<PortadorJugable>(); // Busca el jugador en la escena
    }

    void Update()
    {
        Habilidad1();
        ActualizarCooldownUI();
    }

    private void Habilidad1()
    {
        if (Keyboard.current[teclaHabilidad1].wasPressedThisFrame && !isCooldown)
        {
            isCooldown = true;
            cooldownTimer = habilidad.cooldown;
            iconoHabilidad.fillAmount = 1;
            if (textoCooldown != null)
                textoCooldown.text = habilidad.cooldown.ToString("F1");
            // Aquí pasas el jugador a la habilidad
            if (portadorJugable != null)
                habilidad.Usar(portadorJugable);
            else
                Debug.LogWarning("No se encontró un PortadorJugable en la escena.");
        }
    }

    private void ActualizarCooldownUI()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            iconoHabilidad.fillAmount = cooldownTimer / habilidad.cooldown;
            if (textoCooldown != null)
                textoCooldown.text = Mathf.Ceil(cooldownTimer).ToString();

            if (cooldownTimer <= 0)
            {
                isCooldown = false;
                iconoHabilidad.fillAmount = 0;
                if (textoCooldown != null)
                    textoCooldown.text = "";
            }
        }
    }
}