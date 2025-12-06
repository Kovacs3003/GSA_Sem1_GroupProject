using UnityEngine;

public class SplitMeshBySubmesh : MonoBehaviour
{
    [ContextMenu("Split Mesh By SubMesh")]
    void Split()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();


        Mesh mesh = mf.sharedMesh;
        Material[] materials = mr.sharedMaterials;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            GameObject part = new GameObject("Part_" + i);
            part.transform.SetParent(transform.parent);
            part.transform.localPosition = transform.localPosition;
            part.transform.localRotation = transform.localRotation;
            part.transform.localScale = transform.localScale;

            Mesh newMesh = new Mesh();
            newMesh.vertices = mesh.vertices;
            newMesh.normals = mesh.normals;
            newMesh.uv = mesh.uv;
            newMesh.triangles = mesh.GetTriangles(i);

            part.AddComponent<MeshFilter>().mesh = newMesh;
            part.AddComponent<MeshRenderer>().material = materials[i];
        }

        Debug.Log("Done");
    }
}
