using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }
    
    void Start()
    {
        //scoreText.text = "xYou Scored: \n" + scoreKeeper.GetScore(); //set scoretext in start
        scoreText.text = scoreKeeper.GetScore().ToString();
        Debug.Log("TEXTTTTTT");
        Debug.Log(scoreKeeper.GetScore());
    }
}
