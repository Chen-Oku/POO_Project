using UnityEngine;
using System.Collections;

public class IceNovaEffect : MonoBehaviour
{
    [Header("Configuración Visual")]
    [SerializeField] private float duracion = 2f;
    [SerializeField] private AnimationCurve escalaCurva;
    [SerializeField] private bool aplicaDañoContinuo = false;
    [SerializeField] private float intervaloTick = 0.5f; // Para daño continuo
    
    // Variables que vendrán de HabilidadAOE
    private float radio;
    private int daño;
    private LayerMask capasObjetivos;
    private PortadorJugable lanzador;
    
    private void Start()
    {
        // Auto-destrucción después de la duración
        Destroy(gameObject, duracion);
        
        // Iniciar animación de escala
        //StartCoroutine(AnimarEscala());
        
        // Si el daño es continuo, iniciar corrutina de ticks de daño
        if (aplicaDañoContinuo)
        {
            StartCoroutine(AplicarDañoContinuo());
        }
        else
        {
            // Aplicar daño una sola vez
            AplicarDañoEnArea();
        }
    }
    
    // Método para inicializar externamente desde HabilidadAOE
    public void Inicializar(int daño, float radio, LayerMask capas, PortadorJugable lanzador = null)
    {
        this.daño = daño;
        this.radio = radio;
        this.capasObjetivos = capas;
        this.lanzador = lanzador;
        
        Debug.Log($"IceNovaEffect inicializado: daño={daño}, radio={radio}");
    }
    
    /* private IEnumerator AnimarEscala()
    {
        float tiempoTranscurrido = 0f;
        Vector3 escalaInicial = transform.localScale;
        
        while (tiempoTranscurrido < duracion)
        {
            float t = tiempoTranscurrido / duracion;
            float factorEscala = escalaCurva != null ? escalaCurva.Evaluate(t) : 1.0f;
            
            transform.localScale = escalaInicial * factorEscala;
            
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    } */
    
    private void AplicarDañoEnArea()
    {
        // Verificar que tenemos valores válidos para radio
        if (radio <= 0f)
        {
            Debug.LogWarning("IceNovaEffect: Radio no inicializado correctamente");
            return;
        }
        
        // CORRECCIÓN: Usa el radio como segundo parámetro en OverlapSphere
        Collider[] objetivos = Physics.OverlapSphere(transform.position, radio, capasObjetivos);
        
        int objetivosGolpeados = 0;
        foreach (var objetivo in objetivos)
        {
            // No queremos dañar al lanzador con su propia habilidad
            if (lanzador != null && objetivo.gameObject == lanzador.gameObject)
                continue;
                
            // Buscar si tiene un IDamageTaker
            IDamageTaker damageTaker = objetivo.GetComponent<IDamageTaker>();
            if (damageTaker == null)
                damageTaker = objetivo.GetComponentInParent<IDamageTaker>();
            
            if (damageTaker != null)
            {
                damageTaker.Damage(daño);
                objetivosGolpeados++;
            }
        }
        
        if (objetivosGolpeados > 0)
        {
            Debug.Log($"IceNova golpeó a {objetivosGolpeados} objetivos por {daño} de daño cada uno");
        }
    }
    
    private IEnumerator AplicarDañoContinuo()
    {
        float tiempoTranscurrido = 0f;
        
        while (tiempoTranscurrido < duracion)
        {
            AplicarDañoEnArea();
            yield return new WaitForSeconds(intervaloTick);
            tiempoTranscurrido += intervaloTick;
        }
    }
    
}