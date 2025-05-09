using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    public GameObject prefabProyectil;
    public float daño;

    public override void Usar(PortadorJugable portador) 
    {
            if (Time.time - ultimoUso < cooldown) return; // Verifica el cooldown

        // Instanciar el proyectil en la posición del portador
        GameObject proyectil = Instantiate(prefabProyectil, portador.transform.position + portador.transform.forward, Quaternion.identity);

        // Configurar el proyectil (por ejemplo, daño y dirección)
        Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();
        if (proyectilScript != null)
        {
            proyectilScript.Inicializar(daño, portador.transform.forward);
        }

    ultimoUso = Time.time; // Actualizar el tiempo del último uso
        /*
        if (Time.time - ultimoUso < cooldown) return;

        // Instanciar el proyectil
         GameObject proyectil = Instantiate(prefabProyectil, portador.transform.position, Quaternion.identity);
        proyectil.GetComponent<Proyectil>().Inicializar(daño);

        // Aplicar daño al objetivo (si es necesario)
        if (portador != null)
        {
            var vida = portador.GetComponent<SistemaVida>();
            if (vida != null)
            {
                vida.RecibirDaño(daño);
            }
        } */

        ultimoUso = Time.time;
        
    }
}

/* internal class Proyectil
{
    internal void Inicializar(float daño)
    {
        throw new NotImplementedException();
    }
} */