using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
    public int cantidadElemento; // Cantidad del elemento a usar
    private ControlIndicadores controlIndicadores; // Control de indicadores del jugador
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
        controlIndicadores = GetComponent<ControlIndicadores>();
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

    // al pulsar TAB se cierra o se abre el inventario. Se llama al evento InputSystem OnBotonInventario
    public void AbrirCerrarVentanaInventario()
    {
        if (EstaAbierto())
        {
            ventanaInventario.SetActive(false); // Cerrar la ventana del inventario
            controlJugador.ModoInventario(false); // Cambiar el modo del jugador a no inventario
        }
        else
        {
            ventanaInventario.SetActive(true); // Abrir la ventana del inventario
            controlJugador.ModoInventario(true); // Cambiar el modo del jugador a inventario
        }
    }

    public void OnBotonInventario(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)        
            AbrirCerrarVentanaInventario(); // Llamar al método para abrir o cerrar la ventana del inventario        
    }

    // Consulta si la ventana del inventario esta abierta o cerrada
    bool EstaAbierto()
    {
        return ventanaInventario.activeInHierarchy; // Comprobar si la ventana del inventario está abierta
    }

    // Método para actualizar el inventario
    public void AnadirElemento(DatosElemento elemento) 
    {
        ElementosEstanteria elementoParaAlmacenar = ObtenerElementoAlmacenado(elemento); // Obtener el elemento almacenado en la estantería
        if (elementoParaAlmacenar != null)
        {
            elementoParaAlmacenar.cantidad++; // Aumentar la cantidad del elemento almacenado
            ActualizarUI(); // Actualizar la interfaz de usuario
            return;
        }

        ElementosEstanteria objetoVacio = ObtenerObjetoVacio(); // Obtener un objeto vacío
        if (objetoVacio != null)
        {
            objetoVacio.datosElemento = elemento; // Asignar el elemento a la posición vacía
            objetoVacio.cantidad = 1; // Inicializar la cantidad del elemento
            ActualizarUI(); // Actualizar la interfaz de usuario
            return;
        }
        else
        {
            Debug.Log("No hay espacio en el inventario"); // Mensaje de error si no hay espacio en el inventario
            SoltarElemento(elemento); // Soltar el elemento si no hay espacio
        }
    }

    void SoltarElemento(DatosElemento elemento)
    {
        Instantiate(elemento.prefabSoltar, posicionSoltar.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    void ActualizarUI()
    {
        for (int i = 0; i < elementosEstanteriaUIs.Length; i++)
        {
            if (elementosEstanteria[i].datosElemento != null)
                elementosEstanteriaUIs[i].Establecer(elementosEstanteria[i]); // Actualizar la interfaz de usuario del elemento
            else
                elementosEstanteriaUIs[i].Limpiar(); // Limpiar la interfaz de usuario si el elemento es nulo
        }
    }

    ElementosEstanteria ObtenerElementoAlmacenado(DatosElemento elemento)
    {
        for (int i = 0; i < elementosEstanteria.Length; i++)
        {
            if (elementosEstanteria[i].datosElemento == elemento)
            {
                return elementosEstanteria[i]; // Retorna el elemento almacenado si se encuentra
            }
        }
        return null; // Retorna null si no se encuentra el elemento almacenado
    }

    ElementosEstanteria ObtenerObjetoVacio()
    {
        for (int i = 0; i < elementosEstanteria.Length; i++)
        {
            if (elementosEstanteria[i].datosElemento == null)
            {
                return elementosEstanteria[i]; // Retorna el objeto vacío si se encuentra
            }
        }
        return null; // Retorna null si no se encuentra un objeto vacío
    }

    public void ElementoSeleccionado(int indice)
    {
        if (elementosEstanteria[indice] == null)
            return; // Si el elemento es nulo, no hacer nada
        else
        {
            elementosEstanteriaSeleccionado = elementosEstanteria[indice]; // Asignar el elemento seleccionado
            IndiceElementoSeleccionado = indice; // Asignar el índice del elemento seleccionado

            nombreElementoSeleccionado.text = elementosEstanteriaSeleccionado.datosElemento.nombre; // Asignar el nombre del elemento seleccionado
            descripcionElementoSeleccionado.text = elementosEstanteriaSeleccionado.datosElemento.descripcion; // Asignar la descripción del elemento seleccionado

            botonSoltarElementoSeleccionado.gameObject.SetActive(true); // Activar el botón de soltar el elemento seleccionado
            botonUsarElementoSeleccionado.gameObject.SetActive(true); // Activar el botón de usar el elemento seleccionado si tiene necesidades
        }
    }

    public void EliminarElementoSeleccionado()
    {
        elementosEstanteriaSeleccionado.cantidad--; // Disminuir la cantidad del elemento seleccionado
        if (elementosEstanteriaSeleccionado.cantidad <= 0)
        {
            elementosEstanteriaSeleccionado.datosElemento = null; // Eliminar el elemento seleccionado si la cantidad es menor o igual a 0
            LimpiarVentanaElementoSeleccionado(); // Limpiar el elemento seleccionado
        }
        ActualizarUI(); // Actualizar la interfaz de usuario
    }

    private void LimpiarVentanaElementoSeleccionado()
    {

        elementosEstanteriaSeleccionado = null;

        nombreElementoSeleccionado.text = string.Empty; // Limpiar el nombre del elemento seleccionado
        descripcionElementoSeleccionado.text = string.Empty; // Limpiar la descripción del elemento seleccionado

        botonSoltarElementoSeleccionado.gameObject.SetActive(false);
        botonUsarElementoSeleccionado.gameObject.SetActive(false);
        
    }

    public void OnBotonUsar()
    {
        // ver que tipo de elemento es y usarlo
        switch (elementosEstanteriaSeleccionado.datosElemento.tipoUsoElemento)
        {
            case TipoUsoElemento.Hambre:
                controlIndicadores.hambre.Sumar(cantidadElemento);
                break;
            case TipoUsoElemento.Sed:
                controlIndicadores.sed.Sumar(cantidadElemento);
                break;
            case TipoUsoElemento.Descanso:
                controlIndicadores.descanso.Sumar(cantidadElemento);
                break;
            default:
                Debug.LogWarning("Tipo de elemento no implementado: " + elementosEstanteriaSeleccionado.datosElemento.tipoUsoElemento);
                break;
        }
        EliminarElementoSeleccionado(); // Eliminar el elemento seleccionado
    }

    public void OnBotonSoltar()
    {        
        SoltarElemento(elementosEstanteriaSeleccionado.datosElemento); // Soltar el elemento seleccionado
        EliminarElementoSeleccionado(); // Eliminar el elemento seleccionado de la estantería
    }
}

public class ElementosEstanteria
{
    public DatosElemento datosElemento; // Datos del elemento
    public int cantidad; // Cantidad del elemento
}