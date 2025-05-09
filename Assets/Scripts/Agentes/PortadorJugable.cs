using UnityEngine;

public class PortadorJugable : PortadorGeneral 
{

    public SistemaMana sistemaMana; // Instancia para el maná
    public SistemaHabilidades sistemaHabilidades; // Sistema de habilidades
    
    //public SistemaHabilidades sistemaHabilidades;

private void Start()
    {
        // Inicializa las estadísticas si no están configuradas

        // Crear una instancia de SistemaVida
        SistemaVida sistemaVida = new SistemaVida
        {
            vidaActual = 100,
            vidaMaxima = 100,
            vidaMinima = 0
        };

         // Modificar valores
        sistemaVida.RecibirDaño(20);
        sistemaVida.Curar(10);

        Debug.Log($"Vida actual: {sistemaVida.VidaActual}");

       /*  if (sistemaVida == null)
        {
            sistemaVida = new SistemaVida
            {
                vidaActual = 100,
                vidaMaxima = 100,
                vidaMinima = 0
            };
        }
 */
        if (sistemaMana == null)
        {
            sistemaMana = new SistemaMana
            {
                manaActual = 50,
                manaMaximo = 100,
                manaMinimo = 0
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
