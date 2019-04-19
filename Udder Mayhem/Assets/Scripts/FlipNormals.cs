using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNormals : MonoBehaviour {

	void Start () {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;

        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++) {
            normals[i] = -1 * normals[i];
        }

        mesh.normals = normals;

        for (int i = 0; i < mesh.subMeshCount; i++) {
            int[] triangles = mesh.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3) {
                // Swap order of triangle vertices
                int t = triangles[j];
                triangles[j] = triangles[j + 1];
                triangles[j + 1] = t;
            }

            mesh.SetTriangles(triangles, i);
        }
	}
}
