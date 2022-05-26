using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SMainMenu : MonoBehaviour
{
    #region Panels
    public GameObject pMenu;
    public GameObject pScore;
    public GameObject pSettings;
    #endregion
    #region Audio
    public AudioSource aMusic;
    public Slider sVolume;
    #endregion


    public Dropdown DropDifficult;
    public Dropdown DropResolution;

    public Text tableRecords;
    public SExcelScore sExcel;

    public Toggle toggleWindow;
    public Text DifficultyDescribe;
    FullScreenMode mode;
    List<string> resolutions;

    private void Awake()
    {
         tableRecords = sExcel.ShowRes(tableRecords);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        LoadResolution();
        WindowMode();
        DifficultyChange();
    }
    public void LoadResolution()
    {
        resolutions = new List<string>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i].width.ToString() + "x" + Screen.resolutions[i].height.ToString());
        }
        DropResolution.AddOptions(resolutions);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Settings(bool b)
    {
        if (b)
        {
            pMenu.SetActive(false);
            pScore.SetActive(false);
            pSettings.SetActive(true);
        }
        else if (!b)
        {
            pMenu.SetActive(true);
            pScore.SetActive(true);
            pSettings.SetActive(false);
        }
    }
    public void WindowMode()
    {
        if (toggleWindow.isOn)        
            mode = FullScreenMode.Windowed;        
        if(!toggleWindow.isOn)
            mode = FullScreenMode.FullScreenWindow;
        Screen.fullScreenMode = mode;
    }
    public void DifficultyChange()
    {
        SDifficulty.lvlDif = DropDifficult.value;
        if (DropDifficult.value == 0)
            DifficultyDescribe.text = "Big ammo startpack \n" +
                "More ammo bonus \nLow enemy hp \n0 extra points for killing enemies";
        else if (DropDifficult.value == 1)
            DifficultyDescribe.text = "Medium ammo startpack\n" +
                "Little less ammo bonus\nMore enemy hp\n1 extra point for killing enemies";
        else if (DropDifficult.value == 2)
            DifficultyDescribe.text = "Small ammo startpack\n" +
                "Low ammo bonus\nFat enemies hp\n3 extra point for killing enemies ";

    }
    public void ResolutionChange()
    {
        int count = DropResolution.value;
        int w = Screen.resolutions[count].width;
        int h = Screen.resolutions[count].height;
        Screen.SetResolution(w,h,mode);
    }
    public void VolumeChange()
    {
        aMusic.volume = sVolume.value;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
