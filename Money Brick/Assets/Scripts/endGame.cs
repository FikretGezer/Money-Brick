using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endGame : MonoBehaviour
{
    GameObject moneyCount;
    GameObject panel;
    playerScript playerScript;
    [SerializeField] TextMeshProUGUI endGameMoney;
    private void Awake()
    {
        moneyCount = gameObject.transform.GetChild(0).gameObject;
        panel = gameObject.transform.GetChild(1).gameObject;
        playerScript = FindObjectOfType<playerScript>();
    }
    public void GameFinished()
    {
        moneyCount.SetActive(false);
        panel.SetActive(true);
        endGameMoney.text = "$ "+(playerScript.moneyStack.transform.childCount * 100).ToString();
    }
}
