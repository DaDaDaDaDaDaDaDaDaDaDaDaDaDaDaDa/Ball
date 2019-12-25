using Assets.Scripts.MISC;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    public GameObject[] VFXs;
    
    public void GenerateVFXOnPosition(VFX vfxType, Vector3 pos, float destroyTime)
    {
        var vfx = Instantiate(VFXs.First(v => v.GetComponent<VFXObject>().VFX == vfxType), pos, Quaternion.identity);
        Destroy(vfx, destroyTime);
    }
    public void GenerateVFXOnPosition(VFX vfxType, Vector3 pos, Vector3 rot, float destroyTime)
    {
        var vfx = Instantiate(VFXs.First(v => v.GetComponent<VFXObject>().VFX == vfxType), pos, Quaternion.Euler(rot));
        Destroy(vfx, destroyTime);
    }
}
