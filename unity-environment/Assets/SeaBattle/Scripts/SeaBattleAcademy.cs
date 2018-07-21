using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class SeaBattleAcademy : Academy {
	[SerializeField]SeaManager seaManager;
	public override void AcademyReset ()
	{
		seaManager.Reset();
	}

	public override void AcademyStep()
	{
		
	}

}
