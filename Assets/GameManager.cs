using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IEnumerator coroutine;
    private static GameManager instance;
    public GameObject[] Enemy;
    public bool ready = true;
    public bool isJump = false;
    public bool isOver = false;
    public GameObject Board;
    private float speed;
    public TextMesh scoreText;
    public TextMesh boardScoreText;
    public TextMesh boardBestText;
    public int score;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    void Start()
    {
        ready = true;
        speed = 5.0f;
        score = 0;
    }

    void MakeObj()
    {
        if(isOver == false)
        Instantiate(Enemy[Random.Range(0, Enemy.Length)]);
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if(Input.GetMouseButtonDown(0) && ready == true)
        {
            StartCoroutine("GetScore",0.1f);
            ready = false;
            InvokeRepeating("MakeObj", 1.0f, 1.5f);
        }
        if(isOver == true)
        {
            Board.transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (Board.transform.position.y > 0) speed = 0;
        }
    }
    public void GameOver()
    {
        isOver = true;
        StopCoroutine("GetScore");

        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        else if (score <= PlayerPrefs.GetInt("BestScore"))
        {

        }
        boardScoreText.text = score.ToString();
        boardBestText.text = PlayerPrefs.GetInt("BestScore").ToString();

    }
    IEnumerator GetScore(float delayTime)
    {
        score += 1;
        yield return new WaitForSeconds(delayTime);
        StartCoroutine("GetScore", 0.1f);
    }
}
