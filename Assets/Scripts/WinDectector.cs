using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinDectector : MonoBehaviour
{
    public Transform playerPoint;
    public TextMeshProUGUI endingText;
    private bool mazeFinished;
    private bool win;
    private bool lose;

    // Start is called before the first frame update
    void Start()
    {
        mazeFinished = false;
        win = false;
        lose = false;
        endingText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player wins or loses
        //according to completion of maze and player position

        if (TreeManager.score >= 100)
        {
            mazeFinished = true;
        }
        
        var playerX = playerPoint.position.x;
        var playerZ = playerPoint.position.z;
        var playerY = playerPoint.position.y;

        if (mazeFinished)
        {
            if (playerY< -49)
            {
                lose = true;
            }else if (playerZ < -125)
            {
                win = true;
            }
        }
        else{
            if (playerY < -40)
            {
                lose = true;
            }
        }

        checkGameStatus();
       
    }

    void checkGameStatus()
    {
        // Print message according to game status
        if (lose)
        {
           
            endingText.text = "Game Over";

        }else if (win)
        {
            endingText.text = "You win!";
        }

    }
}
