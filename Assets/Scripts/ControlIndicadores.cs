using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlIndicadores : MonoBehaviour, IDeterioro
{
    public Indicador hambre;
    public Indicador sed;
    public Indicador descanso;
    public Indicador salud;
    public TextMeshProUGUI muerteTxt;

    public UnityEvent OnSufrirDeterioro;

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
        if(hambre.valorActual <= 0.0f || sed.valorActual <= 0.0f){
            // Reducir salud si se acaba la comida o bebida
            if (hambre.valorActual <= 0.0f)
                salud.Restar(reduccionSaludConHambre * Time.deltaTime);

            if (sed.valorActual <= 0.0f)
                salud.Restar(reduccionSaludConSed * Time.deltaTime);
        } else {
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
        }

        // Comprobar si el jugador ha muerto
        if (salud.valorActual <= 0.0f)
            Muerto();                  
    }

    public void Muerto()
    {        
        Time.timeScale = 0.0f;
        muerteTxt.gameObject.SetActive(true);
    }

    public void IncrementarIndicadores(string indicador, float cantidad)
    {
        switch (indicador.ToLower())
        {
            case "hambre":
                hambre.Sumar(cantidad);
                break;
            case "sed":
                sed.Sumar(cantidad);
                break;
            case "descanso":
                descanso.Sumar(cantidad);
                break;
            case "salud":
                salud.Sumar(cantidad);
                break;
            default:
                Debug.Log("ERROR: Indicador no reconocido - " + indicador);
                break;
        }
    }

    public void ProducirDeterioro(float cantidad)
    {
        salud.Restar(cantidad);
        OnSufrirDeterioro?.Invoke();
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

public interface IDeterioro
{
    void ProducirDeterioro(float cantidadDeterioro);
}