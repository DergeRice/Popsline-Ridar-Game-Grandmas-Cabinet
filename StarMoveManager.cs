using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMoveManager : MonoBehaviour
{

    [SerializeField] GameObject star,starParent;
    [SerializeField] int Amount = 20;
    float randomX, randomY,SpawnTime;

    bool SpawnBool;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomPos();
        //StartCoroutine(MakeStar());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpawnTime += Time.deltaTime;

        if(SpawnTime > 1f)
        {
            SpawnTime = 0;
            for (int i = 0; i < Amount; i++)
            {
                SetRandomPos();
                GameObject temp = Instantiate(star, new Vector3(randomX,randomY, -110), Quaternion.Euler(60, -90, 0));
                temp.transform.parent = starParent.transform;
                //temp.transform.localScale = Vector3.one;
            }
        }
    }

    //IEnumerator MakeStar()
    //{
        

    //    yield return new WaitForSeconds(1f);

    //    StartCoroutine(MakeStar());
    //}

    void SetRandomPos()
    {
         randomX = Random.Range(-200f, 200f);
         randomY = Random.Range(-10f, 25f);
    }
}
