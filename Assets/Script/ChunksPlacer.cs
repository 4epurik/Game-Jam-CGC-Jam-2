using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    [SerializeField] private CoinCollector _coinCollector;
    public Transform Player;
    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();
    private MeshRenderer _renderer;

    private void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    private void Update()
    {
        if (Player.position.z > spawnedChunks[spawnedChunks.Count - 1].End.position.z - 40)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        int minChunk = (_coinCollector.GetCoins() > 50) ? 3 : 0;
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(minChunk,ChunkPrefabs.Length)]);
        // Chunk newChunk = Instantiate(ChunkPrefabs[3]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.transform.position - newChunk.Begin.localPosition;
        // SetChankPosition(spawnedChunks[spawnedChunks.Count - 1], newChunk);
        spawnedChunks.Add(newChunk);

        if (spawnedChunks.Count >= 3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }

    //private Chunk GetRandomChunk()
    //{
    //    List<float> chances = new List<float>();
    //    for (int i = 0; i < ChunkPrefabs.Length; i++)
    //    {
    //        chances.Add(ChunkPrefabs[i].ChanceFromDistance.Evaluate(Player.transform.position.z));
    //    }

    //    float value = Random.Range(0, chances.Sum());
    //    float sum = 0;

    //    for (int i = 0; i < chances.Count; i++)
    //    {
    //        sum += chances[i];
    //        if (value < sum)
    //        {
    //            return ChunkPrefabs[i];
    //        }
    //    }

    //    return ChunkPrefabs[ChunkPrefabs.Length-1];
    //}
}