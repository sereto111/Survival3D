using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAlertaDeterioro : MonoBehaviour
{
    public Image imagenMarcoRojo;
    public float velocidadDesaparecer;
    private Coroutine desaparecer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aparecer()
    {
        imagenMarcoRojo.enabled = true;
        desaparecer = StartCoroutine(Desaparecer());

    }

    IEnumerator Desaparecer()
    {
        float alpha = 1.0f;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime / velocidadDesaparecer;
            imagenMarcoRojo.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
        yield return null;
    }
}
