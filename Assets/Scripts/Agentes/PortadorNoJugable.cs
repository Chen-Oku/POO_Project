using UnityEngine;

public class PortadorNoJugable : PortadorGeneral
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
