using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Dragontailgames.Utils;

public class GameManager : MonoBehaviour
{
    public CharacterController playerController;

    public List<TileFloor> tileFloors = new List<TileFloor>();

    public List<GameObject> prefabFloors = new List<GameObject>();

    public List<CharacterController> finishers = new List<CharacterController>();

    private Map<int> map = new Map<int>(new Dictionary<int, float>()
        {
            { 0, 20 },
            { 1, 20 },
            { 2, 20 },
            { 3, 20 },
            { 4, 20 },
        });

    private MapPreset mapPreset = new MapPreset();

    public Level currentLevel;

    void Awake()
    {
        Manager.Instance.gameManager = this;
        if (Manager.Instance.levels.Count > 0)
        {
            if (Manager.Instance.levels.Count > Manager.Instance.currentLevel)
            {
                currentLevel = Manager.Instance.levels[Manager.Instance.currentLevel];
            }
            else
            {
                currentLevel = new Level()
                {
                    level = 2,
                    tiles = Manager.Instance.currentLevel,
                    reward = 500 + 100 * Manager.Instance.currentLevel,
                };
            }
        }
        CreateMap() ;
    }

    public void CreateMap()
    {
        int currentLevel = 0;

        if (PlayerPrefs.HasKey("LEVEL"))
        {
            currentLevel = PlayerPrefs.GetInt("LEVEL");
        }

        List<int> tiles = new List<int>();

        if(currentLevel<mapPreset.presetMap.Count)
        {
            tiles = mapPreset.GetMap(currentLevel);
        }
        else
        {
            tiles = map.GenerateMap(9 + Mathf.FloorToInt((currentLevel-mapPreset.presetMap.Count) / 3), true);
        }

        float nextY = 0;
        for(int i = 0;i<tiles.Count;i++)
        {
            GameObject floor = Instantiate(prefabFloors[i], new Vector3(0, 0, 0), Quaternion.identity);
            Vector3 size = floor.GetComponent<MeshRenderer>().bounds.size;
            floor.transform.position = new Vector3(0, nextY, size.z * i);
            nextY += size.y - 0.2f;
        }

        GameObject finishLine = Instantiate(prefabFloors[prefabFloors.Count - 1], new Vector3(0, 0, 0), Quaternion.identity);
        finishLine.transform.position = new Vector3(0, nextY, finishLine.GetComponent<MeshRenderer>().bounds.size.z * tiles.Count);
    }

    public void CharacterFinishRun(CharacterController controller)
    {
        if (controller == playerController)
        {
            int reward = Mathf.RoundToInt((float)(currentLevel.reward - (currentLevel.reward * (0.25 * finishers.Count))));
            PlayerManager.Instance.FinishRun(reward);
            Manager.Instance.currentLevel++;
            Manager.Instance.canvasManager.EndGame(reward);
        }
        else
        {
            finishers.Add(controller);
        }
    }

    public void NextLevel()
    {
        SceneLoadManager.instance.Reload();
    }

    public void GotoMenu()
    {
        SceneLoadManager.instance.gotoMenu();
    }

    public void LoadPresetMap()
    {
        var presetMap = SaveAndLoad.Load("preset_map", ".dt");
    }
}

[System.Serializable]
public class tempVar
{
    public List<List<int>> map = new List<List<int>>()
    {
        new List<int>()
        {   
            0,1,2,3
        },
        new List<int>()
        {
            2,0,1,3
        },
    };
}
