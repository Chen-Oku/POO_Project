using UnityEngine;
using System.Collections.Generic;

public class SistemaHabilidades : MonoBehaviour
{
    public List<HabilidadBase> habilidades;

    public void UsarHabilidad(int index) {
        if (index >= 0 && index < habilidades.Count) {
            habilidades[index].Usar();
        }
    }

    public void AgregarHabilidad(HabilidadBase hab) {
        habilidades.Add(hab);
    }

    public void RemoverHabilidad(HabilidadBase hab) {
        habilidades.Remove(hab);
    }
}
