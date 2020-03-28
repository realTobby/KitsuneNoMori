using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.World.Destroyables
{
    public class StoneDestroyable : BaseDestroyable
    {
        public StoneDestroyable()
        {
            base.SetMaxHealth(10);
            base.SetCurrentHealth(10);
        }
    }
}
