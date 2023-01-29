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
      this.components = new System.ComponentModel.Container();
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
      this.btnDumpPreviews = new System.Windows.Forms.Button();
      this.listViewTFC = new System.Windows.Forms.ListView();
      this.checkFileName = new System.Windows.Forms.CheckBox();
      this.groupFileName = new System.Windows.Forms.GroupBox();
      this.textNameIncludeFilter = new System.Windows.Forms.TextBox();
      this.textNameExcludeFilter = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.checkResolution = new System.Windows.Forms.CheckBox();
      this.checkCookedImage = new System.Windows.Forms.CheckBox();
      this.groupResolution = new System.Windows.Forms.GroupBox();
      this.numericFilterResolution = new System.Windows.Forms.NumericUpDown();
      this.sliderFilterResolution = new System.Windows.Forms.TrackBar();
      this.groupCookedImage = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.numericNoCandidates = new System.Windows.Forms.NumericUpDown();
      this.pictureCooked = new System.Windows.Forms.PictureBox();
      this.btnCookedFilterBrowse = new System.Windows.Forms.Button();
      this.textCookedFilter = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.labelCandidates = new System.Windows.Forms.Label();
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
      this.imgListTFC = new System.Windows.Forms.ImageList(this.components);
      this.openFileDialogCookedFilter = new System.Windows.Forms.OpenFileDialog();
      this.tabControl1.SuspendLayout();
      this.all.SuspendLayout();
      this.filename.SuspendLayout();
      this.filenameAndId.SuspendLayout();
      this.byImageSimilarity.SuspendLayout();
      this.groupFileName.SuspendLayout();
      this.groupResolution.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericFilterResolution)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sliderFilterResolution)).BeginInit();
      this.groupCookedImage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericNoCandidates)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureCooked)).BeginInit();
      this.SuspendLayout();
      // 
      // btnSubmitAll
      // 
      this.btnSubmitAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitAll.Location = new System.Drawing.Point(649, 541);
      this.btnSubmitAll.Name = "btnSubmitAll";
      this.btnSubmitAll.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitAll.TabIndex = 5;
      this.btnSubmitAll.Text = "Create Filter";
      this.btnSubmitAll.UseVisualStyleBackColor = true;
      this.btnSubmitAll.Click += new System.EventHandler(this.btnSubmitAll_Click);
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
      this.tabControl1.Size = new System.Drawing.Size(760, 611);
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
      this.all.Size = new System.Drawing.Size(752, 583);
      this.all.TabIndex = 0;
      this.all.Text = "All .tfc In Directory";
      this.all.UseVisualStyleBackColor = true;
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
      this.textPreviewAll.Location = new System.Drawing.Point(6, 541);
      this.textPreviewAll.Name = "textPreviewAll";
      this.textPreviewAll.ReadOnly = true;
      this.textPreviewAll.Size = new System.Drawing.Size(637, 23);
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
      this.filename.Size = new System.Drawing.Size(752, 583);
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
      this.textPreviewFilename.Location = new System.Drawing.Point(6, 541);
      this.textPreviewFilename.Name = "textPreviewFilename";
      this.textPreviewFilename.ReadOnly = true;
      this.textPreviewFilename.Size = new System.Drawing.Size(637, 23);
      this.textPreviewFilename.TabIndex = 8;
      // 
      // btnSubmitFilename
      // 
      this.btnSubmitFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitFilename.Location = new System.Drawing.Point(649, 541);
      this.btnSubmitFilename.Name = "btnSubmitFilename";
      this.btnSubmitFilename.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitFilename.TabIndex = 6;
      this.btnSubmitFilename.Text = "Create Filter";
      this.btnSubmitFilename.UseVisualStyleBackColor = true;
      this.btnSubmitFilename.Click += new System.EventHandler(this.btnSubmitFilename_Click);
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
      this.filenameAndId.Size = new System.Drawing.Size(752, 583);
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
      this.richTextBoxIdSelect.Size = new System.Drawing.Size(345, 379);
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
      this.textPreviewFilenameAndId.Location = new System.Drawing.Point(6, 541);
      this.textPreviewFilenameAndId.Name = "textPreviewFilenameAndId";
      this.textPreviewFilenameAndId.ReadOnly = true;
      this.textPreviewFilenameAndId.Size = new System.Drawing.Size(637, 23);
      this.textPreviewFilenameAndId.TabIndex = 9;
      // 
      // btnSubmitFileNameAndId
      // 
      this.btnSubmitFileNameAndId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitFileNameAndId.Location = new System.Drawing.Point(649, 541);
      this.btnSubmitFileNameAndId.Name = "btnSubmitFileNameAndId";
      this.btnSubmitFileNameAndId.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitFileNameAndId.TabIndex = 7;
      this.btnSubmitFileNameAndId.Text = "Create Filter";
      this.btnSubmitFileNameAndId.UseVisualStyleBackColor = true;
      this.btnSubmitFileNameAndId.Click += new System.EventHandler(this.btnSubmitFileNameAndId_Click);
      // 
      // byImageSimilarity
      // 
      this.byImageSimilarity.Controls.Add(this.btnDumpPreviews);
      this.byImageSimilarity.Controls.Add(this.listViewTFC);
      this.byImageSimilarity.Controls.Add(this.checkFileName);
      this.byImageSimilarity.Controls.Add(this.groupFileName);
      this.byImageSimilarity.Controls.Add(this.checkResolution);
      this.byImageSimilarity.Controls.Add(this.checkCookedImage);
      this.byImageSimilarity.Controls.Add(this.groupResolution);
      this.byImageSimilarity.Controls.Add(this.groupCookedImage);
      this.byImageSimilarity.Controls.Add(this.label3);
      this.byImageSimilarity.Controls.Add(this.labelCandidates);
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
      this.byImageSimilarity.Size = new System.Drawing.Size(752, 583);
      this.byImageSimilarity.TabIndex = 3;
      this.byImageSimilarity.Text = "By Image Similarity";
      this.byImageSimilarity.UseVisualStyleBackColor = true;
      this.byImageSimilarity.Enter += new System.EventHandler(this.byImageSimilarity_Enter);
      // 
      // btnDumpPreviews
      // 
      this.btnDumpPreviews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDumpPreviews.BackColor = System.Drawing.Color.LightGray;
      this.btnDumpPreviews.Location = new System.Drawing.Point(618, 109);
      this.btnDumpPreviews.Name = "btnDumpPreviews";
      this.btnDumpPreviews.Size = new System.Drawing.Size(128, 23);
      this.btnDumpPreviews.TabIndex = 60;
      this.btnDumpPreviews.Text = "Dump Previews";
      this.btnDumpPreviews.UseVisualStyleBackColor = false;
      this.btnDumpPreviews.Click += new System.EventHandler(this.btnDumpPreviews_Click);
      // 
      // listViewTFC
      // 
      this.listViewTFC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewTFC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.listViewTFC.Location = new System.Drawing.Point(226, 131);
      this.listViewTFC.Name = "listViewTFC";
      this.listViewTFC.Size = new System.Drawing.Size(520, 417);
      this.listViewTFC.TabIndex = 46;
      this.listViewTFC.UseCompatibleStateImageBehavior = false;
      this.listViewTFC.SelectedIndexChanged += new System.EventHandler(this.listViewTFC_SelectedIndexChanged);
      // 
      // checkFileName
      // 
      this.checkFileName.AutoSize = true;
      this.checkFileName.Location = new System.Drawing.Point(10, 426);
      this.checkFileName.Name = "checkFileName";
      this.checkFileName.Size = new System.Drawing.Size(15, 14);
      this.checkFileName.TabIndex = 58;
      this.checkFileName.UseVisualStyleBackColor = true;
      this.checkFileName.CheckedChanged += new System.EventHandler(this.checkFileName_CheckedChanged);
      // 
      // groupFileName
      // 
      this.groupFileName.Controls.Add(this.textNameIncludeFilter);
      this.groupFileName.Controls.Add(this.textNameExcludeFilter);
      this.groupFileName.Controls.Add(this.label6);
      this.groupFileName.Controls.Add(this.label5);
      this.groupFileName.Enabled = false;
      this.groupFileName.Location = new System.Drawing.Point(6, 424);
      this.groupFileName.Name = "groupFileName";
      this.groupFileName.Size = new System.Drawing.Size(214, 124);
      this.groupFileName.TabIndex = 57;
      this.groupFileName.TabStop = false;
      this.groupFileName.Text = "    File Name";
      // 
      // textNameIncludeFilter
      // 
      this.textNameIncludeFilter.Location = new System.Drawing.Point(6, 37);
      this.textNameIncludeFilter.Name = "textNameIncludeFilter";
      this.textNameIncludeFilter.Size = new System.Drawing.Size(202, 23);
      this.textNameIncludeFilter.TabIndex = 1;
      this.textNameIncludeFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNameIncludeFilter_KeyDown);
      this.textNameIncludeFilter.Leave += new System.EventHandler(this.textNameIncludeFilter_Leave);
      // 
      // textNameExcludeFilter
      // 
      this.textNameExcludeFilter.Location = new System.Drawing.Point(6, 81);
      this.textNameExcludeFilter.Name = "textNameExcludeFilter";
      this.textNameExcludeFilter.Size = new System.Drawing.Size(201, 23);
      this.textNameExcludeFilter.TabIndex = 3;
      this.textNameExcludeFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNameExcludeFilter_KeyDown);
      this.textNameExcludeFilter.Leave += new System.EventHandler(this.textNameExcludeFilter_Leave);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 63);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(191, 15);
      this.label6.TabIndex = 2;
      this.label6.Text = "Exclude all TFC Names Containing:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(4, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(202, 15);
      this.label5.TabIndex = 0;
      this.label5.Text = "Only Include TFC Names Containing:";
      // 
      // checkResolution
      // 
      this.checkResolution.AutoSize = true;
      this.checkResolution.Location = new System.Drawing.Point(10, 316);
      this.checkResolution.Name = "checkResolution";
      this.checkResolution.Size = new System.Drawing.Size(15, 14);
      this.checkResolution.TabIndex = 56;
      this.checkResolution.UseVisualStyleBackColor = true;
      this.checkResolution.CheckedChanged += new System.EventHandler(this.checkResolution_CheckedChanged);
      // 
      // checkCookedImage
      // 
      this.checkCookedImage.AutoSize = true;
      this.checkCookedImage.Checked = true;
      this.checkCookedImage.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkCookedImage.Enabled = false;
      this.checkCookedImage.Location = new System.Drawing.Point(10, 126);
      this.checkCookedImage.Name = "checkCookedImage";
      this.checkCookedImage.Size = new System.Drawing.Size(15, 14);
      this.checkCookedImage.TabIndex = 55;
      this.checkCookedImage.UseVisualStyleBackColor = true;
      this.checkCookedImage.CheckedChanged += new System.EventHandler(this.checkCookedImage_CheckedChanged);
      // 
      // groupResolution
      // 
      this.groupResolution.Controls.Add(this.numericFilterResolution);
      this.groupResolution.Controls.Add(this.sliderFilterResolution);
      this.groupResolution.Enabled = false;
      this.groupResolution.Location = new System.Drawing.Point(6, 314);
      this.groupResolution.Name = "groupResolution";
      this.groupResolution.Size = new System.Drawing.Size(214, 104);
      this.groupResolution.TabIndex = 54;
      this.groupResolution.TabStop = false;
      this.groupResolution.Text = "    Resolution";
      // 
      // numericFilterResolution
      // 
      this.numericFilterResolution.Location = new System.Drawing.Point(6, 73);
      this.numericFilterResolution.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.numericFilterResolution.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.numericFilterResolution.Name = "numericFilterResolution";
      this.numericFilterResolution.Size = new System.Drawing.Size(202, 23);
      this.numericFilterResolution.TabIndex = 56;
      this.numericFilterResolution.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.numericFilterResolution.ValueChanged += new System.EventHandler(this.numericFilterResolution_ValueChanged);
      // 
      // sliderFilterResolution
      // 
      this.sliderFilterResolution.LargeChange = 1;
      this.sliderFilterResolution.Location = new System.Drawing.Point(6, 22);
      this.sliderFilterResolution.Maximum = 6;
      this.sliderFilterResolution.Name = "sliderFilterResolution";
      this.sliderFilterResolution.Size = new System.Drawing.Size(202, 45);
      this.sliderFilterResolution.TabIndex = 0;
      this.sliderFilterResolution.Scroll += new System.EventHandler(this.sliderFilterResolution_Scroll);
      // 
      // groupCookedImage
      // 
      this.groupCookedImage.Controls.Add(this.label4);
      this.groupCookedImage.Controls.Add(this.numericNoCandidates);
      this.groupCookedImage.Controls.Add(this.pictureCooked);
      this.groupCookedImage.Controls.Add(this.btnCookedFilterBrowse);
      this.groupCookedImage.Controls.Add(this.textCookedFilter);
      this.groupCookedImage.Location = new System.Drawing.Point(6, 124);
      this.groupCookedImage.Name = "groupCookedImage";
      this.groupCookedImage.Size = new System.Drawing.Size(214, 184);
      this.groupCookedImage.TabIndex = 53;
      this.groupCookedImage.TabStop = false;
      this.groupCookedImage.Text = "    Cooked Image";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 59);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(66, 15);
      this.label4.TabIndex = 54;
      this.label4.Text = "No. Results";
      // 
      // numericNoCandidates
      // 
      this.numericNoCandidates.Location = new System.Drawing.Point(6, 77);
      this.numericNoCandidates.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numericNoCandidates.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericNoCandidates.Name = "numericNoCandidates";
      this.numericNoCandidates.Size = new System.Drawing.Size(67, 23);
      this.numericNoCandidates.TabIndex = 53;
      this.numericNoCandidates.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numericNoCandidates.ValueChanged += new System.EventHandler(this.numericNoCandidates_ValueChanged);
      // 
      // pictureCooked
      // 
      this.pictureCooked.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pictureCooked.Location = new System.Drawing.Point(79, 22);
      this.pictureCooked.Name = "pictureCooked";
      this.pictureCooked.Size = new System.Drawing.Size(128, 128);
      this.pictureCooked.TabIndex = 48;
      this.pictureCooked.TabStop = false;
      // 
      // btnCookedFilterBrowse
      // 
      this.btnCookedFilterBrowse.Location = new System.Drawing.Point(6, 126);
      this.btnCookedFilterBrowse.Name = "btnCookedFilterBrowse";
      this.btnCookedFilterBrowse.Size = new System.Drawing.Size(67, 23);
      this.btnCookedFilterBrowse.TabIndex = 52;
      this.btnCookedFilterBrowse.Text = "Select File";
      this.btnCookedFilterBrowse.UseVisualStyleBackColor = true;
      this.btnCookedFilterBrowse.Click += new System.EventHandler(this.btnCookedFilterBrowse_Click);
      // 
      // textCookedFilter
      // 
      this.textCookedFilter.Location = new System.Drawing.Point(6, 155);
      this.textCookedFilter.Name = "textCookedFilter";
      this.textCookedFilter.ReadOnly = true;
      this.textCookedFilter.Size = new System.Drawing.Size(201, 23);
      this.textCookedFilter.TabIndex = 51;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 106);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(38, 15);
      this.label3.TabIndex = 47;
      this.label3.Text = "Filters";
      // 
      // labelCandidates
      // 
      this.labelCandidates.AutoSize = true;
      this.labelCandidates.Location = new System.Drawing.Point(226, 114);
      this.labelCandidates.Name = "labelCandidates";
      this.labelCandidates.Size = new System.Drawing.Size(66, 15);
      this.labelCandidates.TabIndex = 45;
      this.labelCandidates.Text = "Candidates";
      // 
      // labelSelectFileHashCooked
      // 
      this.labelSelectFileHashCooked.AutoSize = true;
      this.labelSelectFileHashCooked.Location = new System.Drawing.Point(130, 24);
      this.labelSelectFileHashCooked.Name = "labelSelectFileHashCooked";
      this.labelSelectFileHashCooked.Size = new System.Drawing.Size(89, 15);
      this.labelSelectFileHashCooked.TabIndex = 37;
      this.labelSelectFileHashCooked.Text = "Cooked Hashes";
      this.labelSelectFileHashCooked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textSelectFileHashCooked
      // 
      this.textSelectFileHashCooked.Location = new System.Drawing.Point(226, 21);
      this.textSelectFileHashCooked.Name = "textSelectFileHashCooked";
      this.textSelectFileHashCooked.ReadOnly = true;
      this.textSelectFileHashCooked.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.textSelectFileHashCooked.Size = new System.Drawing.Size(226, 23);
      this.textSelectFileHashCooked.TabIndex = 34;
      // 
      // btnSelectFileHashCooked
      // 
      this.btnSelectFileHashCooked.Location = new System.Drawing.Point(458, 20);
      this.btnSelectFileHashCooked.Name = "btnSelectFileHashCooked";
      this.btnSelectFileHashCooked.Size = new System.Drawing.Size(93, 23);
      this.btnSelectFileHashCooked.TabIndex = 39;
      this.btnSelectFileHashCooked.Text = "Select File";
      this.btnSelectFileHashCooked.UseVisualStyleBackColor = true;
      this.btnSelectFileHashCooked.Click += new System.EventHandler(this.btnSelectFileHashCooked_Click);
      // 
      // btnGenerateHashCooked
      // 
      this.btnGenerateHashCooked.Location = new System.Drawing.Point(557, 20);
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
      this.labelSelectFileHashTFC.Location = new System.Drawing.Point(152, 53);
      this.labelSelectFileHashTFC.Name = "labelSelectFileHashTFC";
      this.labelSelectFileHashTFC.Size = new System.Drawing.Size(68, 15);
      this.labelSelectFileHashTFC.TabIndex = 40;
      this.labelSelectFileHashTFC.Text = "TFC Hashes";
      this.labelSelectFileHashTFC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnGenerateHashTFC
      // 
      this.btnGenerateHashTFC.Location = new System.Drawing.Point(557, 50);
      this.btnGenerateHashTFC.Name = "btnGenerateHashTFC";
      this.btnGenerateHashTFC.Size = new System.Drawing.Size(170, 23);
      this.btnGenerateHashTFC.TabIndex = 41;
      this.btnGenerateHashTFC.Text = "Generate TFC Hashes";
      this.btnGenerateHashTFC.UseVisualStyleBackColor = true;
      this.btnGenerateHashTFC.Click += new System.EventHandler(this.btnGenerateHashTFC_Click);
      // 
      // textSelectFileHashTFC
      // 
      this.textSelectFileHashTFC.Location = new System.Drawing.Point(226, 50);
      this.textSelectFileHashTFC.Name = "textSelectFileHashTFC";
      this.textSelectFileHashTFC.ReadOnly = true;
      this.textSelectFileHashTFC.Size = new System.Drawing.Size(226, 23);
      this.textSelectFileHashTFC.TabIndex = 38;
      // 
      // btnSelectFileHashTFC
      // 
      this.btnSelectFileHashTFC.Location = new System.Drawing.Point(458, 49);
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
      this.labelSelectDirectoryCookedReference.Location = new System.Drawing.Point(66, 82);
      this.labelSelectDirectoryCookedReference.Name = "labelSelectDirectoryCookedReference";
      this.labelSelectDirectoryCookedReference.Size = new System.Drawing.Size(154, 15);
      this.labelSelectDirectoryCookedReference.TabIndex = 44;
      this.labelSelectDirectoryCookedReference.Text = "Cooked Reference Directory";
      this.labelSelectDirectoryCookedReference.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // textSelectDirectoryCookedReference
      // 
      this.textSelectDirectoryCookedReference.Location = new System.Drawing.Point(226, 79);
      this.textSelectDirectoryCookedReference.Name = "textSelectDirectoryCookedReference";
      this.textSelectDirectoryCookedReference.ReadOnly = true;
      this.textSelectDirectoryCookedReference.Size = new System.Drawing.Size(226, 23);
      this.textSelectDirectoryCookedReference.TabIndex = 43;
      // 
      // btnSelectDirectoryCookedReference
      // 
      this.btnSelectDirectoryCookedReference.Location = new System.Drawing.Point(458, 78);
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
      this.label2.Size = new System.Drawing.Size(403, 15);
      this.label2.TabIndex = 12;
      this.label2.Text = "Finds the TFC selector based off a Cooked Texture (as Extracted by UModel)";
      // 
      // textPreviewImageSimilarity
      // 
      this.textPreviewImageSimilarity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textPreviewImageSimilarity.Location = new System.Drawing.Point(6, 554);
      this.textPreviewImageSimilarity.Name = "textPreviewImageSimilarity";
      this.textPreviewImageSimilarity.ReadOnly = true;
      this.textPreviewImageSimilarity.Size = new System.Drawing.Size(633, 23);
      this.textPreviewImageSimilarity.TabIndex = 10;
      // 
      // btnSubmitImageSimilarity
      // 
      this.btnSubmitImageSimilarity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSubmitImageSimilarity.Location = new System.Drawing.Point(645, 554);
      this.btnSubmitImageSimilarity.Name = "btnSubmitImageSimilarity";
      this.btnSubmitImageSimilarity.Size = new System.Drawing.Size(101, 23);
      this.btnSubmitImageSimilarity.TabIndex = 11;
      this.btnSubmitImageSimilarity.Text = "Create Filter";
      this.btnSubmitImageSimilarity.UseVisualStyleBackColor = true;
      this.btnSubmitImageSimilarity.Click += new System.EventHandler(this.btnSubmitImageSimilarity_Click);
      // 
      // openFileDialogSelectFileHashCooked
      // 
      this.openFileDialogSelectFileHashCooked.DefaultExt = "json";
      this.openFileDialogSelectFileHashCooked.Filter = "json files (*.json)|*.json";
      this.openFileDialogSelectFileHashCooked.RestoreDirectory = true;
      this.openFileDialogSelectFileHashCooked.Title = "Select Cooked Hashes Json File";
      // 
      // openFileDialogSelectFileHashTFC
      // 
      this.openFileDialogSelectFileHashTFC.DefaultExt = "json";
      this.openFileDialogSelectFileHashTFC.Filter = "json files (*.json)|*.json";
      this.openFileDialogSelectFileHashTFC.Title = "Select TFC Hashes Json File";
      // 
      // imgListTFC
      // 
      this.imgListTFC.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imgListTFC.ImageSize = new System.Drawing.Size(16, 16);
      this.imgListTFC.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // openFileDialogCookedFilter
      // 
      this.openFileDialogCookedFilter.DefaultExt = "png";
      this.openFileDialogCookedFilter.Filter = "png files (*.png)|*.png";
      this.openFileDialogCookedFilter.RestoreDirectory = true;
      this.openFileDialogCookedFilter.Title = "Select Cooked Image Png";
      // 
      // SelectorWizard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 635);
      this.Controls.Add(this.tabControl1);
      this.Name = "SelectorWizard";
      this.Text = "Selector Wizard";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectorWizard_FormClosing);
      this.tabControl1.ResumeLayout(false);
      this.all.ResumeLayout(false);
      this.all.PerformLayout();
      this.filename.ResumeLayout(false);
      this.filename.PerformLayout();
      this.filenameAndId.ResumeLayout(false);
      this.filenameAndId.PerformLayout();
      this.byImageSimilarity.ResumeLayout(false);
      this.byImageSimilarity.PerformLayout();
      this.groupFileName.ResumeLayout(false);
      this.groupFileName.PerformLayout();
      this.groupResolution.ResumeLayout(false);
      this.groupResolution.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericFilterResolution)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sliderFilterResolution)).EndInit();
      this.groupCookedImage.ResumeLayout(false);
      this.groupCookedImage.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericNoCandidates)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureCooked)).EndInit();
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
    private Label label3;
    private ListView listViewTFC;
    private Label labelCandidates;
    private ImageList imgListTFC;
    private GroupBox groupResolution;
    private TrackBar sliderFilterResolution;
    private GroupBox groupCookedImage;
    private PictureBox pictureCooked;
    private Button btnCookedFilterBrowse;
    private TextBox textCookedFilter;
    private CheckBox checkResolution;
    private CheckBox checkCookedImage;
    private NumericUpDown numericFilterResolution;
    private CheckBox checkFileName;
    private GroupBox groupFileName;
    private OpenFileDialog openFileDialogCookedFilter;
    private Button btnDumpPreviews;
    private Label label4;
    private NumericUpDown numericNoCandidates;
    private TextBox textNameIncludeFilter;
    private Label label5;
    private TextBox textNameExcludeFilter;
    private Label label6;
  }
}