using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NonAniMove : MonoBehaviour
{

    public Vector3 Amount;
    public float LerpTime,ComeBackTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    [ContextMenu("dd")]
    public void Shake()
    {
        //transform.DOMove(transform.position + Amount, LerpTime).SetEase(Ease.OutBounce);
        //transform.DOMove(transform.position, ComeBackTime).SetEase(Ease.OutBounce).SetDelay(LerpTime);
        transform.DOShakePosition(0.1f,0.005f,1,90,false,true,ShakeRandomnessMode.Harmonic);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Shake();
        }
    }
}
