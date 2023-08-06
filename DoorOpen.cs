using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public float MyDoorNum;
    // Start is called before the first frame update
    void Start()
    {
        MyDoorNum = float.Parse(gameObject.name.Replace("door_v",""))-1;
        GetComponent<Animator>().SetFloat("DoorNum", MyDoorNum);
    }

    // Update is called once per frame
    void Update()
    {
    }

    [ContextMenu("Open")]
    public void OpenDoor()
    {
        GetComponent<Animator>().SetTrigger("DoorOpen");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GetComponent<Animator>().SetTrigger("DoorOpen");
        }
    }
}
