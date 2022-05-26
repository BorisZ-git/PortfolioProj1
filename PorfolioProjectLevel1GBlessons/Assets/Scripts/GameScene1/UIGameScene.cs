using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class UIGameScene : MonoBehaviour
{
    public static int score;
    public GameObject PMenu;
    public GameObject PGameOver;
    public GameObject PSettings;
    public GameObject[] buttonsMenu;
    public Text goScore;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;


    public Slider sHPBar;
    public Text tscore;
    public Text tammo1;
    public Text tammo2;
    public Image displayEffect;
    public InputField nick;
    public Button bRecordScore;
    public SExcelScore sExcel;

    private int num;
    private float timer;
    private bool effect;
    private float flashSpeed = 3f;
    private GameObject player;
    private SCharacter playerHP;
    private SPlayerShooting gun;

    private bool dead;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<SCharacter>();
        gun = player.GetComponentInChildren<SPlayerShooting>();
        nick.text = "";
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        if (gun.pause)
            gun.pause = false;
        dead = false;
    }
    void Update()
    {
        if (!dead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuOnOff();
            }
            if (effect)
            {
                displayEffect.color = Color.Lerp(displayEffect.color, Color.clear, flashSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                if (timer >= 5)
                {
                    timer = 0f;
                    effect = false;
                }
            }
        }
        if (dead)
        {
            nick.text = nick.text.Replace(" ", "");
            if (!string.IsNullOrEmpty(nick.text))
                bRecordScore.interactable = true;
            else
                bRecordScore.interactable = false;
        }
    }
    private void LateUpdate()
    {
        UIUpdate();
    }
    public void MenuOnOff()
    {
        Pause();
        PMenu.SetActive(num % 2 != 0);
    }
    public void GameOver()
    {
        tscore.enabled = false;
        dead = true;
        Pause();
        PGameOver.SetActive(true);
        goScore.text = ("Your score: " + score);
    }
    public void Pause()
    {
        num++;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        LowPass();
        if (gun != null)
            gun.pause = num % 2 != 0;
        if (gun == null)
            print("gun=null");
    }
    public void LowPass()
    {
        if (Time.timeScale == 0)
            paused.TransitionTo(.01f);
        else if (Time.timeScale == 1)
            unpaused.TransitionTo(.01f);
    }
    public void SettingsMenu(int value)
    {
        PSettings.SetActive(value % 2 != 0);
        for (int i = 0; i < buttonsMenu.Length; i++)
        {
            buttonsMenu[i].SetActive(value % 2 == 0);
        }
    }
    public void BackMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void BRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }
    public void BExit()
    {
        Application.Quit();
    }
    public void BRecord()
    {
        sExcel.SaveRes(nick.text, score);
    }
    private void UIUpdate()
    {
        sHPBar.value = playerHP.hp;
        tammo1.text = "Ammo: " + SPlayerShooting.Ammo1;
        tammo2.text = "Ammo: " + SPlayerShooting.Ammo2;
        tscore.text = "Score: " + score;
    }
    public void ShowEffect(Color col)
    {
        effect = true;
        displayEffect.color = col;
        timer = 0f;
    }

}
