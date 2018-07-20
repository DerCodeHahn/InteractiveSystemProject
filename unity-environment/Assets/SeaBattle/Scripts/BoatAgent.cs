using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class BoatAgent : Agent 
{
    private SeaManager seaManager;

    private float speed = 0.25f;

    [SerializeField]
    private GameObject canonBall;
    private float canonBallSpeed = 2.0f;

    private void Start()
    {
        seaManager = GetComponentInParent<SeaManager>();
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 direction = new Vector3(vectorAction[1], 0, vectorAction[0]);
        direction.Normalize();
        GetComponent<CharacterController>().Move(direction * speed);
        transform.forward = direction;

        Vector3 shotDirection = new Vector3(vectorAction[2], 0, vectorAction[2]);
        GameObject canonball = Instantiate(canonBall, gameObject.transform.position, canonBall.transform.rotation);
        canonBall.GetComponent<Rigidbody>().AddForce(shotDirection * canonBallSpeed);
    }

    public override void CollectObservations()
    {
        BoatAgent opponent = (this == seaManager.BoatAgents[0]) ? seaManager.BoatAgents[1] : seaManager.BoatAgents[0];
        Vector3 enemyDirection = opponent.transform.position - transform.position;
        AddVectorObs(enemyDirection);
    }

    void OnCollsionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("ball"))
            Debug.Log("Hit by canonball! gulp gulp gulp!");
    }


}
