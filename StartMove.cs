using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{

    public float Amount;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right* Amount;
    }
}
