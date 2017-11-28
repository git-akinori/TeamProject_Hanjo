using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {

    private int enemyNum = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            ++enemyNum;
            //Debug.Log(gameObject.name + " apper");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            --enemyNum;
            //Debug.Log(gameObject.name + " disapper");
        }
    }

    public int EnemyNum { get { return enemyNum; } }
}
