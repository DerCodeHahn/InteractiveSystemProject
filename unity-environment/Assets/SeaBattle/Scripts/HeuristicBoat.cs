using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class HeuristicBoat : MonoBehaviour, Decision
{
	[SerializeField] private float shootRange;
	
	[SerializeField] private float EvadeAngle;
	Color modus;

	struct DirectionPair{
		public Vector2 direction;
		public int indexKey2;
		public int indexKey1;

        public DirectionPair(Vector2 direction, int indexKey1, int indexKey2)
        {
            this.direction = direction;
            this.indexKey1 = indexKey1;
            this.indexKey2 = indexKey2;
        }
    }
	DirectionPair[] directionPair;
	private void Start() {
		directionPair = new DirectionPair[8];							//x , y
		directionPair[0] = new DirectionPair(Vector2.up					, 1	, 0);
		directionPair[1] = new DirectionPair(Vector2.up + Vector2.right	, 1	, 1);
		directionPair[2] = new DirectionPair(Vector2.right				, 0	, 1);
		directionPair[3] = new DirectionPair(Vector2.right + Vector2.down, -1,1);
		directionPair[4] = new DirectionPair(Vector2.down				, -1, 0);
		directionPair[5] = new DirectionPair(Vector2.down + Vector2.left, -1, -1);
		directionPair[6] = new DirectionPair(Vector2.left				, 0, -1);
		directionPair[7] = new DirectionPair(Vector2.left + Vector2.up	, 1, -1);
	}
	Vector2 targetDirection;
	Vector2 cannonballDirection;
    public float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        targetDirection = new Vector2(vectorObs[0], vectorObs[1]); // targetDirection
		float[] action = new float[3];
		cannonballDirection = new Vector2(vectorObs[4], vectorObs[5]);
		Vector2 cannonBallVelocity = new Vector2(vectorObs[6], vectorObs[7]);
		
		if(cannonballDirection.magnitude >= 0.1f && Vector2.Angle(-cannonballDirection, cannonBallVelocity) <= EvadeAngle)
		{
			EvadeCannonball(ref action);
			modus = Color.red;
		}
		else if(targetDirection.magnitude >= shootRange)
		{
			MoveToTarget(ref action, targetDirection);
			modus = Color.green;
		}
		else{
			ShootAtTarget(ref action);
			modus = Color.yellow;
		}

		return action;
    }

    private void EvadeCannonball(ref float[] action)
    {
        Vector3 targetDirectionV3 = new Vector3(cannonballDirection.x, 0, cannonballDirection.y); 
		Vector3 cannonBallTangent = Vector3.Cross(targetDirectionV3, Vector3.up);
		Vector2 v2Tangent = new Vector2(cannonBallTangent.x,cannonBallTangent.z);

		if(Vector3.Angle(targetDirectionV3, cannonBallTangent) < Vector3.Angle(targetDirectionV3, -cannonBallTangent))
			v2Tangent *= -1;

		MoveToTarget(ref action,  v2Tangent);
    }

    Vector3 tangent;
    private void ShootAtTarget(ref float[] action)
    {
        Vector3 targetDirectionV3 = new Vector3(targetDirection.x, 0, targetDirection.y); 
		tangent = Vector3.Cross(targetDirectionV3, Vector3.up);
		action[2] = 1;
		if(Vector3.Angle(targetDirectionV3, tangent) > Vector3.Angle(targetDirectionV3, tangent))
		{
			tangent *= -1;
			action[2] *=-1;
		}
		Vector2 v2Tangent = new Vector2(tangent.x,tangent.z);
		MoveToTarget(ref action,  v2Tangent);


    }

    void MoveToTarget(ref float[] action, Vector2 direction){
		DirectionPair pair = FindBestDirection(direction);
			
		action[0] = pair.indexKey1;
		action[1] = pair.indexKey2;
	}

	DirectionPair FindBestDirection(Vector2 direction){
		float bestAngle = Mathf.Infinity;
		DirectionPair bestPair = new DirectionPair(Vector2.zero,-1, -1);
		foreach (DirectionPair pair in directionPair)
		{
			float angle = Vector2.Angle(direction, pair.direction);
			if(angle < bestAngle)
			{
				bestAngle = angle;
				bestPair = pair;
			}
		}
		return bestPair;
	}
	//List>float> emptyList = new List<float>();
    public List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        return null ;
    }
	
	private void OnDrawGizmos() {
		Gizmos.color = modus;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Vector3 pos = new Vector3(targetDirection.x, 0, targetDirection.y);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position,transform.position+(pos));
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position,(transform.position+(tangent)).normalized);

	}
}
