using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Yarn.Unity.Example;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private float animMoveSpeed;
    public float jumpSpeed;
    private float tempY;
    private float gravity;
    public float defaultGravity = 20;
    private bool doubleJumped;

    private float playerVelocity;
    public float maxPlayerVelocity;

    private bool lastGrounded;
    private int groundedFrame;

    private Vector3 finalMovement;

    CharacterController cc;
    Animator anim;
    GameObject rig;
    GameObject rigHolder;

    private Vector3 translationVector;

    private enum PState {Walking, Hoverbike, Driving};

    private PState playerstate = PState.Walking;

    private GameObject hoverbike;

    private float defaultColliderRadius;
    private float defaultColliderHeight;

    private float bikeColliderRadius;
    private float bikeColliderHeight;

    public CameraScript cameraScript;

    private Vector3 upright;

    bool doMovement = true;

    CarS carScript;

    //How many parts the player has collected

    private int collected;



    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().Play("Background music");

        gravity = defaultGravity;
        cc = gameObject.GetComponent<CharacterController>();

        //Player's collider while walking
        defaultColliderRadius = cc.radius;
        defaultColliderHeight = cc.height;

        //Player's collider while on hoverbike
        bikeColliderRadius = 3f * defaultColliderRadius;
        bikeColliderHeight = 2f * defaultColliderHeight;

        anim = GetComponentInChildren<Animator>();

        //Locate the rig (which is animated and rotated on y) and the rig holder (which is tilted to match terrain normal)
        rig = GameObject.Find("MaleFree1");
        rigHolder = GameObject.Find("RigHolder");

        anim.SetBool("Grounded", true);
        translationVector = new Vector3(0f, 0f, 0f);
        groundedFrame = 5;

        //cameraScript.LerpToFov(35f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //BEGIN PLAYER MOVEMENT CODE
        //if we movin (not in car)
        if (doMovement)
        {



            animMoveSpeed = anim.GetFloat("MoveSpeed");

            //Set the move animation's running speed to the current movement speed, but lerped.

            //anim.SetFloat("MoveSpeed", Mathf.Lerp(animMoveSpeed, new Vector3(translationVector.x, 0f, translationVector.z).magnitude*20f,7f*Time.deltaTime));

            anim.SetFloat("MoveSpeed", Mathf.Lerp(animMoveSpeed, new Vector3(cc.velocity.x, 0f, cc.velocity.z).magnitude, 0.35f));


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                //Debug.Log("pressin");
                if (playerstate == PState.Walking)
                {
                    playerVelocity = Mathf.Lerp(playerVelocity, maxPlayerVelocity, 0.04f);
                }
                else if (playerstate == PState.Hoverbike)
                {
                    playerVelocity = Mathf.Lerp(playerVelocity, maxPlayerVelocity, 0.01f);
                }

            }

            else
            {
                playerVelocity = Mathf.Lerp(playerVelocity, 0f, 0.05f);
            }

            if (playerVelocity >= maxPlayerVelocity)
            {
                playerVelocity = maxPlayerVelocity;
            }

            if (playerVelocity < 0f)
            {
                playerVelocity = 0f;
            }


            if (cc.isGrounded && !lastGrounded)
            {
                anim.SetTrigger("Land");
            }

            tempY = translationVector.y;

            translationVector = (Quaternion.AngleAxis(33, Vector3.up) * new Vector3(-Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal")));
            //translationVector = Quaternion.AngleAxis(33, Vector3.up) * new Vector3(-Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime, translationVector.y, Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime);

            translationVector *= playerVelocity;
            translationVector.y = tempY;



            if (cc.isGrounded)
            {
                //translationVector = Quaternion.AngleAxis(33, Vector3.up) * new Vector3(-Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime, 0, Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime);
                translationVector.y = 0f;
                if (Input.GetButton("Jump") && playerstate == PState.Walking)
                {

                    translationVector.y = jumpSpeed;
                    anim.SetTrigger("Jump");

                    doubleJumped = false;
                }
                //playerVelocity *= 0.9f;
            }
            else
            {
                if (!doubleJumped && Input.GetButtonDown("Jump") && playerstate == PState.Walking)
                {
                    translationVector.y = jumpSpeed;
                    anim.SetTrigger("Jump");
                    doubleJumped = true;
                }
            }

            /*if(playerstate == PState.Hoverbike && Input.GetButton("Jump"))
            {
                translationVector.y += 4f;
            }
            */




            translationVector.y -= gravity * Time.deltaTime;

            //Debug.Log(translationVector.y);
            //Debug.Log(translationVector);

            if (playerstate == PState.Hoverbike && Input.GetButton("Jump"))
            {
                finalMovement = Vector3.Slerp(new Vector3(cc.velocity.x, 0f, cc.velocity.z), new Vector3(translationVector.x, 0f, translationVector.z), 0.05f);
                translationVector.y += 1.1f * gravity * Time.deltaTime;
            }
            else if (playerstate == PState.Hoverbike)
            {
                finalMovement = Vector3.Slerp(new Vector3(cc.velocity.x, 0f, cc.velocity.z), new Vector3(translationVector.x, 0f, translationVector.z), 0.05f);
                translationVector.y += 0.8f * gravity * Time.deltaTime;
            }
            else
            {
                finalMovement = Vector3.Slerp(new Vector3(cc.velocity.x, 0f, cc.velocity.z), new Vector3(translationVector.x, 0f, translationVector.z), 0.35f);

            }
            

            finalMovement.y = translationVector.y;


            //TODO: Check me out: took out all the velocity and final movement bullshit and made a much more responsive controller.
            //cc.Move(finalMovement * Time.deltaTime);
            cc.Move(translationVector*Time.deltaTime);
            //cc.Move(translationVector * Time.deltaTime + 0.1f*new Vector3(cc.velocity.x,0f,cc.velocity.z)*Time.deltaTime);

            //this.gameObject.transform.localEulerAngles = new Vector3(0f, 20f, 0f);
            //if(translationVector.x > 0.01f && translationVector.z > 0.01f)
            //{
            //    rig.transform.rotation = Quaternion.LookRotation(translationVector);
            //Mathf.Rad2Deg * Mathf.Acos(translationVector.z/ translationVector.x)
            //}

            
            //This code rotates the rig, which is the player's walking direction, and the rig holder, which tilts according to ground normal.
            if (translationVector.x >= 0.01f || translationVector.x <= -0.01f || translationVector.z >= 0.01f || translationVector.z <= -0.01f)
            {
                
                if (playerstate == PState.Walking)
                {
                    Ray ray = new Ray(transform.position, -transform.up);
                    RaycastHit[] hits = null;
                    RaycastHit validHit;
                    Quaternion rot;
                    Quaternion tilt = Quaternion.identity;
                    float smooth = 5f;

                    if (playerstate == PState.Walking)
                    {
                        hits = Physics.RaycastAll(ray, 2f);
                    }
                    else
                    {
                        hits = Physics.RaycastAll(ray, 20f);
                    }


                    if (hits != null)

                    {
                        foreach (RaycastHit possibleGround in hits)
                        {
                            if (possibleGround.collider.gameObject.tag == "TiltToMe")
                            {
                                validHit = possibleGround;
                                rot = Quaternion.FromToRotation(rigHolder.transform.up, validHit.normal) * rigHolder.transform.rotation;

                                rot = Quaternion.Slerp(rigHolder.transform.rotation, rot, Time.deltaTime * smooth);

                                Vector3 rotVector = rot.eulerAngles;

                                rotVector.y = 0f;

                                rigHolder.transform.localRotation = Quaternion.Euler(rotVector);

                                //rig.transform.rotation = tilt * Quaternion.Slerp(rig.transform.rotation, Quaternion.LookRotation(new Vector3(cc.velocity.x, 0f, cc.velocity.z)), 0.9f);

                            }
                        }

                    }
                    else
                    {
                        rigHolder.transform.rotation = Quaternion.Slerp(rigHolder.transform.rotation, Quaternion.LookRotation(rigHolder.transform.up), Time.deltaTime * smooth);

                    }
                    
                    //Rotate the rig (which direction player is looking) lerping from the old rig rotation for smoothness.
                    Vector3 temp = Quaternion.Slerp(rig.transform.rotation, Quaternion.LookRotation(new Vector3(cc.velocity.x, 0f, cc.velocity.z)), 0.4f).eulerAngles;
                    temp.x = 0f; temp.z = 0f;
                    rig.transform.localRotation = Quaternion.Euler(temp);
                }
               else
                {
                    //Rotate the rig for vehicles based on velocity (vehicle's heading should always look where its going)
                    Vector3 temp = Quaternion.Slerp(rig.transform.rotation, Quaternion.LookRotation(new Vector3(cc.velocity.x, cc.velocity.y, cc.velocity.z)), 0.2f).eulerAngles;
                    //temp.x = 0f; temp.z = 0f;

                    

                    rig.transform.localRotation = Quaternion.Euler(temp);
               }






                //rig.transform.rotation = Quaternion.LookRotation(new Vector3(cc.velocity.x, 0f, cc.velocity.z));
            }

         

            //anim.SetFloat("MoveSpeed", cc.velocity.magnitude);


            //TODO: Fix glitchy groundedness checking below.
            /*
            groundedFrame += 1;
            if(groundedFrame >= 5)
            {
                anim.SetBool("Grounded", cc.isGrounded);
                groundedFrame = 0;
            }
            */
            anim.SetBool("Grounded", cc.isGrounded);

            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("Wave");
            }




            lastGrounded = cc.isGrounded;
            //Debug.Log(playerVelocity);


        }
        //END MOVEMENT CODE

        //INTERACTIONS
        //if (Input.GetKeyDown(KeyCode.E))
        if(Input.GetButtonDown("Interact"))
        {
            if (FindObjectOfType<DialogueRunner>().IsDialogueRunning)
            {
                FindObjectOfType<DialogueUI>().MarkLineComplete();
            }
            else if (playerstate == PState.Walking)
            {
                Collider[] inRange = Physics.OverlapSphere(transform.position, 3f);
                List<GameObject> interactable = new List<GameObject>();
                foreach (Collider coll in inRange)
                {
                    if (coll.gameObject.tag == "Interactable")
                    {
                        interactable.Add(coll.gameObject);
                    }
                }

                GameObject closest = null;
                float dist = 999f;

                foreach (GameObject interactObj in interactable)
                {

                    if ((this.transform.position - interactObj.transform.position).magnitude < dist)
                    {

                        closest = interactObj;
                        dist = (this.transform.position - interactObj.transform.position).magnitude;
                    }

                }

                if (closest != null)
                {
                    //BEGIN INTERACTIONS (between player and interactable objects)
                    Debug.Log("Player interacted with: " + closest.GetComponent<PlayerInteractable>().interactName);
                    if (closest.GetComponent<HoverbikeScript>() != null)
                    {
                        cameraScript.LerpToFov(75f, 0.02f);

                        //gravity = 5f;
                        HoverbikeScript hover = closest.GetComponent<HoverbikeScript>();
                        this.hoverbike = closest;
                        playerstate = PState.Hoverbike;

                        this.transform.position = closest.transform.Find("Seat").transform.position;

                        this.transform.rotation = Quaternion.identity;
                        rigHolder.transform.rotation = Quaternion.identity;
                        rig.transform.rotation = hoverbike.transform.rotation;
                        hoverbike.transform.parent = rig.transform;

                        cc.radius = bikeColliderRadius;
                        cc.height = bikeColliderHeight;

                        maxPlayerVelocity += 8f;
                    }
                    //Interaction: Entering car
                    else if (closest.GetComponent<PlayerInteractable>().interactName == "car")
                    {
                        playerstate = PState.Driving;
                        SetPlayerActive(false);

                        carScript = closest.GetComponent<CarS>();
                        carScript.enabled = true;
                        cameraScript.target = closest.transform;
                    }
                    else if (closest.GetComponent<PlayerInteractable>().interactName == "pickupdestroy")
                    {
                        collected += 1;
                        if(collected >= 5) //TODO: switch to 10 collectable objects
                        {
                            SceneManager.LoadScene(2);
                        }
                        Destroy(closest);
                    }
                    else if(closest.GetComponent<PlayerInteractable>().interactName == "dialogue")
                    {
                        if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
                        {
                            return;
                        }

                        FindObjectOfType<DialogueRunner>().StartDialogue(closest.GetComponent<NPC>().talkToNode);
                    }
                }
            }
            //Interaction: Exiting hoverbike
            else if (playerstate == PState.Hoverbike)
            {
                //gravity = defaultGravity;
                Debug.Log("BIKE DISMOUNT");
                hoverbike.transform.parent = null;
                hoverbike.transform.rotation = Quaternion.identity;

                cc.radius = defaultColliderRadius;
                cc.height = defaultColliderHeight;
                playerstate = PState.Walking;
                maxPlayerVelocity -= 8f;

                cameraScript.LerpToFov(60f, 0.1f);
            }
            //Interaction: Exiting vehicle
            else if (playerstate == PState.Driving)
            {
                carScript.enabled = false;
                playerstate = PState.Walking;
                SetPlayerActive(true);
                this.transform.position = carScript.gameObject.transform.Find("DoorExitPoint").transform.position;
                cameraScript.target = this.gameObject.transform;
            }
        }

        ///END INTERACTIONS

        if (playerstate != PState.Walking)
        {
            anim.SetFloat("MoveSpeed", 0f);
        }

        //For testing different controller buttons. Mappings seem to differ betwen platforms? I.E. playstation button map is different on osx, win
        /*
        for(int i = 0; i <=15; i++)
        {
            if (Input.GetKey("joystick button " + i.ToString()))
            {
                print(i.ToString());
            }
        }
        */
        
    }

    //Activate and deactive player walking controller
    void SetPlayerActive(bool active)
    {
        doMovement = active;
        rig.SetActive(active);
        this.GetComponent<CharacterController>().enabled = active;

    }


}
