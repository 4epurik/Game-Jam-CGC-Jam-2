using System.Collections.Generic;
using Script;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    [SerializeField] private CoinCollector _coinCollector;
    public Transform Player;
    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    private void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    private void Update()
    {
        if (Player.position.z > spawnedChunks[spawnedChunks.Count - 1].End.position.z - 120)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        int minChunk = (_coinCollector.GetCoins() > 50) ? 3 : 0;
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(minChunk,ChunkPrefabs.Length)]);
        var oldChankPosition = spawnedChunks[spawnedChunks.Count - 1].End.transform.position;
        var offsetZ = newChunk.Begin.localPosition.x * newChunk.transform.localScale.x;
        newChunk.transform.position = new Vector3(newChunk.transform.position.x, newChunk.transform.position.y, (int) (oldChankPosition.z + offsetZ));
        spawnedChunks.Add(newChunk);

        if (spawnedChunks.Count >= 6)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }
}