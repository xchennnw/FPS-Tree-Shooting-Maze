using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour
{

    private TreeManager treeManager;
    public int posX;
    public int posZ;

    // Start is called before the first frame update
    void Start()
    {
        treeManager = GameObject.Find("TreeManager").GetComponent<TreeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision co)
    {
        if(co.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            treeManager.UpdateScore();
            treeManager.decreaseTree(posX, posZ);
        }
       
        
      
    }
}
