using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlIndicadores : MonoBehaviour
{
    public Indicador hambre;
    public Indicador sed;
    public Indicador descanso;
    public Indicador salud;

    // variables para reducir la salud cuando se acaba la comida o la bebida
    public float reduccionSaludConHambre;
    public float reduccionSaludConSed;


    // Start is called before the first frame update
    void Start()
    {
        hambre.valorActual = hambre.valorInicial;
        sed.valorActual = sed.valorInicial;
        descanso.valorActual = descanso.valorInicial;
        salud.valorActual = salud.valorInicial;
    }

    // Update is called once per frame
    void Update()
    {
        // Actualizar la barra de hambre
        hambre.barraUI.fillAmount = hambre.ObtenerPorcentaje(hambre.valorActual, hambre.valorMaximo);
        hambre.Restar(hambre.indiceDeterioro * Time.deltaTime);

        // Actualizar la barra de sed
        sed.barraUI.fillAmount = sed.ObtenerPorcentaje(sed.valorActual, sed.valorMaximo);
        sed.Restar(sed.indiceDeterioro * Time.deltaTime);

        // Actualizar la barra de descanso
        descanso.barraUI.fillAmount = descanso.ObtenerPorcentaje(descanso.valorActual, descanso.valorMaximo);
        descanso.Sumar(descanso.indiceRecuperacion * Time.deltaTime);

        // Actualizar la barra de salud
        salud.barraUI.fillAmount = salud.ObtenerPorcentaje(salud.valorActual, salud.valorMaximo);
        //salud.Restar(salud.indiceDeterioro * Time.deltaTime);

        // Reducir salud si se acaba la comida o bebida
        /*if (hambre.valorActual <= 0.0f)
            salud.Restar(reduccionSaludConHambre * Time.deltaTime);

        if (sed.valorActual <= 0.0f)
            salud.Restar(reduccionSaludConSed * Time.deltaTime);*/
    }
}

[System.Serializable]
public class Indicador
{
    [HideInInspector]
    public float valorActual;

    public float valorInicial;
    public float valorMaximo;

    public float indiceRecuperacion;
    public float indiceDeterioro;
    public Image barraUI;

    public void Sumar(float cantidad)
    {
        valorActual += cantidad;
        /*
        if (valorActual > valorMaximo)
            valorActual = valorMaximo;
        */

        valorActual = Mathf.Min(valorActual + cantidad, valorMaximo);
    }

    public void Restar(float cantidad)
    {
        valorActual -= indiceDeterioro * Time.deltaTime;
        /*
        if (valorActual < 0.0f)
            valorActual = 0.0f;
        */

        valorActual = Mathf.Max(valorActual - cantidad, 0.0f);
    }

    public float ObtenerPorcentaje(float valorActual, float valorMaximo)
    {
        return valorActual / valorMaximo;
    }
}
