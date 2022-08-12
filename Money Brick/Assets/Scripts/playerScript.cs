using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    Animator playerAnim;
    Rigidbody playerBody;
    CharacterController playerController;
    endGame endgame;
    float gravity = -9.81f;
    Vector3 velocity;
    
    [SerializeField]
    float speed;
    [SerializeField]
    public GameObject moneyStack;

    bool isGrounded;
    public Transform groundCheck;
    public float groundDist = .4f;
    public LayerMask groundMask;
    float x;

    public GameObject[] cubes;
    List<GameObject> cubesCollected = new List<GameObject>();
    GameObject lastCubeCollected;
    //float oncekiCube=56.591f;
    //float oncekiCube = 490.25f; //yolun sonunu belirlemek için yazýldý. Bu sayede paralarý ona göre dizecek.(3.73 trigger ile fark)
    float oncekiCube = 318.695f; //yolun sonunu belirlemek için yazýldý. Bu sayede paralarý ona göre dizecek.(3.73 trigger ile fark)
    bool isOver;
    public bool isGameFinished;
    [SerializeField]public TextMeshProUGUI moneyCountText;
    public int moneyCount;
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
        cubes = GameObject.FindGameObjectsWithTag("money");
        endgame = FindObjectOfType<endGame>();
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
        if (Physics.Raycast(transform.position, -transform.up, out var hit, Mathf.Infinity))
        {
            var obj = hit.collider.gameObject;
            if (obj.tag == "money")
            {
                if (transform.position.z > (lastCubeCollected.transform.position.z-.25f) 
                   && transform.position.z < (lastCubeCollected.transform.position.z + .25f))
                {
                    var pos = transform.position;
                    pos.z = lastCubeCollected.transform.position.z;
                    transform.position = pos;
                    isOver = true;
                    endgame.GameFinished();
                }                    
            }  
        }
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
            for (int i = 0; i < moneyStack.transform.childCount; i++)
            {
                cubesCollected.Add(moneyStack.transform.GetChild(i).gameObject);
                lastCubeCollected = moneyStack.transform.GetChild(moneyStack.transform.childCount - 1).gameObject;
            }
            for (int i = 0; i < cubesCollected.Count; i++)
            {
                moneyStack.transform.parent = null;
                cubesCollected[i].transform.position = new Vector3(transform.position.x, 0, oncekiCube);
                cubesCollected[i].GetComponent<BoxCollider>().isTrigger = false;
                oncekiCube += .5f;
            }
            isGameFinished = true;
        }
    }
}
