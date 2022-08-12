using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyControl : MonoBehaviour
{
    Rigidbody moneyBody;
    Collider moneyCollider;
    playerScript playerScript;
    public GameObject moneyStack;
    float moneyStackedObjectY;
    bool doesHitAnObject;
    private void Awake()
    {
        moneyBody = this.gameObject.GetComponent<Rigidbody>();
        moneyCollider = this.gameObject.GetComponent<BoxCollider>();
        playerScript = FindObjectOfType<playerScript>();
        moneyStack = GameObject.FindGameObjectWithTag("moneyStackObject");
    }
    private void LateUpdate()
    {
        if (moneyStack.transform.childCount != 0 && doesHitAnObject)
        {
            moneyStackedObjectY = 0;
            for (int i = 0; i < moneyStack.transform.childCount; i++)
            {
                moneyStack.transform.GetChild(i).localPosition = new Vector3(moneyStack.transform.GetChild(i).localPosition.x,
                moneyStackedObjectY,
                moneyStack.transform.GetChild(i).localPosition.z);
                moneyStackedObjectY += .125f;
            }
            playerScript.moneyCount = moneyStack.transform.childCount * 100;
            doesHitAnObject = false;
        }
        if(playerScript.isGameFinished)
        {
            moneyBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="wall")
        {
            this.gameObject.transform.parent = null;
            moneyCollider.isTrigger = false;
            moneyBody.useGravity = true;
            if (playerScript.moneyCount != 0)
                playerScript.moneyCount -= 100;
            else
                playerScript.moneyCount = 0;
            playerScript.moneyCountText.text = "$ " + playerScript.moneyCount.ToString();
            doesHitAnObject = true;
        }
    }
}
