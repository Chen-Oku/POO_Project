using UnityEngine;

public class ManaRecovery : MonoBehaviour
{
    public int cantidadRecuperacion = 10; // Cantidad de maná a recuperar por segundo
    public int tiempoRecuperacion = 1; // Intervalo de tiempo entre recuperaciones

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
                jugador.sistemaMana.ModificarValor(cantidadRecuperacion);
                tiempoUltimaRecuperacion = Time.time;

                Debug.Log($"Maná recuperado: {cantidadRecuperacion}. Maná actual: {jugador.sistemaMana.manaActual}");
            }
        }
    }
}