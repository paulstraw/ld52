using UnityEngine;
using DG.Tweening;

namespace LD52
{
  public class LeafPeeker : MonoBehaviour
  {
    void Start()
    {
      Cursor.visible = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
      var sr = collider.GetComponent<SpriteRenderer>();
      var color = sr.color;
      color.a = 0.1f;

      sr.DOColor(color, 0.1f);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
      var sr = collider.GetComponent<SpriteRenderer>();
      var color = sr.color;
      color.a = 1f;

      sr.DOColor(color, 0.1f);
    }
  }
}
