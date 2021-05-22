using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SettingsData;


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
    public SettingsData SettingsData;

    public Image InkAmountBar;
    public GameObject drawingSlotPrefab;

    public int[] inkAmount = new int[4]; // classic, bounce, balloon, fade

    public int inkSelected = 0;

    public Material spotTouchedMat;

    public Material inkMat;
    public Material inkBounceMat;
    public Material inkBalloonMat;

    public AudioSource audioSource;
    public AudioClip inkClassicSound;
    public AudioClip inkBounceSound;
    public AudioClip inkBalloonSound;
    public AudioClip inkFadeSound;

    private GameObject actualDrawSpot;

    private bool clickActive = false;

    private List<DataVertices> verticesData = new List<DataVertices>();

    void Start()
    {
        inkAmount[0] = 10;
        inkAmount[1] = 10;
        inkAmount[2] = 10;
        inkAmount[3] = 10;

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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inkSelected += 1;
            if (inkSelected == 4)
                inkSelected = 0;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inkSelected -= 1;
            if (inkSelected == -1)
                inkSelected = 3;
        }

        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Slot1))
            inkSelected = 0;
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Slot2))
            inkSelected = 1;
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Slot3))
            inkSelected = 2;
        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Slot4))
            inkSelected = 3;

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
        InkAmountBar.fillAmount = (inkAmount[inkSelected] / 30f);
    }

    public void AddInk(int type, int amount)
    {
        inkAmount[type] += amount;
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
        if (drawingSpot.name == "stored" || inkAmount[inkSelected] == 0)
            return;
        drawingSpot.GetComponent<Renderer>().material = spotTouchedMat;
        drawingSpot.name = "stored";

        inkAmount[inkSelected] -= 1;

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

        obj.AddComponent<MeshCollider>();

        obj.transform.localScale = new Vector3(1f, 1f, 3f);

        if (inkSelected == 0)
        {
            obj.GetComponent<Renderer>().material = inkMat;
            audioSource.PlayOneShot(inkClassicSound, 1);
        }
        else if (inkSelected == 1)
        {
            obj.GetComponent<Renderer>().material = inkBounceMat;
            obj.tag = "Bounce";
            audioSource.PlayOneShot(inkBounceSound, 1);
        }
        else if (inkSelected == 2)
        {
            obj.GetComponent<Renderer>().material = inkBalloonMat;
            obj.AddComponent<BalloonScript>();
            obj.tag = "BalloonInk";
            audioSource.PlayOneShot(inkBalloonSound, 1);
        }
        else if (inkSelected == 3)
        {
            obj.AddComponent<FadeScript>();
            audioSource.PlayOneShot(inkFadeSound, 1);
        }

        verticesData = new List<DataVertices>();

    }


}
