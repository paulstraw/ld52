using System;
using UnityEngine;

namespace LD52
{
  public class TruckMotor : MonoBehaviour
  {
    [SerializeField]
    float speed;

    [SerializeField]
    WheelJoint2D frontWheel;

    [SerializeField]
    WheelJoint2D rearWheel;

    [SerializeField]
    AnimationCurve pitchCurve;

    bool isRunning = false;

    AudioSource audioSource;

    public float Throttle
    {
      get;
      set;
    } = 0;

    void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
      if (!isRunning && (Throttle > 0.1f || Throttle < -0.1f))
      {
        isRunning = true;
        audioSource.Play();
      }
      else if (isRunning && Throttle < 0.1f && Throttle > -0.1f)
      {
        isRunning = false;
        audioSource.Pause();
      }

      audioSource.pitch = pitchCurve.Evaluate(Mathf.Abs(Throttle));

      var fm = frontWheel.motor;
      fm.motorSpeed = speed * Throttle;
      frontWheel.motor = fm;

      var rm = rearWheel.motor;
      rm.motorSpeed = speed * Throttle;
      rearWheel.motor = fm;
    }
  }
}
