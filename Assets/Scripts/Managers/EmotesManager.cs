using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmotesManager : Singleton<EmotesManager>
{
    public GameObject[] EmotePrefabs;
    public GameObject GenerateEmoteOnPosition(EmotesEnum emotesEnum, Vector3 pos)
    {
        var prefab = EmotePrefabs.First(p => p.GetComponent<Emotes>().Enum == emotesEnum);
        return Instantiate(prefab, pos, Quaternion.identity);
    }
}
