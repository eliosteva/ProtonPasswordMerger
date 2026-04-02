using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PasswordMerger
{
	public partial class Form2 : Form
	{

		public class Settings
		{
			public bool addTopLevelDomain;
			public bool matchExact;
			public bool matchWithDiffUrls;
			public bool matchWithNoName;
			public bool recursive;
		}

		public Action<Settings> StartEvent;

		public Form2()
		{
			InitializeComponent();
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			Settings set = new();
			set.addTopLevelDomain = checkBoxAddTopLevelDomain.Checked;
			set.matchExact = checkBoxMatchExact.Checked;
			set.matchWithDiffUrls = checkBoxMatchUrls.Checked;
			set.matchWithNoName = checkBoxNoName.Checked;
			set.recursive = checkBoxRecursive.Checked;

			StartEvent?.Invoke(set);
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
