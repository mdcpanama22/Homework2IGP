using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    public static GM instance = null;
    public Vector3 worldBounds;
    private Text HighScoreT;
    float oldHighScore;
    private Text CurrentScoreT;
    private int CurrentScoreR;

    public bool lostLife;
    private float lInitialT;
    private float lFinalT;

    private GameObject Player;
    private GameObject Apples;

    public AudioClip[] Sounds;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 screen;
        screen.x = Screen.width;
        screen.y = Screen.height;
        screen.z = 0;

        worldBounds = Camera.main.ScreenToWorldPoint(screen);

        //GUI

        HighScoreT = GameObject.Find("High").GetComponent<Text>();
        oldHighScore = PlayerPrefs.GetInt("HS", 1000);
        HighScore(oldHighScore);
        CurrentScoreT = GameObject.Find("Current").GetComponent<Text>();

        //Score
        CurrentScoreR = 0;

        //Player
        Player = GameObject.Find("Player");

        //Apples
        Apples = GameObject.Find("Apples");

    }

    // Update is called once per frame
    void Update()
    {
        if (lostLife)
        {
            lFinalT = Time.time;
            if(lFinalT - lInitialT > 3.0f)
            {
                lostLife = false;
            }
        }
    }

    public void ChangeScore(int score)
    {
        GetComponent<AudioSource>().clip = Sounds[1];
        GetComponent<AudioSource>().Play();
        CurrentScoreR += score;
        string CS = CurrentScoreR.ToString();
        string REAL = "";
        for(int i = 0; i < 6 - CS.Length; ++i)
        {
            REAL += "0";
        }
        REAL += CS;
        CurrentScoreT.text = "Current Score: " + REAL;
    }
    public void HighScore(float score)
    {
        string CS = score.ToString();
        string REAL = "";
        for (int i = 0; i < 6 - CS.Length; ++i)
        {
            REAL += "0";
        }
        REAL += CS;
        HighScoreT.text = "High Score: " + REAL;
    }
    public void LoseLife()
    {
        GetComponent<AudioSource>().clip = Sounds[0];
        GetComponent<AudioSource>().Play();
        Player.GetComponent<AudioSource>().clip = Sounds[2];
        Player.GetComponent<AudioSource>().Play();
        foreach (Transform a in Apples.transform)
        {
            DestroyImmediate(a.gameObject);
        }
        if(Player.transform.childCount == 1)
        {
            if(oldHighScore < CurrentScoreR)
            {
                PlayerPrefs.SetInt("HS", CurrentScoreR);
                HighScore(CurrentScoreR);
            }
            
            SceneManager.LoadScene("SampleScene");
            lostLife = true;
            lInitialT = Time.time;
        }
        else
        {
            DestroyImmediate(Player.transform.GetChild(Player.transform.childCount - 1).gameObject);
        }
    }
}
