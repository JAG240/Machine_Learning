using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Testing : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = new GameObject("Land");
        parent.transform.parent = this.gameObject.transform;

        for(int x = 0; x < 128; x++)
        {
            for(int z = 0; z < 128; z++)
            {
                GameObject cubeTemp = Instantiate(cube, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
                cubeTemp.GetComponent<MeshRenderer>().material = mats[Random.Range(0, 2)];
            }
        }

        //MergeMeshes();
    }

    private void MergeMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }
}
