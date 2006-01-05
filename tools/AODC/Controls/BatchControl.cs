using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace AODC.Controls
{
	/// <summary>
	/// Zusammenfassung für BatchControl.
	/// </summary>
	public class BatchControl : System.Windows.Forms.UserControl
	{
		public delegate void StartBatch(ArrayList sourcefiles, ArrayList targetfiles);
		public event StartBatch OnStartBatch;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lbxSourcefiles;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lbxTargetfiles;
		private System.Windows.Forms.Button btnChooseFile;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.Button btnRemove;
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BatchControl()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.lbxSourcefiles = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lbxTargetfiles = new System.Windows.Forms.ListBox();
			this.btnChooseFile = new System.Windows.Forms.Button();
			this.btnConvert = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Source files:";
			// 
			// lbxSourcefiles
			// 
			this.lbxSourcefiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbxSourcefiles.Location = new System.Drawing.Point(88, 8);
			this.lbxSourcefiles.Name = "lbxSourcefiles";
			this.lbxSourcefiles.Size = new System.Drawing.Size(368, 69);
			this.lbxSourcefiles.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 88);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Target files:";
			// 
			// lbxTargetfiles
			// 
			this.lbxTargetfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbxTargetfiles.Location = new System.Drawing.Point(88, 88);
			this.lbxTargetfiles.Name = "lbxTargetfiles";
			this.lbxTargetfiles.Size = new System.Drawing.Size(368, 69);
			this.lbxTargetfiles.TabIndex = 3;
			// 
			// btnChooseFile
			// 
			this.btnChooseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnChooseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnChooseFile.Location = new System.Drawing.Point(464, 8);
			this.btnChooseFile.Name = "btnChooseFile";
			this.btnChooseFile.Size = new System.Drawing.Size(24, 23);
			this.btnChooseFile.TabIndex = 4;
			this.btnChooseFile.Text = "...";
			this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
			// 
			// btnConvert
			// 
			this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConvert.Location = new System.Drawing.Point(16, 130);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(64, 23);
			this.btnConvert.TabIndex = 5;
			this.btnConvert.Text = "Convert";
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemove.Location = new System.Drawing.Point(464, 40);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(24, 23);
			this.btnRemove.TabIndex = 6;
			this.btnRemove.Text = "X";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// BatchControl
			// 
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(this.btnChooseFile);
			this.Controls.Add(this.lbxTargetfiles);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lbxSourcefiles);
			this.Controls.Add(this.label1);
			this.Name = "BatchControl";
			this.Size = new System.Drawing.Size(496, 160);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Handles the Click event of the btnChooseFile control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnChooseFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog		= new OpenFileDialog();

			openFileDialog.InitialDirectory		= "c:\\" ;
			openFileDialog.Filter				= "odt files (*.odt)|*.odt";
			openFileDialog.FilterIndex			= 1 ;
			openFileDialog.RestoreDirectory		= true ;
			openFileDialog.Multiselect			= true;

			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.FillFiles(openFileDialog.FileNames);
			}
		}

		/// <summary>
		/// Fills the files.
		/// </summary>
		/// <param name="fileNames">The file names.</param>
		private void FillFiles(string[] fileNames)
		{
			string theFile						= null;
			foreach(string file in fileNames)
			{
				this.lbxSourcefiles.Items.Add(file);
				theFile							= file;
				theFile							= theFile.Substring(0, file.Length-3);
				theFile							+= "html";
				this.lbxTargetfiles.Items.Add(theFile);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnRemove control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			int index							= this.lbxSourcefiles.SelectedIndex;
			if(index != -1)
			{
				this.lbxSourcefiles.Items.RemoveAt(index);
				if(index+1 <= this.lbxTargetfiles.Items.Count)
					this.lbxTargetfiles.Items.RemoveAt(index);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnConvert control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnConvert_Click(object sender, System.EventArgs e)
		{
			ArrayList sourceFiles				= new ArrayList();
			ArrayList targetFiles				= new ArrayList();

			if(OnStartBatch != null)
			{
				for(int i=0; i < this.lbxSourcefiles.Items.Count; i++)
				{
					sourceFiles.Add(this.lbxSourcefiles.Items[i].ToString());
					targetFiles.Add(this.lbxTargetfiles.Items[i].ToString());
				}
				OnStartBatch(sourceFiles, targetFiles);
			}
		}
	}
}
