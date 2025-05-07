using UnityEngine;

public class HabilidadAOE : HabilidadBase
{
    public float condicionRegeneracion;

    public float radio = 5f;
    public float daño = 10f;
    
    public override void Usar(/*SistemaVida vida, SistemaMana mana, GameObject objetivo = null*/) {
       /*  Collider[] enemigos = Physics.OverlapSphere(vida.transform.position, radio);
        foreach (var enemigo in enemigos) {
            // Aplica daño a los enemigos en el área
            var vidaEnemigo = enemigo.GetComponent<SistemaVida>();
            if (vidaEnemigo != null) {
                vidaEnemigo.RecibirDaño(daño);
            }
        } */
    }

    public void ConsumirMana(SistemaMana mana) {
        mana.ModificarValor(-10); // ejemplo
    }
}
