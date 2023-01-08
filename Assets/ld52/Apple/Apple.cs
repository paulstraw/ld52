using UnityEngine;
using System.Collections.Generic;

namespace LD52
{
  public class Apple : MonoBehaviour
  {
    public event System.Action<Apple> OnDisconnected;

    [SerializeField, Layer]
    int ripeAppleLayer;

    [SerializeField]
    float initialGrowthRate;

    [SerializeField]
    List<Sprite> growthStageSprites;

    [SerializeField]
    Sprite chompedSprite;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float cutForce;

    int currentGrowthStage = 0;

    float lastGrowthAt;

    float growthRate;

    bool hasBeenChomped = false;

    bool hasBeenCut = false;

    int initialLayer;

    public bool IsRipe
    {
      get;
      private set;
    } = false;

    void Start()
    {
      lastGrowthAt = Time.time;
      growthRate = initialGrowthRate * Random.Range(0.75f, 1.25f);
      initialLayer = gameObject.layer;
      UpdateSprite();
    }

    void Update()
    {
      if (IsRipe) return;

      float now = Time.time;
      if (now - lastGrowthAt < growthRate) return;
      lastGrowthAt = now;

      currentGrowthStage += 1;
      growthRate = initialGrowthRate * Random.Range(0.75f, 1.25f);
      UpdateSprite();

      if (currentGrowthStage == growthStageSprites.Count - 1)
      {
        IsRipe = true;
        gameObject.layer = ripeAppleLayer;
      }
    }

    void UpdateSprite()
    {
      spriteRenderer.sprite = growthStageSprites[currentGrowthStage];
    }

    public bool Chomp()
    {
      if (hasBeenChomped || hasBeenCut) return false;
      hasBeenChomped = true;
      OnDisconnected?.Invoke(this);

      spriteRenderer.sortingOrder = 9;

      rb.bodyType = RigidbodyType2D.Dynamic;
      rb.AddTorque(Random.Range(-20f, 20f));
      rb.AddForce(
        new Vector2(
          Random.Range(-5f, 5f),
          Random.Range(-5f, 5f)
        ),
        ForceMode2D.Impulse
      );

      gameObject.layer = initialLayer;
      spriteRenderer.sprite = chompedSprite;

      return true;
    }

    public bool Cut(Vector3 cutterPosition)
    {
      if (hasBeenChomped || hasBeenCut) return false;
      hasBeenCut = true;
      OnDisconnected?.Invoke(this);

      spriteRenderer.sortingOrder = 9;

      Vector3 cutDirection = (transform.position - cutterPosition).normalized;

      rb.bodyType = RigidbodyType2D.Dynamic;
      rb.AddTorque(Random.Range(-20f, 20f));
      rb.AddForce(
        cutDirection * cutForce,
        ForceMode2D.Impulse
      );

      gameObject.layer = initialLayer; // TODO:

      return true;
    }
  }
}
