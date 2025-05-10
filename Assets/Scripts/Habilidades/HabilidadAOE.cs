using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadAOE", menuName = "Scriptable Objects/Habilidad AOE")]
public class HabilidadAOE : HabilidadBase
{
    public float radio = 5f;
    public int daño = 15;
    public float cooldownTiempo = 2f;
    [SerializeField] private int costoDeMana = 30; // Nuevo campo serializado
    public GameObject efectoAOE; // Efecto visual del AOE
    public LayerMask capasObjetivos; // Para filtrar qué objetos son afectados
    
    public override void Usar(PortadorJugable portador)
    {
        // Asegúrate de que estás modificando la instancia correcta
        Debug.Log($"ID de la habilidad: {this.GetHashCode()}");
        base.costoMana = costoDeMana; // Asignar el costo de mana
        
        Debug.Log($"Costo de maná de la habilidad: {costoMana}");
        
        if (Time.time - ultimoUso < cooldown)
        {
            Debug.Log("Habilidad en cooldown, espera un momento.");
            return;
        }
        
        if (portador == null) return;
        
        Debug.Log($"Sistema de maná nulo?: {portador.sistemaMana == null}");
        Debug.Log($"ID del sistema de maná: {portador.sistemaMana.GetHashCode()}");
        
        // Verificar si hay suficiente mana antes de usar la habilidad
        if (portador.sistemaMana != null && !portador.sistemaMana.TieneSuficienteMana(costoMana))
        {
            Debug.Log($"Mana insuficiente. Necesitas {costoMana} mana para usar esta habilidad.");
            return;
        }
        
        // Consumir mana primero
        if (portador.sistemaMana != null)
        {
            portador.sistemaMana.ConsumirMana(costoMana);
            Debug.Log($"Has consumido {costoMana} puntos de maná. Maná restante: {portador.sistemaMana.ManaActual}");
            
            // Fuerza una actualización de la UI directamente
            ManaUI manaUI = FindAnyObjectByType<ManaUI>();
            if (manaUI != null)
            {
                manaUI.ActualizarUIMana(); // Asegúrate de que este método sea público
            }
        }
        
        // Mostrar efecto visual
        if (efectoAOE != null)
        {
            GameObject efecto = Instantiate(efectoAOE, portador.transform.position, Quaternion.identity);
            efecto.transform.localScale = new Vector3(radio * 2, radio * 2, radio * 2);
            Destroy(efecto, 2f);
        }
        
        // Detectar enemigos en el área
        Collider[] objetivos = Physics.OverlapSphere(portador.transform.position, radio, capasObjetivos);
        
        int objetivosGolpeados = 0;
        foreach (var objetivo in objetivos)
        {
            // No dañar al portador
            if (objetivo.gameObject == portador.gameObject)
                continue;
            
            // Buscar si tiene un PortadorGeneral con sistemaVida
            PortadorGeneral portadorGolpeado = objetivo.GetComponent<PortadorGeneral>();
            if (portadorGolpeado != null && portadorGolpeado.sistemaVida != null)
            {
                portadorGolpeado.sistemaVida.RecibirDaño(daño);
                objetivosGolpeados++;
            }
        }
        
        Debug.Log($"Habilidad AOE afectó a {objetivosGolpeados} objetivos por {daño} de daño cada uno.");
        
        ultimoUso = Time.time;
    }   
}