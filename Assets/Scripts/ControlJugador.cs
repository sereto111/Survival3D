using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    private Vector2 ratonDelta;

    [Header("Vista Camara")]
    public Transform camara;
    public float minVistaX, maxVistaX;
    public float sensibilidadRaton;
    private float rotacionActualCamara;

    [Header("Movimiento Jugador")]
    public float velocidadMovimiento;
    private Vector2 movimientoActualEntrada;
    private Rigidbody fisica;

    [Header("Salto")]
    public float fuerzaSalto;
    public LayerMask capaSuelo; // Capa del suelo

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
        //Cursor.visible = false; // Hacer invisible el cursor
    }

    public void OnVistaInput(InputAction.CallbackContext context)
    {
        ratonDelta = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovimientoJugador();
    }

    private void LateUpdate()
    {
        VistaCamara();
    }

    private void VistaCamara()
    {
        rotacionActualCamara += ratonDelta.y * sensibilidadRaton;
        rotacionActualCamara = Mathf.Clamp(rotacionActualCamara, minVistaX, maxVistaX);
        camara.localEulerAngles = new Vector3(-rotacionActualCamara, 0, 0); // Rotacion de la camara (arriba y abajo)
        transform.eulerAngles += new Vector3(0, ratonDelta.x * sensibilidadRaton, 0); // Rotacion del jugador
    }

    private void Awake()
    {
        fisica = GetComponent<Rigidbody>();
    }

    // Movimiento Jugador
    public void OnMovimientoInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            movimientoActualEntrada = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            movimientoActualEntrada = Vector2.zero;
        }
    }

    private void MovimientoJugador()
    {
        Vector3 direccion = transform.forward * movimientoActualEntrada.y + transform.right * movimientoActualEntrada.x; // Movimiento en la direccion del jugador
        direccion *= velocidadMovimiento; // Velocidad del jugador
        direccion.y = fisica.velocity.y; // Mantener la velocidad vertical (gravedad)
        fisica.velocity = direccion; // Aplicar la velocidad al jugador
    }

    // Salto Jugador
    public void OnSaltoInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && EstaEnSuelo())
        {
            fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse); // Aplicar fuerza de salto
        }
    }

    private bool EstaEnSuelo()
    {
        if (fisica == null) return false; // Si no hay fisica, no se puede saltar
        
        Ray[] rayos = new Ray[4] // Rayos para detectar el suelo
        {
            new Ray(transform.position + transform.forward*0.2f, Vector3.down), // Centro
            new Ray(transform.position - transform.forward*0.2f, Vector3.down), // Derecha
            new Ray(transform.position + transform.right*0.2f, Vector3.down), // Izquierda
            new Ray(transform.position - transform.right*0.2f, Vector3.down) // Delante
        };

        foreach (Ray r in rayos)
            return Physics.Raycast(r, 0.7f, capaSuelo); // Si el rayo toca el suelo
        
        return false; // No esta en el suelo
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Color de los rayos
        Gizmos.DrawRay(transform.position + transform.forward * 0.2f, Vector3.down * 0.7f); // Centro
        Gizmos.DrawRay(transform.position - transform.forward * 0.2f, Vector3.down * 0.7f); // Derecha
        Gizmos.DrawRay(transform.position + transform.right * 0.2f, Vector3.down * 0.7f); // Izquierda
        Gizmos.DrawRay(transform.position - transform.right * 0.2f, Vector3.down * 0.7f); // Delante
    }
}
