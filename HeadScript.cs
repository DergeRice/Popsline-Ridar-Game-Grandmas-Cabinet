using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    public float speedLimit;
    Rigidbody rb;
    private Vector3 mouseStartPosition;
    [SerializeField]
    GameObject BoomParti;

    int ExplosionCnt;
    Vector3 reflectDir;

    bool Spining;

    [SerializeField]
    GameObject[] ThiefHeads = new GameObject[6];

    public GameObject Rapid, Fire;

    // Start is called before the first frame update
    void Start()
    {
        int activeHeadNum = Random.Range(0,6);
        ThiefHeads[activeHeadNum].SetActive(true);
        rb = GetComponent<Rigidbody>();
        transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360),0));
        //reflectDir = new Vector3(Random.Range(200,350),0,0);
        Spining = true;
    }

    private static readonly WaitForSeconds Delay = new WaitForSeconds(0.1f);

    // Update is called once per frame
    void Update()
    {

        if(Spining)
        {
            //Vector3 temp = new Vector3(90,0,reflectDir.normalized.z*100);
            transform.GetChild(0).Rotate(0,1,0);
        }
       
    }
    
    

    private void OnMouseDown()
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        Vector3 mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dragVector = (mouseEndPosition - mouseStartPosition).normalized;
        rb.AddForce(dragVector*80,ForceMode.Impulse);
    }

    // private void OnMouseDown() 
    // {
    //     // rb.AddForce(new Vector3(Random.Range(-3,3)*50,0,Random.Range(-3,3)*50),ForceMode.Impulse);
    //     //rigid.AddExplosionForce(20,transform.position,0.1f);
        
    // }

    void FixedUpdate()
    {

        float currentSpeed = rb.velocity.magnitude;
        //Vector3 velocity = rb.velocity.normalized * speed; 
        // 현재 속도가 초기 힘의 크기와 다르면, 힘의 크기를 초기 힘의 크기로 유지

        
        if(currentSpeed < speedLimit)
        {
             // 힘의 방향은 현재 공의 이동 방향으로 유지
            Vector3 forceDirection = rb.velocity;
            rb.AddForce(forceDirection,ForceMode.VelocityChange);
        }
           
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(Random.insideUnitSphere.normalized*100,ForceMode.Impulse);
        }

        if(ExplosionCnt >= 10)
        {
            Invoke(nameof(Boom),3f);
            ExplosionCnt = 0; //reset 
        }

    }

    void Boom()
    {
        Instantiate(BoomParti,transform.position,Quaternion.identity);
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.tag == "Wall")
        {
            Vector3 contactPoint = other.contacts[0].point;
            // reflectDir = Vector3.Reflect(rb.velocity, other.contacts[0].normal);
            // reflectDir = new Vector3(reflectDir.x,reflectDir.y,70);
            //rb.AddForce(reflectDir,ForceMode.VelocityChange);
            
            Spining = true;
            HitWall();
            
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Boom")
        {
            StartCoroutine(DisableColliderDelayed(other));
        }
    }
    private IEnumerator DisableColliderDelayed(Collider collider)
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
        Destroy(gameObject);
    }

    public void HitWall()
    {
        ExplosionCnt++;
        //ransform.localScale = transform.localScale * 1.1f;
    }

    
}
