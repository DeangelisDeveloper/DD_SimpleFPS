using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("General")]
    public Player player;
    [SerializeField] Text scoreText = null;
    public static Manager instance;
    int score;

    void Start()
    {
        instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        score = 0;
    }

    public void SetScore(int addScore)
    {
        score += addScore;
        scoreText.text = "SCORE: " + score.ToString();
    }
}
