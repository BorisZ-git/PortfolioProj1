using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerShooting : MonoBehaviour
{
    #region Ammo
    private static int ammo1;
    private static int ammo2;
    public static int Ammo1 { get { return ammo1; } set { ammo1 = value; } }
    public static int Ammo2 { get { return ammo2; } set { ammo2 = value; } }
    #endregion
    #region AngerBonusEffects
    public ParticleSystem redAnger;
    private Color gunLineColor;
    #endregion
    public Animator animCont;
    public float damagePerShoot = 20f;
    public float timeBetweenShoot = 0.3f;
    public float range = 100f;
    public AudioClip[] audioClips;
    public GameObject rocket;
    public bool doubleDamage, pause;
    private UIGameScene ui;
    private int value;

    private float timerRifle,timerRocket, timerBonus;
    Ray hit;
    RaycastHit shootHit;
    public GameObject hitParticle;

    ParticleSystem gunParticle;
    LineRenderer gunLine;
    public Light gunLight;
    public Light fireLight;

    AudioSource audioSource;
    float effectDisplayTime = 0.1f;

    private void Awake()
    {
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLineColor = gunLine.startColor;
        audioSource = GetComponent<AudioSource>();
        ui = GameObject.FindGameObjectWithTag("UIGameScene").GetComponent<UIGameScene>();
    }
    private void Start()
    {
        value = SDifficulty.SetDifficulty(3,2,1);
        Ammo1 = value*50;
        Ammo2 = value*10;
    }
    void Update()
    {
        if (!pause)
        {
            timerRifle += Time.deltaTime; timerRocket += Time.deltaTime;
            if(Input.GetButton("Fire1") && timerRifle >= timeBetweenShoot ||
                Input.GetButton("Fire2") && timerRocket >= timeBetweenShoot * 2)
            {
                animCont.SetTrigger("IsAttack");
                if (Input.GetButton("Fire1"))
                    Shoot();
                if (Input.GetButton("Fire2"))
                    Launch();
            }
            if (timerRifle >= timeBetweenShoot * effectDisplayTime)
                DisableEffects();
            if (doubleDamage)
                DoubleDamage();
        }
    }
    private void DisableEffects()
    {
        gunLight.enabled = false;
        gunLine.enabled = false;
        fireLight.enabled = false;
    }
    private void Shoot()
    {
        timerRifle = 0f;
        if (Ammo1 <= 0)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        if (Ammo1 > 0)
        {
            Ammo1--;
            audioSource.clip = audioClips[0];
            audioSource.Play();
            gunLight.enabled = true;
            fireLight.enabled = true;
            gunParticle.Stop();
            gunParticle.Play();
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            hit.origin = transform.position;
            hit.direction = transform.forward;
            if (Physics.Raycast(hit, out shootHit, range))
            {
                SCharacter health = shootHit.collider.GetComponent<SCharacter>();
                if (health != null)
                {
                    health.TakeDamage(damagePerShoot);
                    shootHit.collider.GetComponent<AudioSource>().Play();
                }
                gunLine.SetPosition(1, shootHit.point);
                Instantiate(hitParticle, shootHit.collider.transform.position, shootHit.collider.transform.rotation);
            }
            else
                gunLine.SetPosition(1, hit.origin + hit.direction * range);
        }
    }
    private void Launch()
    {
        timerRocket = 0f;
        if (Ammo2 > 0)
        {
            Instantiate(rocket, transform.position, transform.rotation);
            Ammo2--;
        }
        else
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
    }
    public void AddAmmo(int type)
    {
        ui.ShowEffect(new Color(255f, 255f, 0f, 0.3f));
        if(type == 1)
            Ammo1 += value*10;
        if (type == 2)
            Ammo2 += value;
    }
    private void DoubleDamage()
    {
        timerBonus += Time.deltaTime;
        if (timerBonus > 10f)
        {
            doubleDamage = false;
            damagePerShoot = 20f;
            redAnger.Stop();
            gunLine.startColor = gunLineColor;
        }
    }
    public void ActiveBonusDamage()
    {
        timerBonus = 0f;
        doubleDamage = true;
        damagePerShoot = 40f;
        redAnger.Play();
        gunLine.startColor = new Color(1f, 0f, 0f);
        ui.ShowEffect(new Color(128f, 0f, 128f, 0.3f));
    }

}
