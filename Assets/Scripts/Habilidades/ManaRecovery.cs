using UnityEngine;

public class ManaRecovery : MonoBehaviour
{
    public int cantidadRecuperacion = 10; // Cantidad de maná a recuperar por segundo
    public float tiempoRecuperacion = 1f; // Intervalo de tiempo entre recuperaciones

    private float tiempoUltimaRecuperacion;

    private void OnTriggerStay(Collider other)
    {
        // Verificar si el objeto que entra tiene el componente PortadorJugable
        PortadorJugable jugador = other.GetComponent<PortadorJugable>();
        if (jugador != null && jugador.sistemaMana != null)
        {
            // Recuperar maná si ha pasado suficiente tiempo
            if (Time.time >= tiempoUltimaRecuperacion + tiempoRecuperacion)
            {
                // Usar RecuperarMana en lugar de ModificarValor
                jugador.sistemaMana.RecuperarMana(cantidadRecuperacion);
                tiempoUltimaRecuperacion = Time.time;   
            }
        }
    }
}