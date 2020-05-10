using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieTraeProgrammeringEksamen {
    class PaintSettings {
        private int currentPosX = 0;
        private int currentPosY = 0;


        public int CurrentPosX {
            get { return currentPosX; }
            set { currentPosX = value; }
        }

        public int CurrentPosY {
            get { return currentPosY; }
            set { currentPosY = value; }
        }

        
    }
}
