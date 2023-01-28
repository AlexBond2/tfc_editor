using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using PaladinsTfcExtend;

namespace PaladinsTfc {
  public partial class Gui : Form {

    int colOfSelectorGenerator;
    int colOfReplacementGenerator;

    BindingList<SettingsRow> operationList;
    PersitantData persitantData;

    paladins_tfc.src.gui.SelectorWizard selectorWizard;

    public Gui() {
      InitializeComponent();

      persitantData = PersitantData.load();

      operationList = new BindingList<SettingsRow>();
      operationList.Add(new SettingsRow());

      textSelectDirectoryInput.Text = persitantData.inputDirectory;
      textSelectDirectoryOutput.Text = persitantData.outputDirectory;
      textSelectDirectoryInput.cursorToRight();
      textSelectDirectoryOutput.cursorToRight();

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
      ddcolValid.DataPropertyName = "Enabled";
      ddcolValid.Width = 25;
      ddcolValid.Resizable = DataGridViewTriState.False;
      ddcolValid.DisplayIndex = 0;

      DataGridViewTextBoxColumn ddcolSelector = new DataGridViewTextBoxColumn();
      ddcolSelector.HeaderText = "Selector";
      ddcolSelector.DataPropertyName = "Selector";
      ddcolSelector.DisplayIndex = 2;
      ddcolSelector.Width = 250;

      DataGridViewButtonColumn ddcolSelectorGenerate = new DataGridViewButtonColumn();
      ddcolSelectorGenerate.HeaderText = "";
      ddcolSelectorGenerate.Text = "🧙";
      ddcolSelectorGenerate.ToolTipText = "Selector Wizard";
      ddcolSelectorGenerate.UseColumnTextForButtonValue = true;
      ddcolSelectorGenerate.Width = 20;
      ddcolSelectorGenerate.Resizable = DataGridViewTriState.False;
      ddcolSelectorGenerate.DisplayIndex = 3;

      DataGridViewComboBoxColumn ddcolOperationType = new DataGridViewComboBoxColumn();
      ddcolOperationType.HeaderText = "Operation Type";
      ddcolOperationType.DataPropertyName = "OperationType";
      ddcolOperationType.DataSource = new List<string>() { SelectOptionOperationType.Dump, SelectOptionOperationType.Replace };
      ddcolOperationType.DisplayIndex = 4;
      ddcolOperationType.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

      DataGridViewTextBoxColumn ddcolReplacement = new DataGridViewTextBoxColumn();
      ddcolReplacement.HeaderText = "Replacement";
      ddcolReplacement.DataPropertyName = "ReplacementPath";
      ddcolReplacement.DisplayIndex = 5;
      ddcolReplacement.Width = 250;

      DataGridViewButtonColumn ddcolReplacementGenerate = new DataGridViewButtonColumn();
      ddcolReplacementGenerate.HeaderText = "";
      ddcolReplacementGenerate.Text = "🧙‍";
      ddcolReplacementGenerate.ToolTipText = "Replacement File Wizard";
      ddcolReplacementGenerate.UseColumnTextForButtonValue = true;
      ddcolReplacementGenerate.Width = 20;
      ddcolReplacementGenerate.Resizable = DataGridViewTriState.False;
      ddcolReplacement.DisplayIndex = 6;

      ddGrid.RowTemplate.MinimumHeight = 25;
      ddGrid.EditMode = DataGridViewEditMode.EditOnEnter;
      var cols = new List<DataGridViewColumn>() { ddcolValid, ddcolSelector, ddcolSelectorGenerate, ddcolOperationType, ddcolReplacement, ddcolReplacementGenerate };
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

      selectorWizard = new paladins_tfc.src.gui.SelectorWizard(this, persitantData);
    }

    private void Gui_Load(object sender, EventArgs e) {
      openFileDialogSelectDirectoryInput = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      openFileDialogSelectDirectoryInput.IsFolderPicker = true;
      openFileDialogSelectDirectoryInput.RestoreDirectory = true;
      openFileDialogSelectDirectoryOutput = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      openFileDialogSelectDirectoryOutput.IsFolderPicker = true;
      openFileDialogSelectDirectoryOutput.RestoreDirectory = true;
    }

