using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowPoint : MonoBehaviour
{
    public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position,goal.transform.position) < 5f)
        {
            Destroy(this.gameObject);
        }
        this.transform.LookAt(goal);
    }
}
