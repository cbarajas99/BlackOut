using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotScript : MonoBehaviour
{
    public GameObject Robot;
    public bool Power;
    public string DialogueKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool PowerOnRobot()
    {
        if (!Power)
        {
            Power = true;
            return true;
            //robot is now on
        }
        //robot is now off
        Power = false;
        return false;
    }

    void Dialogue()
    {
        if(DialogueKey == "")
        {
            //run whatever dialogue
        }
    }
}
