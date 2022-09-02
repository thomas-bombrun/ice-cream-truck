using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDispenser : MonoBehaviour
{
    public GameObject conePrefab;
    public Cone GetNewCone()
    {
        Cone cone = Instantiate(conePrefab).GetComponent<Cone>();
        cone.transform.position = transform.position;
        return cone;
    }
}
