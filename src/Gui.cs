using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaladinsTfc
{  
  public partial class Gui : Form
  {

    int colOfSelectorGenerator;

    BindingList<Operation> operationList;

    public Gui(){
      InitializeComponent();

      operationList = new BindingList<Operation>();
      operationList.Add(new Operation(SelectorType.FileAndId, "x.tfc#1-5", OperationType.Dump, null));
      operationList.Add(new Operation(SelectorType.FileAndId, "y.tfc#1-7", OperationType.Dump, null));
      
      /*
      DataGridViewButtonColumn ddcolDelete = new DataGridViewButtonColumn();
      ddcolDelete.HeaderText = "";
      ddcolDelete.Text = "";
      ddcolDelete.ToolTipText = "Delete";
      ddcolDelete.UseColumnTextForButtonValue = true;
      ddcolDelete.Width = 25;
      ddcolDelete.Resizable = DataGridViewTriState.False;
      ddcolDelete.DefaultCellStyle.BackColor = Color.Red;
      ddcolDelete.DefaultCellStyle.ForeColor = Color.Red;*/

      DataGridViewComboBoxColumn ddcolSelectorType = new DataGridViewComboBoxColumn();
      ddcolSelectorType.HeaderText = "Selector Type";
      ddcolSelectorType.DataSource = new List<string>(){SelectorType.FileAndId, SelectorType.File, SelectorType.All};

      DataGridViewTextBoxColumn ddcolSelector = new DataGridViewTextBoxColumn();
      ddcolSelector.HeaderText = "Selector";

      DataGridViewButtonColumn ddcolSelectorGenerate = new DataGridViewButtonColumn();
      ddcolSelectorGenerate.HeaderText = "";
      ddcolSelectorGenerate.Text = "<-";
      ddcolSelectorGenerate.ToolTipText = "Generate Selector from Source Image";
      ddcolSelectorGenerate.UseColumnTextForButtonValue = true;
      ddcolSelectorGenerate.Width = 25;
      ddcolSelectorGenerate.Resizable = DataGridViewTriState.False;

      DataGridViewComboBoxColumn ddcolOperationType = new DataGridViewComboBoxColumn();
      ddcolOperationType.HeaderText = "Operation Type";
      ddcolOperationType.DataSource = new List<string>(){OperationType.Dump, OperationType.Replace};

      DataGridViewTextBoxColumn ddcolReplacement = new DataGridViewTextBoxColumn();
      ddcolReplacement.HeaderText = "Replacement";

      ddGrid.RowTemplate.MinimumHeight = 25;
      ddGrid.EditMode = DataGridViewEditMode.EditOnEnter;
      var cols = new List<DataGridViewColumn>(){ddcolSelectorType, ddcolSelector, ddcolSelectorGenerate, ddcolOperationType, ddcolReplacement};
      colOfSelectorGenerator = cols.IndexOf(ddcolSelectorGenerate);
      ddGrid.Columns.AddRange(cols.ToArray());
      ddGrid.DataSource = operationList;
      (ddGrid.DataSource as BindingList<Operation>).Add(new Operation(SelectorType.FileAndId, "z.tfc#10-11", OperationType.Dump, null));

      /*
      //ddataGridViewOperation.Rows.Add(null, null, new Operation(SelectorType.FileAndId, "x.tfc#1-5", OperationType.Dump, null));
      
      
      ddataColumnSelectorType.Items.AddRange(SelectorType.FileAndId, SelectorType.File, SelectorType.All);
      ddataColumnOperation.Items.AddRange(OperationType.Dump, OperationType.Replace);
      
      ddataColumnDelete.Width = 26;
      ddataColumnDelete.Resizable = DataGridViewTriState.False;
      ddataColumnEdit.Width = 26;
      ddataColumnEdit.Resizable = DataGridViewTriState.False;*/
    }

    private void Gui_Load(object sender, EventArgs e){
    }

    private void ddGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      if (e.ColumnIndex == colOfSelectorGenerator) {
        MessageBox.Show("TODO","Generate Selector");
      }
      /*var editingControl = this.ddGrid.EditingControl as DataGridViewComboBoxEditingControl;
      if (editingControl != null)
        editingControl.DroppedDown = true;*/
    }
  }

  public static class SelectorType {
    public const string FileAndId = "File#Id";
    public const string File = "File";
    public const string All = "All";
  }
  public static class OperationType {
    public const string Dump = "Dump";
    public const string Replace = "Replace";
  }
  public class Operation {
    public string buttonDelete = "";
    public string buttonEdit = "";
    public string selectorType;
    public string selector;
    public string operation;
    public string replacementPath;

    public Operation(string selectorType, string selector, string operation, string replacementPath) {
      this.selectorType = selectorType;
      this.selector = selector;
      this.operation = operation;
      this.replacementPath = replacementPath;
    }
  }
}

