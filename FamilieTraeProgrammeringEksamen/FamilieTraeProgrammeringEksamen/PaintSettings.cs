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
        private int archivedPosX2 = 0;
        private int archivedPosY2 = 0;

        private int kid1X = 0;
        private int kid1Y = 0;

        private int kid2X = 0;
        private int kid2Y = 0;

        private int kid3X = 0;
        private int kid3Y = 0;

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

        public int ArchivedPosX2 {
            get { return archivedPosX2; }
            set { archivedPosX2 = value; }
        }

        public int ArchivedPosY2 {
            get { return archivedPosY2; }
            set { archivedPosY2 = value; }
        }

        public int Kid1X {
            get { return kid1X; }
            set { kid1X = value; }
        }

        public int Kid1Y {
            get { return kid1Y; }
            set { kid1Y = value; }
        }

        public int Kid2X {
            get { return kid2X; }
            set { kid2X = value; }
        }

        public int Kid2Y {
            get { return kid2Y; }
            set { kid2Y = value; }
        }

        public int Kid3X {
            get { return kid3X; }
            set { kid3X = value; }
        }

        public int Kid3Y {
            get { return kid3Y; }
            set { kid3Y = value; }
        }
    }
}
