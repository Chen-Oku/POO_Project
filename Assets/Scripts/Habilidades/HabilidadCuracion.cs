using UnityEngine;


[CreateAssetMenu(fileName = "NuevaHabilidadCuracion", menuName = "Scriptable Objects/HabilidadBase")]

public class HabilidadCuracion : HabilidadBase
{
    public int condicionRegeneracion;
    public int cantidadCuracion = 20;

        public override void Usar(PortadorJugable portador) 
        {
            if (portador == null || portador.sistemaVida == null) return;

        portador.sistemaVida.Curar(cantidadCuracion);
        Debug.Log($"Curaste {cantidadCuracion} puntos de vida.");
        }

     /*public override void Usar(SistemaVida vida, SistemaMana mana, GameObject objetivo = null) {
        if (Time.time - ultimoUso < cooldown) return;

        var sistemaVida = portador.GetComponent<SistemaVida>();
        if (sistemaVida != null) {
            sistemaVida.Curar(cantidadCuracion);
            ultimoUso = Time.time;
        } 
    }*/
    public void ConsumirMana(SistemaMana mana) {
        mana.ModificarValor(-10); // ejemplo
    }
}
