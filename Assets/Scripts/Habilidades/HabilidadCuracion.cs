using UnityEngine;


[CreateAssetMenu(fileName = "NuevaHabilidadCuracion", menuName = "Scriptable Objects/HabilidadBase")]

public class HabilidadCuracion : HabilidadBase
{
    public int condicionRegeneracion;
    public int cantidadCuracion = 20;
    public override int costoMana { get; set; } = 15;

        public override void Usar(PortadorJugable portador) 
        {
            if (portador == null || portador.sistemaVida == null) return;

        portador.sistemaVida.Curar(cantidadCuracion);
        Debug.Log($"Curaste {cantidadCuracion} puntos de vida.");

        SistemaMana mana = portador.GetComponent<SistemaMana>();
            if (mana != null)
            {
                mana.ModificarValor(-costoMana); // Consumir el costo de mana
            }
        }
    // Consumir mana

    public void ConsumirMana(SistemaMana mana) 
    {
        mana.ModificarValor(-10); // ejemplo
    }
}
