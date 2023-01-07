using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace LD52
{
  public class AppleSpawner : MonoBehaviour
  {
    [SerializeField]
    int desiredAppleCount;

    [SerializeField]
    float spawnDelay;

    [SerializeField]
    GameObject applePrefab;

    [SerializeField]
    Transform appleSpawnPointsParent;


    List<Transform> appleSpawnPoints = new List<Transform>();

    List<Apple> apples = new List<Apple>();

    Dictionary<Apple, int> appleSpawnPointIndices = new Dictionary<Apple, int>();

    float lastSpawnTime = 0;

    void Start()
    {
      foreach (Transform transform in appleSpawnPointsParent)
      {
        appleSpawnPoints.Add(transform);
      }
    }

    void Update()
    {
      if (apples.Count < desiredAppleCount)
      {
        MaybeSpawnApple();
      }
    }

    void MaybeSpawnApple()
    {
      float now = Time.time;
      if (now - lastSpawnTime < spawnDelay) return;
      lastSpawnTime = now;

      int spawnPointIndex = -1;
      while (spawnPointIndex == -1)
      {
        spawnPointIndex = Random.Range(0, appleSpawnPoints.Count);

        if (appleSpawnPointIndices.Values.Contains(spawnPointIndex))
        {
          spawnPointIndex = -1;
        }
      }


      Transform spawnPoint = appleSpawnPoints[spawnPointIndex];

      var appleGo = Instantiate(applePrefab, spawnPoint.position, spawnPoint.rotation);

      Apple apple = appleGo.GetComponent<Apple>();

      appleSpawnPointIndices[apple] = spawnPointIndex;

      apple.OnDisconnected += HandleAppleDisconnected;
      apples.Add(apple);
    }

    void HandleAppleDisconnected(Apple apple)
    {
      appleSpawnPointIndices.Remove(apple);
      apples.Remove(apple);
    }
  }
}
