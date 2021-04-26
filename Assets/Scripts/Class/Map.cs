using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map<T>
{
    public Dictionary<T, float> map;

    public Map(Dictionary<T, float> _map)
    {
        map = _map;
    }

    public List<T> GenerateMap(int amount, bool checkVariation)
    {
        List<T> mapGenerated = new List<T>();

        for(int i = 0; i< amount; i++)
        {
            float maxProbability = 0;
            foreach(var mapItem in map)
            {
                maxProbability += mapItem.Value;
            }
            float randValue = Random.Range(0, maxProbability);
            float startValue = 0;
            for(int j = 0; j<map.Count;j++)
            {
                var item = map.ElementAt(j);
                float maxValue = item.Value + startValue;
                startValue = maxValue;
                if (maxValue > randValue)
                {
                    if(mapGenerated.Count<=0 || !mapGenerated[mapGenerated.Count-1].Equals(item.Key))
                    {
                        mapGenerated.Add(item.Key);
                        map[item.Key] /= 2;
                    }
                    else
                    {
                        i--;
                    }
                    break;
                }
            }
        }

        if(checkVariation)
        {
            var hasVariation = CheckAllVariationContains(map, mapGenerated);
            if(!hasVariation)
            {
                mapGenerated = GenerateMap(amount, checkVariation);
            }
        }

        return mapGenerated;
    }

    public bool CheckAllVariationContains(Dictionary<T, float> map, List<T> generatedMap)
    {
        List<T> tempMap = map.Keys.ToList();
        return tempMap.All(elem => generatedMap.Contains(elem));
    }
}
