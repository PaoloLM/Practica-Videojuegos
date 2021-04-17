using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    // Start is called before the first frame update
   
    public static Game obj;

    public int maxVidas = 3;

    public bool gamePausa = false;
    public int score = 0;

    void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePausa = false;
        UIManager.obj.startGame();
    }

    public void addScore(int scoreGive)
    {
        score += scoreGive;
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        obj = null;
    }
}
