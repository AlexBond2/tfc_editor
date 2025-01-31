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
using Pfim;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Img = SixLabors.ImageSharp;
using System.Numerics;
using PaladinsTfc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;

namespace paladins_tfc.src.gui {
  public partial class SelectorWizard : Form {
    Color failColor = Color.FromArgb(255, Color.FromArgb(0xfb9b9b));
    string TFCdir;
    PersitantData persitantData;
    ulong activeHash;
    Dictionary<string, ulong> cookedHashes = new Dictionary<string, ulong>();
    Dictionary<string, ulong> tfcHashes = new Dictionary<string, ulong>();
    string lastCookedHashPath;
    string lastTFCHashPath;
    Image loadingImage;
    Image failToLoadImage;
    Gui parentGui;
    bool filterIsValid = false;

    List<Thread> imageLoaders = new List<Thread>();
    SettingsRow activeRow;

    public SelectorWizard(Gui argParentGui, PersitantData argPersitantData) {
      InitializeComponent();

      parentGui = argParentGui;

      openFileDialogSelectDirectoryCookedReference = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      openFileDialogSelectDirectoryCookedReference.IsFolderPicker = true;
      openFileDialogSelectDirectoryCookedReference.RestoreDirectory = true;
      openFileDialogSelectDirectoryCookedReference.Title = "Select Directory Containing Cooked Assets";

      persitantData = argPersitantData;
      textSelectFileHashTFC.Text = persitantData.hashesTFC;
      textSelectFileHashTFC.cursorToRight();

      TFCdir = persitantData.inputDirectory;

      imgListTFC.ImageSize = new Size(128, 128);
      imgListTFC.ColorDepth = ColorDepth.Depth16Bit;
      listViewTFC.LargeImageList = imgListTFC;

      loadingImage = makeLoadingImage();
      failToLoadImage = makeFailToLoadImage();

      //64 128 256 512 1024 2048 4096
    }

    public void setDataRow(SettingsRow row) {
      this.activeRow = row;
    }

    private Image makeLoadingImage() {
      int width = 128;
      int height = 128;
      Bitmap bmp = new Bitmap(width, height);
      using (Graphics gfx = Graphics.FromImage(bmp))
      using (SolidBrush brush = new SolidBrush(Color.LightGray)) {
        gfx.FillRectangle(brush, 0, 0, width, height);
        gfx.DrawString("Loading", new Font("Arial", 16), new SolidBrush(Color.Gray), 20, 40);
      }
      return bmp;
    }
    private Image makeFailToLoadImage() {
      int width = 128;
      int height = 128;
      Bitmap bmp = new Bitmap(width, height);
      using (Graphics gfx = Graphics.FromImage(bmp))
      using (SolidBrush brush = new SolidBrush(Color.Magenta)) {
        gfx.FillRectangle(brush, 0, 0, width, height);
        gfx.DrawString("Can't Load\nPreview", new Font("Arial", 16), new SolidBrush(Color.Black), 10, 30);
      }
      return bmp;
    }
    private bool makeRedIfNotTFCPath(TextBox tb, string s) {
      string failString = null;
      if (string.IsNullOrEmpty(s)) {
        failString = "No TFC file selected";
      } else if (Path.GetExtension(s).ToLower() != ".tfc") {
        failString = $"Not a tfc file \"{s}\"";
      } else if (s.Contains(" ")) {
        failString = $"The tfc file may not include spaces \"{s}\"";
      }
      if (!string.IsNullOrEmpty(failString)) {
        failWith(tb, failString);
        return true;
      }
      return false;
    }

    private void succeedWith(TextBox tb, string succeedString) {
      filterIsValid = true;
      tb.BackColor = default(Color);
      tb.Text = succeedString;
    }
    private void failWith(TextBox tb, string failString) {
      filterIsValid = false;
      tb.BackColor = failColor;
      tb.Text = failString;
    }
    private void failWithSimilarity(string failString) {
      failWith(textPreviewImageSimilarity, failString);
    }

    private void invalidateFileName() {
      string selectedPath = textFilenameName.Text;
      if (makeRedIfNotTFCPath(textPreviewFilename, selectedPath)) {
        return;
      }
      succeedWith(textPreviewFilename, selectedPath);
    }

    private void invalidateFileNameAndId() {
      string selectedPath = textFilenameAndIdName.Text;
      if (makeRedIfNotTFCPath(textPreviewFilenameAndId, selectedPath)) {
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
        } catch (Exception e) {
        }
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
        } catch (Exception) { }
        failLine = index;
        failMessage = "Not a number range";
        break;
      }

