using UnityEngine;


[CreateAssetMenu(fileName = "HabilidadBase", menuName = "Scriptable Objects/HabilidadBase")]

public class HabilidadCuracion : HabilidadBase
{
    public float condicionRegeneracion;
    public float cantidadCuracion = 20f;

        public override void Usar(PortadorJugable portador) 
        {

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
