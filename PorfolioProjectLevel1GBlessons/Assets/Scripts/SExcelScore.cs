using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;


public class SExcelScore : MonoBehaviour
{
    FileStream file;
    StreamReader sr;
    StreamWriter sw;
    string[] str;
    List<ScoreRecord> scoreRecords;
    public RectTransform Content;
    
    public Text ShowRes(Text text)
    {
        file = File.Open(Application.persistentDataPath + "/tableScore.txt", FileMode.OpenOrCreate);
        file.Close();
        LoadRes();
        foreach (var item in scoreRecords)
        {
            text.text += item.Nick + " " + item.Score + "\n";
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.rectTransform.sizeDelta.y + 21);
        }
        Content.sizeDelta = new Vector2(Content.sizeDelta.x, text.rectTransform.sizeDelta.y);
        return text;
    }
    public void SaveRes(string name, int score)
    {
        LoadRes();
        sw = new StreamWriter(Application.persistentDataPath + "/tableScore.txt");
        foreach (var item in scoreRecords)
        {
            sw.WriteLine(item.Nick + " " + item.Score);
        }
        sw.WriteLine(name + " " + score);
        sw.Close();
    }
    private void LoadRes()
    {
        sr = new StreamReader(Application.persistentDataPath + "/tableScore.txt");
        scoreRecords = new List<ScoreRecord>();
        while (!sr.EndOfStream)
        {
            str = sr.ReadLine().Split(new char[] { ' ' });
            ScoreRecord record = new ScoreRecord(str[0], int.Parse(str[1]));
            scoreRecords.Add(record);
        }
        scoreRecords = scoreRecords.OrderByDescending(scoreRecords => scoreRecords.Score).ToList();
        sr.Close();
    }
    public class ScoreRecord
    {
        string nick;
        public string Nick { get => nick; set { nick = value; } }

        int score;
        public int Score { get => score; set { score = value; } }
        public ScoreRecord(string n, int s)
        {
            Nick = n;
            Score = s;
        }
    }
}
