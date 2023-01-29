using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paladins_tfc.src.gui {
  public partial class ConfirmDialog : Form {
    public ConfirmDialog(string[] content) {
      InitializeComponent();
      rtextPreview.Lines = content;
    }

    private void btnConfirm_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) {
      this.Close();
    }
  }
}
