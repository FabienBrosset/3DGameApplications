using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct DataVertices
{
    public DataVertices(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public float x { get; private set; }
    public float y { get; private set; }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawingManager : MonoBehaviour
{

    public GameObject drawingSlotPrefab;

    public int inkClassicAmount = 10;

    public Material spotTouchedMat;
    public Material inkMat;

    private GameObject actualDrawSpot;

    private bool clickActive = false;

    private List<DataVertices> verticesData = new List<DataVertices>();

    void Start()
    {
        SetDrawingSpot();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            clickActive = true;
        if (Input.GetMouseButtonUp(0))
        {
            clickActive = false;

            Destroy(GameObject.Find("DrawSpot"));
            CreateMesh();
            SetDrawingSpot();
        }

        if (clickActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "DrawingSpot")
                {
                    StoreSpotDrawing(hit.transform.gameObject);
                }
            }
        }
    }

    private void SetDrawingSpot()
    {
        float x = -10f;
        float y = -4.5f;

        actualDrawSpot = new GameObject();
        actualDrawSpot.name = "DrawSpot";
        GameObject _spot;

        while (y != 7f)
        {
            _spot = Instantiate(drawingSlotPrefab, new Vector3(x, y, 0f), Quaternion.identity);

            _spot.transform.parent = actualDrawSpot.transform;

            x += 0.5f;
            if (x == 10.5f)
            {
                x = -10f;
                y += 0.5f;
            }
        }
    }

    private void StoreSpotDrawing(GameObject drawingSpot)
    {
        if (drawingSpot.name == "stored" || inkClassicAmount == 0)
            return;
        drawingSpot.GetComponent<Renderer>().material = spotTouchedMat;
        drawingSpot.name = "stored";

        inkClassicAmount -= 1;

        verticesData.Add(new DataVertices((drawingSpot.transform.position.x - 0.25f), (drawingSpot.transform.position.y - 0.25f)));
    }


    private void CreateMesh()
    {

        int _verticesCount = 0;
        int _trianglesCount = 0;
        int _triangleArrayCount = 0;

        Vector3[] vertices = new Vector3[(verticesData.Count * 8)];
       
        foreach (DataVertices data in verticesData)
        {
            vertices[_verticesCount] = new Vector3(data.x, data.y, 0);
            vertices[_verticesCount + 1] = new Vector3(data.x + 0.50f, data.y, 0);
            vertices[_verticesCount + 2] = new Vector3(data.x + 0.50f, data.y + 0.50f, 0);
            vertices[_verticesCount + 3] = new Vector3(data.x, data.y + 0.50f, 0);

            vertices[_verticesCount + 4] = new Vector3(data.x, data.y + 0.50f, 0.50f);
            vertices[_verticesCount + 5] = new Vector3(data.x + 0.50f, data.y + 0.50f, 0.50f);
            vertices[_verticesCount + 6] = new Vector3(data.x + 0.50f, data.y, 0.50f);
            vertices[_verticesCount + 7] = new Vector3(data.x, data.y, 0.50f);
            _verticesCount += 8;
        }


        int[] triangles = new int[(verticesData.Count * 36)];
        
        for (int data = 0; data != verticesData.Count; data += 1)
        { 
            triangles[_triangleArrayCount] = _trianglesCount;
            triangles[_triangleArrayCount + 1] = _trianglesCount + 2;
            triangles[_triangleArrayCount + 2] = _trianglesCount + 1;
            triangles[_triangleArrayCount + 3] = _trianglesCount;
            triangles[_triangleArrayCount + 4] = _trianglesCount + 3;
            triangles[_triangleArrayCount + 5] = _trianglesCount + 2;

            triangles[_triangleArrayCount + 6] = _trianglesCount + 2;
            triangles[_triangleArrayCount + 7] = _trianglesCount + 3;
            triangles[_triangleArrayCount + 8] = _trianglesCount + 4;
            triangles[_triangleArrayCount + 9] = _trianglesCount + 2;
            triangles[_triangleArrayCount + 10] = _trianglesCount + 4;
            triangles[_triangleArrayCount + 11] = _trianglesCount + 5;

            triangles[_triangleArrayCount + 12] = _trianglesCount + 1;
            triangles[_triangleArrayCount + 13] = _trianglesCount + 2;
            triangles[_triangleArrayCount + 14] = _trianglesCount + 5;
            triangles[_triangleArrayCount + 15] = _trianglesCount + 1;
            triangles[_triangleArrayCount + 16] = _trianglesCount + 5;
            triangles[_triangleArrayCount + 17] = _trianglesCount + 6;

            triangles[_triangleArrayCount + 18] = _trianglesCount;
            triangles[_triangleArrayCount + 19] = _trianglesCount + 7;
            triangles[_triangleArrayCount + 20] = _trianglesCount + 4;
            triangles[_triangleArrayCount + 21] = _trianglesCount;
            triangles[_triangleArrayCount + 22] = _trianglesCount + 4;
            triangles[_triangleArrayCount + 23] = _trianglesCount + 3;

            triangles[_triangleArrayCount + 24] = _trianglesCount + 5;
            triangles[_triangleArrayCount + 25] = _trianglesCount + 4;
            triangles[_triangleArrayCount + 26] = _trianglesCount + 7;
            triangles[_triangleArrayCount + 27] = _trianglesCount + 5;
            triangles[_triangleArrayCount + 28] = _trianglesCount + 7;
            triangles[_triangleArrayCount + 29] = _trianglesCount + 6;

            triangles[_triangleArrayCount + 30] = _trianglesCount;
            triangles[_triangleArrayCount + 31] = _trianglesCount + 6;
            triangles[_triangleArrayCount + 32] = _trianglesCount + 7;
            triangles[_triangleArrayCount + 33] = _trianglesCount;
            triangles[_triangleArrayCount + 34] = _trianglesCount + 1;
            triangles[_triangleArrayCount + 35] = _trianglesCount + 6;

            _trianglesCount += 8;
            _triangleArrayCount += 36;
        }

        GameObject obj = new GameObject();

        obj.name = "Mesh";
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();

        obj.GetComponent<Renderer>().material = inkMat;
        obj.AddComponent<MeshCollider>();
        

    }


}

/*


public class MeshGenerator : MonoBehaviour {

	void Start () {
		CreateCube ();
	}

	private void CreateCube () {
		Vector3[] vertices = {
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (0, 1, 0),
			new Vector3 (0, 1, 1),
			new Vector3 (1, 1, 1),
			new Vector3 (1, 0, 1),
			new Vector3 (0, 0, 1),
		};

		int[] triangles = {
			0, 2, 1, //face front
			0, 3, 2,
			2, 3, 4, //face top
			2, 4, 5,
			1, 2, 5, //face right
			1, 5, 6,
			0, 7, 4, //face left
			0, 4, 3,
			5, 4, 7, //face back
			5, 7, 6,
			0, 6, 7, //face bottom
			0, 1, 6
		};
			
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.Optimize ();
		mesh.RecalculateNormals ();
	}
}


 * */