    private void ddGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      if (e.ColumnIndex == colOfSelectorGenerator) {
        string TFCInputpath = textSelectDirectoryInput.Text;
        if (string.IsNullOrEmpty(TFCInputpath)) {
          MessageBox.Show("You must set the TFC Input Directory to use the selector Wizard", "Error");
        } else {
          var selectedSettingsRow = (SettingsRow)(ddGrid.Rows[e.RowIndex].DataBoundItem);
          selectorWizard.setDataRow(selectedSettingsRow);
          selectorWizard.Show(this);
        }
      } else if (e.ColumnIndex == colOfReplacementGenerator) {
        MessageBox.Show("TODO", "Pick File");
      }
      /*var editingControl = this.ddGrid.EditingControl as DataGridViewComboBoxEditingControl;
      if (editingControl != null)
        editingControl.DroppedDown = true;*/
    }

    private void btnSelectDirectoryInput_Click(object sender, EventArgs e) {
      if (openFileDialogSelectDirectoryInput.ShowDialog() == CommonFileDialogResult.Ok) {
        persitantData.inputDirectory = openFileDialogSelectDirectoryInput.FileName;
        textSelectDirectoryInput.Text = persitantData.inputDirectory;
        textSelectDirectoryInput.cursorToRight();
        persitantData.write();
      }
    }

    private void btnSelectDirectoryOutput_Click(object sender, EventArgs e) {
      if (openFileDialogSelectDirectoryOutput.ShowDialog() == CommonFileDialogResult.Ok) {
        persitantData.outputDirectory = openFileDialogSelectDirectoryOutput.FileName;
        textSelectDirectoryOutput.Text = persitantData.outputDirectory;
        textSelectDirectoryOutput.cursorToRight();persitantData.write();
        persitantData.write();
      }
    }

    private void Gui_FormClosing(object sender, FormClosingEventArgs e) {
      persitantData.inputDirectory = textSelectDirectoryInput.Text;
      persitantData.outputDirectory = textSelectDirectoryOutput.Text;
      persitantData.write();
    }

    public void invalidateGrid() {
      ddGrid.Invalidate();
    }

    private void btnRun_Click(object sender, EventArgs e) {
      MessageBox.Show("TODO");
    }

    private void btnOperationAdd_Click(object sender, EventArgs e) {
      operationList.Add(new SettingsRow());
    }

    private void btnOperationDuplicate_Click(object sender, EventArgs e) {
      SettingsRow activeRow = (SettingsRow)ddGrid.CurrentRow.DataBoundItem;
      operationList.Add(new SettingsRow(
        activeRow.Selector, 
        activeRow.OperationType, 
        activeRow.ReplacementPath, 
        activeRow.Enabled
      ));
    }

    private void btnOperationDelete_Click(object sender, EventArgs e) {
      ddGrid.Rows.RemoveAt(ddGrid.CurrentRow.Index);
      var rows = ddGrid.SelectedRows;
      foreach (DataGridViewRow row in ddGrid.SelectedRows) {
        ddGrid.Rows.RemoveAt(row.Index);
      }
    }
  }
  public static class SelectOptionOperationType {
    public const string Dump = "Dump";
    public const string Replace = "Replace";
  }
  public class SettingsRow {
    string selector;
    string operation;
    string replacementPath;
    bool enabled;

    public SettingsRow(
      string selector = "",
      string operation = SelectOptionOperationType.Dump,
      string replacementPath = "",
      bool enabled = true
      ) {
      this.selector = selector;
      this.operation = operation;
      this.replacementPath = replacementPath;
      this.enabled = enabled;
    }

    // THE ORDER OF GETTERS MATTER!!!! DO NOT REARRAGNGE
    public bool Enabled { get { return enabled; } set { enabled = value; } }
    public string Selector { get { return selector; } set { selector = value; } }
    public string OperationType { get { return operation; } set { operation = value; } }
    public string ReplacementPath { get { return replacementPath; } set { replacementPath = value; } }
  }
}

