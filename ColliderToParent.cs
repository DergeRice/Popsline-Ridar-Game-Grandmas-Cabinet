using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag  == "Ball")
        //{
        //    transform.parent.GetComponent<MonsterMove>().OnCollisionEnter(collision);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            transform.parent.GetComponent<MonsterMove>().ParticlePos = collision.transform.parent.gameObject.GetComponent<BallScript>().MovPos;
            transform.parent.GetComponent<MonsterMove>().BallHit();
        }
    }
}
