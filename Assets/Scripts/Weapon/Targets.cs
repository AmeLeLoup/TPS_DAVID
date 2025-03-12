using System;
using Unity.VisualScripting;
using UnityEngine;

public class Targets : MonoBehaviour
{
    
    [SerializeField] private LayerMask bulletLayer;
    
    private void OnCollisionEnter(Collision other)
    {
        // C'est très chiant a faire avec des layers. Plus simple les tags pour ce genre de situation
        // j'ai eu le droit a un petit cour sur le binaire et l'hexadécimale a cause de sa
        if (((1 << other.gameObject.layer) & bulletLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
    
}