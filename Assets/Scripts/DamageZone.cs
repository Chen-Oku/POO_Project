using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [Tooltip("Cantidad de daño que recibe el jugador por tick")]
    [SerializeField] private int damageAmount = 5;
    
    [Tooltip("Tiempo entre cada aplicación de daño (en segundos)")]
    [SerializeField] private float damageInterval = 1f;
    
    private float timeSinceLastDamage = 0.1f;
    private IDamageTaker damageTaker;
    private PortadorGeneral portadorJugador;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            
            // Buscar directamente el PortadorGeneral o derivados
            portadorJugador = player.GetComponent<PortadorGeneral>();
            
            if (portadorJugador == null)
            {
                portadorJugador = player.GetComponentInChildren<PortadorGeneral>();
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si perdimos la referencia y recuperarla si es necesario
            if (portadorJugador == null)
            {
                GameObject player = other.gameObject;
                portadorJugador = player.GetComponent<PortadorGeneral>();
                if (portadorJugador == null)
                {
                    portadorJugador = player.GetComponentInChildren<PortadorGeneral>();
                }
            }
            
            // Incrementar el tiempo transcurrido
            timeSinceLastDamage += Time.deltaTime;
            
            // Aplicar daño cuando pase el intervalo
            if (timeSinceLastDamage >= damageInterval)
            {
                if (portadorJugador != null)
                {
                    portadorJugador.Damage(damageAmount);
                }
                timeSinceLastDamage = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portadorJugador = null;
        }
    }
    
    private void ApplyDamage()
    {
        if (damageTaker != null)
        {
            damageTaker.Damage(damageAmount);
            return;
        }

        // buscar directamente un PortadorGeneral si no encontramos IDamageTaker
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            PortadorGeneral portador = playerObject.GetComponent<PortadorGeneral>();
            if (portador == null)
            {
                portador = playerObject.GetComponentInChildren<PortadorGeneral>();
            }
            
            if (portador != null)
            {
                portador.Damage(damageAmount);
                //Debug.Log($"Aplicando daño de {damageAmount} directamente al PortadorGeneral");
            }
            else
            {
                //Debug.LogWarning("No se pudo encontrar un PortadorGeneral en el jugador");
            }
        }
    }
}