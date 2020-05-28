using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilieTraeProgrammeringEksamen {
    public class TreeNode {
        public Node root;

        public class Node {
            public int data;
            public Node left, right;
            public Node(int data) {
                this.data = data;
                this.left = null;
                this.right = null;
            }
        }

        public Node ArrToBST(int[] arr, int start, int end) {
            if(start > end) {
                return null;
            }

            // Tager midten af træet og lavet det til root
            int mid = (start + end) / 2;
            Node root = new Node(arr[mid]);

            root.left = ArrToBST(arr, start, mid - 1);

            root.right = ArrToBST(arr, mid + 1, end);

            return root;
        }

        public void PrintTree(Node root) {
            if(root != null) {
                PrintTree(root.left);
                Console.Write($"{root.data} ");
                PrintTree(root.right);
            }
        }

        public bool searchingForX;

        public int Find(int value, Node root) {
            if (root == null) return 0;

            if (searchingForX) {
                // Der tjekkes, om value (musseklikkets X-værdi) er indenfor den bestemte range, som en rektangel har
                if (value >= root.data && value <= root.data + 50) { // 50 = Rektanglernes bredde
                    if (PositionMatchesWithPerson(root.data)) {
                        return root.data;
                    }
                    int rightDepth = MaxDepth(root, 0);
                    for(int i = 0; i < rightDepth; i++) {
                        return Find(value, root.right);
                    }
                    int leftDepth = MaxDepth(root, 1);
                    for (int i = 0; i < leftDepth; i++) {
                        return Find(value, root.left);
                    }
                }
            }
            else {
                // Herunder tjekkes der i stedet om musseklikkets Y-værdi er indenfor Y-værdien af rektanglens range
                if (value >= root.data && value <= root.data + 25) { // 25 = Rektanglernes højde
                    return root.data;
                }
            }

            // Hvis value er lavere end root skal man gå til venstre i BST
            if(value < root.data) {
                return Find(value, root.left);
            }

            // Hvis value er højere end root skal man gå til højre i BST
            if (value > root.data) {
                return Find(value, root.right);
            }
            
            return 0;
        }

        public int MaxDepth(Node root, int side) {
            if (root == null) return 0;
            if (side == 0) {
                return MaxDepth(root.right, 0) + 1;
            }
            else {
                return MaxDepth(root.left, 1) + 1;
            }
        }

        bool PositionMatchesWithPerson(int Xvalue) {
            // Try catch bliver her brugt til at tjekke, om der sker en exception ved defineringen af tryFetchingPersonID. Hvis databasen ikke kan finde noget, returneres intet, hvilket ville skabe
            // en exception. 
            try {
                int tryFetchingPersonID = Convert.ToInt32(CommandReadQuery($"select ID from CurrentIDPos where PosX = {Xvalue} and PosY = {Form1.Yvalue}"));
                return true;
            }
            catch {
                return false;
            } 
        }

        MySqlConnection sqlCon = new MySqlConnection("Data Source=195.249.237.86,3306;Initial Catalog=FamilieTræ;Persist Security Info=true;User ID=Kristoffer;password=12345678;");
        MySqlCommand sqlCmd;
        public string CommandReadQuery(string query) {
            sqlCon.Open();
            sqlCmd = new MySqlCommand(query, sqlCon);
            MySqlDataReader da = sqlCmd.ExecuteReader();
            while (da.Read()) {
                string result = da.GetValue(0).ToString();
                sqlCon.Close();
                return result;
            }
            sqlCon.Close();
            return "Database is empty";
        }
    }
}
