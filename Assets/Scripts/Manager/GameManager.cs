using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Dragontailgames.Utils;

public class GameManager : MonoBehaviour
{
    public CharacterController playerController;

    public List<GameObject> prefabFloors = new List<GameObject>();

    public List<CharacterController> finishers = new List<CharacterController>();

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
        CreateMap(currentLevel.tiles, currentLevel.level) ;
    }

    public void CreateMap(int tiles, int level)
    {
        for(int i = 0;i<tiles;i++)
        {
            GameObject floor = Instantiate(prefabFloors[Random.Range(0, level)], new Vector3(0, 0, 0), Quaternion.identity);
            float size = floor.GetComponent<MeshRenderer>().bounds.size.z;
            floor.transform.position = new Vector3(0, 0, size * i);
        }

        GameObject finishLine = Instantiate(prefabFloors[prefabFloors.Count - 1], new Vector3(0, 0, 0), Quaternion.identity);
        finishLine.transform.position = new Vector3(0, 0, finishLine.GetComponent<MeshRenderer>().bounds.size.z * tiles);
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
}
