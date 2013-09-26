using UnityEngine;
using System.Collections;

public class AutoMover : MonoBehaviour
{
	public enum MoveType { Loop, Wave }

	public MoveType moveType;
	public float duration;
	[Range(0, 1)] public float offset;
	public Vector3 fromPosition;
	public Vector3 toPosition;
	public Vector3 fromRotation;
	public Vector3 toRotation;
	public Vector3 fromScale = Vector3.one;
	public Vector3 toScale = Vector3.one;

	void Start()
	{
		if (fromPosition != toPosition)
			StartCoroutine(Position());
		if (fromRotation != toRotation)
			StartCoroutine(Rotation());
		if (fromScale != toScale)
			StartCoroutine(Scale());
	}

	IEnumerator Position()
	{
		var from = transform.localPosition + fromPosition;
		var to = transform.localPosition + toPosition;
		if (moveType == MoveType.Loop)
		{
			while (true)
			{
				transform.localPosition = Auto.Loop(duration, from, to, offset);
				yield return 0;
			}
		}
		else
		{
			while (true)
			{
				transform.localPosition = Auto.Wave(duration, from, to, offset);
				yield return 0;
			}
		}
	}

	IEnumerator Rotation()
	{
		var from = transform.localRotation * Quaternion.Euler(fromRotation);
		var to = transform.localRotation * Quaternion.Euler(toRotation);
		if (moveType == MoveType.Loop)
		{
			while (true)
			{
				transform.localRotation = Auto.Loop(duration, from, to, offset);
				yield return 0;
			}
		}
		else
		{
			while (true)
			{
				transform.localRotation = Auto.Wave(duration, from, to, offset);
				yield return 0;
			}
		}
	}

	IEnumerator Scale()
	{
		var from = fromScale;
		var to = toScale;
		if (moveType == MoveType.Loop)
		{
			while (true)
			{
				transform.localScale = Auto.Loop(duration, from, to, offset);
				yield return 0;
			}
		}
		else
		{
			while (true)
			{
				transform.localScale = Auto.Wave(duration, from, to, offset);
				yield return 0;
			}
		}
	}
}
