using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardGiver : MonoBehaviour {
	public float reward;
	void OnTriggerEnter(Collider col)
	{
		BoatAgent ba = col.GetComponent<BoatAgent>();
		if(ba != null)
		{
			if(ba != GetComponentInParent<BoatAgent>())
			{
				ba.AddReward(reward);
			}
		}
	}
}
