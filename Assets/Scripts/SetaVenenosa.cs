using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaVenenosa : MonoBehaviour
{
    public float cantidadVeneno;
    public float indiceDeterioro; // Tiempo recarga de veneno

    private List<IDeterioro> listaParaDeteriorar = new List<IDeterioro>(); // Lista de objetos que se deterioran al entrar en contacto con la seta venenosa

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ManejarDeterioro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ManejarDeterioro()
    {
        while (true)
        {
            for (int i=0; i < listaParaDeteriorar.Count; i++)
                listaParaDeteriorar[i].ProducirDeterioro(cantidadVeneno);
            
            yield return new WaitForSeconds(indiceDeterioro);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<IDeterioro>() != null)
            listaParaDeteriorar.Add(collision.gameObject.GetComponent<IDeterioro>());
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<IDeterioro>() != null)
            listaParaDeteriorar.Remove(collision.gameObject.GetComponent<IDeterioro>());           
    }
}
