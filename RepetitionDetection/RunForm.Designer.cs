namespace RepetitionDetection
{
    partial class RunForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelExponent = new System.Windows.Forms.Label();
            this.labelAlphabet = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.labelRunsCount = new System.Windows.Forms.Label();
            this.labelCharGenerator = new System.Windows.Forms.Label();
            this.labelStrategy = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRun = new System.Windows.Forms.TextBox();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelStrategy);
            this.groupBox1.Controls.Add(this.labelCharGenerator);
            this.groupBox1.Controls.Add(this.labelRunsCount);
            this.groupBox1.Controls.Add(this.labelLength);
            this.groupBox1.Controls.Add(this.labelAlphabet);
            this.groupBox1.Controls.Add(this.labelExponent);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 189);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // labelExponent
            // 
            this.labelExponent.AutoSize = true;
            this.labelExponent.Location = new System.Drawing.Point(7, 20);
            this.labelExponent.Name = "labelExponent";
            this.labelExponent.Size = new System.Drawing.Size(74, 13);
            this.labelExponent.TabIndex = 0;
            this.labelExponent.Text = "labelExponent";
            // 
            // labelAlphabet
            // 
            this.labelAlphabet.AutoSize = true;
            this.labelAlphabet.Location = new System.Drawing.Point(7, 101);
            this.labelAlphabet.Name = "labelAlphabet";
            this.labelAlphabet.Size = new System.Drawing.Size(71, 13);
            this.labelAlphabet.TabIndex = 2;
            this.labelAlphabet.Text = "labelAlphabet";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(6, 55);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(62, 13);
            this.labelLength.TabIndex = 3;
            this.labelLength.Text = "labelLength";
            // 
            // labelRunsCount
            // 
            this.labelRunsCount.AutoSize = true;
            this.labelRunsCount.Location = new System.Drawing.Point(6, 79);
            this.labelRunsCount.Name = "labelRunsCount";
            this.labelRunsCount.Size = new System.Drawing.Size(82, 13);
            this.labelRunsCount.TabIndex = 4;
            this.labelRunsCount.Text = "labelRunsCount";
            // 
            // labelCharGenerator
            // 
            this.labelCharGenerator.AutoSize = true;
            this.labelCharGenerator.Location = new System.Drawing.Point(6, 138);
            this.labelCharGenerator.Name = "labelCharGenerator";
            this.labelCharGenerator.Size = new System.Drawing.Size(98, 13);
            this.labelCharGenerator.TabIndex = 5;
            this.labelCharGenerator.Text = "labelCharGenerator";
            // 
            // labelStrategy
            // 
            this.labelStrategy.AutoSize = true;
            this.labelStrategy.Location = new System.Drawing.Point(6, 160);
            this.labelStrategy.Name = "labelStrategy";
            this.labelStrategy.Size = new System.Drawing.Size(68, 13);
            this.labelStrategy.TabIndex = 6;
            this.labelStrategy.Text = "labelStrategy";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLength);
            this.groupBox2.Controls.Add(this.textBoxRun);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(308, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 189);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Run:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(63, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 63);
            this.button1.TabIndex = 2;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Text length:";
            // 
            // textBoxRun
            // 
            this.textBoxRun.Location = new System.Drawing.Point(87, 21);
            this.textBoxRun.Name = "textBoxRun";
            this.textBoxRun.Size = new System.Drawing.Size(37, 20);
            this.textBoxRun.TabIndex = 2;
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(248, 21);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(64, 20);
            this.textBoxLength.TabIndex = 3;
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 500);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RunForm";
            this.Text = "RunForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label labelAlphabet;
        private System.Windows.Forms.Label labelExponent;
        private System.Windows.Forms.Label labelRunsCount;
        private System.Windows.Forms.Label labelStrategy;
        private System.Windows.Forms.Label labelCharGenerator;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.TextBox textBoxRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}