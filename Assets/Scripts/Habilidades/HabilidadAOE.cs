using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadAOE", menuName = "Scriptable Objects/HabilidadBase")]

public class HabilidadAOE : HabilidadBase
{
    public float condicionRegeneracion;

    public float radio = 5f;
    public int daño = 10;
    public override int costoMana { get; set; } = 30;
    
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
        // Consumir mana
        SistemaMana mana = portador.GetComponent<SistemaMana>();
        if (mana != null)
        {
            mana.ModificarValor(-costoMana); // Consumir el costo de mana
        }
    }

    public void ConsumirMana(SistemaMana mana) {
        mana.ModificarValor(-10); // ejemplo
    }
}
