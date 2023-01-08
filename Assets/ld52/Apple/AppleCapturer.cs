using UnityEngine;

namespace LD52
{
  public class AppleCapturer : MonoBehaviour
  {
    void OnTriggerEnter2D(Collider2D collider)
    {
      var apple = collider.GetComponent<Apple>();
      if (apple == null) return;
      float delay = Random.Range(0.1f, 0.2f);

      this.Invoke(() =>
      {
        apple.Capture(transform);
      }, delay);
    }
  }
}
