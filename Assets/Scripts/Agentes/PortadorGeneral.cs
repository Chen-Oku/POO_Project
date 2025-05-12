using UnityEngine;

public class PortadorGeneral : MonoBehaviour, IDamageTaker
{
    public SistemaVida sistemaVida { get; protected set; }
    
    [SerializeField] protected int vidaMaxima = 100;
    [SerializeField] protected int vidaInicial = 100;
    
    protected virtual void Awake()
    {
        // Inicializar el sistema de vida
        sistemaVida = new SistemaVida(vidaMaxima, vidaInicial);
    }
    
    public virtual void Damage(int amount)
    {
        if (sistemaVida != null)
        {
            sistemaVida.RecibirDaño((int)amount);
            OnDamageReceived(amount); // Método virtual para comportamiento específico
        }
    }
    
    public virtual void Heal(int amount)
    {
        if (sistemaVida != null)
        {
            sistemaVida.Curar((int)amount);
            OnHealReceived(amount); // Método virtual para comportamiento específico
        }
    }
    
    // Métodos virtuales para sobrescribir en clases hijas
    protected virtual void OnDamageReceived(int amount) { }
    protected virtual void OnHealReceived(int amount) { }
    
    // Para compatibilidad con el código existente
    public void Heal()
    {
        Heal(10);
    }
}