namespace RepetitionDetection
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNumerator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDenumerator = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDetectEqual = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxCharGenerator = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPeriodsCount = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.textBoxRunsCount = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(116, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Repetition-free generator";
            // 
            // textBoxNumerator
            // 
            this.textBoxNumerator.Location = new System.Drawing.Point(6, 31);
            this.textBoxNumerator.Name = "textBoxNumerator";
            this.textBoxNumerator.Size = new System.Drawing.Size(28, 20);
            this.textBoxNumerator.TabIndex = 2;
            this.textBoxNumerator.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "/";
            // 
            // textBoxDenumerator
            // 
            this.textBoxDenumerator.Location = new System.Drawing.Point(58, 31);
            this.textBoxDenumerator.Name = "textBoxDenumerator";
            this.textBoxDenumerator.Size = new System.Drawing.Size(29, 20);
            this.textBoxDenumerator.TabIndex = 4;
            this.textBoxDenumerator.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDetectEqual);
            this.groupBox1.Controls.Add(this.textBoxDenumerator);
            this.groupBox1.Controls.Add(this.textBoxNumerator);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 95);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exponent";
            // 
            // checkBoxDetectEqual
            // 
            this.checkBoxDetectEqual.AutoSize = true;
            this.checkBoxDetectEqual.Checked = true;
            this.checkBoxDetectEqual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDetectEqual.Location = new System.Drawing.Point(7, 58);
            this.checkBoxDetectEqual.Name = "checkBoxDetectEqual";
            this.checkBoxDetectEqual.Size = new System.Drawing.Size(144, 17);
            this.checkBoxDetectEqual.TabIndex = 5;
            this.checkBoxDetectEqual.Text = "detect equal to exponent";
            this.checkBoxDetectEqual.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxRunsCount);
            this.groupBox2.Controls.Add(this.textBoxLength);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxPeriodsCount);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.listBoxCharGenerator);
            this.groupBox2.Location = new System.Drawing.Point(183, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 238);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generator parameters";
            // 
            // listBoxCharGenerator
            // 
            this.listBoxCharGenerator.FormattingEnabled = true;
            this.listBoxCharGenerator.Items.AddRange(new object[] {
            "Random uniform",
            "Random binary",
            "Random uniform, but not last"});
            this.listBoxCharGenerator.Location = new System.Drawing.Point(6, 95);
            this.listBoxCharGenerator.Name = "listBoxCharGenerator";
            this.listBoxCharGenerator.Size = new System.Drawing.Size(147, 43);
            this.listBoxCharGenerator.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Char generator";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Repetition removing strategy";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Remove border",
            "Remove period"});
            this.listBox1.Location = new System.Drawing.Point(6, 166);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(146, 30);
            this.listBox1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Periods count:";
            this.label5.Visible = false;
            // 
            // textBoxPeriodsCount
            // 
            this.textBoxPeriodsCount.Location = new System.Drawing.Point(84, 202);
            this.textBoxPeriodsCount.Name = "textBoxPeriodsCount";
            this.textBoxPeriodsCount.Size = new System.Drawing.Size(25, 20);
            this.textBoxPeriodsCount.TabIndex = 5;
            this.textBoxPeriodsCount.Text = "1";
            this.textBoxPeriodsCount.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(19, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 49);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkedListBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(378, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 238);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(177, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Output path:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Conversion coefficient",
            "Repetition statistics"});
            this.checkedListBox1.Location = new System.Drawing.Point(10, 74);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(184, 34);
            this.checkedListBox1.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Length:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Runs count:";
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(75, 16);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(100, 20);
            this.textBoxLength.TabIndex = 8;
            this.textBoxLength.Text = "1000";
            // 
            // textBoxRunsCount
            // 
            this.textBoxRunsCount.Location = new System.Drawing.Point(75, 37);
            this.textBoxRunsCount.Name = "textBoxRunsCount";
            this.textBoxRunsCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxRunsCount.TabIndex = 9;
            this.textBoxRunsCount.Text = "100";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 313);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "RepetitionDetection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNumerator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDenumerator;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDetectEqual;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxCharGenerator;
        private System.Windows.Forms.TextBox textBoxPeriodsCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBoxRunsCount;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}