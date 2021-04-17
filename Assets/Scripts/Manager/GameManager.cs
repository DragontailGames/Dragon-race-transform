using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GameManager : MonoBehaviour
{
    public CharacterController playerController;

    public List<GameObject> prefabFloors = new List<GameObject>();

    public int tileCount = 5;

    void Awake()
    {
        Manager.Instance.gameManager = this;

        CreateMap(tileCount);
    }

    public void CreateMap(int tiles)
    {
        for(int i = 0;i<tiles;i++)
        {
            GameObject floor = Instantiate(prefabFloors[Random.Range(0, prefabFloors.Count-1)], new Vector3(0, 0, 0), Quaternion.identity);
            float size = floor.GetComponent<MeshRenderer>().bounds.size.z;
            floor.transform.position = new Vector3(0, 0, size * i);
        }

        GameObject finishLine = Instantiate(prefabFloors[prefabFloors.Count - 1], new Vector3(0, 0, 0), Quaternion.identity);
        finishLine.transform.position = new Vector3(0, 0, finishLine.GetComponent<MeshRenderer>().bounds.size.z * tiles);
    }
}
