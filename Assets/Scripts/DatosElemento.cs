using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Elemento", menuName = "Elemento Inventario")]
public class DatosElemento : ScriptableObject
{
    [Header("Información")]
    public string nombre;
    public string descripcion;
    public Sprite icono;
    public GameObject prefabSoltar;
}
