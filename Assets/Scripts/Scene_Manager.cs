using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Manager : MonoBehaviour {








    public float gameTimer = 0.0f;
    public float cycletime = 60.0f;


    public enum GameState
    {
        START,
        STARTING,
        RUNNING,
        COMPLETE
    };

    public GameState gameState = GameState.RUNNING;

	// Use this for initialization
	void Start () {
       // arduinoTalker = FindObjectOfType<SoloArduinoTalker>();
       // gameSound.Stop();
	}


  void  UpdateText()
    {
       /* if (debugMode)
        {
            //reportText.text = "in:" + arduinoTalker.inPotdata + string.Format(" LR:{0} WS:{1} MC:{2}", arduinoTalker.lrScore, arduinoTalker.wsScore, arduinoTalker.mcScore);
        }
        else
        {
            switch (gameState)
            {
                case GameState.START:

                    {
                        scoreText1.text = "";
                        scoreText2.text = "";
                        reportText.text = string.Format("Waiting for game start: {0:0.00}", previousAxisValue);
                        break;
                    }
                case GameState.RUNNING:
                    {

                        reportText.text = "";
                        scoreText1.text = string.Format("Balance: {0:0}\nWind: {1:0}\nEnergy {2:0}", pollutant2 * 50, pollutant0 * 50, pollutant1* 50);
                        scoreText2.text = scoreText1.text;
                        break;
                    }
                case GameState.COMPLETE:
                    {
                        scoreText1.text = "";
                        scoreText2.text = "";
                        reportText.text = "YAY! You saved the planet";
                        break;
                    }
            }
        }*/
  
    }

	// Update is called once per frame
	void Update () {
    
            UpdateValues();
        
  

    }


     void UpdateValues()
    {
        gameTimer += (Time.deltaTime/cycletime);
        if ((gameTimer >= 2.0f) || (gameTimer <= 0.0f))
        {

            cycletime = cycletime * -1;
        }
        
    }




}