      if (failLine >= 0) {
        string linePreview = richTextBoxIdSelect.Lines[failLine].Truncate(20);
        string error = $"Invalid selector on line: {failLine + 1} ({failMessage}), \"{linePreview}\""; ;
        failWith(textPreviewFilenameAndId, error);
        return;
      }

      string idSelector = "";
      if (selectorPattern.Count >= 1) {
        idSelector = ":" + string.Join(",", selectorPattern.ToArray());
      }
      succeedWith(textPreviewFilenameAndId, selectedPath + idSelector);
    }

    /*Thread imageInvThread;
    private void invalidateByImageSimilarity() {
      if(imageInvThread != null) {
        imageInvThread.Interrupt();
        imageInvThread.Join();
      }
      imageInvThread = new Thread(
        new ThreadStart(() => {
          try {
            this.Invoke(invalidateByImageSimilarityThread);
          } catch (Exception) {
          }
        })
      );
      imageInvThread.Start();
    }*/

    private void invalidateByImageSimilarity() {
      if (this.Width < 1000) {
        this.Width = 1000;
      }

      // reload tfcHashes
      string? failReason = tryRemakeHashListIfOutdated(tfcHashes, textSelectFileHashTFC.Text, lastTFCHashPath);
      if (!string.IsNullOrEmpty(failReason)) {
        failWithSimilarity(failReason);
        return;
      };
      lastTFCHashPath = textSelectFileHashTFC.Text;
      if (tfcHashes.Count == 0) {
        failWithSimilarity("Cooked Hashes contains no data");
        return;
      }

      // Cooked image filter
      if (string.IsNullOrEmpty(textImageFilter.Text)) {
        failWithSimilarity($"No cooked image filter defined");
        return;
      }


      // Find cooked Hash
      ulong? maybeCookedHash = getFilterImageHash();
      if (maybeCookedHash.HasValue == false) {
        failWithSimilarity($"Image filter can't be hashed");
        return;
      }
      ulong cookedHash = maybeCookedHash.GetValueOrDefault();

      // Filter hashes resolution
      IEnumerable<KeyValuePair<string, ulong>> filteredTfcHashes;

      int filterRes = (int)numericFilterResolution.Value;
      string nameIncludeFilter = textNameIncludeFilter.Text;
      bool nameIncludeFilterEnabled = !string.IsNullOrEmpty(nameIncludeFilter);
      string nameExcludeFilter = textNameExcludeFilter.Text;
      bool nameExcludeFilterEnabled = !string.IsNullOrEmpty(nameExcludeFilter);
      bool resolutionFilterChecked = checkResolution.Checked;
      bool nameFilterChecked = checkFileName.Checked;
      filteredTfcHashes = tfcHashes.Where((kv) => {
        string key = kv.Key;
        HashInfo hi = infoFromHashKey(key);
        if (resolutionFilterChecked) {
          if (hi.resolution != filterRes) {
            return false;
          }
        }
        if (nameFilterChecked) {
          if (nameIncludeFilterEnabled) {
            if (hi.unparsedName.Contains(nameIncludeFilter) == false) {
              return false;
            }
          }
          if (nameExcludeFilterEnabled) {
            if (hi.unparsedName.Contains(nameExcludeFilter)) {
              return false;
            }
          }
        }
        return true;
      });

      // Check if no filter matches
      if (filteredTfcHashes.Count((x) => true) == 0) {
        failWithSimilarity($"Found no tfc matching filters");
        listViewTFC.Items.Clear();
        imgListTFC.Images.Clear();
        return;
      }

      // Generate similarity scores
      var similarityScores = new ConcurrentBag<(string, double)>();
      Parallel.ForEach(filteredTfcHashes, tfcHashItem => {
        string tfcFileName = tfcHashItem.Key;
        ulong tfcHash = tfcHashItem.Value;
        double percentageImageSimilarity = CompareHash.Similarity(cookedHash, tfcHash);
        var similarityTuple = (tfcFileName, percentageImageSimilarity);
        similarityScores.Add(similarityTuple);
      });

      // Display
      var sorted = similarityScores.OrderBy((x) => -x.Item2);
      int nCandidates = (int)Math.Max(numericNoCandidates.Minimum, 
        Math.Min(numericNoCandidates.Maximum, numericNoCandidates.Value)
      );

      foreach (Thread thread in imageLoaders) {
        thread.Interrupt();
        thread.Join();
      }
      imageLoaders.Clear();
      listViewTFC.Items.Clear();
      imgListTFC.Images.Clear();

      foreach (var ((name, match), index) in sorted.Take(nCandidates).Select((item, index) => (item, index))) {
        string tag = name;
        listViewTFC.Items.Add(new ListViewItem {
          ImageIndex = index,
          Text = $"{match}% Match\n{name}",
          Tag = tag,
        });
        string ddsPath = $"{persitantData.outputDirectory}/dump/{name}";
        //imgListTFC.Images.Add(loadDDS(ddsPath));
        imgListTFC.Images.Add(loadingImage);
        imageLoaders.Add(ddsLoaderThread(index, ddsPath));
      }
      invalidateByImageSimilarityListView();

      foreach (var item in imageLoaders) {
        item.Start();
      }
    }
    struct HashInfo {
      public string fileWithoutEnding;
      public string fileWithEnding;
      public int id;
      public int resolution;
      public int dxtMode;
      public string unparsedName;
    }
    private static HashInfo infoFromHashKey(string hashKey) {
      string[] pathSplit = hashKey.Split("_");
      int lastindex = pathSplit.Length - 1;
      string datablock = pathSplit[lastindex];
      int id = int.Parse(pathSplit[lastindex - 1]);
      string fileName = string.Join("_", pathSplit[0..(lastindex - 1)]);

      string[] dataBlockSplit = datablock.Split("xDXT");
      int resolution = int.Parse(dataBlockSplit[0]);
      string postDXTandmaybeEnding = dataBlockSplit[1];
      int dxtMode = int.Parse(postDXTandmaybeEnding.Split(".")[0]);

      return new HashInfo() {
        fileWithoutEnding = fileName,
        fileWithEnding = fileName + ".tfc",
        id = id,
        resolution = resolution,
        dxtMode = dxtMode,
        unparsedName = hashKey
      };
    }
    private void invalidateByImageSimilarityListView() {
      var selectedItems = listViewTFC.SelectedItems;
      if (selectedItems.Count == 0) {
        failWithSimilarity("No Image(s) Selected From among Previews");
        return;
      }
      if (selectedItems.Count >= 2) {
        failWithSimilarity("Multiple Images Selected");
        return;
      }

      var selectedItem = listViewTFC.SelectedItems[0];
      string selectedKey = selectedItem.Tag.ToString();
      string selector;
      try {
        HashInfo hi = infoFromHashKey(selectedKey);
        selector = $"{hi.fileWithEnding}:{hi.id}";
      } catch {
        failWithSimilarity($"ERROR: {selectedKey} is invalid. This is most likely caused by a version mismatch between hash tables and this program");
        return;
      }

      succeedWith(textPreviewImageSimilarity, selector);
    }

    private Thread ddsLoaderThread(int index, string ddsPath) {
      return new Thread(
        new ThreadStart(() => {
          try {
            Image ddsImage = loadDDS(ddsPath); //slow
            Image scaledImage = imgListTFC.Images[index];
            Rectangle destRect = new Rectangle(0, 0, scaledImage.Width, scaledImage.Height);
            using (Graphics gfx = Graphics.FromImage(scaledImage)) {
              gfx.DrawImage(ddsImage, destRect, 0, 0, ddsImage.Width, ddsImage.Height, GraphicsUnit.Pixel);
            }
            imgListTFC.Images[index] = scaledImage;
            listViewTFC.Invalidate();
          } catch (Exception e) {
            //imgListTFC.Images[index] = failToLoadImage;
            Console.WriteLine($"UI: DDS loader thread canceled because {e.Message}");
          }
        })
     );
    }

    private Image loadDDS(string path) {
      MemoryStream pngStream = new MemoryStream();
      using (var image = Pfimage.FromFile(path)) {
        var handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
        Image bitmap;
        try {
          var data = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
          bitmap = new Bitmap(image.Width, image.Height, image.Stride, PixelFormat.Format32bppArgb, data);
          bitmap.Save(pngStream, System.Drawing.Imaging.ImageFormat.Png);
        } finally {
          handle.Free();
        }
        Image ret = Image.FromStream(pngStream);
        pngStream.Close();
        return ret;
      }
    }

    string lastFilterImagePath;
    ulong? lastFilterImageHash = null;
    private ulong? getFilterImageHash() {
      if(textImageFilter.Text == null) {
        return null;
      }
      if (lastFilterImagePath == textImageFilter.Text) {
        return lastFilterImageHash;
      }
      lastFilterImagePath = textImageFilter.Text;

      string extention = Path.GetExtension(lastFilterImagePath).ToLower();
      Hashing.ImgType imgtype;
      switch (extention) {
        case ".dds":
          imgtype = Hashing.ImgType.DDS;
          break;
        case ".png":
          imgtype = Hashing.ImgType.PNG;
          break;
        default:
          return null;
      }

      DifferenceHash hasher = new DifferenceHash();
      try {
        return hasher.Hash(Hashing.getImg(lastFilterImagePath, imgtype));
      } catch {
        MessageBox.Show($"{lastFilterImagePath} can't be hashed", "ERROR");
        return null;
      }
    }

    private string? tryRemakeHashListIfOutdated(in Dictionary<string, ulong> map, string newPath, string lastPath) {
      if (lastPath == newPath) {
        return null;
      }
      string hashpath = newPath; 
      string fileContent;
      try {
        fileContent = File.ReadAllText(hashpath);
      } catch (Exception) {
        return $"Unabled to open {hashpath}";
      }
      try {
        List<Hashing.HashItem> hil = JsonConvert.DeserializeObject<List<Hashing.HashItem>>(fileContent); ;
        map.Clear();
        foreach (Hashing.HashItem hi in hil) {
          map.Add(hi.path, hi.hash);
        }
      } catch (Exception) {
        return $"Unabled to {hashpath} content is incorrectly formatted";
      }
      return null;
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

    private void btnSelectFileHashTFC_Click(object sender, EventArgs e) {
      if (openFileDialogSelectFileHashTFC.ShowDialog() == DialogResult.OK) {
        persitantData.hashesTFC = openFileDialogSelectFileHashTFC.FileName;
        textSelectFileHashTFC.Text = persitantData.hashesTFC;
        textSelectFileHashTFC.cursorToRight();
        persitantData.write();
        invalidateByImageSimilarity();
      }
    }

    private void btnGenerateHashTFC_Click(object sender, EventArgs e) {
      string outDir = persitantData.outputDirectory;
      string hashDirRoot = outDir ;
      if (string.IsNullOrEmpty(hashDirRoot)) {
        MessageBox.Show("Error", "Error, \"Output Directory\" not set.");
        return;
      }
      string hashDir = Path.Combine(hashDirRoot, "dump");

      DialogResult dr = MessageBox.Show("Generate hash table based of your already dumped files?", "Info", MessageBoxButtons.OKCancel);
      if (dr == DialogResult.OK) {
        var command = new List<string>() {
          "hash", "tfc", hashDir, "--output-directory", outDir
        }; 
        parentGui.Focus();
        this.Hide();
        Extentions.invokeSomeway(this.parentGui, new MethodInvoker(() => {
          parentGui.runCommands(new List<List<string>>() { command });
        }));
      }
    }

    private void sliderFilterResolution_Scroll(object sender, EventArgs e) {
      int resolution = (1 << 6) << sliderFilterResolution.Value;
      numericFilterResolution.Value = resolution;
      invalidateByImageSimilarity();
    }

    private void checkCookedImage_CheckedChanged(object sender, EventArgs e) {
      groupCookedImage.Enabled = checkCookedImage.Checked;
    }

    private void checkResolution_CheckedChanged(object sender, EventArgs e) {
      groupResolution.Enabled = checkResolution.Checked;
      invalidateByImageSimilarity();
    }

    private void numericFilterResolution_ValueChanged(object sender, EventArgs e) {
      int nLeading0s = BitOperations.LeadingZeroCount((uint)(numericFilterResolution.Value));
      int howMuch64isShifter = 32 - 6 - nLeading0s - 1;

      sliderFilterResolution.Value = howMuch64isShifter;
      numericFilterResolution.Value = (1 << 6) << howMuch64isShifter;
      invalidateByImageSimilarity();
    }

    private void checkFileName_CheckedChanged(object sender, EventArgs e) {
      groupFileName.Enabled = checkFileName.Checked;
    }

    private void btnCookedFilterBrowse_Click(object sender, EventArgs e) {
      string lastFilter = textImageFilter.Text;
      if (string.IsNullOrEmpty(lastFilter) == false) {
        openFileDialogImgFilter.InitialDirectory = Path.GetDirectoryName(lastFilter);
      } 
      if (openFileDialogImgFilter.ShowDialog() == DialogResult.OK) {
        textImageFilter.Text = openFileDialogImgFilter.FileName;
        textImageFilter.cursorToRight();

        try {
          Image img = Image.FromFile(textImageFilter.Text);
          pictureImgFilter.SizeMode = PictureBoxSizeMode.Zoom;
          pictureImgFilter.Image = img;
        } catch {
          MessageBox.Show("Image can't be loaded");
          return;
        }
        invalidateByImageSimilarity();
      }
    }

    private void SelectorWizard_FormClosing(object sender, FormClosingEventArgs e) {
      persitantData.hashesTFC = textSelectFileHashTFC.Text;
      persitantData.write();

      if(e.CloseReason == CloseReason.UserClosing) {
        e.Cancel = true;
        this.Hide();
      }
    }

    private bool alertIfInvalid(TextBox tb) {
      if (filterIsValid == false) {
        MessageBox.Show($"{tb.Text} is not a valid filter");
        return true;
      }
      return false;
    }
    private void closeWith(TextBox tb) {
      activeRow.Selector = tb.Text;
      parentGui.invalidateGrid();
      parentGui.Focus();
      this.Hide();
    }

    private void numericNoCandidates_ValueChanged(object sender, EventArgs e) {
      invalidateByImageSimilarity();
    }

    private void btnSubmitAll_Click(object sender, EventArgs e) {
      filterIsValid = true;
      textPreviewAll.Text = "*";
      if (alertIfInvalid(textPreviewAll)) {
        return;
      }
      closeWith(textPreviewAll);
    }

    private void btnSubmitFilename_Click(object sender, EventArgs e) {
      if (alertIfInvalid(textPreviewFilename)) {
        return;
      }
      closeWith(textPreviewFilename);
    }

    private void btnSubmitFileNameAndId_Click(object sender, EventArgs e) {
      if (alertIfInvalid(textPreviewFilenameAndId)) {
        return;
      }
      closeWith(textPreviewFilenameAndId);
    }

    private void btnSubmitImageSimilarity_Click(object sender, EventArgs e) {
      if (alertIfInvalid(textPreviewImageSimilarity)) {
        return;
      }
      closeWith(textPreviewImageSimilarity);
    }

    private void listViewTFC_SelectedIndexChanged(object sender, EventArgs e) {
      invalidateByImageSimilarityListView();
    }

    private void textNameIncludeFilter_Leave(object sender, EventArgs e) {
      invalidateByImageSimilarity();
    }

    private void textNameExcludeFilter_Leave(object sender, EventArgs e) {
      invalidateByImageSimilarity();
    }

    private void textNameIncludeFilter_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        invalidateByImageSimilarity();
      }
    }

    private void textNameExcludeFilter_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        invalidateByImageSimilarity();
      }
    }

    private void btnDumpPreviews_Click(object sender, EventArgs e) {
      var file2dumpids = new Dictionary<string, List<int>>();
      foreach (ListViewItem item in listViewTFC.Items) {
        HashInfo hi;
        try {
          hi = infoFromHashKey(item.Tag.ToString());
        } catch {
          MessageBox.Show($"{item.Tag} is not a valid id, this is probably a programming error, sorry", "Error");
          return;
        }
        string file = hi.fileWithEnding;
        string wholeFilePath = $"{persitantData.inputDirectory}/{file}" ;

        if (file2dumpids.ContainsKey(wholeFilePath)) {
          file2dumpids[wholeFilePath].Add(hi.id);
        } else {
          file2dumpids[wholeFilePath] = new List<int> {hi.id};
        }
      }

      var terminalCommands = new List<List<string>>();
      foreach (var kv in file2dumpids) {
        string dumpids = string.Join(",", kv.Value);
        terminalCommands.Add(new List<string>() {
          "open", kv.Key, "--dump", dumpids, "--output-directory", persitantData.outputDirectory
        });
      }


      

      var callBack = new MethodInvoker(() => {
        Extentions.invokeSomeway(this, new MethodInvoker(() => {
          invalidateByImageSimilarity();
        }));
      });
      parentGui.Focus();
      Extentions.invokeSomeway(this.parentGui, new MethodInvoker(() => {
        parentGui.runCommands(terminalCommands, callBack);
      }));
    }
  }
}
