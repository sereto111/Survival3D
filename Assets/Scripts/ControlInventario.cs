using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlInventario : MonoBehaviour
{
    public ElementosEstanteriaUI[] elementosEstanteriaUIs; // Array de elementos del inventario
    public ElementosEstanteria[] elementosEstanteria; // Array de elementos de la estantería
    public GameObject ventanaInventario; // Ventana del inventario

    public Transform posicionSoltar; // Posición de la estantería
    [Header("elementos seleccionados")]
    private ElementosEstanteria elementosEstanteriaSeleccionado; // Elemento seleccionado
    private int IndiceElementoSeleccionado; // Índice del elemento seleccionado
    public TextMeshProUGUI nombreElementoSeleccionado; // Nombre del elemento seleccionado
    public TextMeshProUGUI descripcionElementoSeleccionado; // Descripcion del elemento seleccionado
    public TextMeshProUGUI nombreNecesidadElementoSeleccionado; // nombreNecesidad del elemento seleccionado
    public TextMeshProUGUI valoresNecesidadElementoSeleccionado; // valoresNecesidad del elemento seleccionado
    public Button botonUsarElementoSeleccionado; // Botón de usar el elemento seleccionado
    public Button botonSoltarElementoSeleccionado; // Botón de soltar el elemento seleccionado

    //necesitamos el control del jugador para poder usar el elemento
    private ControlJugador controlJugador; // Control del jugador
    [Header("eventos")]
    public UnityEvent onabrirVentanaInventario; // Evento al abrir la ventana del inventario
    public UnityEvent oncerrarVentanaInventario; // Evento al cerrar la ventana del inventario

    //sigleton para mantener la informacion entre clases
    public static ControlInventario instance; // Instancia del singleton

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //aprovecho el awake para inicializar el control del jugador
        controlJugador = GetComponent<ControlJugador>();
    }

    void Start()
    {
        ventanaInventario.SetActive(false); // Desactivar la ventana del inventario
        elementosEstanteria = new ElementosEstanteria[elementosEstanteriaUIs.Length];
        for (int i = 0; i < elementosEstanteriaUIs.Length; i++)
        {
            elementosEstanteria[i] = new ElementosEstanteria(); // Inicializar el array de elementos de la estantería
            elementosEstanteriaUIs[i].indice = i; // Asignar el índice del elemento
            elementosEstanteriaUIs[i].Limpiar(); // Limpiar el elemento
        }
    }

    void AbrirCerrarVentanaInventario()
    {

    }

    // Consulta si la ventana del inventario esta abierta o cerrada
    bool EstaAbierto()
    {
        return ventanaInventario.activeInHierarchy; // Comprobar si la ventana del inventario está abierta
    }

    // TOFIX
    // TODO
    // Método para actualizar el inventario
    /*void AnadirElemento(DatosElemento datosElemento) 
    {
        ElementosEstanteria elementoParaAlmacenar = ObtenerElementoAlmacenado(datosElemento); // Obtener el elemento almacenado en la estantería
        if (elementoParaAlmacenar != null)
        {
            elementoParaAlmacenar.cantidad++; // Aumentar la cantidad del elemento almacenado
            ActualizarUI(); // Actualizar la interfaz de usuario
            return;
        }

        ElementosEstanteria objetoVacio = ObtenerObjetoVacio(); // Obtener un objeto vacío
        if (objetoVacio != null)
        {
            objetoVacio.datosElemento = datosElemento; // Asignar el elemento a la posición vacía
            objetoVacio.cantidad = 1; // Inicializar la cantidad del elemento
            ActualizarUI(); // Actualizar la interfaz de usuario
            return;
        }
        else
        {
            Debug.Log("No hay espacio en el inventario"); // Mensaje de error si no hay espacio en el inventario
        }
    }*/

    void SoltarElemento(DatosElemento elemento)
    {

    }

    void ActualizarUI()
    {

    }

    ElementosEstanteria ObtenerElementoAlmacenado(int indice)
    {
        return null; //TODO
    }

    ElementosEstanteria ObtenerObjetoVacio()
    {
        return null; //TODO
    }

    void ElementoSeleccionado(int indice)
    {
        //TODO
    }

    void EliminarElementoSeleccionado(int indice)
    {
        //TODO
    }

    public void OnBotonUsar()
    {
        //TODO
    }

    public void OnBotonSoltar()
    {
        //TODO
    }
}

public class ElementosEstanteria
{
    public DatosElemento datosElemento; // Datos del elemento
    public int cantidad; // Cantidad del elemento
}