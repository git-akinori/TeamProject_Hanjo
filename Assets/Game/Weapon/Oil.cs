using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0.0f, Time.deltaTime / 4, 0.0f);
        transform.localScale += new Vector3(0, Time.deltaTime, 0);

        if (transform.localScale.y >= 2.0f)
        {
            Destroy(gameObject);
        }
	}
}
