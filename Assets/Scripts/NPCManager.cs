using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour {

    [SerializeField]
    int m_npcCount = 1;

    [SerializeField]
    GameObject m_npcPrefab;

    List<GameObject> m_npcs = new List<GameObject>();

    bool m_started = false;

    private void Start()
    {
        if (m_npcPrefab != null)
        {
            InitialCreation();
        }
        else
        {
            Debug.Log("No NPC prefab set. No NPCs created.");
        }
    }

    void InitialCreation()
    {
        if (m_started)
            return;

        m_started = true;
        
        for(int i = 0; i < m_npcCount; i++)
        {
            GameObject go = Instantiate(m_npcPrefab, transform);
            go.name = m_npcPrefab.name + m_npcs.Count;
            m_npcs.Add(go);
        }
    }

    public void AddNewNPC()
    {
        if(m_npcPrefab == null)
        {
            Debug.Log("No NPC prefab set. No NPCs created.");
            return;
        }
        GameObject go = Instantiate(m_npcPrefab, transform);
        go.name = m_npcPrefab.name + m_npcs.Count;
        m_npcs.Add(go);
    }
}
