using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMagicBall : MonoBehaviour
{
    public float speed;
    public float speedRotate;
    public GameObject explosion;
    private float timer;
    bool launch;

    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        Destroy(gameObject, 7f);
        launch = true;
    }
    private void FixedUpdate()
    {
        Aim();
    }
    private void Aim()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 targetVector = player.transform.position - transform.position;
        transform.forward = 
            Vector3.Slerp(transform.forward, targetVector, speedRotate * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!launch)
        {
            if (Hit(other.gameObject))
                Blow();
            //SCharacter character = other.GetComponent<SCharacter>();
            //if (character != null)
            //{
            //    character.TakeDamage(10);
            //    Instantiate(explosion, transform.position, transform.rotation);
            //    Destroy(gameObject);
            //}
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!launch)
        {
            //SCharacter character = collision.gameObject.GetComponent<SCharacter>();
            //if (character != null)
            //{
            //    character.TakeDamage(10);
            //    Instantiate(explosion, transform.position, transform.rotation);
            //    Destroy(gameObject);
            //}
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
            blowException.TakeDamage(10);
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
