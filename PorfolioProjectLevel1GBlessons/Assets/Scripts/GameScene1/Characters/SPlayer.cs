using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayer : MonoBehaviour
{
    public Animator animCont;
    public ParticleSystem bluePoo;
    private UIGameScene ui;
    public float playerSpeed;
    private Rigidbody playerRigidbody;
    Vector3 movement;
    private bool doubleSpeed;
    private float timeBonus;
    #region RayForCursor
    public Camera cam;
    private float hitdist = 0;
    private Plane playerPlane;
    private Ray ray;
    private Quaternion targetRotation;
    private Vector3 targetPoint;
    #endregion
    private void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("UIGameScene").GetComponent<UIGameScene>();
        playerRigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    void Start()
    {
        hitdist = 0;
        playerPlane = new Plane(Vector3.up, transform.position);
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        LookCursor();
        if (doubleSpeed)
            DoubleSpeed();
        if (Input.anyKey)
            Move(h, v);
        Animating(h, v);
    }
    private void DoubleSpeed()
    {
        timeBonus += Time.deltaTime;
        if(timeBonus > 10)
        {
            doubleSpeed = false;
            playerSpeed = 5f;    
            bluePoo.Stop();
        }
    }
    public void ActiveBonusSpeed()
    {
        timeBonus = 0f;
        doubleSpeed = true;
        playerSpeed = 10f;
        bluePoo.Play();
        ui.ShowEffect(new Color(0f, 0f, 1f, 0.3f));
    }
    private void Move(float h,float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * playerSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    private void LookCursor()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (playerPlane.Raycast(ray, out hitdist))
        {
            targetPoint = ray.GetPoint(hitdist);
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerSpeed * Time.deltaTime);
        }
    }
    void Animating(float h, float v)
    {
        animCont.SetBool("IsWalk", h != 0 || v != 0);        
    }
}
