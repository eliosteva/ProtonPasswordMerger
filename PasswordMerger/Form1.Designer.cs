namespace PasswordMerger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			buttonSelectFile = new Button();
			treeViewAll = new TreeView();
			buttonDeduplicate = new Button();
			treeViewHosts = new TreeView();
			buttonWrite = new Button();
			label1 = new Label();
			label2 = new Label();
			listBox1 = new ListBox();
			SuspendLayout();
			// 
			// buttonSelectFile
			// 
			buttonSelectFile.Location = new Point(12, 12);
			buttonSelectFile.Name = "buttonSelectFile";
			buttonSelectFile.Size = new Size(90, 23);
			buttonSelectFile.TabIndex = 0;
			buttonSelectFile.Text = "Select file";
			buttonSelectFile.UseVisualStyleBackColor = true;
			buttonSelectFile.Click += buttonSelectFile_Click;
			// 
			// treeViewAll
			// 
			treeViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			treeViewAll.Location = new Point(12, 59);
			treeViewAll.Name = "treeViewAll";
			treeViewAll.Size = new Size(600, 563);
			treeViewAll.TabIndex = 1;
			// 
			// buttonDeduplicate
			// 
			buttonDeduplicate.Location = new Point(108, 12);
			buttonDeduplicate.Name = "buttonDeduplicate";
			buttonDeduplicate.Size = new Size(90, 23);
			buttonDeduplicate.TabIndex = 2;
			buttonDeduplicate.Text = "Merge";
			buttonDeduplicate.UseVisualStyleBackColor = true;
			buttonDeduplicate.Click += buttonMerge_Click;
			// 
			// treeViewHosts
			// 
			treeViewHosts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			treeViewHosts.Location = new Point(618, 59);
			treeViewHosts.Name = "treeViewHosts";
			treeViewHosts.Size = new Size(604, 563);
			treeViewHosts.TabIndex = 5;
			// 
			// buttonWrite
			// 
			buttonWrite.Location = new Point(204, 12);
			buttonWrite.Name = "buttonWrite";
			buttonWrite.Size = new Size(90, 23);
			buttonWrite.TabIndex = 6;
			buttonWrite.Text = "Save";
			buttonWrite.UseVisualStyleBackColor = true;
			buttonWrite.Click += buttonWrite_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 41);
			label1.Name = "label1";
			label1.Size = new Size(56, 15);
			label1.TabIndex = 7;
			label1.Text = "Flat view:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(618, 41);
			label2.Name = "label2";
			label2.Size = new Size(86, 15);
			label2.TabIndex = 8;
			label2.Text = "Sorted by host:";
			// 
			// listBox1
			// 
			listBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			listBox1.FormattingEnabled = true;
			listBox1.Location = new Point(12, 630);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(1210, 169);
			listBox1.TabIndex = 9;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1234, 811);
			Controls.Add(listBox1);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(buttonWrite);
			Controls.Add(treeViewHosts);
			Controls.Add(buttonDeduplicate);
			Controls.Add(treeViewAll);
			Controls.Add(buttonSelectFile);
			MaximizeBox = false;
			MaximumSize = new Size(1250, 850);
			MinimizeBox = false;
			MinimumSize = new Size(1250, 850);
			Name = "Form1";
			SizeGripStyle = SizeGripStyle.Hide;
			Text = "ProtonPasswordMerger";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button buttonSelectFile;
		private TreeView treeViewAll;
		private Button buttonDeduplicate;
		private TreeView treeViewHosts;
		private Button buttonWrite;
		private Label label1;
		private Label label2;
		private ListBox listBox1;
	}
}
