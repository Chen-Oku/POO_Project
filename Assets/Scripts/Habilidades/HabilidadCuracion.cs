using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadCuracion", menuName = "Scriptable Objects/Habilidad Curacion")]

public class HabilidadCuracion : HabilidadBase
{

    public int cantidadCuracion = 30;
    public float cooldownTiempo = 2f;
    public GameObject efectoCuracion; // Efecto visual para la curación
    
    public override void Usar(PortadorJugable portador)
    {
        if (Time.time - ultimoUso < cooldown) return;
        
        if (portador == null) return;
        
        // Aplicar curación
        if (portador.sistemaVida != null)
        {
            portador.sistemaVida.Curar(cantidadCuracion);
            Debug.Log($"{portador.name} se ha curado {cantidadCuracion} puntos de vida.");
            
            // Mostrar efecto visual
            if (efectoCuracion != null)
            {
                GameObject efecto = Instantiate(efectoCuracion, portador.transform.position, Quaternion.identity);
                efecto.transform.SetParent(portador.transform);
                Destroy(efecto, 2f); // Destruir el efecto después de 2 segundos
            }
            
            // Consumir mana
            if (portador.sistemaMana != null)
            {
                portador.sistemaMana.ModificarValor(-costoMana);
            }
            
            ultimoUso = Time.time;
        }
    }
}

   /*  public GameObject prefabAreaCuracion;
    public int condicionRegeneracion = 5;
    public int cantidadCuracion = 20;
    public int duracion = 5; // Duración del área de curación
    public int duracionArea = 5; // Duración del área de curación
    public int cantidadCuracionPorSegundo = 10;

    private void Start()
    {
        Destroy(prefabAreaCuracion, duracion); // El área desaparece después de cierto tiempo
    }

    public override void Usar(PortadorJugable portador) 
    {
        if (portador == null || portador.sistemaVida == null) return;

        // Instanciar el área de curación en la posición del jugador
        if (prefabAreaCuracion != null)
        {
            GameObject area = GameObject.Instantiate(
                prefabAreaCuracion, 
                portador.transform.position, 
                Quaternion.identity
            );

            // Si tu prefab tiene un script para la curación, puedes configurarlo aquí:
            var areaCuracion = area.GetComponent<AreaCuracion>();
            if (areaCuracion != null)
            {
                areaCuracion.duracion = duracionArea;
                areaCuracion.cantidadCuracionPorSegundo = cantidadCuracionPorSegundo;
            }
        }

        // Consumir maná
        if (portador.sistemaMana != null)
        {
            portador.sistemaMana.ModificarValor(-costoMana);
        }
    }

    public void ConsumirMana(SistemaMana mana) 
    {
        mana.ModificarValor(-10); // ejemplo
    }

    private void OnTriggerStay(Collider other)
    {
        PortadorJugable jugador = other.GetComponent<PortadorJugable>();
        if (jugador != null && jugador.sistemaVida != null)
        {
            jugador.sistemaVida.Curar(cantidadCuracionPorSegundo * Time.deltaTime);
        }
    }
} */
