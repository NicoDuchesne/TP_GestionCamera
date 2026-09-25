using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    private List<AViewVolume> activeViewVolumes = new List<AViewVolume>();
    private Dictionary<AView, List<AViewVolume>> volumesPerViews = new Dictionary<AView, List<AViewVolume>>();
    public static ViewVolumeBlender Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        foreach (AView view in volumesPerViews.Keys)
        {
            view.weight = 0f;
        }
        
        activeViewVolumes.Sort((a, b) =>
        {
            if (a.priority == b.priority) return b.Uid.CompareTo(a.Uid);
            return b.priority.CompareTo(a.priority);
        });

        foreach (AViewVolume v in activeViewVolumes)
        {
            float weight = Mathf.Clamp(v.ComputeSelfWeight(), 0, 1) ;
            float reaminingWeight = 1f - weight;

            foreach (AView view in volumesPerViews.Keys)
            {
                view.weight *= reaminingWeight;
            }
            
            v.view.weight += weight;
        }
    }

    public void AddVolume(AViewVolume viewVolume)
    {
        activeViewVolumes.Add(viewVolume);
        AView view = viewVolume.view;

        if (!volumesPerViews.ContainsKey(view))
        {
            volumesPerViews.Add(view, new List<AViewVolume>());
            view.SetActive(true);
            Update();
        }
        
        volumesPerViews[view].Add(viewVolume);
    }
    
    public void RemoveVolume(AViewVolume viewVolume)
    {
        activeViewVolumes.Remove(viewVolume);
        AView view = viewVolume.view;

        if (volumesPerViews.ContainsKey(view))
        {
            volumesPerViews[view].Remove(viewVolume);
            if (volumesPerViews[view].Count == 0)
            {
                volumesPerViews.Remove(view);
                view.SetActive(false);
                Update();
            }
        }
        
    }

    public void OnGUI()
    {
        foreach (AViewVolume vol in activeViewVolumes)
        {
            string s = vol.name + "; ";
            GUILayout.Label(s);
        }
    }
}
