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

        
        public bool IsUnlocked
        {
            get
            {
                return isUnlocked;
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

            int treesCount = UnityEngine.Random.Range(3, 6);


            for(int tree = 0; tree < treesCount; tree++)
            {
                ChunkObjectModel newObject = new ChunkObjectModel();
                newObject.ObjectType = ChunkObjectType.TREE;
                newObject.Position = new Vector3(UnityEngine.Random.Range(-4f, 4f), 0, UnityEngine.Random.Range(-4f, 4f));
                chunkObjects.Add(newObject);
            }

            


        }

        private void Start()
        {

        }

    }
}
