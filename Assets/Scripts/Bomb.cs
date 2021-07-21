using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject BlowUpEffect;
    float damage = 1f;
    float timer = 3f;

    RaycastHit2D hit2D_X;
    RaycastHit2D hit2D_NX;
    RaycastHit2D hit2D_Y;
    RaycastHit2D hit2D_NY;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1f;
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            hit2D_X = Physics2D.Linecast(gameObject.transform.position, gameObject.transform.position + new Vector3(1, 0, 0), 1 << LayerMask.NameToLayer("Default"));
            hit2D_NX = Physics2D.Linecast(gameObject.transform.position, gameObject.transform.position + new Vector3(-1, 0, 0), 1 << LayerMask.NameToLayer("Default"));
            hit2D_Y = Physics2D.Linecast(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 1, 0), 1 << LayerMask.NameToLayer("Default"));
            hit2D_NY = Physics2D.Linecast(gameObject.transform.position, gameObject.transform.position + new Vector3(0, -1, 0), 1 << LayerMask.NameToLayer("Default"));

            //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(1, 0, 0));
            //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(-1, 0, 0));
            //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 1, 0));
            //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, -1, 0));


            if (hit2D_X.collider)
            { 
                if (hit2D_X.collider.gameObject.GetComponent<ActorHP>())
                {
                    hit2D_X.collider.gameObject.GetComponent<ActorHP>().SetHP(-damage);
                }

                if (hit2D_X.collider.gameObject.GetComponent<Dog>())
                {
                    hit2D_X.collider.gameObject.GetComponent<Dog>().GetDirty();
                }

                if (hit2D_X.collider.gameObject.GetComponent<Farmer>())
                {
                    hit2D_X.collider.gameObject.GetComponent<Farmer>().GetDirty();
                }
            }
            if (hit2D_NX.collider)
            { 
                if (hit2D_NX.collider.gameObject.GetComponent<ActorHP>())
                {
                    hit2D_NX.collider.gameObject.GetComponent<ActorHP>().SetHP(-damage);
                }

                if (hit2D_NX.collider.gameObject.GetComponent<Dog>())
                {
                    hit2D_NX.collider.gameObject.GetComponent<Dog>().GetDirty();
                }

                if (hit2D_NX.collider.gameObject.GetComponent<Farmer>())
                {
                    hit2D_NX.collider.gameObject.GetComponent<Farmer>().GetDirty();
                }
            }
            if (hit2D_Y.collider)
            { 
                if (hit2D_Y.collider.gameObject.GetComponent<ActorHP>())
                {
                    hit2D_Y.collider.gameObject.GetComponent<ActorHP>().SetHP(-damage);
                }

                if (hit2D_Y.collider.gameObject.GetComponent<Dog>())
                {
                    hit2D_Y.collider.gameObject.GetComponent<Dog>().GetDirty();
                }

                if (hit2D_Y.collider.gameObject.GetComponent<Farmer>())
                {
                    hit2D_Y.collider.gameObject.GetComponent<Farmer>().GetDirty();
                }
            }
            if (hit2D_NY.collider)
            { 
                if (hit2D_NY.collider.gameObject.GetComponent<ActorHP>())
                {
                    hit2D_NY.collider.gameObject.GetComponent<ActorHP>().SetHP(-damage);
                }

                if (hit2D_NY.collider.gameObject.GetComponent<Dog>())
                {
                    hit2D_NY.collider.gameObject.GetComponent<Dog>().GetDirty();
                }

                if (hit2D_NY.collider.gameObject.GetComponent<Farmer>())
                {
                    hit2D_NY.collider.gameObject.GetComponent<Farmer>().GetDirty();
                }
            }
            GameObject newBlowUpEffect = Instantiate(BlowUpEffect, gameObject.transform.position, BlowUpEffect.transform.rotation);
            newBlowUpEffect.SetActive(true);
            Destroy(newBlowUpEffect, 2);
            Destroy(gameObject);
        }
    }
}
