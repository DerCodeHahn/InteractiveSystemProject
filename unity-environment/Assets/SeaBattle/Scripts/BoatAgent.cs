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
    private float canonBallSpeed = 1500.0f;

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
        GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.VelocityChange);
        //GetComponent<CharacterController>().Move(direction* speed);
        if(direction != Vector3.zero)
            transform.forward = direction;
        float shootDirection = vectorAction[2];
        if(shootDirection != 0 && shootTimer >= 1/ShootingSpeed)
        {
            GameObject ccc = Instantiate(canonBall, transform.position, Quaternion.identity);
			ccc.GetComponent<Rigidbody>().AddForce(transform.right * shootDirection * canonBallSpeed);
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
        AddPositionAndVelocityObservervation(enemy);//  +4

        GameObject nextCannonball = seaManager.GetNexCannonball(this);
        AddPositionAndVelocityObservervation(nextCannonball);// +4
    }
    //Adds 4 observeration floats
    void AddPositionAndVelocityObservervation(GameObject traget)
    {
        Vector3 targetDirection = traget.transform.position - transform.position;
        AddVectorObs(V3toV2(targetDirection));
        AddVectorObs(V3toV2(traget.GetComponent<Rigidbody>().velocity));
    }

    Vector2 V3toV2(Vector3 v3){
        return new Vector2(v3.x,v3.z);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "ball"){
            Destroy(col.gameObject);
            SetReward(0);
            seaManager.GetOther(this).AddReward(0.1f);
            seaManager.GetOther(this).score++;
            seaManager.Reset();
        }
        
    }


}
