using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;

namespace paladins_tfc.src.gui
{
  partial class SelectorWizard
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
      this.btnSubmitAll = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.all = new System.Windows.Forms.TabPage();
      this.labelAll = new System.Windows.Forms.Label();
      this.textPreviewAll = new System.Windows.Forms.TextBox();
      this.filename = new System.Windows.Forms.TabPage();
      this.btnFilenameNameBrowse = new System.Windows.Forms.Button();
      this.textFilenameName = new System.Windows.Forms.TextBox();
      this.labelFilenameName = new System.Windows.Forms.Label();
      this.textPreviewFilename = new System.Windows.Forms.TextBox();
      this.btnSubmitFilename = new System.Windows.Forms.Button();
      this.filenameAndId = new System.Windows.Forms.TabPage();
      this.richTextBoxIdSelect = new System.Windows.Forms.RichTextBox();
      this.labelFilenameAndIdHeader = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnFilenameAndIdNameBrowse = new System.Windows.Forms.Button();
      this.textFilenameAndIdName = new System.Windows.Forms.TextBox();
      this.labelFilenameAndIdName = new System.Windows.Forms.Label();
      this.textPreviewFilenameAndId = new System.Windows.Forms.TextBox();
      this.btnSubmitFileNameAndId = new System.Windows.Forms.Button();
      this.byImageSimilarity = new System.Windows.Forms.TabPage();
      this.labelSelectFileHashCooked = new System.Windows.Forms.Label();
      this.textSelectFileHashCooked = new System.Windows.Forms.TextBox();
      this.btnSelectFileHashCooked = new System.Windows.Forms.Button();
      this.btnGenerateHashCooked = new System.Windows.Forms.Button();
      this.labelSelectFileHashTFC = new System.Windows.Forms.Label();
      this.btnGenerateHashTFC = new System.Windows.Forms.Button();
      this.textSelectFileHashTFC = new System.Windows.Forms.TextBox();
      this.btnSelectFileHashTFC = new System.Windows.Forms.Button();
      this.labelSelectDirectoryCookedReference = new System.Windows.Forms.Label();
      this.textSelectDirectoryCookedReference = new System.Windows.Forms.TextBox();
      this.btnSelectDirectoryCookedReference = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.textPreviewImageSimilarity = new System.Windows.Forms.TextBox();
      this.btnSubmitImageSimilarity = new System.Windows.Forms.Button();
      this.openFileDialogSelectFileHashCooked = new System.Windows.Forms.OpenFileDialog();
      this.openFileDialogSelectFileHashTFC = new System.Windows.Forms.OpenFileDialog();
      this.tabControl1.SuspendLayout();
      this.all.SuspendLayout();
      this.filename.SuspendLayout();
      this.filenameAndId.SuspendLayout();
      this.byImageSimilarity.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnSubmitAll
      // 
      this.btnSubmitAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitAll.Location = new System.Drawing.Point(370, 478);
      this.btnSubmitAll.Name = "btnSubmitAll";
      this.btnSubmitAll.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitAll.TabIndex = 5;
      this.btnSubmitAll.Text = "Create Filter";
      this.btnSubmitAll.UseVisualStyleBackColor = true;
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.all);
      this.tabControl1.Controls.Add(this.filename);
      this.tabControl1.Controls.Add(this.filenameAndId);
      this.tabControl1.Controls.Add(this.byImageSimilarity);
      this.tabControl1.Location = new System.Drawing.Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(415, 496);
      this.tabControl1.TabIndex = 6;
      // 
      // all
      // 
      this.all.Controls.Add(this.labelAll);
      this.all.Controls.Add(this.textPreviewAll);
      this.all.Controls.Add(this.btnSubmitAll);
      this.all.Location = new System.Drawing.Point(4, 24);
      this.all.Name = "all";
      this.all.Padding = new System.Windows.Forms.Padding(3);
      this.all.Size = new System.Drawing.Size(407, 468);
      this.all.TabIndex = 0;
      this.all.Text = "All .tfc In Directory";
      this.all.UseVisualStyleBackColor = true;
      this.all.Enter += new System.EventHandler(this.all_Enter);
      // 
      // labelAll
      // 
      this.labelAll.AutoSize = true;
      this.labelAll.Location = new System.Drawing.Point(6, 3);
      this.labelAll.Name = "labelAll";
      this.labelAll.Size = new System.Drawing.Size(277, 45);
      this.labelAll.TabIndex = 0;
      this.labelAll.Text = "Selects all textures in all tfc files\r\n(WARNING: Dumping all textures can take 10" +
    "0+GB)\r\n\r\n";
      // 
      // textPreviewAll
      // 
      this.textPreviewAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textPreviewAll.Location = new System.Drawing.Point(6, 478);
      this.textPreviewAll.Name = "textPreviewAll";
      this.textPreviewAll.ReadOnly = true;
      this.textPreviewAll.Size = new System.Drawing.Size(358, 23);
      this.textPreviewAll.TabIndex = 7;
      this.textPreviewAll.Text = "*";
      // 
      // filename
      // 
      this.filename.Controls.Add(this.btnFilenameNameBrowse);
      this.filename.Controls.Add(this.textFilenameName);
      this.filename.Controls.Add(this.labelFilenameName);
      this.filename.Controls.Add(this.textPreviewFilename);
      this.filename.Controls.Add(this.btnSubmitFilename);
      this.filename.Location = new System.Drawing.Point(4, 24);
      this.filename.Name = "filename";
      this.filename.Padding = new System.Windows.Forms.Padding(3);
      this.filename.Size = new System.Drawing.Size(407, 468);
      this.filename.TabIndex = 1;
      this.filename.Text = "By Filename";
      this.filename.UseVisualStyleBackColor = true;
      this.filename.Enter += new System.EventHandler(this.filename_Enter);
      // 
      // btnFilenameNameBrowse
      // 
      this.btnFilenameNameBrowse.Location = new System.Drawing.Point(276, 21);
      this.btnFilenameNameBrowse.Name = "btnFilenameNameBrowse";
      this.btnFilenameNameBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnFilenameNameBrowse.TabIndex = 2;
      this.btnFilenameNameBrowse.Text = "Browse";
      this.btnFilenameNameBrowse.UseVisualStyleBackColor = true;
      this.btnFilenameNameBrowse.Click += new System.EventHandler(this.btnFilenameNameBrowse_Click);
      // 
      // textFilenameName
      // 
      this.textFilenameName.Location = new System.Drawing.Point(6, 21);
      this.textFilenameName.Name = "textFilenameName";
      this.textFilenameName.Size = new System.Drawing.Size(264, 23);
      this.textFilenameName.TabIndex = 1;
      this.textFilenameName.TextChanged += new System.EventHandler(this.textFilenameName_TextChanged);
      // 
      // labelFilenameName
      // 
      this.labelFilenameName.AutoSize = true;
      this.labelFilenameName.Location = new System.Drawing.Point(6, 3);
      this.labelFilenameName.Name = "labelFilenameName";
      this.labelFilenameName.Size = new System.Drawing.Size(181, 15);
      this.labelFilenameName.TabIndex = 0;
      this.labelFilenameName.Text = "Selects all textures in specific file.";
      // 
      // textPreviewFilename
      // 
      this.textPreviewFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textPreviewFilename.Location = new System.Drawing.Point(6, 439);
      this.textPreviewFilename.Name = "textPreviewFilename";
      this.textPreviewFilename.ReadOnly = true;
      this.textPreviewFilename.Size = new System.Drawing.Size(288, 23);
      this.textPreviewFilename.TabIndex = 8;
      // 
      // btnSubmitFilename
      // 
      this.btnSubmitFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitFilename.Location = new System.Drawing.Point(300, 439);
      this.btnSubmitFilename.Name = "btnSubmitFilename";
      this.btnSubmitFilename.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitFilename.TabIndex = 6;
      this.btnSubmitFilename.Text = "Create Filter";
      this.btnSubmitFilename.UseVisualStyleBackColor = true;
      // 
      // filenameAndId
      // 
      this.filenameAndId.Controls.Add(this.richTextBoxIdSelect);
      this.filenameAndId.Controls.Add(this.labelFilenameAndIdHeader);
      this.filenameAndId.Controls.Add(this.label1);
      this.filenameAndId.Controls.Add(this.btnFilenameAndIdNameBrowse);
      this.filenameAndId.Controls.Add(this.textFilenameAndIdName);
      this.filenameAndId.Controls.Add(this.labelFilenameAndIdName);
      this.filenameAndId.Controls.Add(this.textPreviewFilenameAndId);
      this.filenameAndId.Controls.Add(this.btnSubmitFileNameAndId);
      this.filenameAndId.Location = new System.Drawing.Point(4, 24);
      this.filenameAndId.Name = "filenameAndId";
      this.filenameAndId.Size = new System.Drawing.Size(407, 468);
      this.filenameAndId.TabIndex = 2;
      this.filenameAndId.Text = "By Filename and Id";
      this.filenameAndId.UseVisualStyleBackColor = true;
      this.filenameAndId.Enter += new System.EventHandler(this.filenameAndId_Enter);
      // 
      // richTextBoxIdSelect
      // 
      this.richTextBoxIdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.richTextBoxIdSelect.Location = new System.Drawing.Point(6, 156);
      this.richTextBoxIdSelect.Name = "richTextBoxIdSelect";
      this.richTextBoxIdSelect.Size = new System.Drawing.Size(345, 316);
      this.richTextBoxIdSelect.TabIndex = 13;
      this.richTextBoxIdSelect.Text = "";
      this.richTextBoxIdSelect.TextChanged += new System.EventHandler(this.richTextBoxIdSelect_TextChanged);
      // 
      // labelFilenameAndIdHeader
      // 
      this.labelFilenameAndIdHeader.AutoSize = true;
      this.labelFilenameAndIdHeader.Location = new System.Drawing.Point(6, 3);
      this.labelFilenameAndIdHeader.Name = "labelFilenameAndIdHeader";
      this.labelFilenameAndIdHeader.Size = new System.Drawing.Size(245, 15);
      this.labelFilenameAndIdHeader.TabIndex = 15;
      this.labelFilenameAndIdHeader.Text = "Selects specific texture ids within specific file.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 93);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(320, 60);
      this.label1.TabIndex = 14;
      this.label1.Text = "Id:s to Select:\r\nEach line have a\r\n- \"X\" for a specific id \r\n- \"A-B\" for all numb" +
    "er between A and B (including A and B)";
      // 
      // btnFilenameAndIdNameBrowse
      // 
      this.btnFilenameAndIdNameBrowse.Location = new System.Drawing.Point(276, 53);
      this.btnFilenameAndIdNameBrowse.Name = "btnFilenameAndIdNameBrowse";
      this.btnFilenameAndIdNameBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnFilenameAndIdNameBrowse.TabIndex = 12;
      this.btnFilenameAndIdNameBrowse.Text = "Browse";
      this.btnFilenameAndIdNameBrowse.UseVisualStyleBackColor = true;
      this.btnFilenameAndIdNameBrowse.Click += new System.EventHandler(this.btnFilenameAndIdNameBrowse_Click);
      // 
      // textFilenameAndIdName
      // 
      this.textFilenameAndIdName.Location = new System.Drawing.Point(6, 54);
      this.textFilenameAndIdName.Name = "textFilenameAndIdName";
      this.textFilenameAndIdName.Size = new System.Drawing.Size(264, 23);
      this.textFilenameAndIdName.TabIndex = 11;
      this.textFilenameAndIdName.TextChanged += new System.EventHandler(this.textFilenameAndIdName_TextChanged);
      // 
      // labelFilenameAndIdName
      // 
      this.labelFilenameAndIdName.AutoSize = true;
      this.labelFilenameAndIdName.Location = new System.Drawing.Point(6, 36);
      this.labelFilenameAndIdName.Name = "labelFilenameAndIdName";
      this.labelFilenameAndIdName.Size = new System.Drawing.Size(73, 15);
      this.labelFilenameAndIdName.TabIndex = 10;
      this.labelFilenameAndIdName.Text = "File to Select";
      // 
      // textPreviewFilenameAndId
      // 
      this.textPreviewFilenameAndId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textPreviewFilenameAndId.Location = new System.Drawing.Point(6, 478);
      this.textPreviewFilenameAndId.Name = "textPreviewFilenameAndId";
      this.textPreviewFilenameAndId.ReadOnly = true;
      this.textPreviewFilenameAndId.Size = new System.Drawing.Size(358, 23);
      this.textPreviewFilenameAndId.TabIndex = 9;
      // 
      // btnSubmitFileNameAndId
      // 
      this.btnSubmitFileNameAndId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitFileNameAndId.Location = new System.Drawing.Point(370, 478);
      this.btnSubmitFileNameAndId.Name = "btnSubmitFileNameAndId";
      this.btnSubmitFileNameAndId.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitFileNameAndId.TabIndex = 7;
      this.btnSubmitFileNameAndId.Text = "Create Filter";
      this.btnSubmitFileNameAndId.UseVisualStyleBackColor = true;
      // 
      // byImageSimilarity
      // 
      this.byImageSimilarity.Controls.Add(this.labelSelectFileHashCooked);
      this.byImageSimilarity.Controls.Add(this.textSelectFileHashCooked);
      this.byImageSimilarity.Controls.Add(this.btnSelectFileHashCooked);
      this.byImageSimilarity.Controls.Add(this.btnGenerateHashCooked);
      this.byImageSimilarity.Controls.Add(this.labelSelectFileHashTFC);
      this.byImageSimilarity.Controls.Add(this.btnGenerateHashTFC);
      this.byImageSimilarity.Controls.Add(this.textSelectFileHashTFC);
      this.byImageSimilarity.Controls.Add(this.btnSelectFileHashTFC);
      this.byImageSimilarity.Controls.Add(this.labelSelectDirectoryCookedReference);
      this.byImageSimilarity.Controls.Add(this.textSelectDirectoryCookedReference);
      this.byImageSimilarity.Controls.Add(this.btnSelectDirectoryCookedReference);
      this.byImageSimilarity.Controls.Add(this.label2);
      this.byImageSimilarity.Controls.Add(this.textPreviewImageSimilarity);
      this.byImageSimilarity.Controls.Add(this.btnSubmitImageSimilarity);
      this.byImageSimilarity.Location = new System.Drawing.Point(4, 24);
      this.byImageSimilarity.Name = "byImageSimilarity";
      this.byImageSimilarity.Size = new System.Drawing.Size(407, 468);
      this.byImageSimilarity.TabIndex = 3;
      this.byImageSimilarity.Text = "By Image Similarity";
      this.byImageSimilarity.UseVisualStyleBackColor = true;
      this.byImageSimilarity.Enter += new System.EventHandler(this.byImageSimilarity_Enter);
      // 
      // labelSelectFileHashCooked
      // 
      this.labelSelectFileHashCooked.AutoSize = true;
      this.labelSelectFileHashCooked.Location = new System.Drawing.Point(83, 55);
      this.labelSelectFileHashCooked.Name = "labelSelectFileHashCooked";
      this.labelSelectFileHashCooked.Size = new System.Drawing.Size(89, 15);
      this.labelSelectFileHashCooked.TabIndex = 37;
      this.labelSelectFileHashCooked.Text = "Cooked Hashes";
      this.labelSelectFileHashCooked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textSelectFileHashCooked
      // 
      this.textSelectFileHashCooked.Location = new System.Drawing.Point(179, 52);
      this.textSelectFileHashCooked.Name = "textSelectFileHashCooked";
      this.textSelectFileHashCooked.ReadOnly = true;
      this.textSelectFileHashCooked.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.textSelectFileHashCooked.Size = new System.Drawing.Size(226, 23);
      this.textSelectFileHashCooked.TabIndex = 34;
      this.textSelectFileHashCooked.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // btnSelectFileHashCooked
      // 
      this.btnSelectFileHashCooked.Location = new System.Drawing.Point(411, 51);
      this.btnSelectFileHashCooked.Name = "btnSelectFileHashCooked";
      this.btnSelectFileHashCooked.Size = new System.Drawing.Size(93, 23);
      this.btnSelectFileHashCooked.TabIndex = 39;
      this.btnSelectFileHashCooked.Text = "Select File";
      this.btnSelectFileHashCooked.UseVisualStyleBackColor = true;
      this.btnSelectFileHashCooked.Click += new System.EventHandler(this.btnSelectFileHashCooked_Click);
      // 
      // btnGenerateHashCooked
      // 
      this.btnGenerateHashCooked.Location = new System.Drawing.Point(510, 51);
      this.btnGenerateHashCooked.Name = "btnGenerateHashCooked";
      this.btnGenerateHashCooked.Size = new System.Drawing.Size(170, 23);
      this.btnGenerateHashCooked.TabIndex = 35;
      this.btnGenerateHashCooked.Text = "Generate Cooked Hashes";
      this.btnGenerateHashCooked.UseVisualStyleBackColor = true;
      this.btnGenerateHashCooked.Click += new System.EventHandler(this.btnGenerateHashCooked_Click);
      // 
      // labelSelectFileHashTFC
      // 
      this.labelSelectFileHashTFC.AutoSize = true;
      this.labelSelectFileHashTFC.Location = new System.Drawing.Point(105, 84);
      this.labelSelectFileHashTFC.Name = "labelSelectFileHashTFC";
      this.labelSelectFileHashTFC.Size = new System.Drawing.Size(68, 15);
      this.labelSelectFileHashTFC.TabIndex = 40;
      this.labelSelectFileHashTFC.Text = "TFC Hashes";
      this.labelSelectFileHashTFC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnGenerateHashTFC
      // 
      this.btnGenerateHashTFC.Location = new System.Drawing.Point(510, 81);
      this.btnGenerateHashTFC.Name = "btnGenerateHashTFC";
      this.btnGenerateHashTFC.Size = new System.Drawing.Size(170, 23);
      this.btnGenerateHashTFC.TabIndex = 41;
      this.btnGenerateHashTFC.Text = "Generate TFC Hashes";
      this.btnGenerateHashTFC.UseVisualStyleBackColor = true;
      this.btnGenerateHashTFC.Click += new System.EventHandler(this.btnGenerateHashTFC_Click);
      // 
      // textSelectFileHashTFC
      // 
      this.textSelectFileHashTFC.Location = new System.Drawing.Point(179, 81);
      this.textSelectFileHashTFC.Name = "textSelectFileHashTFC";
      this.textSelectFileHashTFC.ReadOnly = true;
      this.textSelectFileHashTFC.Size = new System.Drawing.Size(226, 23);
      this.textSelectFileHashTFC.TabIndex = 38;
      this.textSelectFileHashTFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // btnSelectFileHashTFC
      // 
      this.btnSelectFileHashTFC.Location = new System.Drawing.Point(411, 80);
      this.btnSelectFileHashTFC.Name = "btnSelectFileHashTFC";
      this.btnSelectFileHashTFC.Size = new System.Drawing.Size(93, 23);
      this.btnSelectFileHashTFC.TabIndex = 36;
      this.btnSelectFileHashTFC.Text = "Select File";
      this.btnSelectFileHashTFC.UseVisualStyleBackColor = true;
      this.btnSelectFileHashTFC.Click += new System.EventHandler(this.btnSelectFileHashTFC_Click);
      // 
      // labelSelectDirectoryCookedReference
      // 
      this.labelSelectDirectoryCookedReference.AutoSize = true;
      this.labelSelectDirectoryCookedReference.Location = new System.Drawing.Point(19, 113);
      this.labelSelectDirectoryCookedReference.Name = "labelSelectDirectoryCookedReference";
      this.labelSelectDirectoryCookedReference.Size = new System.Drawing.Size(154, 15);
      this.labelSelectDirectoryCookedReference.TabIndex = 44;
      this.labelSelectDirectoryCookedReference.Text = "Cooked Reference Directory";
      this.labelSelectDirectoryCookedReference.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textSelectDirectoryCookedReference
      // 
      this.textSelectDirectoryCookedReference.Location = new System.Drawing.Point(179, 110);
      this.textSelectDirectoryCookedReference.Name = "textSelectDirectoryCookedReference";
      this.textSelectDirectoryCookedReference.ReadOnly = true;
      this.textSelectDirectoryCookedReference.Size = new System.Drawing.Size(226, 23);
      this.textSelectDirectoryCookedReference.TabIndex = 43;
      this.textSelectDirectoryCookedReference.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // btnSelectDirectoryCookedReference
      // 
      this.btnSelectDirectoryCookedReference.Location = new System.Drawing.Point(411, 109);
      this.btnSelectDirectoryCookedReference.Name = "btnSelectDirectoryCookedReference";
      this.btnSelectDirectoryCookedReference.Size = new System.Drawing.Size(93, 23);
      this.btnSelectDirectoryCookedReference.TabIndex = 42;
      this.btnSelectDirectoryCookedReference.Text = "Select Folder";
      this.btnSelectDirectoryCookedReference.UseVisualStyleBackColor = true;
      this.btnSelectDirectoryCookedReference.Click += new System.EventHandler(this.btnSelectDirectoryCookedReference_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(456, 15);
      this.label2.TabIndex = 12;
      this.label2.Text = "Finds a Filename and Id selector based off a Cooked Texture (as Extracted by UMod" +
    "el)";
      // 
      // textPreviewImageSimilarity
      // 
      this.textPreviewImageSimilarity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textPreviewImageSimilarity.Location = new System.Drawing.Point(6, 439);
      this.textPreviewImageSimilarity.Name = "textPreviewImageSimilarity";
      this.textPreviewImageSimilarity.ReadOnly = true;
      this.textPreviewImageSimilarity.Size = new System.Drawing.Size(288, 23);
      this.textPreviewImageSimilarity.TabIndex = 10;
      // 
      // btnSubmitImageSimilarity
      // 
      this.btnSubmitImageSimilarity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitImageSimilarity.Location = new System.Drawing.Point(300, 439);
      this.btnSubmitImageSimilarity.Name = "btnSubmitImageSimilarity";
      this.btnSubmitImageSimilarity.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitImageSimilarity.TabIndex = 11;
      this.btnSubmitImageSimilarity.Text = "Create Filter";
      this.btnSubmitImageSimilarity.UseVisualStyleBackColor = true;
      // 
      // openFileDialogSelectFileHashCooked
      // 
      this.openFileDialogSelectFileHashCooked.DefaultExt = "json";
      this.openFileDialogSelectFileHashCooked.Filter = "json files (*.json)|*.json";
      // 
      // openFileDialogSelectFileHashTFC
      // 
      this.openFileDialogSelectFileHashTFC.DefaultExt = "json";
      this.openFileDialogSelectFileHashTFC.Filter = "json files (*.json)|*.json";
      // 
      // SelectorWizard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(439, 520);
      this.Controls.Add(this.tabControl1);
      this.Name = "SelectorWizard";
      this.Text = "Selector Wizard";
      this.tabControl1.ResumeLayout(false);
      this.all.ResumeLayout(false);
      this.all.PerformLayout();
      this.filename.ResumeLayout(false);
      this.filename.PerformLayout();
      this.filenameAndId.ResumeLayout(false);
      this.filenameAndId.PerformLayout();
      this.byImageSimilarity.ResumeLayout(false);
      this.byImageSimilarity.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private Button btnSubmitAll;
    private TabControl tabControl1;
    private TabPage all;
    private TextBox textPreviewAll;
    private Label labelAll;
    private TabPage filename;
    private Button btnFilenameNameBrowse;
    private TextBox textFilenameName;
    private Label labelFilenameName;
    private TabPage filenameAndId;
    private TabPage byImageSimilarity;
    private TextBox textPreviewFilename;
    private Button btnSubmitFilename;
    private TextBox textPreviewFilenameAndId;
    private Button btnSubmitFileNameAndId;
    private TextBox textPreviewImageSimilarity;
    private Button btnSubmitImageSimilarity;
    private Label label1;
    private RichTextBox richTextBoxIdSelect;
    private Button btnFilenameAndIdNameBrowse;
    private TextBox textFilenameAndIdName;
    private Label labelFilenameAndIdName;
    private Label labelFilenameAndIdHeader;
    private Label label2;
    private Label labelSelectFileHashCooked;
    private TextBox textSelectFileHashCooked;
    private Button btnSelectFileHashCooked;
    private Button btnGenerateHashCooked;
    private Label labelSelectFileHashTFC;
    private Button btnGenerateHashTFC;
    private TextBox textSelectFileHashTFC;
    private Button btnSelectFileHashTFC;
    private Label labelSelectDirectoryCookedReference;
    private TextBox textSelectDirectoryCookedReference;
    private Button btnSelectDirectoryCookedReference;
    private OpenFileDialog openFileDialogSelectFileHashCooked;
    private OpenFileDialog openFileDialogSelectFileHashTFC;
    private CommonOpenFileDialog openFileDialogSelectDirectoryCookedReference;
  }
}