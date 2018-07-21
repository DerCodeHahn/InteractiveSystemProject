using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class BoatAgent : Agent 
{
    private SeaManager seaManager;

    private float speed = 10;

    [SerializeField]
    private GameObject canonBall;
    private float canonBallSpeed = 1000.0f;

    private float ShootingSpeed = 4f;
    private float shootTimer = 0;
    private void Start()
    {
        seaManager = GetComponentInParent<SeaManager>();
    }



    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 direction = new Vector3(vectorAction[1], 0, vectorAction[0]);
        direction.Normalize();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.VelocityChange);
        //GetComponent<CharacterController>().Move(direction* speed);
        if(direction != Vector3.zero)
            transform.forward = direction;
        float shootDirection = vectorAction[2];
        if(shootDirection != 0 && shootTimer >= 1/ShootingSpeed)
        {
            GameObject ccc = Instantiate(canonBall, transform.position, Quaternion.identity);
			ccc.GetComponent<Rigidbody>().AddForce(transform.right * shootDirection * canonBallSpeed);
            Physics.IgnoreCollision(GetComponent<Collider>(), ccc.GetComponent<Collider>());
            Destroy(ccc,0.5f);
            shootTimer = 0;
        }
    }
    
    void FixedUpdate(){
        shootTimer += Time.deltaTime;
    }

    public override void CollectObservations()
    {
        Vector2 direction = seaManager.GetOther(this).transform.position - transform.position;
        AddVectorObs(direction);
    }

    void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject.tag == "ball"){
            Destroy(col.gameObject);
            ResetReward();
            seaManager.GetOther(this).AddReward(1);
            seaManager.Reset();
        }
    }


}
