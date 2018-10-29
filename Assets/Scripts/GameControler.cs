using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameState { Idle, Playing, Ended };
public class GameControler : MonoBehaviour {

    [Range (0f, 0.20f)]
    public float parallaxSpeed = 0.2f;
    public RawImage background;
    public RawImage plataform;
    public GameObject uiIdle;
    public GameObject uiScore;
    public Text pointsText;
    public Text recordText;
    
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    private int points = 0;

    // Use this for initialization
    void Start () {
        recordText.text = "PUNTAJE MÁX: " + GetMaxScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {

        bool useraction = Input.GetKeyDown("up") || Input.GetKeyDown(0);

        //Empieza el juego
        if (gameState == GameState.Idle && useraction){
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
        }
        //Juego en marcha0
        else if (gameState == GameState.Playing)
        {
            Parallax();            
        }
        else if (gameState == GameState.Ended)
        {
            if(useraction)
            {
                RestartGame();
            }
        }
    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed * 2, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene("Morgan");
    }


    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
        if (points > GetMaxScore())
        {
            recordText.text = "PUNTAJE MÁX: " + points.ToString();
            SaveScore(points);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);

    }

    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }

}
