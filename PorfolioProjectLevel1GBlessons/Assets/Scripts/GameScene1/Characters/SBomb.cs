using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBomb : MonoBehaviour
{
    public ParticleSystem explosion;
    public Animator _animator;
    private float timer=0f;
    private bool isSteped = false;
    private bool isExploed = false;
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isSteped&&!isExploed)
            StartCoroutine(LaunchBomb());
        if (isExploed)
        {
            StopAllCoroutines();
            Explosion();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SCharacter>()!=null)
        {
            audioSource.Play();
            _animator.SetTrigger("Steped");
            isSteped = true;
        }
    }
    IEnumerator LaunchBomb()
    {
        yield return new WaitForSeconds(1.5f);
        timer++;
        if (timer >= 3)
        {
            isExploed = true;
        }
    }
    private void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
