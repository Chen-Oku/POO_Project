using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHabilidadController : MonoBehaviour
{
    public InputActionAsset inputActions;
    public PortadorJugable portadorJugable; // Asigna el PortadorJugable en el inspector

    private InputAction habilidad1Action;
    private InputAction habilidad2Action;
    private InputAction habilidad3Action;

    private void Awake()
    {
        habilidad1Action = inputActions.FindAction("Habilidad1");
        habilidad2Action = inputActions.FindAction("Habilidad2");
        habilidad3Action = inputActions.FindAction("Habilidad3");
    }

    private void OnEnable()
    {
        habilidad1Action.Enable();
        habilidad2Action.Enable();
        habilidad3Action.Enable();
    }

    private void OnDisable()
    {
        habilidad1Action.Disable();
        habilidad2Action.Disable();
        habilidad3Action.Disable();
    }

    private void Update()
    {
        if (habilidad1Action.WasPressedThisFrame())
            UsarHabilidad(0);
            print("Habilidad 1 usada");
        if (habilidad2Action.WasPressedThisFrame())
            UsarHabilidad(1);
        if (habilidad3Action.WasPressedThisFrame())
            UsarHabilidad(2);
    }

    private void UsarHabilidad(int index)
    {
        if (portadorJugable == null || portadorJugable.sistemaHabilidades == null) return;
        var habilidad = portadorJugable.sistemaHabilidades.ObtenerHabilidad(index);
        if (habilidad != null)
        {
            if (portadorJugable.sistemaMana.valorActual >= habilidad.costoMana)
            {
                habilidad.Usar(portadorJugable);
                portadorJugable.sistemaMana.ModificarValor(-habilidad.costoMana);
                Debug.Log($"Usaste la habilidad: {habilidad.nombre}. Maná restante: {portadorJugable.sistemaMana.valorActual}");
            }
            else
            {
                Debug.Log("No tienes suficiente maná para usar esta habilidad.");
            }
        }
    }
}