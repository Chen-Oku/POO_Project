using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    public GameObject prefabProyectil;
    public float daño;

    public override void Usar() 
    {
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