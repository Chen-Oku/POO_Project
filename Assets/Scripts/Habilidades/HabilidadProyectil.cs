using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    [SerializeField] private GameObject prefabProyectil;
    public int daño;
    
    public override void Usar(PortadorJugable portador) 
    {
            if (Time.time - ultimoUso < cooldown) return; // Verifica el cooldown
                
        Debug.Log(prefabProyectil); // Debug para verificar el prefab
        Debug.Log(portador);// Debug para verificar el portador

        // Instanciar el proyectil en la posición del portador
        GameObject proyectil = Instantiate(
            prefabProyectil,
            portador.puntoDisparo.position,
            Quaternion.LookRotation(portador.puntoDisparo.forward)
        );

        // Configurar el proyectil (por ejemplo, daño y dirección)
        Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();
        if (proyectilScript != null)
        {
            proyectilScript.Inicializar(daño, portador.puntoDisparo.forward);
        }

        // Consumir mana
        SistemaMana mana = portador.sistemaMana;
        if (mana != null)
        {
            mana.ModificarValor(-costoMana); // Consumir el costo de mana
        }
        ultimoUso = Time.time; // Actualizar el tiempo del último uso

    }
}

/* internal class Proyectil
{
    internal void Inicializar(float daño)
    {
        throw new NotImplementedException();
    }
} */