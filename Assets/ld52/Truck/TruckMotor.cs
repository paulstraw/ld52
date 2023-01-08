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

    public float Throttle
    {
      get;
      set;
    } = 0;

    void Update()
    {
      var fm = frontWheel.motor;
      fm.motorSpeed = speed * Throttle;
      frontWheel.motor = fm;

      var rm = rearWheel.motor;
      rm.motorSpeed = speed * Throttle;
      rearWheel.motor = fm;
    }
  }
}
