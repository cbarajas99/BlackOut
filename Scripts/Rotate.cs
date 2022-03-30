using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float speed = 50.0f;
    public int status;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(status == 1) {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }

        
    }
}
