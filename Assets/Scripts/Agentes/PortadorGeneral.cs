using UnityEngine;

public class PortadorGeneral : MonoBehaviour, IDamageTaker
{
    public SistemaVida sistemaVida;

    public void Damage(float amount) {
        sistemaVida.ModificarValor((int)-amount);
    }

    public void Heal() {
        sistemaVida.ModificarValor(10); // ejemplo
    }
}
