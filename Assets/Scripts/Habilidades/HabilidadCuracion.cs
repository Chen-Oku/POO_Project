using UnityEngine;


[CreateAssetMenu(fileName = "HabilidadBase", menuName = "Scriptable Objects/HabilidadBase")]

public class HabilidadCuracion : HabilidadBase
{
    public float condicionRegeneracion;
    public override void Usar() {
        // lógica de disparo
    }

    public void ConsumirMana(SistemaMana mana) {
        mana.ModificarValor(-10); // ejemplo
    }
}
