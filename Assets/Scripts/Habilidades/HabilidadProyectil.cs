using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    public GameObject prefabProyectil;
    public int daño;

    [SerializeField] // Ensure Unity serializes this version
    public new int costoMana { get; set; } = 5;
    

    public override void Usar(PortadorJugable portador) 
    {
            if (Time.time - ultimoUso < cooldown) return; // Verifica el cooldown
                
        Debug.Log(prefabProyectil);
        Debug.Log(portador);

        // Instanciar el proyectil en la posición del portador
        GameObject proyectil = Instantiate(prefabProyectil, portador.transform.position + portador.transform.forward, Quaternion.identity);

        // Configurar el proyectil (por ejemplo, daño y dirección)
        Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();

        if (proyectilScript != null)
        {
            proyectilScript.Inicializar(daño, portador.transform.forward);
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