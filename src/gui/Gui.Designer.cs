using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;

namespace PaladinsTfc
{
  partial class Gui
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
      this.labelSelectDirectoryInput = new System.Windows.Forms.Label();
      this.btnSelectDirectoryInput = new System.Windows.Forms.Button();
      this.btnOperationAdd = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.btnOperationSave = new System.Windows.Forms.Button();
      this.labelOperations = new System.Windows.Forms.Label();
      this.btnOperationLoad = new System.Windows.Forms.Button();
      this.textSelectDirectoryInput = new System.Windows.Forms.TextBox();
      this.labelSelectDirectoryOutput = new System.Windows.Forms.Label();
      this.btnSelectDirectoryOutput = new System.Windows.Forms.Button();
      this.richTextLog = new System.Windows.Forms.RichTextBox();
      this.textSelectDirectoryOutput = new System.Windows.Forms.TextBox();
      this.labelRun = new System.Windows.Forms.Label();
      this.btnOperationDuplicate = new System.Windows.Forms.Button();
      this.ddGrid = new System.Windows.Forms.DataGridView();
      this.labelLog = new System.Windows.Forms.Label();
      this.btnOperationDelete = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.ddGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // labelSelectDirectoryInput
      // 
      this.labelSelectDirectoryInput.AutoSize = true;
      this.labelSelectDirectoryInput.Location = new System.Drawing.Point(87, 16);
      this.labelSelectDirectoryInput.Name = "labelSelectDirectoryInput";
      this.labelSelectDirectoryInput.Size = new System.Drawing.Size(109, 15);
      this.labelSelectDirectoryInput.TabIndex = 7;
      this.labelSelectDirectoryInput.Text = "TFC Input Directory";
      this.labelSelectDirectoryInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnSelectDirectoryInput
      // 
      this.btnSelectDirectoryInput.Location = new System.Drawing.Point(434, 11);
      this.btnSelectDirectoryInput.Name = "btnSelectDirectoryInput";
      this.btnSelectDirectoryInput.Size = new System.Drawing.Size(93, 23);
      this.btnSelectDirectoryInput.TabIndex = 9;
      this.btnSelectDirectoryInput.Text = "Select Folder";
      this.btnSelectDirectoryInput.UseVisualStyleBackColor = true;
      this.btnSelectDirectoryInput.Click += new System.EventHandler(this.btnSelectDirectoryInput_Click);
      // 
      // btnOperationAdd
      // 
      this.btnOperationAdd.Location = new System.Drawing.Point(11, 88);
      this.btnOperationAdd.Name = "btnOperationAdd";
      this.btnOperationAdd.Size = new System.Drawing.Size(184, 23);
      this.btnOperationAdd.TabIndex = 11;
      this.btnOperationAdd.Text = "Add Operation";
      this.btnOperationAdd.UseVisualStyleBackColor = true;
      // 
      // btnRun
      // 
      this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnRun.Location = new System.Drawing.Point(12, 675);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(184, 23);
      this.btnRun.TabIndex = 12;
      this.btnRun.Text = "Run Operations";
      this.btnRun.UseVisualStyleBackColor = true;
      // 
      // btnOperationSave
      // 
      this.btnOperationSave.Location = new System.Drawing.Point(11, 195);
      this.btnOperationSave.Name = "btnOperationSave";
      this.btnOperationSave.Size = new System.Drawing.Size(184, 23);
      this.btnOperationSave.TabIndex = 14;
      this.btnOperationSave.Text = "Save Operations";
      this.btnOperationSave.UseVisualStyleBackColor = true;
      // 
      // labelOperations
      // 
      this.labelOperations.AutoSize = true;
      this.labelOperations.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.labelOperations.Location = new System.Drawing.Point(12, 70);
      this.labelOperations.Name = "labelOperations";
      this.labelOperations.Size = new System.Drawing.Size(65, 15);
      this.labelOperations.TabIndex = 15;
      this.labelOperations.Text = "Operations";
      this.labelOperations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnOperationLoad
      // 
      this.btnOperationLoad.Location = new System.Drawing.Point(11, 224);
      this.btnOperationLoad.Name = "btnOperationLoad";
      this.btnOperationLoad.Size = new System.Drawing.Size(184, 23);
      this.btnOperationLoad.TabIndex = 16;
      this.btnOperationLoad.Text = "Load Operations";
      this.btnOperationLoad.UseVisualStyleBackColor = true;
      // 
      // textSelectDirectoryInput
      // 
      this.textSelectDirectoryInput.Location = new System.Drawing.Point(202, 12);
      this.textSelectDirectoryInput.Name = "textSelectDirectoryInput";
      this.textSelectDirectoryInput.ReadOnly = true;
      this.textSelectDirectoryInput.Size = new System.Drawing.Size(226, 23);
      this.textSelectDirectoryInput.TabIndex = 18;
      this.textSelectDirectoryInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // labelSelectDirectoryOutput
      // 
      this.labelSelectDirectoryOutput.AutoSize = true;
      this.labelSelectDirectoryOutput.Location = new System.Drawing.Point(100, 45);
      this.labelSelectDirectoryOutput.Name = "labelSelectDirectoryOutput";
      this.labelSelectDirectoryOutput.Size = new System.Drawing.Size(96, 15);
      this.labelSelectDirectoryOutput.TabIndex = 19;
      this.labelSelectDirectoryOutput.Text = "Output Directory";
      this.labelSelectDirectoryOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnSelectDirectoryOutput
      // 
      this.btnSelectDirectoryOutput.Location = new System.Drawing.Point(434, 41);
      this.btnSelectDirectoryOutput.Name = "btnSelectDirectoryOutput";
      this.btnSelectDirectoryOutput.Size = new System.Drawing.Size(93, 23);
      this.btnSelectDirectoryOutput.TabIndex = 20;
      this.btnSelectDirectoryOutput.Text = "Select Folder";
      this.btnSelectDirectoryOutput.UseVisualStyleBackColor = true;
      this.btnSelectDirectoryOutput.Click += new System.EventHandler(this.btnSelectDirectoryOutput_Click);
      // 
      // richTextLog
      // 
      this.richTextLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.richTextLog.Location = new System.Drawing.Point(12, 364);
      this.richTextLog.Name = "richTextLog";
      this.richTextLog.ReadOnly = true;
      this.richTextLog.Size = new System.Drawing.Size(183, 290);
      this.richTextLog.TabIndex = 21;
      this.richTextLog.Text = "";
      // 
      // textSelectDirectoryOutput
      // 
      this.textSelectDirectoryOutput.Location = new System.Drawing.Point(202, 41);
      this.textSelectDirectoryOutput.Name = "textSelectDirectoryOutput";
      this.textSelectDirectoryOutput.ReadOnly = true;
      this.textSelectDirectoryOutput.Size = new System.Drawing.Size(226, 23);
      this.textSelectDirectoryOutput.TabIndex = 22;
      this.textSelectDirectoryOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // labelRun
      // 
      this.labelRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.labelRun.AutoSize = true;
      this.labelRun.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.labelRun.Location = new System.Drawing.Point(12, 657);
      this.labelRun.Name = "labelRun";
      this.labelRun.Size = new System.Drawing.Size(84, 15);
      this.labelRun.TabIndex = 24;
      this.labelRun.Text = "Sheduled : X/X";
      // 
      // btnOperationDuplicate
      // 
      this.btnOperationDuplicate.Location = new System.Drawing.Point(11, 117);
      this.btnOperationDuplicate.Name = "btnOperationDuplicate";
      this.btnOperationDuplicate.Size = new System.Drawing.Size(184, 23);
      this.btnOperationDuplicate.TabIndex = 27;
      this.btnOperationDuplicate.Text = "Duplicate Operation";
      this.btnOperationDuplicate.UseVisualStyleBackColor = true;
      // 
      // ddGrid
      // 
      this.ddGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ddGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ddGrid.Location = new System.Drawing.Point(202, 70);
      this.ddGrid.Name = "ddGrid";
      this.ddGrid.RowTemplate.Height = 25;
      this.ddGrid.Size = new System.Drawing.Size(813, 628);
      this.ddGrid.TabIndex = 28;
      this.ddGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ddGrid_CellContentClick);
      // 
      // labelLog
      // 
      this.labelLog.AutoSize = true;
      this.labelLog.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.labelLog.Location = new System.Drawing.Point(11, 346);
      this.labelLog.Name = "labelLog";
      this.labelLog.Size = new System.Drawing.Size(27, 15);
      this.labelLog.TabIndex = 29;
      this.labelLog.Text = "Log";
      this.labelLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnOperationDelete
      // 
      this.btnOperationDelete.Location = new System.Drawing.Point(11, 146);
      this.btnOperationDelete.Name = "btnOperationDelete";
      this.btnOperationDelete.Size = new System.Drawing.Size(184, 23);
      this.btnOperationDelete.TabIndex = 30;
      this.btnOperationDelete.Text = "Delete Operation";
      this.btnOperationDelete.UseVisualStyleBackColor = true;
      // 
      // Gui
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1027, 710);
      this.Controls.Add(this.btnOperationAdd);
      this.Controls.Add(this.labelSelectDirectoryInput);
      this.Controls.Add(this.textSelectDirectoryInput);
      this.Controls.Add(this.btnSelectDirectoryInput);
      this.Controls.Add(this.labelSelectDirectoryOutput);
      this.Controls.Add(this.textSelectDirectoryOutput);
      this.Controls.Add(this.btnSelectDirectoryOutput);
      this.Controls.Add(this.labelOperations);
      this.Controls.Add(this.btnOperationDuplicate);
      this.Controls.Add(this.btnOperationDelete);
      this.Controls.Add(this.btnOperationSave);
      this.Controls.Add(this.btnOperationLoad);
      this.Controls.Add(this.ddGrid);
      this.Controls.Add(this.labelLog);
      this.Controls.Add(this.richTextLog);
      this.Controls.Add(this.labelRun);
      this.Controls.Add(this.btnRun);
      this.Name = "Gui";
      this.Text = "TFC editor";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Gui_FormClosing);
      this.Load += new System.EventHandler(this.Gui_Load);
      ((System.ComponentModel.ISupportInitialize)(this.ddGrid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private Label labelSelectDirectoryInput;
    private Button btnSelectDirectoryInput;
    private Button btnOperationAdd;
    private Button btnRun;
    private Button btnOperationSave;
    private Label labelOperations;
    private Button btnOperationLoad;
    private TextBox textSelectDirectoryInput;
    private Label labelSelectDirectoryOutput;
    private Button btnSelectDirectoryOutput;
    private RichTextBox richTextLog;
    private TextBox textSelectDirectoryOutput;
    private Label labelRun;
    private Button btnOperationDuplicate;
    private DataGridView ddGrid;
    private Label labelLog;
    private Button btnOperationDelete;
    private CommonOpenFileDialog openFileDialogSelectDirectoryInput;
    private CommonOpenFileDialog openFileDialogSelectDirectoryOutput;
  }
}