using UnityEngine;

namespace LD52
{
  public class InchwormAnimationEventProxy : MonoBehaviour
  {
    public event System.Action OnChompFinished;

    public void HandleChompFinished()
    {
      OnChompFinished?.Invoke();
    }
  }
}
