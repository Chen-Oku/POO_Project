using UnityEngine;

public class PortadorNoJugable : PortadorGeneral
{
    [SerializeField] private GameObject indicadorVida; // Prefab del indicador de vida
    [SerializeField] private Transform puntoIndicador; // Punto donde aparecerá el indicador
    [SerializeField] private GameObject efectoMuerte;
    
    private NPCVidaIU barraVida;
    
    protected override void Awake()  // La palabra clave 'virtual' es crucial aquí
    {
        // Inicializar el sistema de vida
        sistemaVida = new SistemaVida(vidaMaxima, vidaInicial);
    }
    
    private void CrearIndicadorVida()
    {
        if (indicadorVida != null)
        {
            Vector3 posicion = puntoIndicador != null ? puntoIndicador.position : transform.position + Vector3.up * 2f;
            GameObject indicador = Instantiate(indicadorVida, posicion, Quaternion.identity);
            indicador.transform.SetParent(transform); // Hacer hijo del enemigo
            
            barraVida = indicador.GetComponent<NPCVidaIU>();
            if (barraVida != null)
            {
                barraVida.InicializarBarra(this);
                
                if (sistemaVida != null)
                {
                    sistemaVida.OnVidaCambiada += barraVida.ActualizarVida;
                    barraVida.ActualizarVida(); // Actualizar inicialmente
                }
            }
        }
    }
    
    protected override void OnDamageReceived(int amount)
    {
        // Efectos específicos del enemigo al recibir daño
        Debug.Log($"Enemigo {name} recibió {amount} de daño. Vida restante: {sistemaVida.VidaActual}");
        
        // Comprobar si ha muerto
        if (sistemaVida.VidaActual <= 0)
        {
            StartCoroutine(Morir());
        }
    }
    
    private System.Collections.IEnumerator Morir()
    {
        // Desactivar componentes
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        
        // Animación de muerte o efectos
        // animator.SetTrigger("Die");
        
        yield return new WaitForSeconds(2f); // Esperar a que termine la animación
        
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        // Limpiar eventos
        if (sistemaVida != null && barraVida != null)
        {
            sistemaVida.OnVidaCambiada -= barraVida.ActualizarVida;
        }
    }
}