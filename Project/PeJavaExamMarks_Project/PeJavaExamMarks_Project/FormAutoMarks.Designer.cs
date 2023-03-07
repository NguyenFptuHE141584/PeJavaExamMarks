
namespace PeJavaExamMarksProject
{
    partial class FormAutoMarks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoMarks));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txTestCase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txClass = new System.Windows.Forms.TextBox();
            this.btGrading = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder Test Case:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folder Class:";
            // 
            // txTestCase
            // 
            this.txTestCase.Location = new System.Drawing.Point(130, 80);
            this.txTestCase.Name = "txTestCase";
            this.txTestCase.Size = new System.Drawing.Size(181, 23);
            this.txTestCase.TabIndex = 2;
            this.txTestCase.DoubleClick += new System.EventHandler(this.txTestCase_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(25, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Auto Marks";
            // 
            // txClass
            // 
            this.txClass.Location = new System.Drawing.Point(130, 134);
            this.txClass.Name = "txClass";
            this.txClass.Size = new System.Drawing.Size(181, 23);
            this.txClass.TabIndex = 4;
            this.txClass.DoubleClick += new System.EventHandler(this.txClass_DoubleClick);
            // 
            // btGrading
            // 
            this.btGrading.Location = new System.Drawing.Point(92, 187);
            this.btGrading.Name = "btGrading";
            this.btGrading.Size = new System.Drawing.Size(137, 23);
            this.btGrading.TabIndex = 5;
            this.btGrading.Text = "Click Grading Exam";
            this.btGrading.UseVisualStyleBackColor = true;
            this.btGrading.Click += new System.EventHandler(this.btGrading_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(204, 36);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(107, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // FormAutoMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 222);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btGrading);
            this.Controls.Add(this.txClass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txTestCase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAutoMarks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Marks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txTestCase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txClass;
        private System.Windows.Forms.Button btGrading;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}