using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManager : MonoBehaviour {

	private BoatAgent[] boatAgents = new BoatAgent[2];

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
}
