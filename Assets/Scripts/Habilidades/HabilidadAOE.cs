using UnityEngine;

public class HabilidadAOE : HabilidadBase
{
    public float condicionRegeneracion;
    public override void Usar() {
        // lógica de disparo
    }

    public void ConsumirMana(SistemaMana mana) {
        mana.ModificarValor(-10); // ejemplo
    }
}
