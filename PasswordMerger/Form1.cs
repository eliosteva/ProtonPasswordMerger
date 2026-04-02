using SimpleJSON;

namespace PasswordMerger
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();
		}

		private void buttonSelectFile_Click(object sender, EventArgs e)
		{
			var filePath = string.Empty;
			var fileContent = string.Empty;

			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				openFileDialog.Filter = "JSON files (*.json)|*.json";
				openFileDialog.FilterIndex = 2;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					//Get the path of specified file
					filePath = openFileDialog.FileName;

					Log("------------------------------");
					Log($"reading file: {filePath}");

					//Read the contents of the file into a stream
					var fileStream = openFileDialog.OpenFile();

					using (StreamReader reader = new StreamReader(fileStream))
					{
						fileContent = reader.ReadToEnd();
					}
				}
			}

			ParseFile(fileContent);
			Visualize();
		}

		private void buttonMerge_Click(object sender, EventArgs e)
		{
			Form2 frm2 = new Form2();
			frm2.StartEvent += OnStart;
			frm2.ShowDialog();
		}

		private void buttonWrite_Click(object sender, EventArgs e)
		{
			// ask to save file
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "JSON files (*.json)|*.json";
			saveFileDialog1.FilterIndex = 2;
			saveFileDialog1.RestoreDirectory = true;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog1.FileName;
				File.WriteAllText(fileName, json.ToString());
			}
		}

		public class Vault
		{
			public string name;
			public JSONNode vaultNode;
			public JSONArray itemsNode;

			public List<Entry> allEntries = new();
			public SortedDictionary<string, List<Entry>> entriesByHost = new();

			public void RemoveItem(Entry entry)
			{
				itemsNode.Remove(entry.jsonItem);
			}
		}

		public class Entry
		{
			public int id;
			public string email;
			public string username;
			public string password;
			public List<string> urls = new();
			public string host = "";
			public bool MarkedForDeletion;

			public JSONNode jsonItem;
			public JSONNode jsonContent;

			public bool CompareExact(Entry entry)
			{
				if (urls.Count != entry.urls.Count) return false;
				foreach (var url in urls) if (!entry.urls.Contains(url)) return false;
				foreach (var url in entry.urls) if (!urls.Contains(url)) return false;

				return CompareNoUrl(entry);
			}

			public bool CompareNoUrl(Entry entry)
			{
				// generally, emails are not case sensitive
				if (!email.ToLowerInvariant().Equals(entry.email.ToLowerInvariant())) return false;

				return host.Equals(entry.host) &&
					username.Equals(entry.username) &&
					password.Equals(entry.password);
			}

			public void MergeUrls(Entry entry)
			{
				foreach (var url in entry.urls)
				{
					if (!urls.Contains(url)) urls.Add(url);
				}
			}

			public string GetUsernameOrMail()
			{
				if (!string.IsNullOrEmpty(username)) return username;
				if (!string.IsNullOrEmpty(email)) return email;
				return "";
			}

			public override string ToString()
			{
				return $"[{id}] - [{host}] - [{GetUsernameOrMail()}] - [{password}]";
			}
		}

		JSONNode json;
		List<Vault> vaults = new();

		void ParseFile(string text)
		{
			Log("parsing file");
			json = JSON.Parse(text);
			ParseJson();
		}

		void ParseJson()
		{
			Log("parsing json");
			int counter = 0;
			vaults.Clear();
			JSONNode vaultsNode = json["vaults"];
			foreach (JSONNode vaultNode in vaultsNode.Children)
			{
				// add vault
				Vault vault = new();
				vault.name = vaultNode["name"];
				Log($"vault {vault.name} found");
				vault.vaultNode = vaultNode;
				vaults.Add(vault);

				vault.itemsNode = vaultNode["items"].AsArray;
				foreach (KeyValuePair<string, JSONNode> itemNode in vault.itemsNode)
				{
					string itemId = itemNode.Value["itemId"];
					var data = itemNode.Value["data"];
					var metadata = data["metadata"];
					string name = metadata["name"];
					var content = data["content"];
					var password = content["password"];
					JSONArray urls = content["urls"].AsArray;
					string itemEmail = content["itemEmail"];
					string itemUsername = content["itemUsername"];

					Entry entry = new Entry();
					entry.id = counter;
					counter++;
					entry.jsonItem = itemNode.Value;
					entry.jsonContent = content;
					entry.email = itemEmail;
					entry.username = itemUsername;
					entry.password = password;

					// parse urls
					if (urls.Count > 0)
					{
						foreach (var url in urls)
						{
							entry.urls.Add(url.Value);
							if (!string.IsNullOrEmpty(url.Value))
							{
								// parse ip or top level host
								if (ValidateIPv4(url.Value)) entry.host = url.Value;
								else entry.host = GetTopLevelDomain(url.Value);
							}
						}
					}
					else
					{
						// use entry name as host
						entry.host = name;
					}

					vault.allEntries.Add(entry);
				}
				Log($"vault {vault.name} has {vault.allEntries.Count} entries");

				vault.allEntries.Sort(delegate (Entry e1, Entry e2) { return e1.host.CompareTo(e2.host); });
				foreach (var e in vault.allEntries)
				{
					if (!vault.entriesByHost.ContainsKey(e.host))
					{
						vault.entriesByHost.Add(e.host, new List<Entry>());
					}
					vault.entriesByHost[e.host].Add(e);
				}
				Log($"vault {vault.name} has {vault.entriesByHost.Count} top level domains");
			}
		}

		void Visualize()
		{
			// visualize all
			treeViewAll.Nodes.Clear();
			foreach (var vault in vaults)
			{
				TreeNode vaultNode = new TreeNode();
				vaultNode.Text = vault.name;
				treeViewAll.Nodes.Add(vaultNode);

				foreach (var e in vault.allEntries)
				{
					TreeNode entryNode = new TreeNode();
					entryNode.Text = e.ToString();
					vaultNode.Nodes.Add(entryNode);
					if (e.urls.Count > 0)
					{
						foreach (var url in e.urls)
						{
							TreeNode nodeUrl = new();
							nodeUrl.Text = url;
							entryNode.Nodes.Add(nodeUrl);
							if (e.MarkedForDeletion) nodeUrl.ForeColor = Color.Red;
						}
					}
					if (e.MarkedForDeletion) entryNode.ForeColor = Color.Red;
				}

				vaultNode.Expand();
			}

			// visualize by hosts
			treeViewHosts.Nodes.Clear();
			foreach (var vault in vaults)
			{
				TreeNode vaultNode = new TreeNode();
				vaultNode.Text = vault.name;
				treeViewHosts.Nodes.Add(vaultNode);

				foreach (KeyValuePair<string, List<Entry>> host in vault.entriesByHost)
				{
					TreeNode hostNode = new TreeNode();
					hostNode.Text = host.Key;
					vaultNode.Nodes.Add(hostNode);
					int validEntries = 0;
					foreach (Entry entry in host.Value)
					{
						if (!entry.MarkedForDeletion)
						{
							validEntries++;
							TreeNode entryNode = new TreeNode();
							entryNode.Text = entry.ToString();
							hostNode.Nodes.Add(entryNode);
							if (entry.urls.Count > 0)
							{
								foreach (var url in entry.urls)
								{
									TreeNode nodeUrl = new();
									nodeUrl.Text = url;
									entryNode.Nodes.Add(nodeUrl);
								}
							}
						}
					}

					// highlight hosts with multiple entries
					if (validEntries > 1)
					{
						hostNode.Text = $"{host.Key} ({validEntries})";
						hostNode.ForeColor = Color.Orange;
					}
				}

				vaultNode.Expand();
			}

		}

		void OnStart(Form2.Settings settings)
		{
			Log("starting merge");
			int maxRecursion = 100;
			int counter = 0;
			int modified = 0;
			do
			{
				if (counter > 0)
				{
					Log($"starting recursion {counter}");
				}
				modified = 0;
				if (settings.addTopLevelDomain) modified += AddTopLevelDomainAsUrl();
				if (settings.matchExact) modified += MergeExact();
				if (settings.matchWithDiffUrls) modified += MergeDifferentUrls();
				if (settings.matchWithNoName) modified += MergeWithoutName();
				counter++;

				Log($"entries modified: {modified}");

				if (counter >= maxRecursion)
				{
					Log("ERROR: max recursion reached!");
					break;
				}
			}
			while (settings.recursive && modified != 0);

			ApplyChanges();
			Visualize();
		}


		/// <summary>
		/// Adds the top level domain in the entry urls
		/// </summary>
		int AddTopLevelDomainAsUrl()
		{
			Log("adding top level domain to urls");
			int modified = 0;
			foreach (var vault in vaults)
			{
				foreach (var host in vault.entriesByHost)
				{
					List<Entry> entries = host.Value;
					foreach (var entry in entries)
					{
						// no need to change already marked for deletion
						if (!entry.MarkedForDeletion)
						{
							string tld = "https://" + host.Key + "/";
							if (!entry.urls.Contains(tld))
							{
								Log("adding top level domain to urls: " + entry.ToString());
								entry.urls.Add(tld);
								modified++;
							}
						}
					}
				}
			}
			return modified;
		}

		/// <summary>
		/// Merge exact entries
		/// </summary>
		int MergeExact()
		{
			Log("merging exact matches");
			int modified = 0;

			foreach (var vault in vaults)
			{
				foreach (var host in vault.entriesByHost)
				{
					Entry ContainsExact(List<Entry> entries, Entry entry)
					{
						foreach (var v in entries)
						{
							if (v.CompareExact(entry) && v != entry && !v.MarkedForDeletion) return v;
						}
						return null;
					}

					List<Entry> originalEntriesByHost = host.Value;

					// foreach entry for this host
					foreach (var entry in originalEntriesByHost)
					{
						if (!entry.MarkedForDeletion)
						{
							// if another equal entry already exists that is not this
							Entry matchingEntry = ContainsExact(originalEntriesByHost, entry);
							if (matchingEntry != null)
							{
								// discard this entry
								Log($"merging exact matches: discarding entry {entry} (exact match in {matchingEntry})");
								entry.MarkedForDeletion = true;
								continue;
							}
						}
					}

					// replace entries for this host
					List<Entry> deduplicated = new();
					foreach (var entry in originalEntriesByHost)
					{
						if (!entry.MarkedForDeletion) deduplicated.Add(entry);
					}
					host.Value.Clear();
					host.Value.AddRange(deduplicated);
				}
			}

			//if (duplicates > 0) MessageBox.Show($"{duplicates} exact duplicate entries found", "Info", MessageBoxButtons.OK);
			return modified;
		}

		/// <summary>
		/// Merges entries with same parameters but different urls, and marks for deletion
		/// </summary>
		int MergeDifferentUrls()
		{
			Log("merging similar matches with different urls");
			int modified = 0;

			foreach (var vault in vaults)
			{
				foreach (var host in vault.entriesByHost)
				{
					Entry ContainsNoUrl(List<Entry> entries, Entry entry)
					{
						foreach (var v in entries)
						{
							if (v.CompareNoUrl(entry) && v != entry && !v.MarkedForDeletion) return v;
						}
						return null;
					}

					List<Entry> originalEntriesByHost = host.Value;

					// foreach entry for this host
					foreach (var entry in originalEntriesByHost)
					{
						if (!entry.MarkedForDeletion)
						{
							// if another equal entry already exists that is not this, but has different urls
							Entry matchingEntry = ContainsNoUrl(originalEntriesByHost, entry);
							if (matchingEntry != null)
							{
								// merge urls with other, discard this entry
								Log($"merging similar matches: discarding entry {entry} (urls merged in {matchingEntry})");
								matchingEntry.MergeUrls(entry);
								entry.MarkedForDeletion = true;
								modified++;
								break;
							}
						}
					}

					// replace entries for this host
					List<Entry> deduplicated = new();
					foreach (var entry in originalEntriesByHost)
					{
						if (!entry.MarkedForDeletion) deduplicated.Add(entry);
					}
					host.Value.Clear();
					host.Value.AddRange(deduplicated);
				}
			}

			//if (merged > 0) MessageBox.Show($"{merged} duplicated entries with different urls merged", "Info", MessageBoxButtons.OK);
			return modified;
		}

		int MergeWithoutName()
		{
			Log("merging similar matches without username or email");
			int modified = 0;

			foreach (var vault in vaults)
			{
				foreach (var entry in vault.allEntries)
				{
					if (!entry.MarkedForDeletion)
					{
						// if this entry has no username
						if (string.IsNullOrEmpty(entry.username) && string.IsNullOrEmpty(entry.email))
						{
							// must have host
							if (!string.IsNullOrEmpty(entry.host))
							{
								// if host exists
								if (vault.entriesByHost.ContainsKey(entry.host))
								{
									foreach (var entryWithHost in vault.entriesByHost[entry.host])
									{
										// if an entry by host has same pwd
										if (entryWithHost.password.Equals(entry.password) && entryWithHost != entry)
										{
											// this entry without username can be deleted as another one with the same host and pwd exists
											Log($"merging similar matches: discarding entry {entry} (already exists with username or email in {entryWithHost})");
											entry.MarkedForDeletion = true;
											modified++;
											break;
										}
									}
								}
							}
						}
					}
				}
			}

			//if (merged > 0) MessageBox.Show($"{merged} duplicated entries with different urls merged", "Info", MessageBoxButtons.OK);
			return modified;
		}

		/// <summary>
		/// Write to JSON and then to file
		/// </summary>
		void ApplyChanges()
		{
			Log("applying changes to json");
			foreach (var vault in vaults)
			{
				foreach (var entry in vault.allEntries)
				{
					if (entry.MarkedForDeletion)
					{
						vault.RemoveItem(entry);
					}
					else
					{
						JSONArray urls = new();
						foreach (var url in entry.urls)
						{
							urls.Add(url);
						}
						entry.jsonContent["urls"] = urls;
					}
				}
			}
		}





		static string GetTopLevelDomain(string url)
		{
			if (string.IsNullOrEmpty(url)) return "";
			try
			{
				var uri = new Uri(url);
				var splitHostName = uri.Host.Split('.');
				if (splitHostName.Length >= 2)
				{
					var secondLevelHostName = splitHostName[splitHostName.Length - 2] + "." + splitHostName[splitHostName.Length - 1];
					return secondLevelHostName;
				}
				else return uri.Host;
			}
			catch (Exception ex)
			{
				return "";
			}
		}

		static bool ValidateIPv4(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				return false;
			}

			try
			{
				var uri = new Uri(url);
				url = uri.Host;
			}
			catch (Exception ex)
			{
				return false;
			}

			string[] splitValues = url.Split('.');
			if (splitValues.Length != 4)
			{
				return false;
			}

			byte tempForParsing;

			return splitValues.All(r => byte.TryParse(r, out tempForParsing));
		}

		void Log(string message)
		{
			listBox1.Items.Add(message);
			listBox1.SelectedIndex = listBox1.Items.Count - 1;
		}


	}
}
