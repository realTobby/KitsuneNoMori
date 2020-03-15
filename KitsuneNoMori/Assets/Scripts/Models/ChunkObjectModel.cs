using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{

    public enum ChunkObjectType
    {
        TREE,
        STONE,
        BUSH,
        FLOWER,
        FENCE,
        PATH
    }

    public class ChunkObjectModel
    {
        public Vector3 Position { get; set; } = new Vector3();
        public ChunkObjectType ObjectType { get; set; }
    }
}
