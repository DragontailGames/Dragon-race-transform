using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPreset
{
    public List<List<int>> presetMap = new List<List<int>>()
    {
        new List<int>()
        {
            0,1,0,
        },
        new List<int>()
        {
            0,1,0,1
        },
        new List<int>()
        {
            0,1,2,0
        },
        new List<int>()
        {
            0,1,2,0,1
        },
        new List<int>()
        {
            0,1,3,0,2,
        },
        new List<int>()
        {
            0,1,3,0,2,1
        },
        new List<int>()
        {
            0,2,1,3,0,2,1
        },
        new List<int>()
        {
            0,1,3,4,0,2,1
        },
        new List<int>()
        {
            0,2,4,1,3,4,0,1
        },
        new List<int>()
        {
            2,4,0,3,2,1,4,3
        },
        new List<int>()
        {
            1,3,2,0,1,4,0,2
        },
    };

    public List<int> GetMap(int level)
    {
        return presetMap[level];
    }
}
