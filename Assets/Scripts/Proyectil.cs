using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;
    public float daño = 10f;
    public float tiempoDeVida = 5f;

    private Vector3 direccion;

    public void Inicializar(float daño, Vector3 direccion)
    {
        this.daño = daño;
        this.direccion = direccion.normalized;
        Destroy(gameObject, tiempoDeVida); // Destruir el proyectil después de un tiempo
    }

    private void Update()
    {
        // Mover el proyectil en la dirección especificada
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar colisiones con otros objetos
        var vida = other.GetComponent<SistemaVida>();
        if (vida != null)
        {
            vida.RecibirDaño(daño); // Aplicar daño al objetivo
        }

        Destroy(gameObject); // Destruir el proyectil al impactar
    }
}