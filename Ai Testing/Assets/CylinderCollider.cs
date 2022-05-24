using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CylinderCollider : MonoBehaviour
{
    public bool IsTrigger = false;
    private bool LastIsTrigger;

    public PhysicMaterial Material;
    private PhysicMaterial LastMaterial;

    public Vector3 Center;
    private Vector3 lastCenter;

    public float Radius = 0.5f;
    private float LastRadius;

    public float Height = 1;
    private float LastHeight;

    [Range(1,20)]
    public int BoxCount = 10;
    private int LastBoxCount;

    [Min(0.01f)]
    public float ColliderWidth = 0.325f;
    private float LastColliderWidth;


    public List<GameObject> Boxes = new List<GameObject>();
    public GameObject BoxCollider;
    void Start()
    {

        foreach (var box in Boxes)
        {
            DestroyImmediate(box);
        }
        Boxes = new List<GameObject>(new GameObject[0]);
            
        if(Boxes.Count == 0) SpawnBoxColliders();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying) GetComponent<CylinderCollider>().enabled = false;
        if (BoxCount != LastBoxCount || Height != LastHeight || Radius != LastRadius || Center != lastCenter || ColliderWidth != LastColliderWidth|| IsTrigger != LastIsTrigger || Material != LastMaterial)
        {

            foreach (var box in Boxes)
            {
                DestroyImmediate(box);
            }
            Boxes = new List<GameObject>(new GameObject[0]);
            SpawnBoxColliders();
        }
    }

    private void SpawnBoxColliders()
    {
        LastBoxCount = BoxCount;
        LastHeight = Height;
        LastRadius = Radius;
        lastCenter = Center;
        LastColliderWidth = ColliderWidth;
        LastIsTrigger = IsTrigger;
        LastMaterial = Material;
        for (int i = 0; i < BoxCount; i++)
        {
            BoxCollider go = Instantiate(BoxCollider, gameObject.transform.position + Center, transform.rotation, transform).GetComponent<BoxCollider>();
            float RotationAngle = 180 / BoxCount;
            go.transform.Rotate(0, RotationAngle * i, 0);
            go.size = new Vector3(ColliderWidth, Height * 2, Radius * 2);
            if(Material != null) go.material = Material;
            go.isTrigger = IsTrigger;
            Boxes.Add(go.gameObject);

        }
    }
}
