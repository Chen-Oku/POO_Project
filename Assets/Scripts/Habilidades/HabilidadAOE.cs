using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadAOE", menuName = "Scriptable Objects/Habilidad AOE")]
public class HabilidadAOE : HabilidadBase
{
    [Header("Configuración de Efecto")]
    public float radio = 5f;
    public int daño = 15;
    public GameObject efectoAOE; // Prefab del IceNova
    public LayerMask capasObjetivos; // Para filtrar qué objetos son afectados
    
    [Header("Configuración de Recursos")]
    [SerializeField] private int costoDeMana = 30;
    
    public override void Usar(PortadorJugable portador)
    {
        // Configurar valores base
        base.costoMana = costoDeMana;
        
        // Verificar cooldown
        if (Time.time - ultimoUso < cooldown)
        {
            Debug.Log("Habilidad en cooldown, espera un momento.");
            return;
        }
        
        if (portador == null) return;
        
        // Verificar si hay suficiente mana antes de usar la habilidad
        if (portador.sistemaMana != null && !portador.sistemaMana.TieneSuficienteMana(costoMana))
        {
            Debug.Log($"Mana insuficiente. Necesitas {costoMana} mana para usar esta habilidad.");
            return;
        }

        // Consumir recurso según el tipo de portador
        if (portador == null) return;

        bool recursoConsumido = false;

        if (portador.tipoPortador == PortadorJugable.TipoPortador.Mana && portador.sistemaMana != null)
        {
            recursoConsumido = portador.sistemaMana.ConsumirMana(portador.sistemaMana.CostoHabilidadAOE);
        }
        else if (portador.tipoPortador == PortadorJugable.TipoPortador.Vida && portador.sistemaVida != null)
        {
            recursoConsumido = portador.sistemaVida.ConsumirVida(portador.sistemaVida.CostoHabilidadAOE);
        }
                
        // Determinar posición para el efecto (ligeramente adelante del jugador)
        Vector3 posicion = portador.transform.position + portador.transform.forward * 2f;
        
        // Mostrar efecto visual
        if (efectoAOE != null)
        {
            GameObject efecto = Instantiate(efectoAOE, posicion, Quaternion.identity);
            IceNovaEffect iceNova = efecto.GetComponent<IceNovaEffect>();
            if (iceNova != null)
            {
                iceNova.Inicializar(daño, radio, capasObjetivos, portador);
                //Debug.Log($"IceNova inicializado con daño={daño}, radio={radio}");
            }
            else
            {
                // Si no tiene el componente, aplicar daño directamente como backup
                Debug.LogWarning("El prefab no tiene componente IceNovaEffect. Aplicando daño directamente.");
                AplicarDañoDirectamente(posicion);
            }
        }
        else
        {
            // Si no hay prefab, aplicar daño directamente
            Debug.LogWarning("No hay prefab de efecto asignado. Aplicando daño directamente.");
            AplicarDañoDirectamente(posicion);
        }
        
        // Registrar el último uso para el cooldown
        ultimoUso = Time.time;
        
    }
    
    // Método de respaldo por si falla el prefab
    private void AplicarDañoDirectamente(Vector3 posicion)
    {
        Collider[] objetivos = Physics.OverlapSphere(posicion, radio, capasObjetivos);
        
        int objetivosGolpeados = 0;
        foreach (var objetivo in objetivos)
        {
            // No dañar al jugador
            if (objetivo.GetComponent<PortadorJugable>() != null)
                continue;
            
            // Método 1: Usar la interfaz IDamageTaker
            IDamageTaker damageTaker = objetivo.GetComponent<IDamageTaker>();
            if (damageTaker == null)
                damageTaker = objetivo.GetComponentInParent<IDamageTaker>();
            
            if (damageTaker != null)
            {
                damageTaker.Damage(daño);
                objetivosGolpeados++;
                continue;
            }
            
            // Método 2: Alternativa usando PortadorGeneral directamente
            PortadorGeneral portadorGolpeado = objetivo.GetComponent<PortadorGeneral>();
            if (portadorGolpeado == null)
                portadorGolpeado = objetivo.GetComponentInParent<PortadorGeneral>();
            
            if (portadorGolpeado != null)
            {
                portadorGolpeado.Damage(daño);
                objetivosGolpeados++;
            }
        }
        
        //Debug.Log($"Habilidad AOE aplicó daño directo a {objetivosGolpeados} objetivos por {daño} de daño cada uno");
    }
}