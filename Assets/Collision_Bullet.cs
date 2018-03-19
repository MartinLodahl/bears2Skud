using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "enemy")
        {
            health otherScript = col.gameObject.GetComponent<health>();
            otherScript.takeDamage(10);
            this.gameObject.SetActive(false);
        }
    }
}
