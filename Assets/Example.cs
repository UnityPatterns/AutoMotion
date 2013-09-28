using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
	public Vector3[] path;
	public float turnTime;
	public float pauseTime;
	public float moveTime;
	public EaseType easeType;

	void Start()
	{
		StartCoroutine(MoveOnPath());
	}

	IEnumerator MoveOnPath()
	{
		transform.localPosition = path[0];
		transform.LookAt(path[1]);
		int pathIndex = 1;

		while (true)
		{
			//Move to the next point
			yield return StartCoroutine(transform.MoveTo(pathIndex[pathIndex], moveTime, easeType));

			//Pause for a moment
			yield return StartCoroutine(Auto.Wait(pauseTime));

			//Find the next point
			pathIndex++;
			if (pathIndex == path.Length)
				pathIndex = 0;

			//Turn towards the next point
			var targetRotation = Quaternion.LookRotation(path[pathIndex] - transform.position);
			yield return StartCoroutine(transform.RotateTo(targetRotation, turnTime));
		}
	}
}
