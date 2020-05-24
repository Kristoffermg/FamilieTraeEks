using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieTraeProgrammeringEksamen {
    class PaintSettings {
        private int currentPosX = 0;
        private int currentPosY = 0;
        private int archivedPosX = 0;
        private int archivedPosY = 0;

        public int CurrentPosX {
            get { return currentPosX; }
            set { currentPosX = value; }
        }

        public int CurrentPosY {
            get { return currentPosY; }
            set { currentPosY = value; }
        }

        public int ArchivedPosX {
            get { return archivedPosX; }
            set { archivedPosX = value; }
        }

        public int ArchivedPosY {
            get { return archivedPosY; }
            set { archivedPosY = value; }
        }

    }
}
