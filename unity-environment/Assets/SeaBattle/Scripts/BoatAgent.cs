using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class BoatAgent : Agent 
{
    private SeaManager seaManager;

    private float speed = 250;

    [SerializeField]
    private GameObject canonBall;
    private float canonBallSpeed = 30000.0f;

    private float ShootingSpeed = 4f;
    private float shootTimer = 0;
    public int score;

    private void Start()
    {
        seaManager = GetComponentInParent<SeaManager>();
    }



    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 direction = new Vector3(vectorAction[1], 0, vectorAction[0]);
        //AddReward(1/direction.magnitude );
        direction.Normalize();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(direction * speed * Time.deltaTime, ForceMode.VelocityChange);
        //GetComponent<CharacterController>().Move(direction* speed);
        if(direction != Vector3.zero)
            transform.forward = direction;
        float shootDirection = vectorAction[2];

        if(shootDirection < 0 )
            shootDirection = -1;
        else if(shootDirection > 0)
            shootDirection = 1;

        if(shootDirection != 0 && shootTimer >= 1/ShootingSpeed)
        {
            GameObject ccc = Instantiate(canonBall, transform.position, Quaternion.identity);
			ccc.GetComponent<Rigidbody>().AddForce(transform.right * shootDirection * Time.deltaTime * canonBallSpeed);
            ccc.GetComponent<BallOwner>().BoatAgent = this;

            seaManager.CannonBalls.Add(ccc);
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
        GameObject enemy = seaManager.GetOther(this).gameObject;
        AddPositionAndVelocityObservervation(enemy);//  +5

        GameObject nextCannonball = seaManager.GetNexCannonball(this);
        AddPositionAndVelocityObservervation(nextCannonball);// +5
    }
    //Adds 4 observeration floats
    void AddPositionAndVelocityObservervation(GameObject target)
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        AddVectorObs(V3toV2(targetDirection.normalized)); // + 2
        float targetDirectionlenth = Mathf.Clamp01(targetDirection.magnitude / 5);
        AddVectorObs(targetDirectionlenth); // + 1
        
        AddVectorObs(V3toV2(target.GetComponent<Rigidbody>().velocity.normalized)); // +2
    }

    Vector2 V3toV2(Vector3 v3){
        return new Vector2(v3.x,v3.z);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "ball"){
            Destroy(col.gameObject);
            SetReward(0);
            seaManager.GetOther(this).SetReward(1f);
            seaManager.GetOther(this).score++;
            seaManager.Reset();
        }
        
    }


}
