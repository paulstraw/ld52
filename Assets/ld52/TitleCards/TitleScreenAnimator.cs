using UnityEngine;
using DG.Tweening;

namespace LD52
{
  public class TitleScreenAnimator : MonoBehaviour
  {
    [SerializeField]
    SpriteRenderer madeFromScratch;

    [SerializeField]
    SpriteRenderer ludumDare;

    [SerializeField]
    SpriteRenderer paulstrawPresents;

    [SerializeField]
    SpriteRenderer card1Background;

    void Awake()
    {
      madeFromScratch.DOFade(0, 0);
      ludumDare.DOFade(0, 0);
      paulstrawPresents.DOFade(0, 0);
    }

    void Start()
    {
      this.Invoke(() =>
      {
        madeFromScratch.DOFade(1, 0.6f);
      }, 0.6f);
      this.Invoke(() =>
      {
        ludumDare.DOFade(1, 0.8f);
      }, 1.5f);
      this.Invoke(() =>
      {
        paulstrawPresents.DOFade(1, 0.9f);
      }, 3f);

      this.Invoke(HideCard1, 6f);
    }

    void HideCard1()
    {
      madeFromScratch.DOFade(0, 1f);
      ludumDare.DOFade(0, 1f);
      paulstrawPresents.DOFade(0, 1f);
      card1Background.DOFade(0, 1f);
    }
  }
}
