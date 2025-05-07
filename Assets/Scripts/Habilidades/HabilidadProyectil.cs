using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadProyectil", menuName = "Scriptable Objects/Habilidad Proyectil")]
public class HabilidadProyectil : HabilidadBase
{
    public float condicionRegeneracion;

    public override void Usar()
    {
        // Aquí va la lógica para lanzar el proyectil.
        Debug.Log("Habilidad de proyectil usada.");
    }
}
