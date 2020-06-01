using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieTraeProgrammeringEksamen {
    class PersonInfo {
        int generation;
        int id;
        string name;
        string surname;
        int age;
        string gender;
        string address;
        string city;
        int isMarried = 0; //0 = nej, 1 = ja, 2 = forlovet, 3 = skilt
        int partnerID;
        int kidsNum, kid1ID, kid2ID, kid3ID, kid4ID;
        int fatherID, motherID;
        int yearBorn, yearDeath;
        string dateBorn, dateDeath;

        #region References

        public int Generation {
            get => generation;
            set => generation = value;
        }

        public int ID {
            get => id;
            set => id = value;
        }

        public string Name {
            get => name;
            set => name = value;
        }

        public string Surname {
            get => surname;
            set => surname = value;
        }

        public int Age {
            get => age;
            set => age = value;
        }

        public string Gender {
            get => gender;
            set => gender = value;
        }

        public string Address {
            get => address;
            set => address = value;
        }

        public string City {
            get => city;
            set => city = value;
        }

        public int IsMarried {
            get => isMarried;
            set => isMarried = value;
        }

        public int PartnerID {
            get => partnerID;
            set => partnerID = value;
        }

        public int KidsNum {
            get => kidsNum;
            set => kidsNum = value;
        }

        public int Kid1ID {
            get => kid1ID;
            set => kid1ID = value;
        }

        public int Kid2ID {
            get => kid2ID;
            set => kid2ID = value;
        }

        public int Kid3ID {
            get => kid3ID;
            set => kid3ID = value;
        }

        public int Kid4ID {
            get => kid4ID;
            set => kid4ID = value;
        }

        public int FatherID {
            get => fatherID;
            set => fatherID = value;
        }

        public int MotherID {
            get => motherID;
            set => motherID = value;
        }

        public int YearBorn {
            get => yearBorn;
            set => yearBorn = value;
        }

        public int YearDeath {
            get => yearDeath;
            set => yearDeath = value;
        }

        public string DateBorn {
            get => dateBorn;
            set => dateBorn = value;
        }

        public string DateDeath {
            get => dateDeath;
            set => dateDeath = value;
        }

        #endregion

        

    }
}
