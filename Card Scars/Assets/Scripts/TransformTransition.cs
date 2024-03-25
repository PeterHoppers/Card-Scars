using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTransition : MonoBehaviour
{
    public AnimationCurve positionCurve;
    public AnimationCurve rotationCurve;
    public AnimationCurve scaleCurve;

    public void MoveTo(Vector3 target, float duration, bool isLocalPosition = true)
    { 
        Vector3 origin = (isLocalPosition) ? transform.localPosition : transform.position;
        StartCoroutine(AnimateMovement(origin, target, duration, isLocalPosition));
    }

    IEnumerator AnimateMovement(Vector3 origin, Vector3 target, float duration, bool isLocalPosition)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey += Time.deltaTime;
            float curvePercent = GetCurvePercent(journey, duration, positionCurve);

            if (isLocalPosition)
            {
                transform.localPosition = Vector3.LerpUnclamped(origin, target, curvePercent);
            }
            else
            {
                transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);
            }            

            yield return null;
        }
    }

    public void ScaleTo(Vector3 targetSize, float duration) 
    {
        StartCoroutine(AnimateScale(transform.localScale, targetSize, duration));
    }

    IEnumerator AnimateScale(Vector3 origin, Vector3 target, float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey += Time.deltaTime;
            float curvePercent = GetCurvePercent(journey, duration, scaleCurve);

            transform.localScale = Vector3.LerpUnclamped(origin, target, curvePercent);

            yield return null;
        }
    }

    public void RotateTo(Quaternion targetRotation, float duration)
    {
        StartCoroutine(AnimateRotation(transform.rotation, targetRotation, duration));
    }

    IEnumerator AnimateRotation(Quaternion origin, Quaternion target, float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey += Time.deltaTime;
            float curvePercent = GetCurvePercent(journey, duration, rotationCurve);

            transform.rotation = Quaternion.SlerpUnclamped(origin, target, curvePercent);

            yield return null;
        }
    }

    float GetCurvePercent(float journey, float duration, AnimationCurve curve)
    {
        float percent = Mathf.Clamp01(journey / duration);
        return curve.Evaluate(percent);
    }
}
