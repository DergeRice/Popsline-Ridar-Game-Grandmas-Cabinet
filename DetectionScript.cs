using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) 
    {
        if(other.collider.tag == "Ball")
        {
            GameManager.HitDetection(transform.parent.gameObject);
            Destroy(other.gameObject);
            
        }
    }


}
