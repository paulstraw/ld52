using UnityEngine;

namespace LD52
{
  public class AppleSplatter : MonoBehaviour
  {
    void OnTriggerEnter2D(Collider2D collider)
    {
      Apple apple = collider.GetComponent<Apple>();
      float wait = Random.Range(0f, 0.2f);

      this.Invoke(() =>
      {
        apple.Splat();
      }, wait);
    }
  }
}
