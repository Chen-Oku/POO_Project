using UnityEngine;
using System.Collections.Generic;

public class SistemaHabilidades : MonoBehaviour
{
    [SerializeField] private int maxHabilidades = 3;
    public List<HabilidadBase> habilidades= new List<HabilidadBase>();

    /* public void UsarHabilidad(int index) {
        if (index >= 0 && index < habilidades.Count) {
            habilidades[index].Usar();
        }
    } */
    public void AgregarHabilidad(HabilidadBase hab) {
        if (habilidades.Count < maxHabilidades) {
            if (!habilidades.Contains(hab)) {
                habilidades.Add(hab);
                Debug.Log($"Habilidad agregada: {hab.nombre}");
            }
        } else {
            Debug.LogWarning($"Límite de {maxHabilidades} habilidades alcanzado. No se puede agregar más.");
        }
    }
    public void RemoverHabilidad(HabilidadBase hab) {
        habilidades.Remove(hab);
    }

    public HabilidadBase ObtenerHabilidad(int index) 
    {
        if (index >= 0 && index < habilidades.Count) 
        {
            return habilidades[index];
        }
        return null;
    }
}

