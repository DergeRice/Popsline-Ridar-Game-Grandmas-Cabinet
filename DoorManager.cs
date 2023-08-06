using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorManager : MonoBehaviour
{

    public GameObject LeftDoor, FrontDoor, RightDoor, LeftDoorDummy, RightDoorDummy;
    public static DoorManager doorManager;
    public float TimetoInactive;

    // Start is called before the first frame update
    void Start()
    {
        doorManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Paze2Event()
    {
        //StopCoroutine(MonsterSpawner.monsterSpawner.newSpawn);
        FrontDoor.GetComponent<MainDoorOpen>().OpenDoor();
        LeftDoor.transform.DOMoveX(LeftDoor.transform.position.x + 220f,2f);

        for(int i=0; i < LeftDoorDummy.transform.childCount; i++)
        {
            StartCoroutine(ActiveDoor(LeftDoorDummy.transform.GetChild(i).gameObject));
            LeftDoorDummy.transform.GetChild(i).DOMoveX(LeftDoorDummy.transform.GetChild(i).position.x + 200, 2f);
        }

        StartCoroutine(SideDoorOpen(LeftDoorDummy.transform.GetChild(LeftDoorDummy.transform.childCount - 1).gameObject));

        RightDoor.transform.DOMoveX(RightDoor.transform.position.x - 220f, 2f);

        for (int i = 0; i < RightDoorDummy.transform.childCount; i++)
        {
            StartCoroutine(ActiveDoor(RightDoorDummy.transform.GetChild(i).gameObject));
            RightDoorDummy.transform.GetChild(i).DOMoveX(RightDoorDummy.transform.GetChild(i).position.x - 200, 2f);
        }

        StartCoroutine(SideDoorOpen(RightDoorDummy.transform.GetChild(RightDoorDummy.transform.childCount - 1).gameObject));

    }
    IEnumerator ActiveDoor(GameObject door)
    {
        door.SetActive(true);
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator InActiveDoor(GameObject door)
    {
        yield return new WaitForSeconds(TimetoInactive);
        door.SetActive(false);
        
    }
    public void returnToPaze1()
    {
        FrontDoor.GetComponent<MainDoorOpen>().DoorClose();
        LeftDoorDummy.transform.GetChild(LeftDoorDummy.transform.childCount - 1).gameObject.GetComponent<MainDoorOpen>().DoorClose();
        RightDoorDummy.transform.GetChild(RightDoorDummy.transform.childCount - 1).gameObject.GetComponent<MainDoorOpen>().DoorClose();
        StartCoroutine(SideDoorReturnPaze1());
    }

    IEnumerator SideDoorOpen(GameObject door)
    {
        yield return new WaitForSeconds(3f);
        door.GetComponent<MainDoorOpen>().OpenDoor();
    }

    IEnumerator SideDoorClose(GameObject door)
    {
        yield return new WaitForSeconds(0f);
        door.GetComponent<MainDoorOpen>().DoorClose();
    }

    IEnumerator SideDoorReturnPaze1()
    {

        float Delta = 2f;
        yield return new WaitForSeconds(Delta);
        LeftDoor.transform.DOMoveX(LeftDoor.transform.position.x - 220f, 2f);

        for (int i = LeftDoorDummy.transform.childCount-1; i >= 0 ; i--)
        {
            LeftDoorDummy.transform.GetChild(i).DOMoveX(LeftDoorDummy.transform.GetChild(i).position.x - 200f, 2f) ;

            StartCoroutine(InActiveDoor(LeftDoorDummy.transform.GetChild(i).gameObject));
        }

        RightDoor.transform.DOMoveX(RightDoor.transform.position.x + 220f, 2f) ;

        for (int i = RightDoorDummy.transform.childCount-1; i >= 0; i--)
        {
            RightDoorDummy.transform.GetChild(i).DOMoveX(RightDoorDummy.transform.GetChild(i).position.x + 200f, 2f);

            StartCoroutine(InActiveDoor(RightDoorDummy.transform.GetChild(i).gameObject));
        }

    }
}
