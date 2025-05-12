using UnityEngine;

public class AreaCuracion : MonoBehaviour
{
    private PortadorJugable propietario;
    private int cantidadCuracionPorTick = 5;
    private float duracion = 5f;
    private float radio = 3f;
    private float tiempoVivo = 0f;
    private float tiempoEntreCuraciones = 1f; // Curar cada segundo
    private float ultimaCuracion = 0f;
    
    // Componentes opcionales para efectos visuales
    private MeshRenderer meshRenderer;
    
    public void Inicializar(PortadorJugable propietario, int cantidadCuracionTotal, float duracion, float radio)
    {
        this.propietario = propietario;
        this.duracion = duracion;
        this.radio = radio;
        
        // Calculamos cuánto curar en cada tick para que el total sea cantidadCuracionTotal
        int numTicks = Mathf.Max(1, Mathf.FloorToInt(duracion / tiempoEntreCuraciones));
        this.cantidadCuracionPorTick = cantidadCuracionTotal / numTicks;
        
        // Configurar collider
        SphereCollider areaTrigger = GetComponent<SphereCollider>();
        if (areaTrigger == null)
        {
            areaTrigger = gameObject.AddComponent<SphereCollider>();
        }
        areaTrigger.isTrigger = true;
        areaTrigger.radius = radio;
        
        // Ajustar escala visual si tiene un MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            transform.localScale = new Vector3(radio * 2, 0.1f, radio * 2); // Ajustar para un círculo plano
        }
        
        // Si tiene un sistema de partículas, ajustar su tamaño
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            var main = ps.main;
            main.startLifetime = duracion;
            
            var shape = ps.shape;
            shape.radius = radio;
        }
    }
    
    private void Update()
    {
        tiempoVivo += Time.deltaTime;
        
        // Si ha pasado el tiempo de vida, destruir el área
        if (tiempoVivo >= duracion)
        {
            // Opcional: efecto de desvanecimiento antes de destruir
            Destroy(gameObject);
            return;
        }
        
        // Curar periódicamente
        if (Time.time >= ultimaCuracion + tiempoEntreCuraciones)
        {
            CurarJugadoresEnArea();
            ultimaCuracion = Time.time;
        }
    }
    
    private void CurarJugadoresEnArea()
    {
        // Buscar todos los personajes dentro del radio
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio);
        
        foreach (var collider in colliders)
        {
            // Intentar curar a cualquier portador que tenga sistema de vida
            PortadorGeneral portador = collider.GetComponent<PortadorGeneral>();
            if (portador != null && portador.sistemaVida != null)
            {
                portador.sistemaVida.Curar(cantidadCuracionPorTick);
                
                // Puedes añadir efectos específicos si el portador es el propietario
                if (portador == propietario)
                {
                    Debug.Log($"El propietario {propietario.name} ha recibido {cantidadCuracionPorTick} de curación.");
                }
                else
                {
                    Debug.Log($"Aliado {portador.name} ha recibido {cantidadCuracionPorTick} de curación.");
                }
            }
        }
    }
}