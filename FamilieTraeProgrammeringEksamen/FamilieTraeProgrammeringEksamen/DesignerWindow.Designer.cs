namespace FamilieTraeProgrammeringEksamen {
    partial class DesignerWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.numberRange = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.IDSpecification = new System.Windows.Forms.NumericUpDown();
            this.DrawFamily = new System.Windows.Forms.Button();
            this.graphicsDisplayBox = new System.Windows.Forms.PictureBox();
            this.CreateTree = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IDSpecification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsDisplayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // numberRange
            // 
            this.numberRange.AutoSize = true;
            this.numberRange.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberRange.Location = new System.Drawing.Point(23, 54);
            this.numberRange.Name = "numberRange";
            this.numberRange.Size = new System.Drawing.Size(139, 21);
            this.numberRange.TabIndex = 0;
            this.numberRange.Text = "Person ID (1-1269)";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SaddleBrown;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1087, 46);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(396, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "Family Tree Application";
            // 
            // IDSpecification
            // 
            this.IDSpecification.Location = new System.Drawing.Point(177, 55);
            this.IDSpecification.Name = "IDSpecification";
            this.IDSpecification.Size = new System.Drawing.Size(52, 20);
            this.IDSpecification.TabIndex = 2;
            this.IDSpecification.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DrawFamily
            // 
            this.DrawFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawFamily.Font = new System.Drawing.Font("Sitka Text", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawFamily.Location = new System.Drawing.Point(235, 49);
            this.DrawFamily.Name = "DrawFamily";
            this.DrawFamily.Size = new System.Drawing.Size(112, 29);
            this.DrawFamily.TabIndex = 3;
            this.DrawFamily.Text = "Draw Family";
            this.DrawFamily.UseVisualStyleBackColor = true;
            this.DrawFamily.Click += new System.EventHandler(this.CreateFamily_Click);
            // 
            // graphicsDisplayBox
            // 
            this.graphicsDisplayBox.BackColor = System.Drawing.SystemColors.Control;
            this.graphicsDisplayBox.Location = new System.Drawing.Point(11, 83);
            this.graphicsDisplayBox.Margin = new System.Windows.Forms.Padding(2);
            this.graphicsDisplayBox.Name = "graphicsDisplayBox";
            this.graphicsDisplayBox.Size = new System.Drawing.Size(1066, 222);
            this.graphicsDisplayBox.TabIndex = 5;
            this.graphicsDisplayBox.TabStop = false;
            this.graphicsDisplayBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // CreateTree
            // 
            this.CreateTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateTree.Font = new System.Drawing.Font("Sitka Text", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateTree.Location = new System.Drawing.Point(922, 49);
            this.CreateTree.Name = "CreateTree";
            this.CreateTree.Size = new System.Drawing.Size(155, 29);
            this.CreateTree.TabIndex = 6;
            this.CreateTree.Text = "Create Family Tree";
            this.CreateTree.UseVisualStyleBackColor = true;
            this.CreateTree.Click += new System.EventHandler(this.CreateTree_Click);
            // 
            // DesignerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 310);
            this.Controls.Add(this.CreateTree);
            this.Controls.Add(this.graphicsDisplayBox);
            this.Controls.Add(this.DrawFamily);
            this.Controls.Add(this.IDSpecification);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numberRange);
            this.Name = "DesignerWindow";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.DesignerWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IDSpecification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsDisplayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label numberRange;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown IDSpecification;
        private System.Windows.Forms.Button DrawFamily;
        private System.Windows.Forms.PictureBox graphicsDisplayBox;
        private System.Windows.Forms.Button CreateTree;
    }
}

