using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    public float interactDistance = 1f;
    public float lightRange = 1f;

    private GameObject player;
    private bool showingInteract;
    private Light light;

    public string interactName;

    // Start is called before the first frame update
    void Start()
    {
        showingInteract = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if((transform.position - player.transform.position).magnitude < interactDistance && !showingInteract)
        {
            light = gameObject.AddComponent<Light>();

            StartCoroutine(FadeIn(light, 300f));

            light.color = Color.blue;
            
            light.range = lightRange;

            showingInteract = true;
        }

        if((transform.position - player.transform.position).magnitude > interactDistance && showingInteract)
        {

            StartCoroutine(FadeOut(light, 3000f));
            showingInteract = false;
            
        }
    }
 
    IEnumerator FadeIn(Light lt, float intensity)
    {
        float duration = 1f;//time you want it to run

        float interval = 0.1f;//interval time between iterations of while loop

        lt.intensity = 0.0f;

        while (duration >= 0.0f)
        {
            
            lt.intensity += 1f;

            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }
    }

    IEnumerator FadeOut(Light lt, float intensity)
    {
        float duration = 1f;//time you want it to run

        float interval = 0.1f;//interval time between iterations of while loop

        //lt.intensity = 0.0f;

        while (duration >= 0.0f && light.intensity > 0f)
        {

            lt.intensity -= 2f;

            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }

        Destroy(lt, 1f);
        //showingInteract = false; */
    }
    
}
