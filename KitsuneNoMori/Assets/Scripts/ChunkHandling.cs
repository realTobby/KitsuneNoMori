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
    // Start is called before the first frame update

    // !!!!! Sort unity objects as children under the ChunkHandler Gameobjects for easy refreshing!
    private List<ChunkModel> chunks = new List<ChunkModel>();

    // lets just make a list for now (dirty hack, dictionary for later) ==> just saw: materials and prefabs come with names, 
    // maybe its possible to difference them with that? then a dictionary wont be needed, dev just needs to have a naming pattern thats not retarded :)
    public List<GameObject> prefabs = new List<GameObject>();
    public List<Material> materials = new List<Material>();
    
    void Start()
    {

        // create first chunk
        ChunkModel firstChunk = new ChunkModel(true, new Vector3(0, 0, 0));
        chunks.Add(firstChunk);


        // create the first locked chunks
        ChunkModel lockedOne = new ChunkModel(false, new Vector3(10, 0, 0));
        ChunkModel lockedTwo = new ChunkModel(false, new Vector3(-10, 0, 0));
        ChunkModel lockedThree = new ChunkModel(false, new Vector3(0, 0, 10));
        ChunkModel lockedFour = new ChunkModel(false, new Vector3(0, 0, -10));

        chunks.Add(lockedOne);
        chunks.Add(lockedTwo);
        chunks.Add(lockedThree);
        chunks.Add(lockedFour);

        RefreshDrawables();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void RefreshDrawables()
    {
        // maybe this needs more logic, but is ok for now
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

        // this is more complex, needs to go through every chunk and then create the objects of that chunk

        // Chunk Base => Plane (Primitive)
        // Resources => Prefabs
        // Enemies => Prefabs
        int chunkIndex = 0;
        foreach(ChunkModel nextChunk in chunks)
        {
            #region CreateBasePlane
            GameObject thisChunk = GameObject.CreatePrimitive(PrimitiveType.Plane);
            thisChunk.name = "chunk_" + chunkIndex;
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
            thisChunk.transform.position = nextChunk.Position;
            thisChunk.transform.parent = this.gameObject.transform;
            #endregion

            #region PopulatePlaneWithObjects
            
            if(nextChunk.IsUnlocked == true)
            {
                int objectIndex = 0;
                foreach (ChunkObjectModel nextObject in nextChunk.ChunkObjects)
                {
                    string objectNamePrefix = "object_";
                    // need to think about offsetting the trees, the position inside the tree object is meant as an "on the chunk postion, where the 0,0,0 is the middle of each chunk" => therefore need to consider the chunks world space postion :)

                    switch (nextObject.ObjectType)
                    {
                        case ChunkObjectType.TREE:
                            Vector3 treePos = new Vector3(nextChunk.Position.x + nextObject.Position.x, nextChunk.Position.y + nextObject.Position.y, nextChunk.Position.z + nextObject.Position.z);
                            GameObject newTree = Instantiate(prefabs.Where(x => x.name == "Tree2").FirstOrDefault(), treePos, Quaternion.identity);
                            newTree.transform.parent = thisChunk.transform;
                            newTree.name = objectNamePrefix + "tree_" + objectIndex;
                            break;
                    }
                    objectIndex++;
                }
            }
            
            #endregion


            chunkIndex++; // this is the very end of the creation, before this will come so much more to create the chunks => items, enemies and so on
        }

        

    }

    public void GetChunk(string touchedChunk)
    {
        foreach(ChunkModel chunk in chunks)
        {
            if (chunk.ChunkIdentifier == touchedChunk)
            {
                if(chunk.IsUnlocked == false)
                {
                    chunks.Where(x => x == chunk).FirstOrDefault().IsUnlocked = true;
                    DeleteDrawables();
                    RefreshDrawables();
                }
            }
        }
    }

}
