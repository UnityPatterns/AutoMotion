using UnityEngine;
using System.Collections;

public delegate bool Predicate();
public delegate float Easer(float t);

public static class Auto
{
	#region Transform coroutines

	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localPosition;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localPosition = start + range * ease(elapsed / duration);
			yield return 0;
		}
		transform.localPosition = target;
	}
	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration)
	{
		return MoveTo(transform, target, duration, Ease.Linear);
	}
	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration, EaseType ease)
	{
		return MoveTo(transform, target, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		var start = transform.localPosition;
		transform.localPosition = target;
		return MoveTo(transform, start, duration, ease);
	}
	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration)
	{
		return MoveFrom(transform, target, duration, Ease.Linear);
	}
	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration, EaseType ease)
	{
		return MoveFrom(transform, target, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localScale;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localScale = start + range * ease(elapsed / duration);
			yield return 0;
		}
		transform.localScale = target;
	}
	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration)
	{
		return ScaleTo(transform, target, duration, Ease.Linear);
	}
	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration, EaseType ease)
	{
		return ScaleTo(transform, target, duration, Ease.FromType(ease));
	}

	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration, Easer ease)
	{
		var start = transform.localScale;
		transform.localScale = target;
		return ScaleTo(transform, start, duration, ease);
	}
	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration)
	{
		return ScaleFrom(transform, target, duration, Ease.Linear);
	}
	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration, EaseType ease)
	{
		return ScaleFrom(transform, target, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localRotation;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localRotation = Quaternion.Lerp(start, target, ease(elapsed / duration));
			yield return 0;
		}
		transform.localRotation = target;
	}
	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration)
	{
		return RotateTo(transform, target, duration, Ease.Linear);
	}
	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration, EaseType ease)
	{
		return RotateTo(transform, target, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration, Easer ease)
	{
		var start = transform.localRotation;
		transform.localRotation = target;
		return RotateTo(transform, start, duration, ease);
	}
	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration)
	{
		return RotateFrom(transform, target, duration, Ease.Linear);
	}
	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration, EaseType ease)
	{
		return RotateFrom(transform, target, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration, Easer ease)
	{
		float elapsed = 0;
		var start = transform.localPosition;
		Vector3 position;
		float t;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			t = ease(elapsed / duration);
			position.x = start.x * (1 - t) * (1 - t) + control.x * 2 * (1 - t) * t + target.x * t * t;
			position.y = start.y * (1 - t) * (1 - t) + control.y * 2 * (1 - t) * t + target.y * t * t;
			position.z = start.z * (1 - t) * (1 - t) + control.z * 2 * (1 - t) * t + target.z * t * t;
			transform.localPosition = position;
			yield return 0;
		}
		transform.localPosition = target;
	}
	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration)
	{
		return CurveTo(transform, control, target, duration, Ease.Linear);
	}
	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration, EaseType ease)
	{
		return CurveTo(transform, control, target, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator CurveFrom(this Transform transform, Vector3 control, Vector3 start, float duration, Easer ease)
	{
		var target = transform.localPosition;
		transform.localPosition = start;
		return CurveTo(transform, control, target, duration, ease);
	}
	public static IEnumerator CurveFrom(this Transform transform, Vector3 control, Vector3 start, float duration)
	{
		return CurveFrom(transform, control, start, duration, Ease.Linear);
	}
	public static IEnumerator CurveFrom(this Transform transform, Vector3 control, Vector3 start, float duration, EaseType ease)
	{
		return CurveFrom(transform, control, start, duration, Ease.FromType(ease));
	}
	
	public static IEnumerator Shake(this Transform transform, Vector3 amount, float duration)
	{
		var start = transform.localPosition;
		var shake = Vector3.zero;
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			shake.Set(Random.Range(-amount.x, amount.x), Random.Range(-amount.y, amount.y), Random.Range(-amount.z, amount.z));
			transform.localPosition = start + shake;
			yield return 0;
		}
		transform.localPosition = start;
	}
	public static IEnumerator Shake(this Transform transform, float amount, float duration)
	{
		return Shake(transform, new Vector3(amount, amount, amount), duration);
	}

	#endregion

	#region Waiting coroutines

	public static IEnumerator Wait(float duration)
	{
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			yield return 0;
		}
	}

	public static IEnumerator WaitUntil(Predicate predicate)
	{
		while (!predicate())
			yield return 0;
	}

	#endregion

	#region Time-based motion
	
	public static float Loop(float duration, float from, float to, float offsetPercent)
	{
		var range = to - from;
		var total = (Time.time + duration * offsetPercent) * (Mathf.Abs(range) / duration);
		if (range > 0)
			return from + Time.time - (range * Mathf.FloorToInt((Time.time / range)));
		else
			return from - (Time.time - (Mathf.Abs(range) * Mathf.FloorToInt((total / Mathf.Abs(range)))));
	}
	public static float Loop(float duration, float from, float to)
	{
		return Loop(duration, from, to, 0);	
	}
	public static Vector3 Loop(float duration, Vector3 from, Vector3 to, float offsetPercent)
	{
		return Vector3.Lerp(from, to, Loop(duration, 0, 1, offsetPercent));
	}
	public static Vector3 Loop(float duration, Vector3 from, Vector3 to)
	{
		return Vector3.Lerp(from, to, Loop(duration, 0, 1));
	}
	public static Quaternion Loop(float duration, Quaternion from, Quaternion to, float offsetPercent)
	{
		return Quaternion.Lerp(from, to, Loop(duration, 0, 1, offsetPercent));
	}
	public static Quaternion Loop(float duration, Quaternion from, Quaternion to)
	{
		return Quaternion.Lerp(from, to, Loop(duration, 0, 1));
	}
	
	public static float Wave(float duration, float from, float to, float offsetPercent)
	{
		var range = (to - from) / 2;
		return from + range + Mathf.Sin(((Time.time + duration * offsetPercent) / duration) * (Mathf.PI * 2)) * range;
	}
	public static float Wave(float duration, float from, float to)
	{
		return Wave(duration, from, to, 0);
	}
	public static Vector3 Wave(float duration, Vector3 from, Vector3 to, float offsetPercent)
	{
		return Vector3.Lerp(from, to, Wave(duration, 0, 1, offsetPercent));
	}
	public static Vector3 Wave(float duration, Vector3 from, Vector3 to)
	{
		return Vector3.Lerp(from, to, Wave(duration, 0, 1));
	}
	public static Quaternion Wave(float duration, Quaternion from, Quaternion to, float offsetPercent)
	{
		return Quaternion.Lerp(from, to, Wave(duration, 0, 1, offsetPercent));
	}
	public static Quaternion Wave(float duration, Quaternion from, Quaternion to)
	{
		return Quaternion.Lerp(from, to, Wave(duration, 0, 1));
	}

	#endregion
}

