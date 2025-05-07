using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    public GameObject prefabProyectil;
    public float daño;

    public override void Usar(GameObject portador, SistemaVida vida, SistemaMana mana, GameObject objetivo = null) {
        if (Time.time - ultimoUso < cooldown) return;

        // Lógica para lanzar proyectil
        GameObject proyectil = Instantiate(prefabProyectil, portador.transform.position + portador.transform.forward, portador.transform.rotation);
        proyectil.GetComponent<Proyectil>().Inicializar(daño);
        ultimoUso = Time.time;
    }
}

internal class Proyectil
{
    internal void Inicializar(float daño)
    {
        throw new NotImplementedException();
    }
}