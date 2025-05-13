using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoElemento : MonoBehaviour, IInteractuable
{
    public DatosElemento elemento;
    public string ObtenerMensajeInteractuable()
    {
        //return "Recogiste " + elemento.nombre;
        return string.Format("Recogiste {0}", elemento.nombre);
    }

    public void OnInteractuar()
    {
        Destroy(gameObject); // se elimina el objeto
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
