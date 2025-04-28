using UnityEngine;

public class PortadorJugable : PortadorGeneral
{
    private SistemaVida sistemaVida;
    private SistemaMana sistemaMana;
        public PortadorJugable(float vidaActual, float vidaMaxima, float manaActual, float manaMaximo) : base(vidaActual, vidaMaxima)
        {
            this.sistemaVida = new SistemaVida(vidaActual, vidaMaxima);
            this.sistemaMana = new SistemaMana(manaActual, manaMaximo);
        }
  
        public void Curar(float cantidad)
        {
            sistemaVida.curar(cantidad);
        }
        public void ConsumirMana(float cantidad)
        {
            sistemaMana.consumirMana(cantidad);
        }
        public void RegenerarMana(float cantidad)
        {
            sistemaMana.regenerarMana(cantidad);
        }
}

