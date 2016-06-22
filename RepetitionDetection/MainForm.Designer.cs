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
            this.textBoxDenominator = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDetectEqual = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxAlphabetSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxRunsCount = new System.Windows.Forms.TextBox();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPeriodsCount = new System.Windows.Forms.TextBox();
            this.labelPeriodsCount = new System.Windows.Forms.Label();
            this.listBoxStrategy = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxCharGenerator = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            // textBoxDenominator
            // 
            this.textBoxDenominator.Location = new System.Drawing.Point(58, 31);
            this.textBoxDenominator.Name = "textBoxDenominator";
            this.textBoxDenominator.Size = new System.Drawing.Size(29, 20);
            this.textBoxDenominator.TabIndex = 4;
            this.textBoxDenominator.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDetectEqual);
            this.groupBox1.Controls.Add(this.textBoxDenominator);
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
            this.groupBox2.Controls.Add(this.textBoxAlphabetSize);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxRunsCount);
            this.groupBox2.Controls.Add(this.textBoxLength);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxPeriodsCount);
            this.groupBox2.Controls.Add(this.labelPeriodsCount);
            this.groupBox2.Controls.Add(this.listBoxStrategy);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.listBoxCharGenerator);
            this.groupBox2.Location = new System.Drawing.Point(183, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 254);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generator parameters";
            // 
            // textBoxAlphabetSize
            // 
            this.textBoxAlphabetSize.Location = new System.Drawing.Point(125, 59);
            this.textBoxAlphabetSize.Name = "textBoxAlphabetSize";
            this.textBoxAlphabetSize.Size = new System.Drawing.Size(50, 20);
            this.textBoxAlphabetSize.TabIndex = 11;
            this.textBoxAlphabetSize.Text = "3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Alphabet size:";
            // 
            // textBoxRunsCount
            // 
            this.textBoxRunsCount.Location = new System.Drawing.Point(125, 38);
            this.textBoxRunsCount.Name = "textBoxRunsCount";
            this.textBoxRunsCount.Size = new System.Drawing.Size(50, 20);
            this.textBoxRunsCount.TabIndex = 9;
            this.textBoxRunsCount.Text = "100";
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(100, 16);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(75, 20);
            this.textBoxLength.TabIndex = 8;
            this.textBoxLength.Text = "1000";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Length:";
            // 
            // textBoxPeriodsCount
            // 
            this.textBoxPeriodsCount.Location = new System.Drawing.Point(90, 215);
            this.textBoxPeriodsCount.Name = "textBoxPeriodsCount";
            this.textBoxPeriodsCount.Size = new System.Drawing.Size(25, 20);
            this.textBoxPeriodsCount.TabIndex = 5;
            this.textBoxPeriodsCount.Text = "1";
            this.textBoxPeriodsCount.Visible = false;
            // 
            // labelPeriodsCount
            // 
            this.labelPeriodsCount.AutoSize = true;
            this.labelPeriodsCount.Location = new System.Drawing.Point(9, 218);
            this.labelPeriodsCount.Name = "labelPeriodsCount";
            this.labelPeriodsCount.Size = new System.Drawing.Size(75, 13);
            this.labelPeriodsCount.TabIndex = 4;
            this.labelPeriodsCount.Text = "Periods count:";
            this.labelPeriodsCount.Visible = false;
            // 
            // listBoxStrategy
            // 
            this.listBoxStrategy.FormattingEnabled = true;
            this.listBoxStrategy.Items.AddRange(new object[] {
            "Remove border",
            "Remove period"});
            this.listBoxStrategy.Location = new System.Drawing.Point(12, 179);
            this.listBoxStrategy.Name = "listBoxStrategy";
            this.listBoxStrategy.Size = new System.Drawing.Size(146, 30);
            this.listBoxStrategy.TabIndex = 3;
            this.listBoxStrategy.SelectedIndexChanged += new System.EventHandler(this.listBoxStrategy_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Repetition removing strategy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Char generator";
            // 
            // listBoxCharGenerator
            // 
            this.listBoxCharGenerator.FormattingEnabled = true;
            this.listBoxCharGenerator.Items.AddRange(new object[] {
            "Random uniform",
            "Random binary",
            "Random uniform, but not last"});
            this.listBoxCharGenerator.Location = new System.Drawing.Point(12, 108);
            this.listBoxCharGenerator.Name = "listBoxCharGenerator";
            this.listBoxCharGenerator.Size = new System.Drawing.Size(147, 43);
            this.listBoxCharGenerator.TabIndex = 0;
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
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.checkedListBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxOutput);
            this.groupBox3.Location = new System.Drawing.Point(378, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 164);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 94);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Save";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Change";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Conversion coefficient",
            "Repetition statistics"});
            this.checkedListBox1.Location = new System.Drawing.Point(10, 117);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(184, 34);
            this.checkedListBox1.TabIndex = 2;
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
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(10, 38);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(177, 20);
            this.textBoxOutput.TabIndex = 0;
            this.textBoxOutput.Text = "C:\\output.txt";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxMessages);
            this.groupBox4.Location = new System.Drawing.Point(378, 223);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 80);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Messages";
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.ForeColor = System.Drawing.Color.Red;
            this.textBoxMessages.Location = new System.Drawing.Point(10, 20);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.Size = new System.Drawing.Size(184, 20);
            this.textBoxMessages.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 364);
            this.Controls.Add(this.groupBox4);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNumerator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDenominator;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDetectEqual;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxCharGenerator;
        private System.Windows.Forms.TextBox textBoxPeriodsCount;
        private System.Windows.Forms.Label labelPeriodsCount;
        private System.Windows.Forms.ListBox listBoxStrategy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBoxRunsCount;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.TextBox textBoxAlphabetSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}