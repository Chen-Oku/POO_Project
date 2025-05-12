using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private int daño;
    private float velocidad;
    private PortadorJugable lanzador;
    
    
    // Para control de retorno del martillo
    private bool debeRegresar = false;
    private float tiempoVuelo = 0f;
    private float tiempoMaximoVuelo = 2f;
    
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }
    private void Update()
    {
        // Si el martillo debe regresar al jugador
        if (debeRegresar && lanzador != null)
        {
            Vector3 direccionAlJugador = (lanzador.puntoDisparo.position - transform.position).normalized;
            rb.linearVelocity = direccionAlJugador * velocidad * 1.5f;
            
            // Si está muy cerca del jugador, destruir
            if (Vector3.Distance(transform.position, lanzador.puntoDisparo.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Control de tiempo de vuelo
            tiempoVuelo += Time.deltaTime;
            if (tiempoVuelo >= tiempoMaximoVuelo)
            {
                debeRegresar = true;
            }
        }
    }

    public void Inicializar(int daño, float velocidad, PortadorJugable portador)
    {
        this.daño = daño;
        this.velocidad = velocidad;
        this.lanzador = portador;
        
        // Inicializa el campo rb de la clase, no una variable local
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        // Ignorar colisiones físicas con el lanzador específicamente
        if (lanzador != null)
        {
            Collider proyectilCollider = GetComponent<Collider>();
            Collider lanzadorCollider = lanzador.GetComponent<Collider>();
            
            if (proyectilCollider != null && lanzadorCollider != null)
            {
                Physics.IgnoreCollision(proyectilCollider, lanzadorCollider, true);
            }
        }

   
        // Rotar el modelo para que quede horizontal
        transform.Rotate(90f, 0f, 0f);
        
        // Obtener la dirección del lanzador si está disponible
        Vector3 direccion = transform.forward;
        if (lanzador != null && lanzador.puntoDisparo != null)
        {
            direccion = lanzador.puntoDisparo.forward;
        }
    
    // Aplicar velocidad en la dirección del lanzador
    rb.linearVelocity = direccion * velocidad;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ignorar colisión con el lanzador
        if (collision.gameObject.TryGetComponent<PortadorJugable>(out var jugadorImpactado) && jugadorImpactado == lanzador)
        {
            return;
        }
        
        IDamageTaker objetoImpactado = collision.gameObject.GetComponent<IDamageTaker>();
        if (objetoImpactado == null)
        {
            // Buscar en los objetos padres por si el collider está en un hijo
            objetoImpactado = collision.gameObject.GetComponentInParent<IDamageTaker>();
        }
        
        // Aplicar daño usando la interfaz
        if (objetoImpactado != null)
        {
            objetoImpactado.Damage(daño);
            Debug.Log($"Proyectil impactó a {collision.gameObject.name} causando {daño} de daño!");
            
            // Crear efecto visual de impacto si tienes uno
            // Instantiate(efectoImpacto, collision.contacts[0].point, Quaternion.identity);
        }
        
        // Iniciar regreso o destrucción después de golpear algo
        if (!debeRegresar)
        {
            rb.linearVelocity = Vector3.zero; // Detener el proyectil
            debeRegresar = true;
        }
        else
        {
            // Si ya estaba regresando, destruirlo al impactar con cualquier cosa
            Destroy(gameObject);
        }
    }
}