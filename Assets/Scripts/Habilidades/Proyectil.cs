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
        /* // No dañar al lanzador
        PortadorJugable jugadorGolpeado = collision.gameObject.GetComponent<PortadorJugable>();
        if (jugadorGolpeado == lanzador) return;
        
        // Buscar si golpeó un portador (enemigo o dummy)
        PortadorGeneral portadorGolpeado = collision.gameObject.GetComponent<PortadorGeneral>();
        if (portadorGolpeado != null && portadorGolpeado.sistemaVida != null)
        {
            portadorGolpeado.sistemaVida.RecibirDaño(daño);
            Debug.Log($"¡Golpeó a {collision.gameObject.name} causando {daño} de daño!");
            
        } */

        // Si colisiona con el lanzador, destruir el proyectil y salir
    PortadorJugable jugadorGolpeado = collision.gameObject.GetComponent<PortadorJugable>();
    if (jugadorGolpeado == lanzador)
    {
        Destroy(gameObject); // Destruir el proyectil
        return;              // Salir del método para no ejecutar el resto del código
    }
    
    // Buscar si golpeó un portador (enemigo o dummy)
    PortadorGeneral portadorGolpeado = collision.gameObject.GetComponent<PortadorGeneral>();
    if (portadorGolpeado != null && portadorGolpeado.sistemaVida != null)
    {
        portadorGolpeado.sistemaVida.RecibirDaño(daño);
        Debug.Log($"¡Golpeó a {collision.gameObject.name} causando {daño} de daño!");
    }
        
        // Iniciar regreso después de golpear algo
        if (!debeRegresar)
        {
            rb.linearVelocity = Vector3.zero;
            debeRegresar = true;
        }
    }
}