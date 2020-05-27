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
            this.numberRange = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numberOfParentGenerations = new System.Windows.Forms.NumericUpDown();
            this.CreateFamily = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfParentGenerations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // numberRange
            // 
            this.numberRange.AutoSize = true;
            this.numberRange.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberRange.Location = new System.Drawing.Point(44, 53);
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
            this.panel1.Size = new System.Drawing.Size(925, 46);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(285, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "Family Tree Application";
            // 
            // numberOfParentGenerations
            // 
            this.numberOfParentGenerations.Location = new System.Drawing.Point(189, 55);
            this.numberOfParentGenerations.Name = "numberOfParentGenerations";
            this.numberOfParentGenerations.Size = new System.Drawing.Size(120, 20);
            this.numberOfParentGenerations.TabIndex = 2;
            this.numberOfParentGenerations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CreateFamily
            // 
            this.CreateFamily.Location = new System.Drawing.Point(327, 51);
            this.CreateFamily.Name = "CreateFamily";
            this.CreateFamily.Size = new System.Drawing.Size(75, 23);
            this.CreateFamily.TabIndex = 3;
            this.CreateFamily.Text = "Create Family";
            this.CreateFamily.UseVisualStyleBackColor = true;
            this.CreateFamily.Click += new System.EventHandler(this.CreateFamily_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(11, 80);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(914, 222);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 310);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CreateFamily);
            this.Controls.Add(this.numberOfParentGenerations);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numberRange);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfParentGenerations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label numberRange;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numberOfParentGenerations;
        private System.Windows.Forms.Button CreateFamily;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

