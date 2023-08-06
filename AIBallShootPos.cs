using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBallShootPos : MonoBehaviour
{
    const float XFerset = 0.45f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(Random.Range(-XFerset,XFerset),0,0);
        InvokeRepeating(nameof(SetRandomPos),09f,0.4f);
    }
    
    void SetRandomPos()
    {
        transform.localPosition = new Vector3(Random.Range(-XFerset,XFerset),0,0);
    }
}
