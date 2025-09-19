using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform Begin;
    public Transform End;
    [SerializeField] private GameObject _coin;

    //public Mesh[] BlockMeshes;

    //public AnimationCurve ChanceFromDistance;

    //private void Start()
    //{
    //    foreach (var filter in GetComponentsInChildren<MeshFilter>())
    //    {
    //        if (filter.sharedMesh == BlockMeshes[0])
    //        {
    //            filter.sharedMesh = BlockMeshes[Random.Range(0, BlockMeshes.Length)];
    //            filter.transform.rotation = Quaternion.Euler(-90, 0, 90 * Random.Range(0,4));
    //        }
    //    }
    //}
    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    { 
        List<GameObject> safeBlocks = FindChildWithTag(gameObject, "safeBlock");
        int countBlock = safeBlocks.Count;
        if (countBlock > 1)
        {
            List<int> coinsNumber = GetRandomNumbers(1, countBlock-1, countBlock/2);

            foreach (var numberBlock in coinsNumber)
            {
                var newCoin = Instantiate(_coin, safeBlocks[numberBlock].transform);
                float[] arrayY = new float[] {2f, 2.9f};
                if (safeBlocks[numberBlock].gameObject.name.Contains("BOX_YelloyWall")
                    && safeBlocks[numberBlock].transform.parent.name.Contains("LongBOX_YelloyWall")) 
                {
                    arrayY[0] = 2.3f;
                    arrayY[1] = 2.9f;
                }
                var coinY = arrayY[Random.Range(0, arrayY.Length)];
                newCoin.transform.localPosition = new Vector3(0, coinY, 0);
            }
        }
    }
    
    private List<int> GetRandomNumbers(int min, int max, int count)
    {
        List<int> numbers = new List<int>();

        HashSet<int> selectedNumbers = new HashSet<int>();
        while (selectedNumbers.Count < count)
        {
            int randomNumber = Random.Range(min, max + 1);
            if (selectedNumbers.Add(randomNumber))
            {
                numbers.Add(randomNumber);
            }
        }
        return numbers;
    }
    
    private List<GameObject> FindChildWithTag(GameObject parent, string tag) {
        List<GameObject> childs = new List<GameObject>();

        Transform[] childrenWithTag = gameObject.GetComponentsInChildren<Transform>(true)
            .Where(t => t.CompareTag(tag))
            .ToArray();

        foreach (Transform objectTag in childrenWithTag)
        {
            childs.Add(objectTag.gameObject);
        }
        return childs;
    }
}