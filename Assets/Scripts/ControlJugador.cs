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

    private void LateUpdate()
    {
        VistaCamara();
    }

    private void VistaCamara()
    {
        rotacionActualCamara += ratonDelta.y * sensibilidadRaton;
        rotacionActualCamara = Mathf.Clamp(rotacionActualCamara, minVistaX, maxVistaX);
        camara.localEulerAngles = new Vector3(-rotacionActualCamara, 0, 0);
    }
}
