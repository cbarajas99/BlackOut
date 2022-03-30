using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireManager : MonoBehaviour
{   
    public Wire beginning;
    public Wire[] Wires;
    public Wire end;
    bool success = false;
    bool fail = false;
    bool play = false;

    public GameObject SuccessText;
    public GameObject TryAgainText;

    void first(){
        GameObject current;
        foreach(Wire w in Wires){
            current = GameObject.Find(w.name);
            w.location = current.transform.position;
        }
         current = GameObject.Find(beginning.name);
         beginning.location = current.transform.position;
         current = GameObject.Find(end.name);
         end.location = current.transform.position;
    }

    void Start(){
        FindObjectOfType<SoundManager>().Play("Puzzle music");
        // turn off final text //
        SuccessText.SetActive(false);
        TryAgainText.SetActive(false);
        
    }

    bool In(Wire wire, int side1){
        int other;
        if(wire.type[0] == side1){
            other = (int)wire.type[1];
        }
        else{
            other = (int)wire.type[0];
        }
         return Out(wire, other);
        
    }
    bool Out(Wire wire, int side){

        if(side == 0){
            bool found = false;
            int i = 0;
            if(new Vector3(wire.location.x, wire.location.y + 1, wire.location.z) == end.location){
                        return true;
                     }
            while(i < Wires.Length && found == false){
                if(Wires[i].location == new Vector3(wire.location.x, wire.location.y + 1, wire.location.z)
                 && (Wires[i].type[0] == 2 || Wires[i].type[1] == 2)){
                     found = true;
                     wire = Wires[i];
                     return In(wire, 2);
                }
                i++;
            }
            return false;
        }
        else if(side == 1){
            bool found = false;
            int i = 0;
            if(new Vector3(wire.location.x + 1, wire.location.y, wire.location.z) == end.location){
                        return true;
                     }
            while(i < Wires.Length && found == false){
                if(Wires[i].location == new Vector3(wire.location.x + 1, wire.location.y, wire.location.z)
                 && (Wires[i].type[0] == 3 || Wires[i].type[1] == 3)){
                     found = true;
                     wire = Wires[i];
                     return In(wire, 3);
                }
                i++;
            }

            return false;
            
        }
        else if(side == 2){
            bool found = false;
            int i = 0;
            if(new Vector3(wire.location.x, wire.location.y - 1, wire.location.z) == end.location){
                        return true;
                     }
            while(i < Wires.Length && found == false){
                if(Wires[i].location == new Vector3(wire.location.x, wire.location.y - 1, wire.location.z)
                 && (Wires[i].type[0] == 0 || Wires[i].type[1] == 0)){
                     found = true;
                     wire = Wires[i];
                     return In(wire, 0);
                }
                i++;
            }
            return false;
        }
        else{
            bool found = false;
            int i = 0;
            if(new Vector3(wire.location.x - 1, wire.location.y, wire.location.z) == end.location){
                        return true;
                     }
            while(i < Wires.Length && found == false){
                if(Wires[i].location == new Vector3(wire.location.x -1, wire.location.y, wire.location.z)
                 && (Wires[i].type[0] == 1 || Wires[i].type[1] == 3)){
                     found = true;
                     wire = Wires[i];
                     return In(wire, 1);
                }
                i++;
            }
            return false;
         }
        return false;
       }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(play){
                play = false;
            }
            else{
            play = true;
            }
        }
        if(play){
            first();
            if(Out(beginning, 2)){
                success = true;;
                SuccessText.SetActive(true);
                Invoke("Change", 6.0f);
            }
            else{
                TryAgainText.SetActive(true);
                fail = true;
                // will restart the puzzle after losing //
                Invoke("Restart", 3.0f);
                
               
            }
        }
        
    }
    void Change(){
        SceneManager.LoadScene("MainMenu");
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }



}
