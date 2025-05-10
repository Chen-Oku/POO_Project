using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    public GameObject prefabProyectil;  // Asigna el prefab de Mjolnir en el inspector
    public float velocidadProyectil = 15f;
    public int daño = 25;
    public float cooldownTiempo = 2f;
        


    public override void Usar(PortadorJugable portador) 
    {
        if (Time.time - ultimoUso < cooldown) return; // Verifica el cooldown
                
         // Verificar referencias
        if (prefabProyectil == null || portador == null || portador.puntoDisparo == null) 
        {
            Debug.LogError("Faltan referencias para lanzar el proyectil");
            return;
        }

        // Instanciar el martillo en el punto de disparo
        GameObject proyectil = Instantiate(
            prefabProyectil,
            portador.puntoDisparo.position,
            portador.puntoDisparo.rotation
        );

        // Configurar el proyectil
        Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();
        if (proyectilScript != null)
        {
            proyectilScript.Inicializar(daño, velocidadProyectil, portador);
        }
        else
        {
            // Si no tiene el componente Proyectil, agregamos un Rigidbody y físicas básicas
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();
            if (rb == null) rb = proyectil.AddComponent<Rigidbody>();
            
            rb.linearVelocity = portador.puntoDisparo.forward * velocidadProyectil;
        }

        // Consumir mana
        if (portador.sistemaMana != null)
        {
            portador.sistemaMana.ModificarValor(-costoMana); 
        }
        
        ultimoUso = Time.time;
        Debug.Log($"¡{portador.name} lanzó Mjolnir!");

    }
}

