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
        // Habilidad 1
        if (habilidad1Action.WasPressedThisFrame())
        {
            Debug.Log("Intentando usar Habilidad 1...");
            UsarHabilidad(1);
        }

        // Habilidad 2
        if (habilidad2Action.WasPressedThisFrame())
        {
            Debug.Log("Intentando usar Habilidad 2...");
            UsarHabilidad(2);
        }

        // Habilidad 3
        if (habilidad3Action.WasPressedThisFrame())
        {
            Debug.Log("Intentando usar Habilidad 3...");
            UsarHabilidad(3);
        }
    }

    private void UsarHabilidad(int index)
{
    if (portadorJugable == null || portadorJugable.sistemaHabilidades == null) return;

    var habilidad = portadorJugable.sistemaHabilidades.ObtenerHabilidad(index);
    if (habilidad == null)
    {
        Debug.LogError($"No se encontró una habilidad en el índice {index}.");
        return;
    }

    // Verificar el tipo de habilidad antes de usarla
    if (habilidad is HabilidadProyectil habilidadProyectil)
    {
        Debug.Log("Usando habilidad de tipo proyectil.");
        habilidadProyectil.Usar(portadorJugable);
    }
    else if (habilidad is HabilidadCuracion habilidadCuracion)
    {
        Debug.Log("Usando habilidad de tipo curación.");
        habilidadCuracion.Usar(portadorJugable);
    }
    else if (habilidad is HabilidadAOE habilidadAOE)
    {
        Debug.Log("Usando habilidad de tipo AOE.");
        habilidadAOE.Usar(portadorJugable);
    }
    else
    {
        Debug.LogError("Tipo de habilidad desconocido.");
    }
}

    /* private void UsarHabilidad(int index)
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
    } */
}