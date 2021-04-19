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
        float nextY = 0;
        for(int i = 0;i<tiles;i++)
        {
            int flootType = Random.Range(0, level);
            GameObject floor = Instantiate(prefabFloors[flootType], new Vector3(0, 0, 0), Quaternion.identity);
            Vector3 size = floor.GetComponent<MeshRenderer>().bounds.size;
            floor.transform.position = new Vector3(0, nextY, size.z * i);
            nextY += size.y - 0.2f;
        }

        GameObject finishLine = Instantiate(prefabFloors[prefabFloors.Count - 1], new Vector3(0, 0, 0), Quaternion.identity);
        finishLine.transform.position = new Vector3(0, nextY, finishLine.GetComponent<MeshRenderer>().bounds.size.z * tiles);
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
