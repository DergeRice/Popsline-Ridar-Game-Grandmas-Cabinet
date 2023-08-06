using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;

public class MonsterMove : MonoBehaviour
{
    Animator ani;

    public GameObject SpawnParticle01, SpawnParticle02;

    public float MyMoveAmount, RotateOffset;
    public GameObject MyNavPos, DeathParticle01,DeathParticle02;
    public bool LookatPos, UpsideDown = false, AmIDead = false;
    public int MaxHp, CurHp;
    

    public MonsterScript MyMonsterScript { get; set; }

    WaitForSeconds WaitTime = new WaitForSeconds(2f);
    float[] MovingTime;

    public Vector3 ParticlePos = Vector3.zero;

    bool ImDead = false; 

    // Start is called before the first frame update
    private void OnEnable()
    {




    }
    private void Awake()
    {
        //MyNavPos = MyMonsterScript.MyNavPos;
         if(Random.Range(0f,1f)>0.5f)Instantiate(SpawnParticle01, transform.position, Quaternion.identity);
        else  Instantiate(SpawnParticle02, transform.position, Quaternion.identity);
        ani = GetComponent<Animator>();
        ani.Play("MonsterIdle");
    }
    void Start()
    {
        
        MovingTime = transform.parent.parent.GetComponent<MonsterScript>().MovingTime;
    }

