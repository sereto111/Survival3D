using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlInteraccion : MonoBehaviour
{
    private Camera camara;
    public float periodoTiempoChequeo = 0.5f;
    private float ultimoTiempoChequeo;
    public float maxDistanciaChequeo;
    public LayerMask capaRayo;

    // acceso objeto interactuable
    private GameObject gameObjectInteractuableActual;
    private IInteractuable interactuableActual;

    // texto
    public TextMeshProUGUI mensajeTexto;

    private void Start()
    {
        camara = Camera.main;

        mensajeTexto.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time - ultimoTiempoChequeo >= periodoTiempoChequeo)
        {
            ultimoTiempoChequeo = Time.time;
            
            // lanzar el rayo
            Ray rayo = camara.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit lanzaRayo;            
            if (Physics.Raycast(rayo, out lanzaRayo, maxDistanciaChequeo, capaRayo))
            {
                if (lanzaRayo.collider.gameObject != gameObjectInteractuableActual)
                {
                    gameObjectInteractuableActual = lanzaRayo.collider.gameObject;
                    interactuableActual = gameObjectInteractuableActual.GetComponent<IInteractuable>();
                    establecerMensajeTexto();
                }
            }
            else
            {
                mensajeTexto.gameObject.SetActive(false);
                gameObjectInteractuableActual = null;
                interactuableActual = null;
            }
        }
    }

    private void establecerMensajeTexto(){
        mensajeTexto.gameObject.SetActive(true);
        mensajeTexto.text = string.Format("<b>[E]</b> {0}", interactuableActual.ObtenerMensajeInteractuable());
    }
}

public interface IInteractuable
{
    string ObtenerMensajeInteractuable(); // para el mensaje al recoger
    void OnInteractuar(); // qué se hace al recoger
}