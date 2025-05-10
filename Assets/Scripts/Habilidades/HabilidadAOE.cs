using UnityEngine;

[CreateAssetMenu(fileName = "NuevaHabilidadAOE", menuName = "Scriptable Objects/Habilidad AOE")]

public class HabilidadAOE : HabilidadBase
{
    public float radio = 5f;
    public int daño = 15;
    public float cooldownTiempo = 2f;
    public GameObject efectoAOE; // Efecto visual del AOE
    public LayerMask capasObjetivos; // Para filtrar qué objetos son afectados

    
    public override void Usar(PortadorJugable portador)
    {
        {
            if (Time.time - ultimoUso < cooldown) return;
            
            if (portador == null) return;
            
            // Mostrar efecto visual
            if (efectoAOE != null)
            {
                GameObject efecto = Instantiate(efectoAOE, portador.transform.position, Quaternion.identity);
                efecto.transform.localScale = new Vector3(radio * 2, radio * 2, radio * 2);
                Destroy(efecto, 2f);
            }
            
            // Detectar enemigos en el área
            Collider[] objetivos = Physics.OverlapSphere(portador.transform.position, radio, capasObjetivos);
            
            int objetivosGolpeados = 0;
            foreach (var objetivo in objetivos)
            {
                // No dañar al portador
                if (objetivo.gameObject == portador.gameObject)
                    continue;
                
                // Buscar si tiene un PortadorGeneral con sistemaVida
                PortadorGeneral portadorGolpeado = objetivo.GetComponent<PortadorGeneral>();
                if (portadorGolpeado != null && portadorGolpeado.sistemaVida != null)
                {
                    portadorGolpeado.sistemaVida.RecibirDaño(daño);
                    objetivosGolpeados++;
                }
            }
            
            Debug.Log($"Habilidad AOE afectó a {objetivosGolpeados} objetivos por {daño} de daño cada uno.");
            
            // Consumir mana
            if (portador.sistemaMana != null)
            {
                portador.sistemaMana.ModificarValor(-costoMana);
            }
            
            ultimoUso = Time.time;
        }
    }   
}