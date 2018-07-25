using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardGiver : MonoBehaviour {
	public float reward;
	void OnTriggerEnter(Collider col)
	{
		BallOwner ba = col.GetComponent<BallOwner>();
		if(ba != null)
		{
			if(ba.BoatAgent != GetComponentInParent<BoatAgent>())
			{
				ba.BoatAgent.AddReward(reward);
			}
		}
	}
}
