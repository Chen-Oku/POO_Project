using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadCuracion", menuName = "Scriptable Objects/Habilidad Curacion")]

public class HabilidadCuracion : HabilidadBase
{

    public int cantidadCuracion = 30;
    public float cooldownTiempo = 2f;
    public GameObject efectoCuracion; // Efecto visual para la curación
    
    public override void Usar(PortadorJugable portador)
    {
        base.costoMana = 20;

        if (Time.time - ultimoUso < cooldown) 
        {
            Debug.Log("Habilidad en cooldown, espera un momento.");
            return;
        }
        
        if (portador == null) return;
        
        // Verificar si hay suficiente mana antes de usar la habilidad
        if (portador.sistemaMana != null && !portador.sistemaMana.TieneSuficienteMana(costoMana))
        {
            Debug.Log($"Mana insuficiente. Necesitas {costoMana} mana para usar esta habilidad.");
            return;
        }
        
        // Aplicar curación
        if (portador.sistemaVida != null)
        {
            // Consumir mana primero
            if (portador.sistemaMana != null)
            {
                portador.sistemaMana.ConsumirMana(costoMana);
                Debug.Log($"Has consumido {costoMana} puntos de maná. Maná restante: {portador.sistemaMana.ManaActual}");
            }
            
            // Aplicar la curación
            portador.sistemaVida.Curar(cantidadCuracion);
            Debug.Log($"{portador.name} se ha curado {cantidadCuracion} puntos de vida.");
            
            // Mostrar efecto visual
            if (efectoCuracion != null)
            {
                GameObject efecto = Instantiate(efectoCuracion, portador.transform.position, Quaternion.identity);
                efecto.transform.SetParent(portador.transform);
                Destroy(efecto, 2f); // Destruir el efecto después de 2 segundos
            }
            
            ultimoUso = Time.time;
        }
    }
}