using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadCuracion", menuName = "Scriptable Objects/Habilidad Curacion")]
public class HabilidadCuracion : HabilidadBase
{
    //[SerializeField] private float tiempoCooldown = 2f; // Solo para configuración inicial
    public int cantidadCuracion = 30;
    public GameObject prefabAreaCuracion; // Prefab del área de curación con efectos incluidos
    public float duracionArea = 5f; // Duración del área de curación
    public float radioCuracion = 3f; // Radio del área de curación
    
 
    public override void Usar(PortadorJugable portador)
    {
        base.costoMana = 20;
       
    if (Time.time - ultimoUso < cooldown) 
    {
        Debug.Log("Habilidad en cooldown, espera un momento.");
        return;
    }
        
        if (portador == null) return;

    bool recursoConsumido = false;

    if (portador.tipoPortador == PortadorJugable.TipoPortador.Mana && portador.sistemaMana != null)
    {
        recursoConsumido = portador.sistemaMana.ConsumirMana(portador.sistemaMana.CostoHabilidadCuracion);
    }
    else if (portador.tipoPortador == PortadorJugable.TipoPortador.Vida && portador.sistemaVida != null)
    {
        recursoConsumido = portador.sistemaVida.ConsumirVida(portador.sistemaVida.CostoHabilidadCuracion);
    }

    if (!recursoConsumido)
    {
        Debug.Log("No tienes suficiente recurso para usar la habilidad.");
        return;
    }
        
        /* // Consumir mana primero
        if (portador.sistemaMana != null)
        {
            portador.sistemaMana.ConsumirMana(costoMana);
            Debug.Log($"Has consumido {costoMana} puntos de maná. Maná restante: {portador.sistemaMana.ManaActual}");
        } */

        // Consumir recurso según el tipo de portador
        if (portador.tipoPortador == PortadorJugable.TipoPortador.Mana)
        {
            if (portador.sistemaMana != null)
                portador.sistemaMana.ModificarValor(-costoMana);
        }
        else // TipoPortador.Vida
        {
            if (portador.sistemaVida != null)
                portador.sistemaVida.ModificarValor(-costoVida);
        }
            
        // Crear el área de curación
        if (prefabAreaCuracion != null)
        {
            // Crear el área un poco por delante del jugador
            Vector3 posicion = portador.transform.position + portador.transform.forward * 2f;
            posicion.y = 0.1f; // Ajustar ligeramente por encima del suelo
            
            GameObject areaInstanciada = Instantiate(prefabAreaCuracion, posicion, Quaternion.identity);
            
            // Configurar el área de curación
            AreaCuracion areaCuracion = areaInstanciada.GetComponent<AreaCuracion>();
            if (areaCuracion == null)
            {
                areaCuracion = areaInstanciada.AddComponent<AreaCuracion>();
            }
            
            // Configurar parámetros del área de curación
            areaCuracion.Inicializar(portador, cantidadCuracion, duracionArea, radioCuracion);
            
            Debug.Log($"Has creado un área de curación que durará {duracionArea} segundos.");
        }
        else
        {
            // Si no hay prefab de área, curar directamente (comportamiento original)
            if (portador.sistemaVida != null)
            {
                portador.sistemaVida.Curar(cantidadCuracion);
                Debug.Log($"{portador.name} se ha curado {cantidadCuracion} puntos de vida.");
            }
        }
            
        ultimoUso = Time.time;
    }
}