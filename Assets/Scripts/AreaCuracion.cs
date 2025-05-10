using UnityEngine;

public class AreaCuracion : MonoBehaviour
{
    public int duracion = 5;
    public int cantidadCuracionPorSegundo = 5;

    private void Start()
    {
        Destroy(gameObject, duracion);
    }

    private void OnTriggerStay(Collider other)
    {
        PortadorJugable jugador = other.GetComponent<PortadorJugable>();
        if (jugador != null && jugador.sistemaVida != null)
        {
            jugador.sistemaVida.Curar((int)(cantidadCuracionPorSegundo * Time.deltaTime));
        }
    }
}