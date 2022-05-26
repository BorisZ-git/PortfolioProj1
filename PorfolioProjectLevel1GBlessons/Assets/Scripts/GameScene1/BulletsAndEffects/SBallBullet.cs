using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBallBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public ParticleSystem explosion;
    bool launch;
    private void Start()
    {
        Destroy(gameObject, 10f);
        launch = true;
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!launch)
            if(Hit(other.gameObject))
                Blow();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!launch)
        {
            if (Hit(collision.gameObject))
                Blow();
            Blow();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        launch = false;
    }

    private bool Hit(GameObject obj)
    {
        SCharacter blowException = obj.GetComponent<SCharacter>();
        if (blowException != null)
        {
            blowException.TakeDamage(damage);
            return true;
        }
        return false;
    }
    private void Blow()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
