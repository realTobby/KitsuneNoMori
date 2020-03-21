using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ChunkModel
    {
        // privates stay lower-camelCase!
        public Vector3 Position { get; set; } = new Vector3();
        private List<ChunkObjectModel> chunkObjects = new List<ChunkObjectModel>();
        private bool isUnlocked = false;
        public string ChunkIdentifier = "";

        
        public bool IsUnlocked
        {
            get
            {
                return isUnlocked;
            }
            set
            {
                isUnlocked = value;
            }
        }

        public List<ChunkObjectModel> ChunkObjects
        {
            get
            {
                return chunkObjects;
            }
        }

        public ChunkModel(bool isUnlocked, Vector3 position)
        {
            this.isUnlocked = isUnlocked;
            Position = position;

            int objectCount = UnityEngine.Random.Range(2, 6);


            for(int objectIndex = 0; objectIndex < objectCount; objectIndex++)
            {
                ChunkObjectModel newObject = new ChunkObjectModel();

                int randomObjectType = UnityEngine.Random.Range(1, 7); // exclusive, max is 6

                switch(randomObjectType)
                {
                    default: // no other objects yet :D
                        newObject.ObjectType = ChunkObjectType.TREE;
                        newObject.Position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
                        chunkObjects.Add(newObject);
                        break;
                }

                
            }

            


        }

    }
}
