using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementosEstanteriaUI : MonoBehaviour
{
    public Button botonElemento; // Botón del elemento
    public Image icono; // Imagen del elemento
    public TextMeshProUGUI textoCantidad; // Nombre del elemento
    public int indice; // Índice del elemento
    private ElementosEstanteria elementoactual; // Elemento actual

    public void Establecer(ElementosEstanteria elemento)
    {
        elementoactual = elemento;
        icono.gameObject.SetActive(true); // Activar la imagen del elemento
        icono.sprite = elemento.datosElemento.icono; // Asignar el icono del elemento
        //comprueba que el elemento sea mayor que 1 y si no lo pone vacio
        textoCantidad.text = elemento.cantidad > 1 ? elemento.cantidad.ToString() : string.Empty; // Asignar la cantidad del elemento
    }
    public void Limpiar()
    {
        elementoactual = null; // Limpiar el elemento actual
        icono.gameObject.SetActive(false); // Desactivar la imagen del elemento
        textoCantidad.text = ""; // Limpiar el texto de la cantidad
    }

    public void OnButtonClick()
    {
        ControlInventario.instance.ElementoSeleccionado(indice); // Seleccionar el elemento al hacer click
    }
}
