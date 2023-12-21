using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanerMaterials : MonoBehaviour
{
    [SerializeField] Material[] materials;

    public Material[] GetArrMaterial
    {
        get
        {
            return materials;
        }
    }

}
