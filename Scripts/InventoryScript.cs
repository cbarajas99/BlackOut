using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public bool gain;
    public string key;

    //put all items here and set to 0
    int wireCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gain)
        {
            if(key == "wire")
            {
                wireCount++;
            }
            
        }
    }
}
