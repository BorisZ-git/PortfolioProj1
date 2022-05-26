using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SParticle : MonoBehaviour
{   
    private void Start()
    {
        transform.position += new Vector3(0f,0.5f,0f);
        Destroy(gameObject, 1f);
    }
}
