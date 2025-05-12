using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInteraccion : MonoBehaviour
{

}

public interface IInteractuable
{
    string ObtenerMensajeInteractuable(); // para el mensaje al recoger
    void OnInteractuar(); // qué se hace al recoger
}