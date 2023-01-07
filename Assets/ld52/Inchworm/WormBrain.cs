using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

namespace LD52
{
  public class WormBrain : MonoBehaviour
  {
    enum WormBrainState
    {
      NeedsDestination,
      HasDestination,
      HasNextTarget,
      HasReachedRipeApple,
      HasChompedRipeApple,
    };

    [SerializeField]
    float destinationSlop;

    [SerializeField]
    float targetSlop;

    [SerializeField]
    float idealNextTargetDistance;

    [SerializeField]
    float maxCandidateSplineDistance;

    [SerializeField]
    float speed;


    [SerializeField]
    SplineContainer splineContainer;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    Vector3 destination;

    Vector3 nextTarget;

    WormBrainState currentBrainState = WormBrainState.NeedsDestination;

    Spline targetSpline;

    void Update()
    {
      switch (currentBrainState)
      {
        case WormBrainState.NeedsDestination:
          PickDestination();
          break;
        case WormBrainState.HasDestination:
          FindNextTarget();
          break;
        case WormBrainState.HasNextTarget:
          MoveTowardNextTarget();
          break;
      }
    }

    void PickDestination()
    {
      List<Spline> candidates = new List<Spline>();

      foreach (Spline spline in splineContainer.Splines)
      {
        float distance = SplineUtility.GetNearestPoint(
          spline,
          (float3)transform.position,
          out float3 nearestPoint,
          out float t
        );

        if (distance <= maxCandidateSplineDistance)
        {
          candidates.Add(spline);
        }
      }

      targetSpline = candidates[UnityEngine.Random.Range(0, candidates.Count)];

      if (targetSpline == null)
      {
        return;
      }

      BezierKnot destinationKnot = targetSpline.Knots.ToList()[UnityEngine.Random.Range(0, targetSpline.Knots.Count())];

      destination = destinationKnot.Position;
      currentBrainState = WormBrainState.HasDestination;
    }

    void FindNextTarget()
    {
      // Debug.Log("FindNextTarget");
      Vector3 directionToDestination = (destination - transform.position).normalized;
      Vector3 idealNextTarget = transform.position + (directionToDestination * idealNextTargetDistance);
      Vector3 newNextTarget = Vector3.zero;
      float bestMatchDistance = float.MaxValue;

      // Debug.DrawLine(transform.position, idealNextTarget, Color.red, 50.0f);
      Spline closestSpline = splineContainer.Splines[0];
      float closestSplinePointT = 0;

      SplineUtility.GetNearestPoint(
        targetSpline,
        (float3)idealNextTarget,
        out float3 nearestPointOnSplineToIdealTarget,
        out float t
      );

      newNextTarget = (Vector3)nearestPointOnSplineToIdealTarget;

      SplineUtility.EvaluateTangent(closestSpline, closestSplinePointT);

      // Debug.DrawLine(transform.position, newNextTarget, Color.magenta, 50.0f);

      nextTarget = newNextTarget;
      currentBrainState = WormBrainState.HasNextTarget;
    }

    void MoveTowardNextTarget()
    {
      Vector3 directionToNextTarget = (nextTarget - transform.position).normalized;
      Debug.DrawRay(transform.position, directionToNextTarget, Color.cyan, 0.01f);

      Vector3 newPos = transform.position + directionToNextTarget * speed * Time.deltaTime;

      float angle = Vector3.SignedAngle(Vector3.left, directionToNextTarget, Vector3.forward);

      spriteRenderer.flipX = true;
      spriteRenderer.flipY = directionToNextTarget.x > 0;
      spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, angle);

      transform.position = newPos;

      if (Vector3.Distance(transform.position, destination) <= destinationSlop)
      {
        currentBrainState = WormBrainState.NeedsDestination;
      }
      else if (Vector3.Distance(transform.position, nextTarget) <= targetSlop)
      {
        currentBrainState = WormBrainState.HasDestination;
      }
    }
  }
}
