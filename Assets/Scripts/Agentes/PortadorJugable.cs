using UnityEngine;
using System.Collections;

public class PortadorJugable : PortadorGeneral
{
    [Header("Configuración de Jugador")]
    [SerializeField] private VidaUI vidaUI; // UI de vida
    public SistemaMana sistemaMana { get; private set; } // Instancia para el maná
    public SistemaHabilidades sistemaHabilidades; // Sistema de habilidades
    public TipoPortador tipoPortador = TipoPortador.Mana; // O Vida, según el caso
    public Transform puntoDisparo;

   [Header("Configuración de Spawn")]
    [SerializeField] private Transform puntoSpawn; // El punto donde aparecerá/reaparecerá
    [SerializeField] private float tiempoRespawn = 2f; // Tiempo antes de respawnear

    public enum TipoPortador
    {
        Mana,
        Vida
    }

    protected override void Awake()
    {
        base.Awake(); // Llamar al Awake del padre para inicializar sistemaVida

        // Initialize the SistemaMana
        sistemaMana = new SistemaMana();
        sistemaMana.InicializarMana();
        ActualizarUI();
    }
    //==================//
    private void Update()
    {
        // Agregar esto para manejar la regeneración automática
        sistemaMana?.ActualizarRegeneracion(Time.deltaTime);
        sistemaVida?.ActualizarRegeneracion(Time.deltaTime);
    }

    // Método para alternar el tipo de portador
    public void CambiarTipoPortador()
    {
        if (tipoPortador == TipoPortador.Mana)
        {
            tipoPortador = TipoPortador.Vida;
            Debug.Log("¡Ahora eres un portador de VIDA!");
        }
        else
        {
            tipoPortador = TipoPortador.Mana;
            Debug.Log("¡Ahora eres un portador de MANA!");
        }
        ActualizarUI();
    }

    protected override void OnDamageReceived(int amount)
    {
        base.OnDamageReceived(amount);
        ActualizarUI();

        // Comprobar si ha muerto
        if (sistemaVida.VidaActual <= 0)
        {
            StartCoroutine(MorirYRespawnear());
        }
    }

    private IEnumerator MorirYRespawnear()
    {
        // Desactivar control del jugador
        DesactivarControl();
                
        Debug.Log("Jugador ha muerto. Respawneando en " + tiempoRespawn + " segundos...");
        
        // Esperar el tiempo de respawn
        yield return new WaitForSeconds(tiempoRespawn);
        
        // Restaurar vida
        sistemaVida.RestaurarVidaCompleta();
        ActualizarUI();
        
        // Restaurar maná
        if (sistemaMana != null)
        {
            sistemaMana.RestaurarManaCompleto();
            ActualizarUI();
        }
        
        // Reposicionar al jugador en el punto de spawn
        RespawnearEnPuntoSpawn();
        
        // Reactivar control del jugador
        ActivarControl();
        
        Debug.Log("Jugador respawneado con vida completa.");
    }
    private void RespawnearEnPuntoSpawn()
    {
        // Determinar el punto de spawn a usar
        Transform puntoDestino = null;
        
        if (puntoSpawn != null)
        {
            puntoDestino = puntoSpawn; // Usar el punto específico para este jugador
        }
        else
        {
            return;
        }
        
        // Mover al jugador al punto de spawn
        transform.position = puntoDestino.position;
        transform.rotation = puntoDestino.rotation;
    }

    // Métodos para desactivar/activar el control del jugador
    private void DesactivarControl()
    {
        // Desactivar componentes específicos que controlan al jugador
        // Por ejemplo, si usas un CharacterController:
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;
        
        // O si tienes un script de movimiento personalizado:
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this && script.GetType().Name.Contains("Control") ||
                script.GetType().Name.Contains("Movement") || 
                script.GetType().Name.Contains("Input"))
            {
                script.enabled = false;
            }
        }
    }

    private void ActivarControl()
    {
        // Reactivar componentes específicos que controlan al jugador
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null) controller.enabled = true;
        
        // Y reactivar scripts de control
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this && script.GetType().Name.Contains("Control") ||
                script.GetType().Name.Contains("Movement") || 
                script.GetType().Name.Contains("Input"))
            {
                script.enabled = true;
            }
        }
    }
    
    protected override void OnHealReceived(int amount)
    {
       base.OnHealReceived(amount);
        ActualizarUI();
        //Debug.Log($"Jugador recibió {amount} de curación. Vida actual: {sistemaVida.VidaActual}");
    }
    //==================//

    private void Start()
    {
        // Buscar automáticamente el punto de disparo si no está asignado
        if (puntoDisparo == null)
        {
            Transform encontrado = transform.Find("PuntoDisparo"); 
            if (encontrado != null)
            {
                puntoDisparo = encontrado;
            }
            else
            {
                // Si no existe, créalo automáticamente
                GameObject nuevoPunto = new GameObject("PuntoDisparo");
                nuevoPunto.transform.SetParent(transform);
                nuevoPunto.transform.localPosition = Vector3.zero; // Puedes ajustar la posición
                puntoDisparo = nuevoPunto.transform;
                //Debug.LogWarning("No se encontró un hijo llamado 'PuntoDisparo'. Se creó uno automáticamente en la posición local (0,0,0).");
            }
        }

            // Crear una instancia de SistemaVida
        SistemaVida sistemaVida = new SistemaVida
        {
            vidaActual = 100,
            vidaMaxima = 100,
            vidaMinima = 0
        };

        /*  // Modificar valores
        sistemaVida.RecibirDaño(20);
        sistemaVida.Curar(10); 
 */
        
        // Inicializa el sistema de habilidades
        if (sistemaHabilidades == null)
        {
            sistemaHabilidades = gameObject.AddComponent<SistemaHabilidades>();
            //Debug.Log("Se ha creado automáticamente un sistema de habilidades");
        }
    }
    private void ActualizarUI()
    {
        if (vidaUI != null)
        {
            vidaUI.ActualizarUIVida(sistemaVida.VidaActual, sistemaVida.VidaMaxima);
        }
    }
}
