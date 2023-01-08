using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace LD52
{
  public class Scoreboard : MonoBehaviour
  {
    int currentLives = 4;

    int score = 0;

    float startTime;

    bool isGameOver = false;

    UIDocument uiDocument;

    AudioSource audioSource;

    Button tryAgainButton;
    Button rateOnLDJamButton;

    [SerializeField]
    AudioClip gameStartClip;

    [SerializeField]
    AudioClip gameOverClip;

    void Start()
    {
      startTime = Time.time;

      uiDocument = GetComponent<UIDocument>();
      audioSource = GetComponent<AudioSource>();

      var rootEl = uiDocument.rootVisualElement;
      tryAgainButton = rootEl.Query<Button>("try-again");
      rateOnLDJamButton = rootEl.Query<Button>("rate-on-ldjam");

      tryAgainButton.RegisterCallback<ClickEvent>(HandleClickTryAgain);
      rateOnLDJamButton.RegisterCallback<ClickEvent>(HandleClickRateOnLDJam);

      Apple.OnCapturedCutApple += HandleCapturedCutApple;
      Apple.OnCapturedChompedApple += HandleCapturedChompedApple;

      audioSource.PlayOneShot(gameStartClip);
    }

    void Update()
    {
      if (isGameOver) return;

      // TODO: Update timer
    }

    void OnDestroy()
    {
      Apple.OnCapturedCutApple -= HandleCapturedCutApple;
      Apple.OnCapturedChompedApple -= HandleCapturedChompedApple;

      tryAgainButton.UnregisterCallback<ClickEvent>(HandleClickTryAgain);
      rateOnLDJamButton.UnregisterCallback<ClickEvent>(HandleClickRateOnLDJam);
    }

    void HandleClickTryAgain(ClickEvent e)
    {
      SceneManager.LoadScene("Game");
    }

    void HandleClickRateOnLDJam(ClickEvent e)
    {
      Application.OpenURL("https://ldjam.com/events/ludum-dare/52/bad-apples");
    }

    void HandleCapturedCutApple()
    {
      if (isGameOver) return;

      score += 1;
    }

    void HandleCapturedChompedApple()
    {
      if (isGameOver) return;

      currentLives -= 1;

      float duration = Time.time - startTime;

      if (currentLives == 0)
      {
        isGameOver = true;

        this.Invoke(() =>
        {
          audioSource.PlayOneShot(gameOverClip);
        }, 0.2f);

        UnityEngine.Cursor.visible = true;

        // TODO: show scoreboard
        uiDocument.enabled = true;
      }
    }
  }
}
