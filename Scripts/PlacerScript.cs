using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerScript : MonoBehaviour
{
   
   //this is the object that the user wants to place
    public GameObject part;
    public Camera camera;
    void Start()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            part.transform.position = objectHit.position;
            part.transform.eulerAngles = objectHit.eulerAngles;
            part.transform.localScale = objectHit.localScale;


        }

    }
    // Update is called once per frame
    void Update()
    {
        //maybe here we can rotate part if player wants or something?
    }
}
