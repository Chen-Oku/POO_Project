using UnityEngine;

public class PortadorJugable : PortadorGeneral 
{

    public SistemaMana sistemaMana; // Instancia para el maná
    public SistemaHabilidades sistemaHabilidades; // Sistema de habilidades
    public Transform puntoDisparo;
    

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

        if (sistemaMana == null)
        {
            sistemaMana = new SistemaMana
            {
                manaActual = 50,
                manaMaximo = 100,
                manaMinimo = 0
            };
        }

        // Inicializa el sistema de habilidades
        if (sistemaHabilidades == null)
        {
            sistemaHabilidades = gameObject.AddComponent<SistemaHabilidades>();
        }
        sistemaHabilidades.AgregarHabilidad(ScriptableObject.CreateInstance<HabilidadProyectil>());
        sistemaHabilidades.AgregarHabilidad(ScriptableObject.CreateInstance<HabilidadCuracion>());
        sistemaHabilidades.AgregarHabilidad(ScriptableObject.CreateInstance<HabilidadAOE>());
    }

    private void Update()
    {
        
    }
}
