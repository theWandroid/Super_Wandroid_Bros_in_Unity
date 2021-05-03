using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text collectableText, scoreLabel, maxScoreLabel, blockLabel, healthLabel, manaLabel;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            int currentObjects = GameManager.sharedInstance.collectedObjects;
            //this.collectableText.text = "" + currentObjects;
            this.collectableText.text = currentObjects.ToString();
            //this.collectableText.text = "" + currentObjects;

            float travelledDistance = PlayerController.sharedInstance.GetDistance();
            this.scoreLabel.text = "Score: \n" + travelledDistance.ToString("f0");

            float maxScore = PlayerPrefs.GetFloat("maxScore", 0);
            this.maxScoreLabel.text = "MaxScore\n" + maxScore.ToString("0");
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            healthLabel.text = $" {PlayerController.sharedInstance.GetHealth()} / {PlayerController.MAX_HEALTH} ";
            manaLabel.text = $" {PlayerController.sharedInstance.GetMana()} / {PlayerController.MAX_MANA} ";

        }

    }
}