#region Easing functions

public enum EaseType { Linear, QuadIn, QuadOut, QuadInOut, CubeIn, CubeOut, CubeInOut, BackIn, BackOut, BackInOut, ExpoIn, ExpoOut, ExpoInOut, SineIn, SineOut, SineInOut, ElasticIn, ElasticOut, ElasticInOut }

public static class Ease
{
	public static readonly Easer Linear = (t) => { return t; };
	public static readonly Easer QuadIn = (t) => { return t * t; };
	public static readonly Easer QuadOut = (t) => { return 1 - QuadIn(1 - t); };
	public static readonly Easer QuadInOut = (t) => { return (t <= 0.5f) ? QuadIn(t * 2) / 2 : QuadOut(t * 2 - 1) / 2 + 0.5f; };
	public static readonly Easer CubeIn = (t) => { return t * t * t; };
	public static readonly Easer CubeOut = (t) => { return 1 - CubeIn(1 - t); };
	public static readonly Easer CubeInOut = (t) => { return (t <= 0.5f) ? CubeIn(t * 2) / 2 : CubeOut(t * 2 - 1) / 2 + 0.5f; };
	public static readonly Easer BackIn = (t) => { return t * t * (2.70158f * t - 1.70158f); };
	public static readonly Easer BackOut = (t) => { return 1 - BackIn(1 - t); };
	public static readonly Easer BackInOut = (t) => { return (t <= 0.5f) ? BackIn(t * 2) / 2 : BackOut(t * 2 - 1) / 2 + 0.5f; };
	public static readonly Easer ExpoIn = (t) => { return (float)Mathf.Pow(2, 10 * (t - 1)); };
	public static readonly Easer ExpoOut = (t) => { return 1 - ExpoIn(t); };
	public static readonly Easer ExpoInOut = (t) => { return t < .5f ? ExpoIn(t * 2) / 2 : ExpoOut(t * 2) / 2; };
	public static readonly Easer SineIn = (t) => { return -Mathf.Cos(Mathf.PI / 2 * t) + 1; };
	public static readonly Easer SineOut = (t) => { return Mathf.Sin(Mathf.PI / 2 * t); };
	public static readonly Easer SineInOut = (t) => { return -Mathf.Cos(Mathf.PI * t) / 2f + .5f; };
	public static readonly Easer ElasticIn = (t) => { return 1 - ElasticOut(1 - t); };
	public static readonly Easer ElasticOut = (t) => { return Mathf.Pow(2, -10 * t) * Mathf.Sin((t - 0.075f) * (2 * Mathf.PI) / 0.3f) + 1; };
	public static readonly Easer ElasticInOut = (t) => { return (t <= 0.5f) ? ElasticIn(t * 2) / 2 : ElasticOut(t * 2 - 1) / 2 + 0.5f; };

	public static Easer FromType(EaseType type)
	{
		switch (type)
		{
		case EaseType.Linear: return Linear;
		case EaseType.QuadIn: return QuadIn;
		case EaseType.QuadOut: return QuadOut;
		case EaseType.QuadInOut: return QuadInOut;
		case EaseType.CubeIn: return CubeIn;
		case EaseType.CubeOut: return CubeOut;
		case EaseType.CubeInOut: return CubeInOut;
		case EaseType.BackIn: return BackIn;
		case EaseType.BackOut: return BackOut;
		case EaseType.BackInOut: return BackInOut;
		case EaseType.ExpoIn: return ExpoIn;
		case EaseType.ExpoOut: return ExpoOut;
		case EaseType.ExpoInOut: return ExpoInOut;
		case EaseType.SineIn: return SineIn;
		case EaseType.SineOut: return SineOut;
		case EaseType.SineInOut: return SineInOut;
		case EaseType.ElasticIn: return ElasticIn;
		case EaseType.ElasticOut: return ElasticOut;
		case EaseType.ElasticInOut: return ElasticInOut;
		}
		return Linear;
	}
}

#endregion