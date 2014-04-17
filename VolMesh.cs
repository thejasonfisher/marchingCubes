using UnityEngine;
using UnityEditor;
using System.Collections;


public class VolMesh
{
	private readonly int size = 20, maxHeight = 100;
	private int xi,yi,zi,seed,Y;
	private float freq;
	public GameObject m_mesh;
	private Mesh mesh;
	private PerlinNoise m_perlin;
	private Vector3 gPos;
	public bool isNew = true;
	
	public void Initialize(int seed_,float freq_,Vector3 gPos_, Material mat)
	{
		seed = seed_;
		freq = freq_;
		gPos = gPos_;
		xi = (int)gPos_.x;
		yi = (int)gPos_.y;
		zi = (int)gPos_.z;
		m_perlin = new PerlinNoise(seed);
		MarchingCubes.SetTarget(0.0f);
		MarchingCubes.SetWindingOrder(2, 1, 0);
		MarchingCubes.SetModeToCubes();
		m_mesh = new GameObject("Mesh(" + xi + "," + yi + "," + zi + ")");
		m_mesh.AddComponent<MeshFilter>();
		m_mesh.AddComponent<MeshRenderer>();
		m_mesh.renderer.material = mat;
		m_mesh.renderer.material.SetTextureScale("_MainTex", new Vector2(0.1f, 0.1f));
		if(Resources.Load<Mesh>("meshes/mesh" + xi + yi + zi))
			isNew = false;
		else
			isNew = true;

	}
	
	public void Build () 
	{
		float[,,] voxels = new float[size+1,size+1,size+1];
		int height;
		float hFactor;
		for(int z = 0; z < size+1; z++){
		for(int y = 0; y < size+1; y++){	
		   height = yi + y;
		   hFactor = (float)(maxHeight - 2*height) / (float)maxHeight;
		for(int x = 0; x < size+1; x++){		
		   voxels[x,y,z] = -hFactor + m_perlin.FractalNoise3D((float)(xi+x), (float)(yi+y), (float)(zi+z), 3, freq, 1.0f); 
		}}}
		
		mesh = MarchingCubes.CreateMesh(voxels);
		
		//UV MAPPING
		Vector3[] vertices = mesh.vertices;
		Vector2[] uvs = new Vector2[mesh.vertices.Length];
		for(int i = 0; i < uvs.Length;i++){
			uvs[i] = new Vector2(vertices[i].x,vertices[i].z);
		}
		mesh.uv = uvs;
		//NORMALS
		mesh.RecalculateNormals();
		//TANGENTS
		TangentSolver.Solve(mesh);
		mesh.Optimize();
		AssetDatabase.CreateAsset(mesh, "Assets/Resources/meshes/mesh" + xi + yi + zi + ".asset");
		//GAMEOBJECT SETUP
		m_mesh.GetComponent<MeshFilter>().mesh = mesh;
		m_mesh.transform.localPosition = gPos;
		m_mesh.AddComponent<MeshCollider>();
	}
	public void Load()
	{
		mesh = Resources.Load<Mesh>("meshes/mesh" + xi + yi + zi);
		m_mesh.GetComponent<MeshFilter>().mesh = mesh;
		m_mesh.transform.localPosition = gPos;
		m_mesh.AddComponent<MeshCollider>();
	}
	public void Delete()
	{
		UnityEngine.Object.Destroy(m_mesh);
	
	}
}
