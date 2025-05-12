using UnityEngine;
using System.Collections;

public class PortadorNoJugable : PortadorGeneral
{
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private NPCVidaUI barraVidaNPC; // Referencia a la UI de vida para NPCs
    
    
    protected override void Awake()
    {
        base.Awake(); // Llamar al Awake del padre para inicializar sistemaVida
        
        // La inicialización de sistemaVida ya ocurre en PortadorGeneral.Awake()
        Debug.Log($"PortadorNoJugable inicializado con {sistemaVida.VidaActual}/{sistemaVida.VidaMaxima} de vida");
    }
    
    private void Start()
    {
        // Inicializar la barra de vida del NPC
        ConfigurarBarraVida();
    }
    
    private void ConfigurarBarraVida()
    {
        // Si no tenemos una barra de vida asignada, intentamos buscarla o crearla
        if (barraVidaNPC == null)
        {
            // Buscar primero en los hijos
            barraVidaNPC = GetComponentInChildren<NPCVidaUI>();
            
            // Si no existe, crear una nueva (opcional, dependiendo de tu implementación)
            if (barraVidaNPC == null && TryGetComponent<NPCVidaUI>(out var vidaUI))
            {
                barraVidaNPC = vidaUI;
            }
        }
        
        // Configurar la barra de vida si existe
        if (barraVidaNPC != null)
        {
            barraVidaNPC.ConfigurarConPortador(this);
            ActualizarUI(); // Actualizar UI inicialmente
        }
    }
    
    protected override void OnDamageReceived(int amount)
    {
        base.OnDamageReceived(amount);
        
        // Actualizar la barra de vida
        if (barraVidaNPC != null)
        {
            barraVidaNPC.ActualizarVida(sistemaVida.VidaActual, sistemaVida.VidaMaxima);
        }
        
        Debug.Log($"Enemigo {gameObject.name} recibió {amount} de daño. Vida restante: {sistemaVida.VidaActual}");
            
        // Comprobar si ha muerto
        if (sistemaVida.VidaActual <= 0)
        {
            StartCoroutine(Morir());
        }
    }
    
    protected override void OnHealReceived(int amount)
    {
        base.OnHealReceived(amount);
        
        // Actualizar la UI si existe
        ActualizarUI();
        
        Debug.Log($"Enemigo {name} fue curado por {amount}. Vida actual: {sistemaVida.VidaActual}");
    }
    
    private void ActualizarUI()
    {
        if (barraVidaNPC != null)
        {
            barraVidaNPC.ActualizarVida(sistemaVida.VidaActual, sistemaVida.VidaMaxima);
        }
    }
    
    private IEnumerator Morir()
    {
        // Desactivar componentes
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        
        // Mostrar efecto de muerte si existe
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }
        
        // Desactivar AI o comportamientos
        var aiComponent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (aiComponent != null)
        {
            aiComponent.enabled = false;
        }
        
        // Ejecutar animación de muerte si hay animador
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
            // Esperar a que termine la animación
            yield return new WaitForSeconds(2f);
        }
        else
        {
            // Si no hay animador, hacer algún efecto simple
            // Por ejemplo, hacer que el objeto caiga o se desvanezca
            Transform modelTransform = transform.Find("Model");
            if (modelTransform != null)
            {
                // Rotar el modelo para simular caída
                float tiempo = 0;
                Quaternion rotacionInicial = modelTransform.rotation;
                Quaternion rotacionFinal = Quaternion.Euler(90f, modelTransform.rotation.eulerAngles.y, modelTransform.rotation.eulerAngles.z);
                
                while (tiempo < 1f)
                {
                    modelTransform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempo);
                    tiempo += Time.deltaTime;
                    yield return null;
                }
                
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        // Desactivar el GameObject (podría ser útil para sistemas de pooling)
        gameObject.SetActive(false);
        
        // Destruir el GameObject si no usas pooling
        Destroy(gameObject);
    }
    
    // Limpiar eventos al destruir el objeto
    private void OnDestroy()
    {
        if (sistemaVida != null)
        {
            // Desuscribir cualquier evento para evitar memory leaks
            if (barraVidaNPC != null)
            {
                try
                {
                    // Use the -= operator to unsubscribe
                    sistemaVida.OnVidaCambiada -= barraVidaNPC.ActualizarVida;
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning($"Error al desuscribirse del evento: {ex.Message}");
                }
            }
        }
    }
}