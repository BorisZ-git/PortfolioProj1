using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRocket : MonoBehaviour
{
    public ParticleSystem explosion;
    public float _speed;
    private float timer = 0;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        if (timer >= 5)        
            Explosion();        
        else if (timer < 5)
            StartCoroutine(TimeOver());
    }
    private void OnTriggerEnter(Collider other)
    {
        SCharacter blowException = other.GetComponent<SCharacter>();
        if (blowException != null)
        {
            if (!other.CompareTag("Player"))
            {
                Explosion();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Explosion();
        }
    }
    IEnumerator TimeOver()
    {
        yield return new WaitForSeconds(1.5f);
        timer++;
    }
    private void Explosion()
    {
        StopAllCoroutines();
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
