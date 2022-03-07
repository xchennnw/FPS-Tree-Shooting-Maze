using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRender : MonoBehaviour
{
    
    private int mazeWidth = 6;
    private int mazeHeight = 10;

    [SerializeField]
    private Transform wallPrefab = null;

    private float size = 5;
    private int prevSign = 0;
    private int sign = 0;

    private WallState[,] maze;
    

    // Start is called before the first frame update
    void Start()
    {
        //Generate the random maze without drawing it
        maze = MazeGenerator.Generate(mazeWidth, mazeHeight);        
    }

    // Update is called once per frame
    void Update()
    {
        
        //draw each row of the maze according to the current score
        //When the score = 100, the maze is completed
        prevSign = sign;
        sign = TreeManager.score/10;
        if(sign>prevSign && sign <= 10)
        {
            DrawOneRow(maze, prevSign);
        }

    }

    //DrawOneRow() draws the jth row of the maze. 
    private void DrawOneRow(WallState[,] maze, int j)
    {

      
        
        float width = mazeWidth * size;
        float height = mazeHeight * size;
        

        //We use the left bottom cell as enter and right top cell as exit of the maze
        for (int i = 0; i < mazeWidth; ++i)
        {
            
                var cell = maze[i, j];
                var position = new Vector3(-width/2 + i*size+17, -46, -height/2 + j*size-90);

                if(i!=mazeWidth-1 && j!=0)
                {
                    if (cell.HasFlag(WallState.UP))
                    {
                        var topWall = Instantiate(wallPrefab, transform) as Transform;
                        topWall.position = position + new Vector3(0, 0, size / 2);
                      
                    }
                }               

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);                
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == mazeWidth - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0, 0);                     
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0 && i!= 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                    }
                }
            

        }
    }


     
}
