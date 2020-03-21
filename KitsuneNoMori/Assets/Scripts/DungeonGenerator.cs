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

        public DungeonGenerator(ChunkModel firstChunkOfTheDungeon)
        {
            dungeon.Add(firstChunkOfTheDungeon);

            DungeonPointer generatorPointer = DungeonPointer.Left;
            int rooms = 15;
            Vector3 pointerPosition = firstChunkOfTheDungeon.Position;
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

        public List<ChunkModel> GetDungeon()
        {
            return dungeon;
        }


    }
}
