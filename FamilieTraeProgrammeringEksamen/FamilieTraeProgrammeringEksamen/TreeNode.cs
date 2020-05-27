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

        public void inOrder(Node root) {
            if(root != null) {
                inOrder(root.left);
                Console.Write(root.data + " ");
                inOrder(root.right);
            }
        }

        public bool searchingForX;

        public int Find(int value, Node root) {
            if (root == null) {
                return 0;
            }

            if(searchingForX) {
                if (value >= root.data && value <= root.data + 50) { // 50 = Rektanglernes bredde
                    return root.data;
                }
            }
            else {
                if (value >= root.data && value <= root.data + 25) { // 25 = Rektanglernes højde
                    return root.data;
                }
            }

            if(value < root.data) {
                return Find(value, root.left);
            }

            if(value > root.data) {
                return Find(value, root.right);
            }
            
            return 0;
        }
    }
}
