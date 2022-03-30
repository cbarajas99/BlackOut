using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    int selectedSlot;
    int prevSelectedSlot;
    public GameObject[] slots;

    public List<GameObject> items = new List<GameObject>(5);

    private Color unselectedSlotColor;

    // Start is called before the first frame update
    void Start()
    {
        selectedSlot = 0;
        unselectedSlotColor = new Color(0, 0, 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        prevSelectedSlot = selectedSlot;

        foreach (GameObject game in slots)
        {
            game.GetComponent<Image>().color = unselectedSlotColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(selectedSlot != 1)
            {
                selectedSlot = 1;
            }
            else
            {
                selectedSlot = -1;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (selectedSlot != 2)
            {
                selectedSlot = 2;
            }
            else
            {
                selectedSlot = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (selectedSlot != 3)
            {
                selectedSlot = 3;
            }
            else
            {
                selectedSlot = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (selectedSlot != 4)
            {
                selectedSlot = 4;
            }
            else
            {
                selectedSlot = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (selectedSlot != 5)
            {
                selectedSlot = 5;
            }
            else
            {
                selectedSlot = -1;
            }
        }

        if(selectedSlot >=1 && selectedSlot <= 6)
        {
            slots[selectedSlot - 1].GetComponent<Image>().color = Color.white;
        }


        //for(int i = 0)
        

    }
}
