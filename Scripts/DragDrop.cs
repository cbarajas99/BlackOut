using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour{

    public LayerMask wires;

    Vector2 prev =  new Vector2(100, 200);

    bool clicked = false;
    void Start(){
        prev = transform.position;
    }
    void OnMouseDown(){
        clicked = true;
        Debug.Log("mousedown");
    }
    void Update(){
        if(clicked){
            //update transform
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            
        }
    }

    void OnMouseUp(){
        //snap
        Vector2 finalPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        //chceck colliders
        if (Physics2D.OverlapCircleAll(finalPosition, .1f, wires).Length > 1){
            transform.position = prev;
        }
        else {
            transform.position = finalPosition;
            prev = transform.position;
        }
        clicked = false;
    }
}
