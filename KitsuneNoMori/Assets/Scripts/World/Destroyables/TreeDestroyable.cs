using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.World.Destroyables
{
    public class TreeDestroyable : BaseDestroyable
    {
        public TreeDestroyable()
        {
            base.SetMaxHealth(5);
            base.SetCurrentHealth(5);
        }
    }
}
