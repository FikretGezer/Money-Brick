using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawn : MonoBehaviour
{
    public GameObject moneyPrefab;
    GameObject prefabedObject;
    float oncekiZ=10;
    float oncekiX;
    private void Awake()
    {
        oncekiX = Random.Range(-4f, 4f);
        
        for (int i = 0; i < 60; i++)
        {
           prefabedObject= Instantiate(moneyPrefab, new Vector3(oncekiX, .62f, oncekiZ), Quaternion.identity);
           oncekiZ = prefabedObject.transform.position.z;
           oncekiZ += Random.Range(1f, 2f)*5f;
           oncekiZ = Mathf.Clamp(oncekiZ, 0, 300f);
           oncekiX = Random.Range(-4f, 4f);
            if (oncekiZ >= 290f)
                break;
        }
    }
}
