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
    int colOfReplacementGenerator;

    BindingList<SettingsRow> operationList;

    public Gui(){
      InitializeComponent();

      operationList = new BindingList<SettingsRow>();
      operationList.Add(new SettingsRow(SelectOptionSelectorType.FileAndId, "x.tfc#1-5", SelectOptionOperationType.Dump, null));
      operationList.Add(new SettingsRow(SelectOptionSelectorType.FileAndId, "y.tfc#1-7", SelectOptionOperationType.Dump, null));

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

      DataGridViewCheckBoxColumn ddcolValid = new DataGridViewCheckBoxColumn();
      ddcolValid.HeaderText = "✔";
      ddcolValid.DataPropertyName = "Valid";
      ddcolValid.Width = 25;
      ddcolValid.ReadOnly = true;
      ddcolValid.Resizable = DataGridViewTriState.False;
      ddcolValid.DisplayIndex = 0;

      DataGridViewComboBoxColumn ddcolSelectorType = new DataGridViewComboBoxColumn();
      ddcolSelectorType.HeaderText = "Selector Type";
      ddcolSelectorType.DataPropertyName = "SelectorType";
      ddcolSelectorType.DataSource = new List<string>(){SelectOptionSelectorType.FileAndId, SelectOptionSelectorType.FileAndIdRange, SelectOptionSelectorType.File, SelectOptionSelectorType.All};
      ddcolSelectorType.DisplayIndex = 1;

      DataGridViewTextBoxColumn ddcolSelector = new DataGridViewTextBoxColumn();
      ddcolSelector.HeaderText = "Selector";
      ddcolSelector.DataPropertyName = "Selector";
      ddcolSelector.DisplayIndex = 2;

      DataGridViewButtonColumn ddcolSelectorGenerate = new DataGridViewButtonColumn();
      ddcolSelectorGenerate.HeaderText = "";
      ddcolSelectorGenerate.Text = "…";
      ddcolSelectorGenerate.ToolTipText = "Generate Selector from Source Image";
      ddcolSelectorGenerate.UseColumnTextForButtonValue = true;
      ddcolSelectorGenerate.Width = 20;
      ddcolSelectorGenerate.Resizable = DataGridViewTriState.False;
      ddcolSelectorGenerate.DisplayIndex = 3;

      DataGridViewComboBoxColumn ddcolOperationType = new DataGridViewComboBoxColumn();
      ddcolOperationType.HeaderText = "Operation Type";
      ddcolOperationType.DataPropertyName = "OperationType";
      ddcolOperationType.DataSource = new List<string>(){SelectOptionOperationType.Dump, SelectOptionOperationType.Replace};
      ddcolOperationType.DisplayIndex = 4;

      DataGridViewTextBoxColumn ddcolReplacement = new DataGridViewTextBoxColumn();
      ddcolReplacement.HeaderText = "Replacement";
      ddcolReplacement.DataPropertyName = "ReplacementPath";
      ddcolReplacement.DisplayIndex = 5;

      DataGridViewButtonColumn ddcolReplacementGenerate = new DataGridViewButtonColumn();
      ddcolReplacementGenerate.HeaderText = "";
      ddcolReplacementGenerate.Text = "…";
      ddcolReplacementGenerate.ToolTipText = "Pick Replacement Image";
      ddcolReplacementGenerate.UseColumnTextForButtonValue = true;
      ddcolReplacementGenerate.Width = 20;
      ddcolReplacementGenerate.Resizable = DataGridViewTriState.False;
      ddcolReplacement.DisplayIndex = 6;

      ddGrid.RowTemplate.MinimumHeight = 25;
      ddGrid.EditMode = DataGridViewEditMode.EditOnEnter;
      var cols = new List<DataGridViewColumn>(){ddcolValid, ddcolSelectorType, ddcolSelector, ddcolSelectorGenerate, ddcolOperationType, ddcolReplacement, ddcolReplacementGenerate};
      ddGrid.Columns.AddRange(cols.ToArray());
      ddGrid.DataSource = operationList;
      colOfSelectorGenerator = ddGrid.Columns.IndexOf(ddcolSelectorGenerate);
      colOfReplacementGenerator = ddGrid.Columns.IndexOf(ddcolReplacementGenerate);
      //(ddGrid.DataSource as BindingList<Operation>).Add(new Operation(SelectorType.FileAndId, "z.tfc#10-11", OperationType.Dump, "yo"));
      //(ddGrid.DataSource as BindingList<Operation>).Add(new Operation(SelectorType.FileAndId, "z.tfc#10-11", OperationType.Dump, "yo2"));

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
        MessageBox.Show("TODO", "Generate Selector");
      } else if (e.ColumnIndex == colOfReplacementGenerator) {
        MessageBox.Show("TODO","Pick File");
      }
      /*var editingControl = this.ddGrid.EditingControl as DataGridViewComboBoxEditingControl;
      if (editingControl != null)
        editingControl.DroppedDown = true;*/
    }

    private void btnGenerateHashCooked_Click(object sender, EventArgs e) {

    }

    private void btnGenerateHashTFC_Click(object sender, EventArgs e) {

    }
  }

  public static class SelectOptionSelectorType {
    public const string FileAndId = "File#Id";
    public const string FileAndIdRange = "File#Id-Id";
    public const string File = "File";
    public const string All = "All";
  }
  public static class SelectOptionOperationType {
    public const string Dump = "Dump";
    public const string Replace = "Replace";
  }
  public class SettingsRow {
    string selectorType;
    string selector;
    string operation;
    string replacementPath;

    public SettingsRow(string selectorType, string selector, string operation, string replacementPath) {
      this.selectorType = selectorType;
      this.selector = selector;
      this.operation = operation;
      this.replacementPath = replacementPath;
    }

    // THE ORDER OF GETTERS MATTER!!!! DO NOT REARRAGNGE
    public bool Valid { get { 
      if (this.OperationType == SelectOptionOperationType.Replace) {
        if (this.SelectorType != SelectOptionSelectorType.FileAndId) {
          return false;
        }
      }
      return true;
    }}
    public string SelectorType { get { return selectorType; } set { selectorType = value; } }
    public string Selector { get { return selector; } set { selector = value; } }
    public string OperationType { get { return operation; } set { operation = value; } }
    public string ReplacementPath { get { return replacementPath; } set { replacementPath = value; } }
  }
}

