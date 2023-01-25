using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaladinsTfcExtend;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;

namespace paladins_tfc.src.gui {
  public partial class SelectorWizard : Form {
    Color failColor = Color.FromArgb(255, Color.FromArgb(0xfb9b9b));
    string TFCdir;
    public SelectorWizard(string TFCdir) {
      this.TFCdir = TFCdir;
      InitializeComponent();
      openFileDialogSelectDirectoryCookedReference = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      openFileDialogSelectDirectoryCookedReference.IsFolderPicker = true;
    }

    public bool makeRedIfFailPath(string s, TextBox tb) {
      string failString = null;
      if (string.IsNullOrEmpty(s)) {
        failString = "No TFC file selected";
      } else if (Path.GetExtension(s).ToLower() != ".tfc") {
        failString = $"Not a tfc file \"{s}\"";
      } else if (s.Contains(" ")) {
        failString = $"The tfc file may not include spaces \"{s}\"";
      }
      if (!string.IsNullOrEmpty(failString)) {
        tb.BackColor = failColor;
        tb.Text = failString;
        return true;
      }
      return false;
    }

    private void invalidateFileName() {
      if (makeRedIfFailPath(textFilenameName.Text, textPreviewFilename)) {
        return;
      }

      textPreviewFilename.BackColor = default(Color);
      textPreviewFilename.Text = textFilenameAndIdName.Text;
    }

    private void invalidateFileNameAndId() {
      if (makeRedIfFailPath(textFilenameAndIdName.Text, textPreviewFilenameAndId)) {
        return;
      }

      List<string> selectorPattern = new List<string>();
      int failLine = -1;
      string failMessage = "";
      foreach (var item in richTextBoxIdSelect.Lines.Select((value, i) => new { i, value })) {
        string line = item.value;
        int index = item.i;

        try {
          int id = int.Parse(line);
          selectorPattern.Add(id.ToString());
          continue;
        } catch (Exception _) { }
        try {
          var nums = line.Split('-');
          if (nums.Length == 2) {
            int low = int.Parse(nums[0]);
            int high = int.Parse(nums[1]);
            if (low < high) {
              selectorPattern.Add($"{low}-{high}");
              continue;
            } else {
              failLine = index;
              failMessage = $"First number must be smaller than second number";
              break;
            }
          }
        } catch (Exception _) { }
        failLine = index;
        failMessage = "Not an integer";
        break;
      }

      if (failLine >= 0) {
        textPreviewFilenameAndId.BackColor = Color.FromArgb(255, Color.FromArgb(0xfb9b9b));
        string linePreview = richTextBoxIdSelect.Lines[failLine].Truncate(20);
        textPreviewFilenameAndId.Text = $"Error on line: {failLine + 1} ({failMessage}), \"{linePreview}\"";
        return;
      }

      textPreviewFilenameAndId.BackColor = default(Color);
      string idSelector = "";
      if (selectorPattern.Count >= 1) {
        idSelector = "#" + string.Join(",", selectorPattern.ToArray());
      }
      textPreviewFilenameAndId.Text = textFilenameAndIdName.Text + idSelector;
    }

    private void invalidateByImageSimilarity() {
      if (this.Width < 800) {
        this.Width = 800;
      }
      textPreviewImageSimilarity.BackColor = failColor;
    }

    private void btnFilenameAndIdNameBrowse_Click(object sender, EventArgs e) {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.DefaultExt = "tfc";
      dialog.Filter = "tfc files (*.tfc)|*.tfc";
      dialog.InitialDirectory = TFCdir;
      if (dialog.ShowDialog() == DialogResult.OK) {
        string relPath = Path.GetRelativePath(TFCdir, dialog.FileName);
        textFilenameAndIdName.Text = relPath;
        textFilenameAndIdName.cursorToRight();
      }
    }

    private void btnFilenameNameBrowse_Click(object sender, EventArgs e) {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.DefaultExt = "tfc";
      dialog.Filter = "tfc files (*.tfc)|*.tfc";
      dialog.InitialDirectory = TFCdir;
      if (dialog.ShowDialog() == DialogResult.OK) {
        string relPath = Path.GetRelativePath(TFCdir, dialog.FileName);
        textFilenameName.Text = relPath;
        textFilenameAndIdName.cursorToRight();
      }
    }

    private void all_Enter(object sender, EventArgs e) {
    }

    private void filename_Enter(object sender, EventArgs e) {
      invalidateFileName();
    }
    private void textFilenameName_TextChanged(object sender, EventArgs e) {
      invalidateFileName();
    }

    private void filenameAndId_Enter(object sender, EventArgs e) {
      invalidateFileNameAndId();
    }
    private void textFilenameAndIdName_TextChanged(object sender, EventArgs e) {
      invalidateFileNameAndId();
    }
    private void richTextBoxIdSelect_TextChanged(object sender, EventArgs e) {
      invalidateFileNameAndId();
    }

    private void byImageSimilarity_Enter(object sender, EventArgs e) {
      invalidateByImageSimilarity();
    }

    private void btnSelectFileHashCooked_Click(object sender, EventArgs e) {
      if (openFileDialogSelectFileHashCooked.ShowDialog() == DialogResult.OK) {
        textSelectFileHashCooked.Text = openFileDialogSelectFileHashCooked.FileName;
        textSelectFileHashCooked.cursorToRight();
      }
    }

    private void btnSelectFileHashTFC_Click(object sender, EventArgs e) {
      if (openFileDialogSelectFileHashTFC.ShowDialog() == DialogResult.OK) {
        textSelectFileHashTFC.Text = openFileDialogSelectFileHashTFC.FileName;
        textSelectFileHashTFC.cursorToRight();
      }
    }

    private void btnSelectDirectoryCookedReference_Click(object sender, EventArgs e) {
      if (openFileDialogSelectDirectoryCookedReference.ShowDialog() == CommonFileDialogResult.Ok) {
        textSelectDirectoryCookedReference.Text = openFileDialogSelectDirectoryCookedReference.FileName;
        textSelectDirectoryCookedReference.cursorToRight();
      }
    }

    private void btnGenerateHashCooked_Click(object sender, EventArgs e) {

    }

    private void btnGenerateHashTFC_Click(object sender, EventArgs e) {

    }
  }
}
