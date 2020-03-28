using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal enum DungeonPointer
    {
        Left,
        Right,
        Up,
        Down
    }

    public class DungeonGenerator
    {

        private List<ChunkModel> dungeon = new List<ChunkModel>();

        private bool CheckNextPosition(Vector3 pos)
        {
            if(dungeon.Where(x=>x.Position == pos).Any() == false)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Constructor of the DungeonGenerator, will create a new dungeon each time a new instance of this class is created 
        /// </summary>
        public DungeonGenerator()
        {
            DungeonPointer generatorPointer = DungeonPointer.Left;
            int rooms = 15;
            Vector3 pointerPosition = new Vector3(0,0,0);

            dungeon.Add(new ChunkModel(true, pointerPosition));

            for (int roomIndex = 0; roomIndex < rooms; roomIndex++)
            {
                switch (generatorPointer)
                {
                    case DungeonPointer.Left:
                        pointerPosition = new Vector3(pointerPosition.x + 1, pointerPosition.y, pointerPosition.z);
                        break;
                    case DungeonPointer.Right:
                        pointerPosition = new Vector3(pointerPosition.x - 1, pointerPosition.y, pointerPosition.z);
                        break;
                    case DungeonPointer.Up:
                        pointerPosition = new Vector3(pointerPosition.x, pointerPosition.y, pointerPosition.z + 1);
                        break;
                    case DungeonPointer.Down:
                        pointerPosition = new Vector3(pointerPosition.x, pointerPosition.y, pointerPosition.z - 1);
                        break;
                }

                if (CheckNextPosition(pointerPosition) == true)
                {
                    ChunkModel nextChunk = new ChunkModel(false, pointerPosition);
                    if(roomIndex >= rooms-1)
                    {
                        nextChunk.isEndChunk = true;
                    }
                    dungeon.Add(nextChunk);
                }
                else
                {
                    roomIndex--;
                }

                int nextDirection = UnityEngine.Random.Range(1, 5); // exclusive, means max of 4
                switch(nextDirection)
                {
                    case 1:
                        generatorPointer = DungeonPointer.Left;
                        break;
                    case 2:
                        generatorPointer = DungeonPointer.Right;
                        break;
                    case 3:
                        generatorPointer = DungeonPointer.Up;
                        break;
                    case 4:
                        generatorPointer = DungeonPointer.Down;
                        break;
                }
            }
        }

        /// <summary>
        /// Retrieves the created dungeon inside the active instance
        /// </summary>
        /// <returns></returns>
        public List<ChunkModel> GetDungeon()
        {
            return dungeon;
        }
    }
}
