using UnityEngine;

namespace LD52
{
  public class ConstantRotation : MonoBehaviour
  {
    public Vector3 rotationSpeed;

    void Start()
    {

    }

    void Update()
    {
      if (rotationSpeed.x != 0)
      {
        transform.RotateAround(transform.position, Vector3.right, rotationSpeed.x * Time.deltaTime);
      }

      if (rotationSpeed.y != 0)
      {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed.y * Time.deltaTime);
      }

      if (rotationSpeed.z != 0)
      {
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed.z * Time.deltaTime);
      }
    }
  }
}
