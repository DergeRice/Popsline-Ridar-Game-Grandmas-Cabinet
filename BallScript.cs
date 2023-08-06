using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallScript : MonoBehaviour
{
    public GameObject Parti, DeathParticle;

    public Vector3 MovPos, PartiPos;
    [SerializeField] float speed;
    [SerializeField] ForceMode dd;

    public bool DamageAble = true;

    public bool Rigid = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Rigid) GetComponent<Rigidbody>().AddForce((MovPos - transform.position) * speed, dd);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster" )
        {
            collision.gameObject.GetComponent<MonsterMove>().ParticlePos = MovPos;
        }
        //DamageAble = false;
        ActionAfterCollision(gameObject.GetComponent<MonsterMove>());

        SoundWhenHit temp;
        Animator ani;
        if(collision.gameObject.TryGetComponent<SoundWhenHit>(out temp))
        {
            AudioManager.audioManager.audioSource.PlayOneShot(temp.ThisObj[Random.Range(0,temp.ThisObj.Count)]);
            
        }
        if(collision.gameObject.TryGetComponent<Animator>(out ani))
        {
            ani.SetTrigger("Hit");
        }
    }

    public void ActionAfterCollision(MonsterMove target)
    {

        //if (Rigid) Instantiate(Parti, transform.position+Vector3.forward, Quaternion.identity);
        Destroy(gameObject);

        
    }
}
