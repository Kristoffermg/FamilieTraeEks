using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieTraeProgrammeringEksamen {
    class PaintSettings {
        private int recPosX = 0;
        private int recPosY = 0;


        public int RecPosX {
            get {
                recPosX += 100;
                return recPosX;
            }

            set {
                
            }
        }

        public int RecPosY {
            get { return recPosY; }
            set {

            }
        }

        
    }
}
