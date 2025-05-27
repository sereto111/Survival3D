using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoUsoElemento
{
    Hambre,
    Sed,
    Descanso
}

[CreateAssetMenu(fileName = "Elemento", menuName = "Elemento Inventario")]
public class DatosElemento : ScriptableObject
{
    [Header("Información")]
    public string nombre;
    public string descripcion;
    public Sprite icono;
    public GameObject prefabSoltar;

    // para distinguir el tipo de elemento
    public TipoUsoElemento tipoUsoElemento; // Tipo de uso del elemento
}