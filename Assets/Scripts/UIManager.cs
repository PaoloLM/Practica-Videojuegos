using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager obj;

    public Text livesLbl;
    public Text scoreLbl;

    public Transform UIPanel;

    private void Awake()
    {
        obj = this;
    }

    public void updateLives()
    {
        livesLbl.text = "" + Player.obj.vida;
    }

    public void updateScore()
    {
        scoreLbl.text = "" + Game.obj.score;
    }

    public void startGame()
    {
        AudioManager.obj.playGui();

        Game.obj.gamePausa = true;
        UIPanel.gameObject.SetActive(true);
    }

    public void hideInitPanel()
    {
        AudioManager.obj.playGui();
        Game.obj.gamePausa = false;
        UIPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
