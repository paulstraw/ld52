using System.Collections.Generic;
using UnityEngine;

namespace LD52
{
  public class CaptureCounter : MonoBehaviour
  {
    public event System.Action OnGameLost;

    int chompedAppleCount = 0;

    [SerializeField]
    List<SpriteRenderer> skullRenderers;

    bool gameLost = false;

    void Start()
    {
      Apple.OnCapturedChompedApple += HandleCaptureChompedApple;
    }

    void OnDestroy()
    {
      Apple.OnCapturedChompedApple -= HandleCaptureChompedApple;
    }

    void HandleCaptureChompedApple()
    {
      if (gameLost) return;

      skullRenderers[chompedAppleCount].color = Color.white;
      chompedAppleCount += 1;

      if (chompedAppleCount == skullRenderers.Count)
      {
        gameLost = true;
      }
    }
  }
}
