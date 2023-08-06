using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = this;
        Display.displays[1].Activate();
        Display.displays[2].Activate();
        Display.displays[3].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shake(float ShakeTime)
    {
        transform.DOShakePosition(ShakeTime);
    }
}
