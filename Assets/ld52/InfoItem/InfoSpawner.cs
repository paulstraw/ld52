using UnityEngine;

namespace LD52
{
  public class InfoSpawner : MonoBehaviour
  {
    [SerializeField]
    GameObject capturedChompedPrefab;

    [SerializeField]
    GameObject capturedCutPrefab;

    [SerializeField]
    Transform infoSpawnPoint;

    void Start()
    {
      Apple.OnCapturedCutApple += HandleCapturedCutApple;
      Apple.OnCapturedChompedApple += HandleCapturedChompedApple;
    }

    void OnDestroy()
    {
      Apple.OnCapturedCutApple -= HandleCapturedCutApple;
      Apple.OnCapturedChompedApple -= HandleCapturedChompedApple;
    }

    void HandleCapturedChompedApple()
    {
      Instantiate(capturedChompedPrefab, infoSpawnPoint.position, infoSpawnPoint.rotation);
    }

    void HandleCapturedCutApple()
    {
      Instantiate(capturedCutPrefab, infoSpawnPoint.position, infoSpawnPoint.rotation);
    }
  }
}
