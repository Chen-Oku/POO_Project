using UnityEngine;

// Componente auxiliar para garantizar que el jugador sea detectado como IDamageTaker
public class PlayerDamageTaker : MonoBehaviour, IDamageTaker
{
    private PortadorGeneral portador;
    
    private void Awake()
    {
        // Buscar un PortadorGeneral o PortadorJugable en este objeto o en el padre
        portador = GetComponent<PortadorGeneral>();
        if (portador == null)
        {
            portador = GetComponentInParent<PortadorGeneral>();
        }
        
        if (portador == null)
        {
            Debug.LogError("PlayerDamageTaker requiere un componente PortadorGeneral en el mismo objeto o en el padre");
        }
    }
    
    public void Damage(int amount)
    {
        if (portador != null)
        {
            portador.Damage(amount);
            Debug.Log($"PlayerDamageTaker redirigió {amount} de daño al portador");
        }
    }
    
    public void Heal(int amount)
    {
        if (portador != null)
        {
            portador.Heal(amount);
        }
    }
}