using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD52
{
  public class TitleScreenController : MonoBehaviour
  {
    PlayerInput playerInput;

    void Start()
    {
      playerInput = new PlayerInput();
      playerInput.TitleScreen.Enable();
    }

    void Update()
    {
      if (playerInput.TitleScreen.Play.WasPerformedThisFrame())
      {
        SceneManager.LoadScene("Game");
      }
    }
  }
}