    // Update is called once per frame
    void Update()
    {
        MovingTime = transform.parent.parent.GetComponent<MonsterScript>().MovingTime;
        if (UpsideDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }


    public void Move(int Type)
    {
        
        if (Type ==0) StartCoroutine(Old1MovCoroutine());
        if (Type == 1) StartCoroutine(Old2MovCoroutine());
        if (Type == 2) StartCoroutine(Old4MovCoroutine());
        if (Type == 3) StartCoroutine(Old5MovCoroutine());
        if (Type == 4) 
        {
            
            StartCoroutine(OldBossMovCoroutine());
        }
        
    }

    IEnumerator Old1MovCoroutine()
    {
        LookatPos = true;
        MyMonsterScript.SetMyNewDestinationPos();
        Vector3 MyWay = (MyNavPos.transform.position - transform.parent.position).normalized;
        MyWay = new Vector3(MyWay.x, MyWay.y, 0);

        yield return new WaitForSeconds(0.1f);

        transform.parent.DOMove(transform.parent.position + (MyWay*MyMoveAmount), MovingTime[0]).SetEase(Ease.Linear);
        LookatPos = false;
        ani.SetBool("Move", true);

        yield return new WaitForSeconds(MovingTime[0]);
        ani.SetBool("Move", false);
        
        StartCoroutine(Old1WaitCoroutine());
    }
    IEnumerator Old1WaitCoroutine()
    {

        yield return WaitTime;
        StartCoroutine(Old1MovCoroutine());
    }
    IEnumerator Old2MovCoroutine()
    {
        LookatPos = true;
        MyMonsterScript.SetMyNewDestinationPos();
        Vector3 MyWay = (MyNavPos.transform.position - transform.parent.position).normalized;
        MyWay = new Vector3(MyWay.x, 0, 0);

        yield return new WaitForSeconds(0.1f);

        transform.parent.DOMove(transform.parent.position + (MyWay * MyMoveAmount), MovingTime[1]).SetEase(Ease.Linear);


        ani.SetTrigger("Jump");

        yield return new WaitForSeconds(MovingTime[1]);

        StartCoroutine(Old2WaitCoroutine(MyWay));
    }
    IEnumerator Old2WaitCoroutine(Vector3 DownPos)
    {
        //DownPos = new Vector3(DownPos.x, -DownPos.y, DownPos.z);
        //transform.parent.DOMove(transform.parent.position + (DownPos * MyMoveAmount), 2f);
        LookatPos = false;
        yield return WaitTime;
        StartCoroutine(Old2MovCoroutine());
    }
    IEnumerator Old4MovCoroutine()
    {
        LookatPos = true;
        MyMonsterScript.SetMyNewDestinationPos();
        Vector3 MyWay = (MyNavPos.transform.position - transform.parent.position).normalized;
        MyWay = new Vector3(MyWay.x, 0, 0);

        yield return new WaitForSeconds(0.1f);
        
        ani.SetTrigger("Jump");
        transform.parent.DOMove(transform.parent.position + (MyWay * MyMoveAmount), MovingTime[2]).SetEase(Ease.Linear);


        yield return new WaitForSeconds(MovingTime[2]);



        StartCoroutine(Old4WaitCoroutine());
    }
    IEnumerator Old4WaitCoroutine()
    {
        LookatPos = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Old4MovCoroutine());
    }
    IEnumerator Old5MovCoroutine()
    {
        LookatPos = true;
        MyMonsterScript.SetMyNewDestinationPos();
        Vector3 MyWay = (MyNavPos.transform.position - transform.parent.position).normalized;
        MyWay = new Vector3(MyWay.x, 0, 0);

        yield return new WaitForSeconds(0.1f);

        if (Random.value > 0.5)
        {
            ani.SetTrigger("Jump");
        }
        else
            ani.SetBool("Move", true);
        transform.parent.DOMove(transform.parent.position + (MyWay * MyMoveAmount), MovingTime[3]).SetEase(Ease.Linear);

        

        yield return new WaitForSeconds(MovingTime[3]);

        ani.SetBool("Move", false);


        StartCoroutine(Old5WaitCoroutine());
    }
    IEnumerator Old5WaitCoroutine()
    {
        LookatPos = false;
        yield return WaitTime;
        StartCoroutine(Old5MovCoroutine());
    }
    IEnumerator OldBossMovCoroutine()
    {
        LookatPos = true;
        MyMonsterScript.SetMyNewDestinationPos();
        Vector3 MyWay = (MyNavPos.transform.position - transform.parent.position).normalized;
        MyWay = new Vector3(MyWay.x, 0, 0);

        yield return new WaitForSeconds(0.1f);

        bool Jump = Random.value > 0.5;
        if (Jump)
        {
            ani.SetTrigger("Jump");
        }
        else
            ani.SetBool("Move", true);
        transform.parent.DOMove(transform.parent.position + (MyWay * MyMoveAmount), MovingTime[4]).SetEase(Ease.Linear);



        yield return new WaitForSeconds(MovingTime[4]);

        ani.SetBool("Move", false);
        if (Jump)
        {
            AudioManager.audioManager.audioSource.PlayOneShot(MyMonsterScript.MobJumpSound[Random.Range(0, MyMonsterScript.MobJumpSound.Length)]);
            //CameraShake.cameraShake.Shake(0.005f);
        }


        StartCoroutine(OldBossWaitCoroutine());
    }
    IEnumerator OldBossWaitCoroutine()
    {
        
        yield return WaitTime;
        StartCoroutine(OldBossMovCoroutine());
    }







    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && collision.gameObject.GetComponent<BallScript>().DamageAble)
        {
            ParticlePos =  collision.gameObject.GetComponent<BallScript>().MovPos;
            collision.gameObject.GetComponent<BallScript>().DamageAble = false;
            BallHit();
            
        }
    }
    
    public void BallHit()
    {

        CurHp--;
        AudioManager.audioManager.audioSource.PlayOneShot(MyMonsterScript.HitSound[(int)MyMonsterScript.MyType]);
        ani.SetTrigger("Hit");
        if(CurHp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if (ImDead == true) return;
        ImDead = true;
        //MyMonsterScript.
        MonsterSpawner.monsterSpawner.SpawnNewMonster((int)MyMonsterScript.MyType,Random.Range(1,4), true, 1,MyMonsterScript.OutsideDoor);
        if (ParticlePos != null && ParticlePos != Vector3.zero)
        {
            if (Random.Range(0f, 1f) > 0.5f) Instantiate(DeathParticle01, transform.position, Quaternion.identity);
            else Instantiate(DeathParticle02, transform.position, Quaternion.identity);
        }
        else
        {
            if (Random.Range(0f, 1f) > 0.5f) Instantiate(DeathParticle01, transform.position, Quaternion.identity);
            else Instantiate(DeathParticle02, transform.position, Quaternion.identity);
        }
        //Instantiate(DeathParticle, transform.position+Vector3.forward, Quaternion.identity);
        Destroy(gameObject.transform.parent.parent.gameObject, 0.2f);
    }

}
