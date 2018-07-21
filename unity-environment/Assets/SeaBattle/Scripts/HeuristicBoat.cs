using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class HeuristicBoat : MonoBehaviour, Decision
{
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
    public float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        targetDirection = new Vector2(vectorObs[0], vectorObs[1]); // targetDirection

		float[] action = new float[3];
		DirectionPair pair = FindBestDirection(targetDirection);
		
		action[0] = pair.indexKey1;
		action[1] = pair.indexKey2;

		return action;
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

    public List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        return new List<float>();
    }

	private void OnDrawGizmos() {
		Vector3 pos = new Vector3(targetDirection.x, 0, targetDirection.y);
		Gizmos.DrawLine(transform.position,transform.position+(pos));
	}
}
