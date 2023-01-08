using UnityEngine;
using DG.Tweening;

namespace LD52
{
  public class InfoItem : MonoBehaviour
  {
    const float duration = 2f;

    const float preFadeDelay = 1.5f;

    const float fadeDuration = duration - preFadeDelay;

    SpriteRenderer sr;

    void Start()
    {
      sr = GetComponent<SpriteRenderer>();

      float newY = transform.position.y + 1;
      transform.DOMoveY(newY, duration);

      this.Invoke(() =>
      {
        sr.DOFade(0, fadeDuration).OnComplete(() =>
        {
          Destroy(gameObject);
        });
      }, preFadeDelay);
    }
  }
}
