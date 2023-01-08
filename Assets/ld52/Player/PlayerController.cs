using UnityEngine;

namespace LD52
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField]
    Transform leafPeeker;

    [SerializeField]
    Boomerang boomerang;

    [SerializeField]
    TruckMotor truckMotor;

    PlayerInput playerInput;

    Camera mainCam;

    float throttle = 0;

    void Start()
    {
      playerInput = new PlayerInput();
      playerInput.Truck.Enable();
      mainCam = Camera.main;
    }

    void Update()
    {
      Vector2 screenLook = playerInput.Truck.Look.ReadValue<Vector2>();
      Vector3 worldLook = mainCam.ScreenToWorldPoint(screenLook);
      worldLook.z = 0;
      leafPeeker.position = worldLook;

      if (playerInput.Truck.ThrowBoomerang.WasPressedThisFrame())
      {
        boomerang.Throw(worldLook);
      }

      if (playerInput.Truck.Drive.WasPressedThisFrame())
      {
        throttle = 0;
      }

      float throttleInput = playerInput.Truck.Drive.ReadValue<float>();

      truckMotor.Throttle = Mathf.SmoothDamp(truckMotor.Throttle, throttleInput, ref throttle, 0.1f);
    }
  }
}
