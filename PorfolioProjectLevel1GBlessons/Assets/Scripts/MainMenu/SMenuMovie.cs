using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMenuMovie : MonoBehaviour
{
    public Animator[] animCont;
    private float timer;
    private void FixedUpdate()
    {
        TicTac();
    }
    private void TicTac()
    {
        timer += Time.deltaTime;
        for (int i = 0; i < animCont.Length; i++)
        {
            animCont[i].SetFloat("Timer", timer);
        }
        if (timer > 39f)
            ResetClock();
    }
    private void ResetClock()
    {
        timer = 0f;
        for (int i = 0; i < animCont.Length; i++)
        {
            animCont[i].SetFloat("Timer", timer);
        }
    }
}
