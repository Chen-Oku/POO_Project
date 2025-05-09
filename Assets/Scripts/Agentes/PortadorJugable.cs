using UnityEngine;

public class PortadorJugable : PortadorGeneral 
{

    public SistemaEstadisticas sistemaMana; // Instancia para el maná
    public SistemaHabilidades sistemaHabilidades; // Sistema de habilidades
    
    //public SistemaHabilidades sistemaHabilidades;

private void Start()
    {
        // Inicializa las estadísticas si no están configuradas
        if (sistemaVida == null)
        {
            sistemaVida = new SistemaVida
            {
                valorActual = 100,
                valorMaximo = 100,
                valorMinimo = 0
            };
        }

        if (sistemaMana == null)
        {
            sistemaMana = new SistemaEstadisticas
            {
                valorActual = 50,
                valorMaximo = 50,
                valorMinimo = 0
            };
        }

        // Inicializa el sistema de habilidades
        if (sistemaHabilidades == null)
        {
            sistemaHabilidades = gameObject.AddComponent<SistemaHabilidades>();
        }
            sistemaHabilidades.AgregarHabilidad(ScriptableObject.CreateInstance<HabilidadProyectil>());
            sistemaHabilidades.AgregarHabilidad(ScriptableObject.CreateInstance<HabilidadCuracion>());
            sistemaHabilidades.AgregarHabilidad(ScriptableObject.CreateInstance<HabilidadAOE>());
    }

    private void Update()
    {
        

       /*  // Usar habilidades con teclas numéricas
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ObtenerHabilidad(0); // Usa la primera habilidad (HabilidadProyectil)
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ObtenerHabilidad(1); // Usa la segunda habilidad (HabilidadCuracion)
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ObtenerHabilidad(2); // Usa la tercera habilidad (HabilidadAOE)
        } */
    }
    /* public HabilidadBase ObtenerHabilidad(int index) 
    {
        //sistemaHabilidades.UsarHabilidad(index);
        if (sistemaHabilidades == null) return null;

    var habilidad = sistemaHabilidades.ObtenerHabilidad(index);
    if (habilidad != null)
    {
        if (sistemaMana.valorActual >= habilidad.costoMana)
        {
            habilidad.Usar(this);
            sistemaMana.ModificarValor(-habilidad.costoMana);
            Debug.Log($"Usaste la habilidad: {habilidad.nombre}. Maná restante: {sistemaMana.valorActual}");
        }
        else
        {
            Debug.Log("No tienes suficiente maná para usar esta habilidad.");
        }
        return habilidad;
    }
    return null;
    } */
}
