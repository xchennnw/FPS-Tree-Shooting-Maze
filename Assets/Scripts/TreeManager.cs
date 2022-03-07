using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeManager : MonoBehaviour
{
    // This class generates trees in the game, and records the player's score
    
    public Transform playerPoint;
    public GameObject tree;
    private int number;

    public static int score;
    public TextMeshProUGUI scoreText;

    private int positionX;
    private int positionZ;


    private float startDelay = 2;
    private float spawnInterval = 0.3f;

    // We take the forest as a 8x4 area to set tree positions
    private int[] treePositions = new int[32];

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        number = 0;
        scoreText.text = "Maze:" + score + "%";

        for(int i=0; i<32; i++)
        {           
            treePositions[i] = 0;
        }
        // Generate trees at some time interval
        InvokeRepeating("instantiateTrees", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
             
    }

    //These two methods will be called when a tree is hit
    public void UpdateScore()
    {
        if (score < 100)
        {
            score += 5;
            scoreText.text = "Maze:" + score + "%";
        }
        
    }

    public void decreaseTree(int xx, int zz)
    {
        number--;
        int i = zz * 8 + xx;
        treePositions[i] = 0;

    }

    void instantiateTrees()
    {
        // Trees are only generated when the player reaches some area.
        var playerX = playerPoint.position.x;
        var playerZ = playerPoint.position.z;
        if (playerX > -30 && playerX < 55 && playerZ > -65 && playerZ < 0)
        {
            oneTree();
        }
    }
    void oneTree()
    {
        // A tree will be generated at a random position on the 8x4 panel.
        // To avoid multiple trees at a same position, we use an array to record whether that position already has a tree.

        if (number < 30)
        {
           
            int xx = Random.Range(0, 8);
            int zz = Random.Range(0, 4);
            int i = zz * 8 + xx;

            if(treePositions[i] == 0)
            {
                positionX = -20 + xx * 10;
                positionZ = -60 + zz * 10;
                var treeObj = Instantiate(tree, new Vector3(positionX, -39f, positionZ), Quaternion.identity) as GameObject;
                treeObj.GetComponent<TreeCollision>().posX = xx;
                treeObj.GetComponent<TreeCollision>().posZ = zz;
                number++;
                treePositions[i] = 1;
            }
           
        }

    }
}
