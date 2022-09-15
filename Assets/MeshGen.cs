using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verts;
    int[] tris;

    public int xSize = 128;
    public int zSize = 128;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        verts = new Vector3[(xSize + 1) * (zSize + 1)];

        for(int i = 0, z = 0; z <= zSize; z++)
        {
            for(int  x = 0; x <= xSize; x++)
            {
                verts[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        tris = new int[xSize * zSize * 6];

        int v = 0;
        int t = 0;

        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                tris[t + 0] = v + 0;
                tris[t + 1] = v + xSize + 1;
                tris[t + 2] = v + 1;
                tris[t + 3] = v + 1;
                tris[t + 4] = v + xSize + 1;
                tris[t + 5] = v + xSize + 2;

                v++;
                t += 6;
            }
            v++;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        Color[] c = new Color[verts.Length];

        for(int i = 0; i < verts.Length; i++)
        {
            c[i] = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.colors = c;

        mesh.RecalculateNormals();
    }
}
