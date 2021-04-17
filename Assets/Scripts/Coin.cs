using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public int scoreGive = 100;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.addScore(scoreGive);

            AudioManager.obj.playCoin();

            UIManager.obj.updateScore();

            FXManager.obj.showPop(transform.position);
            gameObject.SetActive(false);
        }
    }
}
