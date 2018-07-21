using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManager : MonoBehaviour {

	private BoatAgent[] boatAgents = new BoatAgent[2];
    public float spawnRadius = 0.5f;

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
        foreach (BoatAgent boatAgent in boatAgents)
        {
            boatAgent.transform.position = Vector3.zero;
            Vector2 rnd = Random.insideUnitCircle * spawnRadius;
            boatAgent.transform.position += new Vector3(rnd.x,0,rnd.y);
        }
    }

    public BoatAgent GetOther(BoatAgent agent){
        if(boatAgents[0] == agent)
            return boatAgents[1];
        else
            return boatAgents[0];
    }
}
