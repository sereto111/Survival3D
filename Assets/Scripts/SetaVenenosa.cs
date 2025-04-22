using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaVenenosa : MonoBehaviour
{
    public float cantidadVeneno;
    public float indiceDeterioro;

    private List<IDeterioro> listaParaDeteriorar = new List<IDeterioro>(); // Lista de objetos que se deterioran al entrar en contacto con la seta venenosa

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("El jugador ha tocado una seta venenosa.");
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El jugador ha dejado de tocar una seta venenosa.");
        }             
    }
}
