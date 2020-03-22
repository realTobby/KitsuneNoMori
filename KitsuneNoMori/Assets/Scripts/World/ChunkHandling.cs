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


    private void RefreshDrawables()
    {
        DeleteDrawables();
        CreateDrawables();
    }

    private void DeleteDrawables()
    {
        int childCount = gameObject.transform.childCount;
        for(int childIndex = 0; childIndex < childCount; childIndex++)
        {
            Destroy(this.gameObject.transform.GetChild(childIndex).gameObject);
        }
    }

    private void CreateDrawables()
    {
        int chunkIndex = 0;
        foreach(ChunkModel nextChunk in chunks)
        {
            #region CreateBasePlane aka CHUNK
            GameObject thisChunk = GameObject.CreatePrimitive(PrimitiveType.Plane);
            thisChunk.name = "chunk_" + chunkIndex;
            thisChunk.tag = "chunk";
            chunks.Where(x=>x == nextChunk).FirstOrDefault().ChunkIdentifier = "chunk_" + chunkIndex;
            Material groundMat;

            if(nextChunk.IsUnlocked == true)
            {
                groundMat = materials.Where(x => x.name == "grassyGroundMaterial").FirstOrDefault();
            }
            else
            {
                groundMat = materials.Where(x => x.name == "lockedGroundMaterial").FirstOrDefault();
            }

            thisChunk.GetComponent<MeshRenderer>().material = groundMat;
            thisChunk.transform.position = new Vector3(nextChunk.Position.x * CHUNK_LENGTH, nextChunk.Position.y, nextChunk.Position.z * CHUNK_LENGTH);
            thisChunk.transform.parent = this.gameObject.transform;
            #endregion

            #region PopulatePlaneWithObjects
            if(nextChunk.IsUnlocked == true)
            {
                int objectIndex = 0;
                foreach (ChunkObjectModel nextObject in nextChunk.ChunkObjects)
                {
                    Vector3 objectPosition = new Vector3(nextChunk.Position.x * CHUNK_LENGTH + nextObject.Position.x, nextChunk.Position.y, nextChunk.Position.z * CHUNK_LENGTH + nextObject.Position.z );
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
            
            #endregion

            chunkIndex++;
        }
    }

    public void GetChunk(string touchedChunk)
    {
        DeleteDrawables();
        foreach(ChunkModel chunk in chunks)
        {
            if (chunk.ChunkIdentifier == touchedChunk)
            {
                if(chunk.IsUnlocked == false)
                {
                    chunks.Where(x => x == chunk).FirstOrDefault().IsUnlocked = true;
                    // its fine now, need tweaking later when the object numbers go up, because its still deleting and recreating everything when a new chunk is loaded :/
                    break;
                }
            }
        }
        RefreshDrawables();
    }
}
