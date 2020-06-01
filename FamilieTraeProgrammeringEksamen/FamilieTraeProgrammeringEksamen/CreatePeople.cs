using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieTraeProgrammeringEksamen {
    class CreatePeople {

        #region Random value possibilities for creating a person

        List<string> fNames = new List<string>{"Emma", "Alma", "Freja", "Clara", "Agnes", "Anna", "Sofia", "Ella", "Karla", "Ida",
        "Nora", "Ellie", "Josefine", "Luna", "Olivia", "Laura", "Alberte", "Asta", "Lily", "Isabella", "Molly",
        "Frida", "Thea", "Astrid", "Trine"};

        List<string> mNames = new List<string>{"Willam", "Oscar", "Alfred", "Oliver", "Carl", "Lucas", "Noah", "Valdemar", "Malthe",
        "Aksel", "Elias", "Emil", "Arthur", "August", "Magnus", "Viktor", "Viggo", "Felix", "Anton", "Alexander",
        "Mark", "Tristan", "Adam", "Albert", "Bent"};

        List<string> surnames = new List<string>{"Kjær", "Knudsen", "Iversen", "Jeppesen", "Nygaard", "Olsen", "Damgaard", "Bang",
        "Ravn", "Christiansen", "Jepsen", "Juhl", "Holm", "Jespersen"};

        List<string> address = new List<string>{"Mosegårdsvej", "Lumbyholmvej", "Slipager", "Tværgyden", "Gasværksvej", "Sludevej",
        "Kamperhoug", "Lodskovvej"};

        List<string> city = new List<string>{"København", "Hundested", "Klemensker", "Askeby", "Varde", "Rødby", "Frederiksberg"};

        #endregion

        Random rnd = new Random();
        int ID = 1;
        int maxGen = 8;

        public void CreateFamily() { //TODO: rette
            int gen = 0;
            GenerateFamilyMember(0, gen);
            
        }

        //Køres kun i generation 0
        void GenerateFamilyMember(int index, int gen) {
            PersonInfo pi = new PersonInfo();
            PersonInfo partnerPi = new PersonInfo();

            //Sætter generationen
            pi.Generation = gen;

            //Sætter ID
            pi.ID = this.ID;

            //Skabningen af køn
            if (index == 0) {
                pi.Gender = "m";
            }
            else if(index == 1) {
                pi.Gender = "f";
            }

            //Skabningen af navn
            if (pi.Gender == "m") {
                index = rnd.Next(mNames.Count);
                pi.Name = mNames[index];
            }
            else if (pi.Gender == "f") {
                index = rnd.Next(fNames.Count);
                pi.Name = fNames[index];
            }

            //Skabningen af efternavn
            index = rnd.Next(surnames.Count);
            pi.Surname = surnames[index];

            //Skabningen af alder
            index = rnd.Next(40, 80);
            pi.Age = index;

            //Skabning af fødselsår
            index = rnd.Next(0, 1900);
            pi.YearBorn = index;

            //Skabning af dødsår
            pi.YearDeath = pi.YearBorn + pi.Age;

            //Skabning af fødselsdato
            pi.DateBorn = DateOf();

            //Skabning af dødsdato
            pi.DateDeath = DateOf();

            //Skabningen af addressen
            index = rnd.Next(address.Count);
            pi.Address = address[index] + " " + rnd.Next(1,300);

            //Skabning af antal af børn
            index = rnd.Next(1, 4);
            pi.KidsNum = index;

            //Skabning & tilkobling af partnerPi
            partnerPi = GenerateFamilyMember(pi);
            pi.PartnerID = partnerPi.ID;

            //Skaber barn med forældres informationer
            gen++;
            for (int kid = 1; kid <= pi.KidsNum; kid++) {
                GenerateFamilyMember(pi, partnerPi, gen, kid);
            }

            Person p = new Person();
            p.AddRow(pi);
            p.AddRow(partnerPi);
        }

        //Bruges til skabning af alle generationer efter generation 0
        void GenerateFamilyMember(PersonInfo dadPi, PersonInfo momPi, int gen, int kid) {
            PersonInfo pi = new PersonInfo();
            PersonInfo partnerPi = new PersonInfo();
            int index;
            ID++;

            //Sætter generationen
            pi.Generation = gen;

            //Sætter ID
            pi.ID = ID;

            index = rnd.Next(0, 1);
            //Skabningen af køn
            if (index == 0) {
                pi.Gender = "m";
            }
            else if (index == 1) {
                pi.Gender = "f";
            }

            //Skabningen af navn
            if (pi.Gender == "m") {
                index = rnd.Next(mNames.Count);
                pi.Name = mNames[index];
            }
            else if (pi.Gender == "f") {
                index = rnd.Next(fNames.Count);
                pi.Name = fNames[index];
            }

            //Skabningen af efternavn
            if (pi.IsMarried == 1) {
                index = rnd.Next(surnames.Count);
                pi.Surname = surnames[index];
            }
            else {
                pi.Surname = dadPi.Surname;
            }

            //Skabningen af alder
            //TODO: indsæt kriterier ift. om de er døde, i live, gammel, ung eller barn
            index = rnd.Next(40, 100);
            pi.Age = index;

            //Skabning af fødselsår
            pi.YearBorn = dadPi.YearBorn + rnd.Next(18, 39);

            //Skabning af dødsår
            pi.YearDeath = pi.YearBorn + pi.Age;


            //Skabning af fødselsdato
            pi.DateBorn = DateOf();

            //Skabning af dødsdato
            pi.DateDeath = DateOf();

            //Indsæt ID til forældres PersonInfo
            switch (kid) {
                case 1:
                    dadPi.Kid1ID = pi.ID;
                    momPi.Kid1ID = pi.ID;
                    break;
                case 2:
                    dadPi.Kid2ID = pi.ID;
                    momPi.Kid2ID = pi.ID;
                    break;
                case 3:
                    dadPi.Kid3ID = pi.ID;
                    momPi.Kid3ID = pi.ID;
                    break;
                case 4:
                    dadPi.Kid4ID = pi.ID;
                    momPi.Kid4ID = pi.ID;
                    break;
            }

            //Indsættelse af fatherID og momID
            pi.FatherID = dadPi.ID;
            pi.MotherID = momPi.ID;

            //Skabningen af addressen
            index = rnd.Next(address.Count);
            pi.Address = address[index] + " " + rnd.Next(1, 300);

            //Skabning af antal af børn
            index = rnd.Next(1, 100);
            if (index < 90) {
                index = rnd.Next(1, 4);
                pi.KidsNum = index;
            }
            else {
                pi.KidsNum = 0;
            }

            //Skabning & tilkobling af partnerPi
            partnerPi = GenerateFamilyMember(pi);
            pi.PartnerID = partnerPi.ID;

            //Skaber barn med forældres informationer
            gen++;
            if (gen <= maxGen) {
                for (kid = 1; kid <= pi.KidsNum; kid++) {
                    GenerateFamilyMember(pi, partnerPi, gen, kid);
                }
            }

            //Indskriv informationer til database
            Person p = new Person();
            p.AddRow(pi);
            p.AddRow(partnerPi);

        }

        //Bruges til at skabe partnere så det ikke ender ud med kun generation 0 og 1
        PersonInfo GenerateFamilyMember(PersonInfo partnerPi) {
            PersonInfo pi = new PersonInfo();
            int index;
            ID++;

            //Sætter generationen
            pi.Generation = partnerPi.Generation;

            //Sætter ID
            pi.ID = ID;

            //Skabningen af køn
            if (partnerPi.Gender == "m") {
                pi.Gender = "f";
            }
            else if (partnerPi.Gender == "f") {
                pi.Gender = "m";
            }

            //Skabningen af navn
            if (pi.Gender == "m") {
                index = rnd.Next(mNames.Count);
                pi.Name = mNames[index];
            }
            else if (pi.Gender == "f") {
                index = rnd.Next(fNames.Count);
                pi.Name = fNames[index];
            }

            //Skabningen af efternavn
            pi.Surname = partnerPi.Surname;

            //Skabningen af alder
            index = rnd.Next(-6, 6);
            pi.Age = partnerPi.Age + index;

            //Skabning af fødselsår
            pi.YearBorn = partnerPi.YearBorn + index;

            //Skabning af dødsår
            pi.YearDeath = pi.YearBorn + pi.Age;

            //Skabning af fødselsdato
            pi.DateBorn = DateOf();

            //Skabning af dødsdato
            pi.DateDeath = DateOf();

            //Skabningen af addressen
            pi.Address = partnerPi.Address;

            //Skabning af antal af børn
            pi.KidsNum = partnerPi.KidsNum;

            //Tilkobling af partners ID
            pi.PartnerID = partnerPi.ID;

            return pi;
        }

        //Skabning af datoer
        int[] monthLength = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        string DateOf() {
            Random rnd = new Random();
            int dd, mm;
            mm = rnd.Next(1, 12);
            dd = rnd.Next(1, monthLength[mm]);
            return dd + "-" + mm;
        }
        

        bool FamilyMemberIsMarried() {
            return true;
        }
    }
}
