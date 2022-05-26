using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Trigger work");
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Collision work");
    }
}
