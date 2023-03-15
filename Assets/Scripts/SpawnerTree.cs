using UnityEngine;

public class SpawnerTree : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    [SerializeField] private Transform spawner;
    
    private GameObject Tree;
    
    private int sizeX = 2;
    private int sizeZ = 5;
    private float elementOffset = 2f;

    private void Start()
    {
        Instance();
    }
    
    private void Instance()
    {
        var column = sizeX;
        var row = sizeZ;
        for (int z = 0; z < row; z++)
        {
            for (int x = 0; x < column; x++)
            {
                var position = spawner.position + new Vector3(elementOffset * x, 0, elementOffset * z);
                Tree = Instantiate(tree, position, Quaternion.identity);
            }
        }
    }
}
