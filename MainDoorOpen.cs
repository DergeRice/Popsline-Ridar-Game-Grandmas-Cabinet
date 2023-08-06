using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainDoorOpen : MonoBehaviour
{
    public GameObject FirstDoor, SecondDoor, FinalDoor;
    public bool RightDoor;
    public AudioClip[] Sound = new AudioClip[5];  // [1]: door open /  [2]: door silding / [3]: zoom sound

    public GameObject LeftDoorIns, RightDoorIns;
    [Range(0, 10)]
    public float FirstToSecAmount, FirstToFinalAmount, SecToFinalAmount;
    public float FirstFrameTime, SecondFrameTime, FirstToSecDelayTime;

    Vector3[] InitialPos = new Vector3[6];

    
    // Start is called before the first frame update
    void Start()
    {
        InitialPos[0] = FirstDoor.transform.GetChild(0).position;
        InitialPos[1] = FirstDoor.transform.GetChild(1).position;
        InitialPos[2] = SecondDoor.transform.GetChild(0).position;
        InitialPos[3] = SecondDoor.transform.GetChild(1).position;
        InitialPos[4] = FinalDoor.transform.GetChild(0).position;
        InitialPos[5] = FinalDoor.transform.GetChild(1).position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Open")]
    public void OpenDoor()
    {
        InitialPos[0] = FirstDoor.transform.GetChild(0).position;
        InitialPos[1] = FirstDoor.transform.GetChild(1).position;
        InitialPos[2] = SecondDoor.transform.GetChild(0).position;
        InitialPos[3] = SecondDoor.transform.GetChild(1).position;
        InitialPos[4] = FinalDoor.transform.GetChild(0).position;
        InitialPos[5] = FinalDoor.transform.GetChild(1).position;

        Vector3 FirstTOSec0 = FirstDoor.transform.position + ((SecondDoor.transform.GetChild(0).position - transform.position) * FirstToSecAmount / 10);
        FirstTOSec0 = new Vector3(FirstTOSec0.x, FirstDoor.transform.position.y, FirstDoor.transform.position.z);

        Vector3 FirstTOSec1 = FirstDoor.transform.position + ((SecondDoor.transform.GetChild(1).position - transform.position) * FirstToSecAmount / 10);
        FirstTOSec0 = new Vector3(FirstTOSec0.x, FirstDoor.transform.position.y, FirstDoor.transform.position.z);

        Vector3 FirstTOFinal0 = FirstDoor.transform.position + ((FinalDoor.transform.GetChild(0).position - transform.position) * FirstToFinalAmount / 10);
        FirstTOSec0 = new Vector3(FirstTOSec0.x, FirstDoor.transform.position.y, FirstDoor.transform.position.z);

        Vector3 FirstTOFinal1 = FirstDoor.transform.position + ((FinalDoor.transform.GetChild(1).position - transform.position) * FirstToFinalAmount / 10);
        FirstTOSec0 = new Vector3(FirstTOSec0.x, FirstDoor.transform.position.y, FirstDoor.transform.position.z);

        Vector3 SecondTOFinal0 =  SecondDoor.transform.position + ((FinalDoor.transform.GetChild(0).position - transform.position) * SecToFinalAmount / 10);
        FirstTOSec0 = new Vector3(FirstTOSec0.x, SecondDoor.transform.position.y, SecondDoor.transform.position.z);

        Vector3 SecondTOFinal1 = SecondDoor.transform.position + ((FinalDoor.transform.GetChild(1).position - transform.position) * SecToFinalAmount / 10);
        FirstTOSec0 = new Vector3(FirstTOSec0.x, SecondDoor.transform.position.y, SecondDoor.transform.position.z);

        //1 Frame
        FirstDoor.transform.GetChild(0).DOMove(FirstTOSec0, FirstFrameTime).SetEase(Ease.OutBounce);
        FirstDoor.transform.GetChild(1).DOMove(FirstTOSec1, FirstFrameTime).SetEase(Ease.OutBounce);
        if(Sound != null) StartCoroutine(PlaySound(Sound[0], 0));
        if (Sound != null) StartCoroutine(PlaySound(Sound[1], 0));


        //2Frame
        FirstDoor.transform.GetChild(0).DOMove(FirstTOFinal0, SecondFrameTime).SetEase(Ease.OutBounce).SetDelay(FirstToSecDelayTime);
        FirstDoor.transform.GetChild(1).DOMove(FirstTOFinal1, SecondFrameTime).SetEase(Ease.OutBounce).SetDelay(FirstToSecDelayTime);
        if (Sound != null) StartCoroutine(PlaySound(Sound[0], FirstToSecDelayTime+0.2f));

        SecondDoor.transform.GetChild(0).DOMove(SecondTOFinal0, SecondFrameTime).SetEase(Ease.OutBounce).SetDelay(FirstToSecDelayTime);
        SecondDoor.transform.GetChild(1).DOMove(SecondTOFinal1, SecondFrameTime).SetEase(Ease.OutBounce).SetDelay(FirstToSecDelayTime);

        if (Sound != null) StartCoroutine(PlaySound(Sound[1], FirstToSecDelayTime - 0.1f));

        //3Framee

        Camera.main.gameObject.transform.DOMoveZ(-15f, 2f).SetDelay(FirstToSecDelayTime + 0.5f);
        if (Sound != null) StartCoroutine(PlaySound(Sound[2], FirstToSecDelayTime));

    }
    
    IEnumerator PlaySound(AudioClip sound, float Delay)
    {
        yield return new WaitForSeconds(Delay);

        AudioManager.audioManager.audioSource.PlayOneShot(sound);
    }

    [ContextMenu("Close")]
    public void DoorClose()
    {
        FirstDoor.transform.GetChild(0).DOMove(InitialPos[0], 1f);

        FirstDoor.transform.GetChild(1).DOMove(InitialPos[1], 1f);

        if (Sound != null) StartCoroutine(PlaySound(Sound[1], 0));



        SecondDoor.transform.GetChild(0).DOMove(InitialPos[2], 1f);

        SecondDoor.transform.GetChild(1).DOMove(InitialPos[3], 1f);

        FinalDoor.transform.GetChild(0).DOMove(InitialPos[4], 1f);

        FinalDoor.transform.GetChild(1).DOMove(InitialPos[5], 1f);

        Camera.main.gameObject.transform.DOMoveZ(0, 2f).SetDelay(FirstToSecDelayTime + 0.5f);

        if (Sound != null) StartCoroutine(PlaySound(Sound[2], 0));
    }
}
