using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    public GameObject pos;
    Vector3 LookPos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.parent.gameObject.GetComponent<MonsterScript>().MyNavPos;
        LookPos = new Vector3(pos.transform.position.x, transform.position.y, transform.position.z);
        transform.LookAt(LookPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).gameObject.GetComponent<MonsterMove>().LookatPos)
        {
            LookPos = new Vector3(pos.transform.position.x, transform.position.y, transform.position.z);
            //transform.DOLookAt(LookPos, 1f).SetEase(Ease.InCirc);
            transform.LookAt(LookPos);
        }

        


    }


}
