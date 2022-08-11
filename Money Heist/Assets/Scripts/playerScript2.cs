using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerScript2 : MonoBehaviour
{
    Animator playerAnim;
    Rigidbody playerBody;
    CharacterController playerController;
    float gravity = -9.81f;
    Vector3 velocity;
    
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject moneyStack;

    bool isGrounded;
    public Transform groundCheck;
    public float groundDist = .4f;
    public LayerMask groundMask;
    float x;

    public GameObject[] cubes;
    //float oncekiCube=56.591f;
    float oncekiCube = 490.25f;
    bool isOver;
    [SerializeField]Text moneyCountText;
    int moneyCount;
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
        cubes = GameObject.FindGameObjectsWithTag("money");
    }

    private void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
        Movements();
    }

    void Movements()
    {
     //   isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        x = Input.GetAxis("Horizontal");
        Vector3 dir = transform.right * x + transform.forward;

        if (!isOver)
        {
            playerController.Move(dir * speed);
            playerAnim.SetBool("isRunning", true);
        }
        else
            playerAnim.SetBool("isRunning", false);    
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="money")
        {            
            if (moneyStack.transform.childCount==0)
            {
                other.gameObject.transform.position = moneyStack.transform.position;
                other.gameObject.transform.parent = moneyStack.transform;
            }
            else
            {
                other.gameObject.transform.position = new Vector3(moneyStack.transform.position.x,
                moneyStack.transform.position.y+(moneyStack.transform.childCount*.125f), 
                moneyStack.transform.position.z);
                other.gameObject.transform.parent = moneyStack.transform;
            }
            moneyCount += 100;
            moneyCountText.text ="$ " + moneyCount.ToString();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="endORoad")
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                moneyStack.transform.parent = null;
                cubes[i].transform.position = new Vector3(transform.position.x, 0, oncekiCube);
                cubes[i].GetComponent<BoxCollider>().isTrigger = false;
                oncekiCube += .5f;
            }
        }
    }
}
