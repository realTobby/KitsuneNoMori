using Assets.Scripts;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum PrefabKeys
{
    TREE_PREFAB,
    ENEMY_PREFAB
}

public enum MaterialKeys
{
    TREE_MATERIAL,
    ENEMY_MATERIAL,
    PLANE_MATERIAL
}

public class ChunkHandling : MonoBehaviour
{
    private const int CHUNK_LENGTH = 10;

    private DungeonGenerator dg;


    // !!!!! Sort unity objects as children under the ChunkHandler Gameobjects for easy refreshing! ==> oof
    private List<ChunkModel> chunks = new List<ChunkModel>();

    // lets just make a list for now (dirty hack, dictionary for later) ==> just saw: materials and prefabs come with names, 
    // maybe its possible to difference them with that? then a dictionary wont be needed, dev just needs to have a naming pattern thats not retarded :)
    public List<GameObject> prefabs = new List<GameObject>();
    public List<Material> materials = new List<Material>();
    
    void Start()
    {

        // generate chunks
        // create the data
        // create the objects ==> (objects in the scene)
        // hide everything
        // only show the stuff that supposed to be seen 

        // create first chunk
        ChunkModel firstChunk = new ChunkModel(true, new Vector3(0, 0, 0));
        chunks.Add(firstChunk);

        //BuildChunksArround(firstChunk);

        dg = new DungeonGenerator(firstChunk);
        chunks = dg.GetDungeon();

        RefreshDrawables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region ChunkUpdate
    private void RefreshDrawables(ChunkModel targetedChunk)
    {
        DeleteDrawables(targetedChunk);
        CreateDrawables(targetedChunk);
    }

    private void DeleteDrawables(ChunkModel targetedChunk)
    {
        int childCount = gameObject.transform.childCount;
        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            if(this.gameObject.transform.GetChild(childIndex).name == targetedChunk.ChunkIdentifier)
            {
               // Debug.Log("Deleted the chunk in question!");
                Destroy(this.gameObject.transform.GetChild(childIndex).gameObject);
            }
        }
    }

    private void CreateDrawables(ChunkModel targetedChunk, int index)
    {
        targetedChunk.ChunkIdentifier = "chunk_" + index;
        CreateDrawables(targetedChunk);
    }

    private void CreateDrawables(ChunkModel targetedChunk)
    {
        //Debug.Log("When you see this, I will be updating the chunk in question!");
        GameObject thisChunk = GameObject.CreatePrimitive(PrimitiveType.Plane);
        thisChunk.name = targetedChunk.ChunkIdentifier;
        thisChunk.tag = "chunk";
        Material groundMat;

        if (targetedChunk.IsUnlocked == true)
        {
            groundMat = materials.Where(x => x.name == "grassyGroundMaterial").FirstOrDefault();
        }
        else
        {
            groundMat = materials.Where(x => x.name == "lockedGroundMaterial").FirstOrDefault();
        }

        thisChunk.GetComponent<MeshRenderer>().material = groundMat;
        thisChunk.transform.position = new Vector3(targetedChunk.Position.x * CHUNK_LENGTH, targetedChunk.Position.y, targetedChunk.Position.z * CHUNK_LENGTH);
        thisChunk.transform.parent = this.gameObject.transform;

        if (targetedChunk.IsUnlocked == true)
        {
            int objectIndex = 0;
            foreach (ChunkObjectModel nextObject in targetedChunk.ChunkObjects)
            {
                Vector3 objectPosition = new Vector3(targetedChunk.Position.x * CHUNK_LENGTH + nextObject.Position.x, targetedChunk.Position.y, targetedChunk.Position.z * CHUNK_LENGTH + nextObject.Position.z);
                string objectNamePrefix = "object_";
                switch (nextObject.ObjectType)
                {
                    case ChunkObjectType.TREE:
                        GameObject newTree = Instantiate(prefabs.Where(x => x.name == "Tree2").FirstOrDefault(), objectPosition, Quaternion.identity);
                        newTree.transform.parent = thisChunk.transform;
                        newTree.name = objectNamePrefix + "tree_" + objectIndex;
                        break;
                }
                objectIndex++;
            }
        }
    }
    #endregion

    #region ChunkCreate
    private void RefreshDrawables()
    {
        DeleteDrawables();
        CreateDrawables();
    }

    private void DeleteDrawables()
    {
        int childCount = gameObject.transform.childCount;
        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            Destroy(this.gameObject.transform.GetChild(childIndex).gameObject);
        }
    }

    private void CreateDrawables()
    {
        int chunkCount = 0;
        foreach (ChunkModel nextChunk in chunks)
        {
            CreateDrawables(nextChunk, chunkCount);
            chunkCount++;
        }
    }
    #endregion

    #region StuffOtherClassesNeedToCall
    public void GetChunk(string touchedChunk)
    {
        foreach (ChunkModel chunk in chunks)
        {
            if (chunk.ChunkIdentifier == touchedChunk)
            {
                if (chunk.IsUnlocked == false)
                {
                    //Debug.Log("A chunk needs an update :O");
                    chunks.Where(x => x == chunk).FirstOrDefault().IsUnlocked = true;
                    RefreshDrawables(chunk);
                    break;
                }
            }
        }
    }
    #endregion


}
