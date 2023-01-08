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

    void Start()
    {
      playerInput = new PlayerInput();
      playerInput.Enable();
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

      truckMotor.Throttle = playerInput.Truck.Drive.ReadValue<float>();
    }
  }
}
