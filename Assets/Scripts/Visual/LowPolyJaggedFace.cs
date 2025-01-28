using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LowPolyJaggedFace : MonoBehaviour
{
    [SerializeField] private Vector3 faceDirection = Vector3.forward; // Direction of the face to jag
    [SerializeField] private float jaggedness = 0.2f; // Amount of randomness
    [SerializeField] private float minOffset = -0.1f; // Minimum offset for randomness
    [SerializeField] private float maxOffset = 0.1f; // Maximum offset for randomness

    private void Start()
    {
        // Get the mesh
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        mesh.RecalculateNormals(); // Ensure normals are recalculated


        // Get the vertices and triangles
        Vector3[] vertices = mesh.vertices;

        // Randomize the vertices on the specified face
        for (int i = 0; i < vertices.Length; i++)
        {
            // Check if the vertex is on the desired face
            if (Vector3.Dot(mesh.normals[i], faceDirection) > 0.9f) // Adjust dot product tolerance if needed
            {
                // Randomize the vertex position
                vertices[i] += new Vector3(
                    Random.Range(minOffset, maxOffset),
                    Random.Range(minOffset, maxOffset),
                    Random.Range(minOffset, maxOffset) * jaggedness
                );
            }
        }

        // Apply the updated vertices back to the mesh
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}