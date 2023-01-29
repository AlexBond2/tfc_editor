using System.ComponentModel;
using System.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using PaladinsTfcExtend;
using Newtonsoft.Json;

namespace PaladinsTfc {
  public partial class Gui : Form {

    int colOfSelectorGenerator;
    int colOfReplacementGenerator;

    BindingList<SettingsRow> operationList;
    PersitantData persitantData;

    paladins_tfc.src.gui.SelectorWizard selectorWizard;
    string currentlyOpenFile = "operations0.json";

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
      openFileDialogSelectDirectoryInput.Title = "Select Input Directory";
      openFileDialogSelectDirectoryOutput = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      openFileDialogSelectDirectoryOutput.IsFolderPicker = true;
      openFileDialogSelectDirectoryOutput.RestoreDirectory = true;
      openFileDialogSelectDirectoryOutput.Title = "Select Output Directory";
      openFileDialogReplacementFile.Title = "Select replacement dds files (of same resolution and encoding)";
      openFileDialogReplacementFile.Filter = "dds files (*.dds)|*.dds";
      openFileDialogReplacementFile.DefaultExt = "dds";
    }

    private void ddGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      if (e.ColumnIndex == colOfSelectorGenerator) {
        if (string.IsNullOrEmpty(textSelectDirectoryInput.Text) || string.IsNullOrEmpty(textSelectDirectoryOutput.Text)) {
          MessageBox.Show("You must set the TFC Input and Output Directory to use the selector Wizard", "Error");
        } else {
          var selectedSettingsRow = (SettingsRow)(ddGrid.Rows[e.RowIndex].DataBoundItem);
          selectorWizard.setDataRow(selectedSettingsRow);
          selectorWizard.Show(this);
        }
      } else if (e.ColumnIndex == colOfReplacementGenerator) {
        if (openFileDialogReplacementFile.ShowDialog() == DialogResult.OK) {
          var selectedSettingsRow = (SettingsRow)(ddGrid.Rows[e.RowIndex].DataBoundItem);
          string currentDir = Environment.CurrentDirectory;
          string selectedFile = openFileDialogReplacementFile.FileName;
          Console.WriteLine($"{currentDir} -> {selectedFile}");
          string relPath = Path.GetRelativePath(currentDir, selectedFile);
          selectedSettingsRow.ReplacementPath = relPath;
          invalidateGrid();
        }
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

    public struct CLIDumpAction {
      public string selector;
    }
    public struct CLIReplaceAction {
      public int selector;
      public string replacementPath;
    }
    public class CLIActionLists {
      public List<CLIDumpAction> dumpActions { get; set;  }
      public List<CLIReplaceAction> replaceActions { get; set; }
      public CLIActionLists() {
        dumpActions = new List<CLIDumpAction>();
        replaceActions = new List<CLIReplaceAction>();
      }
    }

    private void runError(string s) {
      MessageBox.Show(s);
    }
    private void btnRun_Click(object sender, EventArgs e) {
      string[] imgPaths = Directory.GetFiles(
        textSelectDirectoryInput.Text, 
        $"*.tfc",
        SearchOption.TopDirectoryOnly
      );
      Dictionary<string, CLIActionLists> name2cliActions = new Dictionary<string, CLIActionLists>();
      foreach (var item in imgPaths) {
        string tfcName = Path.GetFileName(item);
        name2cliActions.Add(tfcName, new CLIActionLists());
      }

      // Ensure validity of actions and add them to dictionary
      foreach (var op in operationList) {
        if (op.Enabled == false) continue;

        if(op.OperationType == SelectOptionOperationType.Dump) {
          if (string.IsNullOrEmpty(op.Selector)) {
            runError($"Selector is undefined");
            return;
          }
          if (op.Selector.Contains(" ")) {
            runError($"Selector may not contain spaces");
            return;
          }
          if (op.Selector == "*") {
            foreach (var key in name2cliActions.Keys) {
              name2cliActions[key].dumpActions.Add(new CLIDumpAction() {
                selector = "*"
              });
            }
          } else {

            string file;
            string selector;
            string[] selSplit = op.Selector.Split(":");
            switch (selSplit.Length) {
              case 1:
                file = selSplit[0];
                selector = "*";
                break;
              case 2:
                file = selSplit[0];
                selector = selSplit[1];
                break;
              default:
                runError($"{op.Selector} is not a valid selector");
                return;
            }
            if(name2cliActions.ContainsKey(file) == false) {
              runError($"\"{file}\" does not exist in input directory {textSelectDirectoryInput.Text}");
              return;
            }
            name2cliActions[file].dumpActions.Add(new CLIDumpAction() {
              selector = selector
            });
          }
        } else if (op.OperationType == SelectOptionOperationType.Replace) {
          if (string.IsNullOrEmpty(op.Selector)) {
            runError($"Selector is undefined");
            return;
          }
          if (op.Selector.Contains(" ")) {
            runError($"Selector may not contain spaces");
            return;
          }
          if (op.Selector == "*") {
            runError($"Replace operations can only be applied to a singular texture:id, Not {op.Selector}");
            return;
          }

          string file;
          string strSelector;
          string[] selSplit = op.Selector.Split(":");
          switch (selSplit.Length) {
            case 2:
              file = selSplit[0];
              strSelector = selSplit[1];
              break;
            default:
              runError($"Replace operations can only be applied to a singular texture:id, Not {op.Selector}");
              return;
          }
          if (int.TryParse(strSelector, out int singularIntSelector) == false) {
            runError($"Replace operations can only be applied to a singular texture:id, Not {strSelector}");
            return;
          }
          if (name2cliActions.ContainsKey(file) == false) {
            runError($"\"{file}\" does not exist in input directory {textSelectDirectoryInput.Text}");
            return;
          }
          if (File.Exists(op.ReplacementPath) == false) {
            runError($"{op.ReplacementPath} does not exist");
            return;
          }
          name2cliActions[file].replaceActions.Add(new CLIReplaceAction() {
            selector = singularIntSelector,
            replacementPath = op.ReplacementPath,
          });
        }
      }

      // Combine operations and verify no invalid overlaps
      foreach (var kv in name2cliActions) {
        string tfcName = kv.Key;
        var val = kv.Value;

        List<CLIDumpAction> dumpActions = val.dumpActions;
        List<CLIReplaceAction> replaceActions = val.replaceActions;

        // If any selector is * then replace all with that.
        if(dumpActions.Count(a => a.selector == "*") > 0) {
          val.dumpActions = new List<CLIDumpAction>() { new CLIDumpAction { selector = "*" } };
        }

        Dictionary<int, string> foundIndexes = new Dictionary<int, string>();
        foreach (var repAct in replaceActions) {
          if (foundIndexes.ContainsKey(repAct.selector)) {
            runError($"{tfcName} has multiple replacements for texture with index {repAct.selector}\n" +
              $"{repAct.selector}:{foundIndexes[repAct.selector]}\n" +
              $"{repAct.selector}:{repAct.replacementPath}");
            return;
          }
          foundIndexes[repAct.selector] = repAct.replacementPath;
        }
      }

      // Check output folder
      string outDir = textSelectDirectoryOutput.Text;
      if (string.IsNullOrEmpty(outDir)) {
        runError($"Output directory not set");
        return;
      }
      if (Directory.Exists(outDir) == false) {
        Directory.CreateDirectory(outDir);
      }
      if (outDir.Contains(" ")) {
        runError($"Output directory: {outDir} may not contain spaces, put it elsewhere");
        return;
      }

      // Check input folder
      string inDir = textSelectDirectoryInput.Text;
      if (string.IsNullOrEmpty(inDir)) {
        runError($"Input directory not set");
        return;
      }
      if (Directory.Exists(inDir) == false) {
        runError($"Input directory: {inDir} does not exist");
        return;
      }
      if (outDir.Contains(" ")) {
        runError($"Input directory: {inDir} may not contain spaces, put it elsewhere");
        return;
      }

      //Make Console commands
      List<List<string>> terminalCommands = new List<List<string>>();
      foreach (var kv in name2cliActions) {
        string tfcName = kv.Key;
        var val = kv.Value;

        if(val.dumpActions.Count == 0 && val.replaceActions.Count == 0) {
          continue;
        }

        var terminalCommandArgs = new List<string>();

        terminalCommandArgs.Add("open");
        terminalCommandArgs.Add($"{inDir}\\{tfcName}");
        if (val.dumpActions.Count > 0) {
          terminalCommandArgs.Add("--dump");
          terminalCommandArgs.Add(
            string.Join(",", val.dumpActions.Select(x => x.selector))
          );
        }
        if (val.replaceActions.Count > 0) {
          terminalCommandArgs.Add("--replace");
          terminalCommandArgs.Add(
            string.Join(",", val.replaceActions.Select(x => $"{x.selector}:{x.replacementPath}"))
          );
        }
        terminalCommandArgs.Add("--output-directory");
        terminalCommandArgs.Add(outDir);

        terminalCommands.Add(terminalCommandArgs);
      }
      runCommands(terminalCommands);
    }

    public void runCommands(List<List<string>> terminalCommands) {
      runCommands(terminalCommands, new MethodInvoker(() => { return; }));
    }
    public void runCommands(List<List<string>> terminalCommands, MethodInvoker postDone) {
      string[] preview = terminalCommands.Select(args => $"{string.Join(" ", args)}").ToArray();
      var confirmDialog = new paladins_tfc.src.gui.ConfirmDialog(preview);

      DialogResult dr = confirmDialog.ShowDialog(this);
      if (dr == DialogResult.OK) {
        lockUI();
        Thread commandThread = newCommandThread(this, terminalCommands, postDone);
        commandThread.Start();
      }
    }

    Rectangle prelock_log_bounds;
    private void lockUI() {
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      selectorWizard.FormBorderStyle = FormBorderStyle.FixedSingle;
      selectorWizard.MaximizeBox = false;
      selectorWizard.Enabled = false;
      prelock_log_bounds = rtextLog.Bounds;
      logBounds(0, 0, this.ClientSize.Width, this.ClientSize.Height);
    }
    private void unlockUI() {
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.MaximizeBox = true;
      selectorWizard.FormBorderStyle = FormBorderStyle.Sizable;
      selectorWizard.MaximizeBox = true;
      selectorWizard.Enabled = true;
      logBounds(prelock_log_bounds.X, prelock_log_bounds.Y, prelock_log_bounds.Width, prelock_log_bounds.Height);
    }

    private void logBounds(int x, int y, int width, int height) {
      rtextLog.SetBounds(x, y, width, height);
    }
    private void logWrite(string s) {
      rtextLog.Text += s;
    }

    private Thread newCommandThread(Control control, List<List<string>> terminalCommands, MethodInvoker postDone) {
      return new Thread(
        new ThreadStart(() => {
          var oldBounds = rtextLog.Bounds;

          Extentions.invokeSomeway(control, new MethodInvoker(() => {
            logWrite($"Locking UI\n");
            logWrite($"Starting command executer...\n\n");
          }));
          Thread.Sleep(200);

          foreach (var lcommand in terminalCommands) {
            var command = lcommand.ToArray();

            Extentions.invokeSomeway(control, new MethodInvoker(() => {
              logWrite($"Executing: \"{string.Join(" ", command)}\"\n");
            }));

            PaladinsTfc.Program.commandline(command.ToArray());
            Thread.Sleep(200);

            Extentions.invokeSomeway(control, new MethodInvoker(() => {
              logWrite($"Finished: \"{string.Join(" ", command)}\"\n");
            }));
          }

          MessageBox.Show("All Commands Finished");

          Extentions.invokeSomeway(control, new MethodInvoker(() => {
            unlockUI();
          }));
          Extentions.invokeSomeway(control, new MethodInvoker(() => {
            logWrite($"Unlocked UI\n");
          }));
          postDone.Invoke();
        })
     );
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
      try {
        ddGrid.Rows.RemoveAt(ddGrid.CurrentRow.Index);
        var rows = ddGrid.SelectedRows;
        foreach (DataGridViewRow row in ddGrid.SelectedRows) {
          ddGrid.Rows.RemoveAt(row.Index);
        }
      } catch { }
    }

    private void btnOperationSave_Click(object sender, EventArgs e) {
      string jsData = JsonConvert.SerializeObject(operationList);
      saveDialogOperations.Filter = "Json File|*.json";
      saveDialogOperations.Title = "Save the Current Operations Table as Json File";
      saveDialogOperations.FileName = Path.GetFileName(currentlyOpenFile);
      saveDialogOperations.InitialDirectory = Path.GetDirectoryName(currentlyOpenFile);
      if (saveDialogOperations.ShowDialog() == DialogResult.OK) { 
        string fpath = saveDialogOperations.FileName;
        File.WriteAllText(fpath, jsData);
      }
    }

    private void btnOperationLoad_Click(object sender, EventArgs e) {
      openDialogOperations.Filter = "Json File|*.json";
      openDialogOperations.Title = "Open an Operation Table Json File";
      if (openDialogOperations.ShowDialog() == DialogResult.OK) {
        string fpath = openDialogOperations.FileName;
        string fileContent = File.ReadAllText(fpath);

        List<SettingsRow> lrow = JsonConvert.DeserializeObject<List<SettingsRow>>(fileContent);
        operationList.Clear();
        foreach (var item in lrow) {
          operationList.Add(item);
        }

        currentlyOpenFile = fpath;
      };
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

