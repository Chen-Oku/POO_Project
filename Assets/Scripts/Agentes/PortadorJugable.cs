using UnityEngine;

public class PortadorJugable : PortadorGeneral 
{

    public SistemaMana sistemaMana { get; private set; } // Instancia para el maná
    
    public SistemaHabilidades sistemaHabilidades; // Sistema de habilidades
    public Transform puntoDisparo;
    
    private void Awake()
    {
        // Initialize the SistemaMana
        sistemaMana = new SistemaMana();
        sistemaMana.InicializarMana();
    }
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
                Debug.LogWarning("No se encontró un hijo llamado 'PuntoDisparo'. Se creó uno automáticamente en la posición local (0,0,0).");
            }
        }

        // Inicializa las estadísticas si no están configuradas

        // Crear una instancia de SistemaVida
        SistemaVida sistemaVida = new SistemaVida
        {
            vidaActual = 100,
            vidaMaxima = 100,
            vidaMinima = 0
        };

         // Modificar valores
        sistemaVida.RecibirDaño(20);
        sistemaVida.Curar(10);

        Debug.Log($"Vida actual: {sistemaVida.VidaActual}");

        
        // Inicializa el sistema de habilidades
        if (sistemaHabilidades == null)
        {
            sistemaHabilidades = gameObject.AddComponent<SistemaHabilidades>();
            Debug.Log("Se ha creado automáticamente un sistema de habilidades");
        }
    }
    private void Update()
    {
        // Agregar esto para manejar la regeneración automática
        sistemaMana?.ActualizarRegeneracion(Time.deltaTime);
    }
}
