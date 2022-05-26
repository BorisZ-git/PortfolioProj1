using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBonus : MonoBehaviour
{
    public bool damage, speed, hp, ammoRifle, ammoRocket;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool enter;

    private void Start()
    {
        Destroy(gameObject, 20f);
        audioSource = GetComponent<AudioSource>();
        enter = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!enter)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            enter = true;
            if (hp)            
                other.GetComponent<SCharacter>().TakeHeal(30);            
            if (damage)             
                other.GetComponentInChildren<SPlayerShooting>().ActiveBonusDamage();            
            if (speed)             
                other.GetComponent<SPlayer>().ActiveBonusSpeed();
            if (ammoRifle)
                other.GetComponentInChildren<SPlayerShooting>().AddAmmo(1);
            if (ammoRocket)
                other.GetComponentInChildren<SPlayerShooting>().AddAmmo(2);
        }
        MeshRenderer[] arr = gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].enabled = false;
        }
        Destroy(gameObject, 1f);
    }
}
