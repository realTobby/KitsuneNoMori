using Assets.Scripts.Interfaces;
using Assets.Scripts.Models;
using Assets.Scripts.World.Destroyables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.World.Destroyables
{
    public class DestroyableBehaviour : MonoBehaviour
    {
        public GameObject dropObjectPrefab;
        private ChunkObjectModel myData;
        public IDestroyable destroyableLogic;

        public void Start()
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            
        }

        void Update()
        {
            if(destroyableLogic != null)
            {
                if (destroyableLogic.DropLoot() == true)
                {
                    Instantiate(dropObjectPrefab, this.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }

        public void SetObjectData(ChunkObjectModel model)
        {
            myData = model;
            switch (myData.ObjectType)
            {
                case ChunkObjectType.TREE:
                    destroyableLogic = new TreeDestroyable();
                    this.transform.localScale = new Vector3(75, Random.Range(75, 150), 57);
                    break;
                case ChunkObjectType.STONE:
                    destroyableLogic = new StoneDestroyable();
                    this.transform.localScale = new Vector3(1, Random.Range(0.5f, 2f), 1);
                    break;
            }
        }
    }
}