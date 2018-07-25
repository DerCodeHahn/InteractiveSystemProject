using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManager : MonoBehaviour {

	private BoatAgent[] boatAgents = new BoatAgent[2];
    public float spawnRadius = 0.1f;

    public List<GameObject> CannonBalls;

    int generation = 0;

    public BoatAgent[] BoatAgents
    {
        get
        {
            return boatAgents;
        }

        set
        {
            boatAgents = value;
        }
    }


    // Use this for initialization
    void Start () {
		boatAgents = GetComponentsInChildren<BoatAgent>();
	}

    public void Reset(){

        generation++;
        foreach (BoatAgent boatAgent in boatAgents)
        {
            boatAgent.Done();
            boatAgent.transform.localPosition = Vector3.zero;
            Vector2 rnd = Random.insideUnitCircle * spawnRadius;
            boatAgent.transform.localPosition += new Vector3(rnd.x,0,rnd.y);
        }
        foreach (GameObject canonball in CannonBalls)
            Destroy(canonball);
        CannonBalls.Clear();

        if(spawnRadius <= 0.5f)
            spawnRadius += 0.000001f;

    }
    void OnGUI() {
        GUILayout.Label("Generation " + generation);
        GUILayout.Label("Agent1 Score: " + boatAgents[0].score);
        GUILayout.Label("Agent2 Score: " + boatAgents[1].score);
    }

    public BoatAgent GetOther(BoatAgent agent){
        if(boatAgents[0] == agent)
            return boatAgents[1];
        else
            return boatAgents[0];
    }

    public GameObject GetNexCannonball(BoatAgent boatAgent)
    {
        float minDistance = Mathf.Infinity;
        GameObject closest = null;
        foreach(GameObject cannonball in CannonBalls)
        {
            if(cannonball == null)
                continue;

            float dist = Vector3.Distance(cannonball.transform.position, boatAgent.transform.position);
            if(dist < minDistance)
            {
                dist = minDistance;
                closest = cannonball;
            }
        }
        if(closest == null)
            return boatAgent.gameObject;
        return closest;
    }

    void FixedUpdate()
    {
        CannonBalls.RemoveAll(item => item == null);
    }

}
