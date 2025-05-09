using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadAOE", menuName = "Scriptable Objects/HabilidadBase")]

public class HabilidadAOE : HabilidadBase
{
    public float condicionRegeneracion;

    public float radio = 5f;
    public int daño = 10;
    
    public override void Usar(PortadorJugable portador) 
    {
        if (portador == null) return;

        Collider[] enemigos = Physics.OverlapSphere(portador.transform.position, radio);
        foreach (var enemigo in enemigos)
        {
            var vida = enemigo.GetComponent<SistemaVida>();
            if (vida != null)
            {
                vida.RecibirDaño(daño);
            }
        }

        Debug.Log($"Habilidad AOE usada. Daño: {daño}, Radio: {radio}");
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
