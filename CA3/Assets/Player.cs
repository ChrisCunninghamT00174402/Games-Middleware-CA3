using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    public Animator anim;
    public GameObject Bullet;
    public GameObject Crate;
    public float force = 100f;
    public Vector3 playerPos;
    public GameObject Knight;
    public float rotateSpeed = 5;
    public int maxCrates = 6;
    public int crateCount;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        offset = Knight.transform.position - transform.position;
       // playerPos = Knight.transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        //camera control
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        Knight.transform.Rotate(0, horizontal, 0);
        transform.LookAt(Knight.transform);

        //player control
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetFloat("walkBlend", 0.0F);
            anim.SetBool("isWalking", true);

        }

        else
        {
            anim.SetBool("isWalking", false);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);

        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {

            CmdFire();
        }

        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("isBackwards", true);

        }
        else
        {
            anim.SetBool("isBackwards", false);
        }

        if (Input.GetKey(KeyCode.A))
        {

            anim.SetFloat("walkBlend",-1.0F);
            anim.SetBool("isWalking", true);

        }

        if (Input.GetKey(KeyCode.D))
        {

            anim.SetFloat("walkBlend", 1.0F);
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.Q)){

            anim.SetBool("isAttacking", true);

        }

        else
        {

            anim.SetBool("isAttacking", false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {

            CmdDropCrate();


        }
        if (crateCount < maxCrates)
        {
            CancelInvoke("CmdDropCrate");
        }


    }

    //instantiate bullet over the network
    [Command]
    private void CmdFire()
    {


        GameObject bullets = Instantiate(Bullet, transform.position, Quaternion.Euler(90, 50, 0));
        bullets.GetComponent<Rigidbody>().AddForce(transform.forward * (force * 15));
        NetworkServer.Spawn(bullets);
        Destroy(bullets, .7f);


    }

    [Command]
    private void CmdDropCrate()
    {
        GameObject crates = Instantiate(Crate, transform.position + (transform.forward*2) + (transform.up*0.2f), transform.rotation);
        NetworkServer.Spawn(crates);


    }



    public void FootR() { }

    public void FootL() { }

    public void Hit() { }
}
