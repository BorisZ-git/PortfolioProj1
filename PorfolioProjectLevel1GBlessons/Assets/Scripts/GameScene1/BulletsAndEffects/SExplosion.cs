using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SExplosion : MonoBehaviour
{
    private float damage;
    private Light boomLight;
    private void Start()
    {
        Destroy(gameObject, 0.9f);
        boomLight = GetComponent<Light>();
    }
    private void Update()
    {
        boomLight.intensity -= 0.3f;
        boomLight.range -= 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SCharacter>() != null)
        {
            damage = 100f - (Vector3.Distance(transform.position, other.transform.position)*9);
            other.GetComponent<SCharacter>().TakeDamage(damage);
        }
    }
}
