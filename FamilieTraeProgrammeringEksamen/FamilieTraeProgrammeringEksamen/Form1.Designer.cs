namespace FamilieTraeProgrammeringEksamen {
    partial class Form1 {
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numberOfParentGenerations = new System.Windows.Forms.NumericUpDown();
            this.CreateFamily = new System.Windows.Forms.Button();
            this.clearDatabase = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfParentGenerations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent Generations (1-5)";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SaddleBrown;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1198, 71);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(357, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 57);
            this.label2.TabIndex = 2;
            this.label2.Text = "Family Tree Application";
            // 
            // numberOfParentGenerations
            // 
            this.numberOfParentGenerations.Location = new System.Drawing.Point(60, 125);
            this.numberOfParentGenerations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numberOfParentGenerations.Name = "numberOfParentGenerations";
            this.numberOfParentGenerations.Size = new System.Drawing.Size(180, 26);
            this.numberOfParentGenerations.TabIndex = 2;
            this.numberOfParentGenerations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CreateFamily
            // 
            this.CreateFamily.Location = new System.Drawing.Point(90, 166);
            this.CreateFamily.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CreateFamily.Name = "CreateFamily";
            this.CreateFamily.Size = new System.Drawing.Size(112, 35);
            this.CreateFamily.TabIndex = 3;
            this.CreateFamily.Text = "Create Family";
            this.CreateFamily.UseVisualStyleBackColor = true;
            this.CreateFamily.Click += new System.EventHandler(this.CreateFamily_Click);
            // 
            // clearDatabase
            // 
            this.clearDatabase.Location = new System.Drawing.Point(20, 257);
            this.clearDatabase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clearDatabase.Name = "clearDatabase";
            this.clearDatabase.Size = new System.Drawing.Size(112, 35);
            this.clearDatabase.TabIndex = 4;
            this.clearDatabase.Text = "Clear data";
            this.clearDatabase.UseVisualStyleBackColor = true;
            this.clearDatabase.Click += new System.EventHandler(this.clearDatabase_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(504, 107);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(669, 536);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.clearDatabase);
            this.Controls.Add(this.CreateFamily);
            this.Controls.Add(this.numberOfParentGenerations);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfParentGenerations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numberOfParentGenerations;
        private System.Windows.Forms.Button CreateFamily;
        private System.Windows.Forms.Button clearDatabase;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

