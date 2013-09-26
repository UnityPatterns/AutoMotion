using UnityEngine;
using System.Collections;

public class PathMover : MonoBehaviour
{
	public EaseType moveEase;
	public EaseType stopEase;
	public float moveDuration;
	public float stopTweenDuration;
	public float stopDuration;
	public Vector3 stopScale = Vector3.one;
	public Vector3[] points;
	public int startIndex;

	void Start()
	{
		StartCoroutine(MoveOnPath());
	}

	IEnumerator MoveOnPath()
	{
		int index = startIndex;
		transform.localPosition = points[index];
		while (true)
		{
			index = (index + 1) % points.Length;
			transform.LookAt(points[index]);
			yield return StartCoroutine(transform.MoveTo(points[index], moveDuration, moveEase));
			if (stopTweenDuration > 0)
				StartCoroutine(transform.ScaleFrom(stopScale, stopTweenDuration, stopEase));
			if (stopDuration > 0)
				yield return StartCoroutine(Auto.Wait(stopDuration));
		}
	}
}
