using UnityEngine;

namespace LD52
{
  public class Boomerang : MonoBehaviour
  {
    enum BoomerangState
    {
      Idle,
      MovingToTarget,
      ReturningToHome,
    }

    [SerializeField]
    Transform homeBase;

    [SerializeField]
    float speed;

    [SerializeField]
    float targetSlop;

    BoomerangState currentState = BoomerangState.Idle;

    Vector3 target;

    ConstantRotation constantRotation;

    AudioSource audioSource;

    void Start()
    {
      audioSource = GetComponent<AudioSource>();
      constantRotation = GetComponent<ConstantRotation>();
      AttachToHomeBase();
    }

    void Update()
    {
      switch (currentState)
      {
        case BoomerangState.MovingToTarget:
          MoveToTarget();
          break;
        case BoomerangState.ReturningToHome:
          MoveToTarget();
          break;
      }
    }

    void AttachToHomeBase()
    {
      constantRotation.rotationSpeed = Vector3.zero;
      transform.SetParent(homeBase);
      transform.localPosition = Vector3.zero;
      audioSource.Stop();
    }

    void MoveToTarget()
    {
      var directionToTarget = (target - transform.position).normalized;

      Vector3 newPos = transform.position + directionToTarget * speed * Time.deltaTime;

      transform.position = newPos;

      if (currentState == BoomerangState.ReturningToHome)
      {
        target = homeBase.position;
      }

      if (Vector3.Distance(newPos, target) < targetSlop)
      {
        if (currentState == BoomerangState.MovingToTarget)
        {
          currentState = BoomerangState.ReturningToHome;
          target = homeBase.position;
        }
        else if (currentState == BoomerangState.ReturningToHome)
        {
          AttachToHomeBase();
          currentState = BoomerangState.Idle;
        }
      }
    }

    public void Throw(Vector3 newTarget)
    {
      if (currentState != BoomerangState.Idle) return;

      audioSource.Play();

      target = newTarget;
      currentState = BoomerangState.MovingToTarget;
      constantRotation.rotationSpeed = new Vector3(0, 0, -3000);

      transform.SetParent(null);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
      var apple = collider.GetComponent<Apple>();

      if (apple != null)
      {
        apple.Cut(transform.position);
      }
    }
  }
}
