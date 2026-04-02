namespace PasswordMerger
{
	partial class Form2
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
			checkBoxMatchExact = new CheckBox();
			checkBoxAddTopLevelDomain = new CheckBox();
			checkBoxMatchUrls = new CheckBox();
			checkBoxNoName = new CheckBox();
			buttonStart = new Button();
			buttonCancel = new Button();
			checkBoxRecursive = new CheckBox();
			SuspendLayout();
			// 
			// checkBoxMatchExact
			// 
			checkBoxMatchExact.AutoSize = true;
			checkBoxMatchExact.Checked = true;
			checkBoxMatchExact.CheckState = CheckState.Checked;
			checkBoxMatchExact.Location = new Point(12, 37);
			checkBoxMatchExact.Name = "checkBoxMatchExact";
			checkBoxMatchExact.Size = new Size(138, 19);
			checkBoxMatchExact.TabIndex = 0;
			checkBoxMatchExact.Text = "Merge exact matches";
			checkBoxMatchExact.UseVisualStyleBackColor = true;
			// 
			// checkBoxAddTopLevelDomain
			// 
			checkBoxAddTopLevelDomain.AutoSize = true;
			checkBoxAddTopLevelDomain.Checked = true;
			checkBoxAddTopLevelDomain.CheckState = CheckState.Checked;
			checkBoxAddTopLevelDomain.Location = new Point(12, 12);
			checkBoxAddTopLevelDomain.Name = "checkBoxAddTopLevelDomain";
			checkBoxAddTopLevelDomain.Size = new Size(176, 19);
			checkBoxAddTopLevelDomain.TabIndex = 1;
			checkBoxAddTopLevelDomain.Text = "Add top level domain to urls";
			checkBoxAddTopLevelDomain.UseVisualStyleBackColor = true;
			// 
			// checkBoxMatchUrls
			// 
			checkBoxMatchUrls.AutoSize = true;
			checkBoxMatchUrls.Checked = true;
			checkBoxMatchUrls.CheckState = CheckState.Checked;
			checkBoxMatchUrls.Location = new Point(12, 62);
			checkBoxMatchUrls.Name = "checkBoxMatchUrls";
			checkBoxMatchUrls.Size = new Size(248, 19);
			checkBoxMatchUrls.TabIndex = 2;
			checkBoxMatchUrls.Text = "Merge matching entries with different urls";
			checkBoxMatchUrls.UseVisualStyleBackColor = true;
			// 
			// checkBoxNoName
			// 
			checkBoxNoName.AutoSize = true;
			checkBoxNoName.Checked = true;
			checkBoxNoName.CheckState = CheckState.Checked;
			checkBoxNoName.Location = new Point(12, 87);
			checkBoxNoName.Name = "checkBoxNoName";
			checkBoxNoName.Size = new Size(400, 19);
			checkBoxNoName.TabIndex = 3;
			checkBoxNoName.Text = "Merge matching entries with no username of email (but same domain)";
			checkBoxNoName.UseVisualStyleBackColor = true;
			// 
			// buttonStart
			// 
			buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			buttonStart.Location = new Point(12, 161);
			buttonStart.Name = "buttonStart";
			buttonStart.Size = new Size(75, 23);
			buttonStart.TabIndex = 4;
			buttonStart.Text = "Start";
			buttonStart.UseVisualStyleBackColor = true;
			buttonStart.Click += buttonStart_Click;
			// 
			// buttonCancel
			// 
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			buttonCancel.Location = new Point(93, 161);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(75, 23);
			buttonCancel.TabIndex = 5;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += buttonCancel_Click;
			// 
			// checkBoxRecursive
			// 
			checkBoxRecursive.AutoSize = true;
			checkBoxRecursive.Checked = true;
			checkBoxRecursive.CheckState = CheckState.Checked;
			checkBoxRecursive.Location = new Point(12, 112);
			checkBoxRecursive.Name = "checkBoxRecursive";
			checkBoxRecursive.Size = new Size(76, 19);
			checkBoxRecursive.TabIndex = 6;
			checkBoxRecursive.Text = "Recursive";
			checkBoxRecursive.UseVisualStyleBackColor = true;
			// 
			// Form2
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(465, 196);
			ControlBox = false;
			Controls.Add(checkBoxRecursive);
			Controls.Add(buttonCancel);
			Controls.Add(buttonStart);
			Controls.Add(checkBoxNoName);
			Controls.Add(checkBoxMatchUrls);
			Controls.Add(checkBoxAddTopLevelDomain);
			Controls.Add(checkBoxMatchExact);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			MinimizeBox = false;
			Name = "Form2";
			Text = "Form2";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox checkBoxMatchExact;
		private CheckBox checkBoxAddTopLevelDomain;
		private CheckBox checkBoxMatchUrls;
		private CheckBox checkBoxNoName;
		private Button buttonStart;
		private Button buttonCancel;
		private CheckBox checkBoxRecursive;
	}
}