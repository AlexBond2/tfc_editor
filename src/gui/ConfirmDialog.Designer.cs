namespace paladins_tfc.src.gui {
  partial class ConfirmDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.rtextPreview = new System.Windows.Forms.RichTextBox();
      this.btnConfirm = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // rtextPreview
      // 
      this.rtextPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rtextPreview.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.rtextPreview.Location = new System.Drawing.Point(12, 27);
      this.rtextPreview.Name = "rtextPreview";
      this.rtextPreview.Size = new System.Drawing.Size(953, 670);
      this.rtextPreview.TabIndex = 0;
      this.rtextPreview.Text = "";
      this.rtextPreview.WordWrap = false;
      // 
      // btnConfirm
      // 
      this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnConfirm.Location = new System.Drawing.Point(841, 703);
      this.btnConfirm.Name = "btnConfirm";
      this.btnConfirm.Size = new System.Drawing.Size(124, 23);
      this.btnConfirm.TabIndex = 1;
      this.btnConfirm.Text = "Yes, Run Them";
      this.btnConfirm.UseVisualStyleBackColor = true;
      this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(711, 703);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(124, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "No, Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(306, 15);
      this.label1.TabIndex = 3;
      this.label1.Text = "You are about to run the following commands. Confirm?";
      // 
      // ConfirmDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(977, 738);
      this.Controls.Add(this.rtextPreview);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnConfirm);
      this.Name = "ConfirmDialog";
      this.Text = "ConfirmDialog";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RichTextBox rtextPreview;
    private Button btnConfirm;
    private Button btnCancel;
    private Label label1;
  }
}