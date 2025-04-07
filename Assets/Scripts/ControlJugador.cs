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

    [Header("Movimiento Jugador")]
    public float velocidadMovimiento;
    private Vector2 movimientoActualEntrada;
    private Rigidbody fisica;

    private void Awake()
    {
        fisica = GetComponent<Rigidbody>();
    }

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
}
