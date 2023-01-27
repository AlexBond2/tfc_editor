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

    List<Thread> imageLoaders = new List<Thread>();

    public SelectorWizard(PersitantData argPersitantData) {
      InitializeComponent();

      openFileDialogSelectDirectoryCookedReference = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      openFileDialogSelectDirectoryCookedReference.IsFolderPicker = true;
      openFileDialogSelectDirectoryCookedReference.RestoreDirectory = true;

      persitantData = argPersitantData;
      textSelectDirectoryCookedReference.Text = persitantData.cookedReferenceDirectory;
      textSelectFileHashCooked.Text = persitantData.hashesCooked;
      textSelectFileHashTFC.Text = persitantData.hashesTFC;
      textSelectDirectoryCookedReference.cursorToRight();
      textSelectFileHashCooked.cursorToRight();
      textSelectFileHashTFC.cursorToRight();

      TFCdir = persitantData.inputDirectory;

      imgListTFC.ImageSize = new Size(128, 128);
      imgListTFC.ColorDepth = ColorDepth.Depth16Bit;
      listViewTFC.LargeImageList = imgListTFC;

      loadingImage = makeLoadingImage();

      //64 128 256 512 1024 2048 4096
    }

    public Image makeLoadingImage() {
      int width = 128;
      int height = 128;
      Bitmap bmp = new Bitmap(width, height);
      using (Graphics gfx = Graphics.FromImage(bmp))
      using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, 50, 50))) {
        gfx.FillRectangle(brush, 0, 0, width, height);
      }
      return bmp;
    }
    public bool makeRedIfNotTFCPath(string s, TextBox tb) {
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

    public void failWith(TextBox tb, string failString) {
      tb.BackColor = failColor;
      tb.Text = failString;
    }
    public void failWithSimilarity(string failString) {
      failWith(textPreviewImageSimilarity, failString);
    }

    private void invalidateFileName() {
      if (makeRedIfNotTFCPath(textFilenameName.Text, textPreviewFilename)) {
        return;
      }
      textPreviewFilename.BackColor = default(Color);
      textPreviewFilename.Text = textFilenameAndIdName.Text;
    }

    private void invalidateFileNameAndId() {
      if (makeRedIfNotTFCPath(textFilenameAndIdName.Text, textPreviewFilenameAndId)) {
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
      if (this.Width < 1000) {
        this.Width = 1000;
      }
      failWith(textPreviewImageSimilarity, "Default Dummy Message");

      // reload cookedHashes
      string? failReason = tryRemakeHashListIfOutdated(cookedHashes, textSelectFileHashCooked.Text, lastCookedHashPath);
      if (!string.IsNullOrEmpty(failReason)) {
        failWithSimilarity(failReason);
        return;
      };
      lastCookedHashPath = textSelectFileHashCooked.Text;
      if (cookedHashes.Count == 0) {
        failWithSimilarity("Cooked Hashes contains no data");
        return;
      }

      // reload tfcHashes
      failReason = tryRemakeHashListIfOutdated(tfcHashes, textSelectFileHashTFC.Text, lastTFCHashPath);
      if (!string.IsNullOrEmpty(failReason)) {
        failWithSimilarity(failReason);
        return;
      };
      lastTFCHashPath = textSelectFileHashTFC.Text;
      if (tfcHashes.Count == 0) {
        failWithSimilarity("Cooked Hashes contains no data");
        return;
      }

      // Ensure Reference directory is loaded
      if (string.IsNullOrEmpty(textSelectDirectoryCookedReference.Text)) {
        failWithSimilarity($"No cooked reference directory is defined");
        return;
      }
      string cookedRefDir = textSelectDirectoryCookedReference.Text;

      // Cooked image filter
      if (string.IsNullOrEmpty(textCookedFilter.Text)) {
        failWithSimilarity($"No cooked image filter defined");
        return;
      }
       
      // Find cooked Hash
      string cookedFilterFileName = Path.GetRelativePath(cookedRefDir, textCookedFilter.Text);
      if (!cookedHashes.TryGetValue(cookedFilterFileName, out ulong cookedHash)) {
        failWithSimilarity($"Cooked Hashes do not contain a definition for ${cookedFilterFileName}");
        return;
      }

      // Generate similarity scores
      var similarityScores = new ConcurrentBag<(string,double)>();
      Parallel.ForEach(tfcHashes, tfcHashItem => {
        string tfcFileName = tfcHashItem.Key;
        ulong tfcHash = tfcHashItem.Value;
        double percentageImageSimilarity = CompareHash.Similarity(cookedHash, tfcHash);
        var similarityTuple = (tfcFileName, percentageImageSimilarity);
        similarityScores.Add(similarityTuple);
      });

      // Display
      var sorted = similarityScores.OrderBy((x) => -x.Item2);
      int nCandidates = (int)Math.Max(3, Math.Min(50, numericNoCandidates.Value));

      foreach (Thread thread in imageLoaders) {
        thread.Interrupt();
        thread.Join();
      }
      imageLoaders.Clear();
      listViewTFC.Items.Clear();
      imgListTFC.Images.Clear();

      Console.WriteLine();
      foreach (var ((name, match), index) in sorted.Take(nCandidates).Select((item, index) => (item, index))) {
        string tag = $"tag{filename}";
        listViewTFC.Items.Add(new ListViewItem {
          ImageIndex = index,
          Text = $"{match}% Match\n{name}",
          Tag = tag,
        });
        string ddsPath = $"{persitantData.outputDirectory}/dump/{name}";
        //imgListTFC.Images.Add(loadDDS(ddsPath));
        imgListTFC.Images.Add(loadingImage);
        imageLoaders.Add(ddsLoaderThread(index, ddsPath));

        Console.WriteLine($"{cookedFilterFileName} {match}% match with {name}");
      }

      foreach (var item in imageLoaders) {
        item.Start();
      }
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
        invalidateByImageSimilarity();
      }
    }

    private void btnSelectFileHashTFC_Click(object sender, EventArgs e) {
      if (openFileDialogSelectFileHashTFC.ShowDialog() == DialogResult.OK) {
        textSelectFileHashTFC.Text = openFileDialogSelectFileHashTFC.FileName;
        textSelectFileHashTFC.cursorToRight();
        invalidateByImageSimilarity();
      }
    }

    private void btnSelectDirectoryCookedReference_Click(object sender, EventArgs e) {
      if (openFileDialogSelectDirectoryCookedReference.ShowDialog() == CommonFileDialogResult.Ok) {
        textSelectDirectoryCookedReference.Text = openFileDialogSelectDirectoryCookedReference.FileName;
        textSelectDirectoryCookedReference.cursorToRight();
      }
    }

    private void btnGenerateHashCooked_Click(object sender, EventArgs e) {
      MessageBox.Show("TODO: generate cooked hashes");
    }

    private void btnGenerateHashTFC_Click(object sender, EventArgs e) {
      MessageBox.Show("TODO: generate TFC hashes");
    }

    private void sliderFilterResolution_Scroll(object sender, EventArgs e) {
      int resolution = (1 << 6) << sliderFilterResolution.Value;
      numericFilterResolution.Value = resolution; 
    }

    private void checkCookedImage_CheckedChanged(object sender, EventArgs e) {
      groupCookedImage.Enabled = checkCookedImage.Checked;
    }

    private void checkResolution_CheckedChanged(object sender, EventArgs e) {
      groupResolution.Enabled = checkResolution.Checked;
    }

    private void numericFilterResolution_ValueChanged(object sender, EventArgs e) {
      int nLeading0s = BitOperations.LeadingZeroCount((uint)(numericFilterResolution.Value));
      int howMuch64isShifter = 32 - 6 - nLeading0s - 1;
      sliderFilterResolution.Value = howMuch64isShifter;
      numericFilterResolution.Value = (1 << 6) << howMuch64isShifter;
    }

    private void checkFileName_CheckedChanged(object sender, EventArgs e) {
      groupFileName.Enabled = checkFileName.Checked;
    }

    private void btnCookedFilterBrowse_Click(object sender, EventArgs e) {
      string lastFilter = textCookedFilter.Text;
      if (string.IsNullOrEmpty(lastFilter)) {
        openFileDialogCookedFilter.InitialDirectory = textSelectDirectoryCookedReference.Text;
      } else {
        openFileDialogCookedFilter.InitialDirectory = Path.GetDirectoryName(lastFilter);
      }
      if (openFileDialogCookedFilter.ShowDialog() == DialogResult.OK) {
        textCookedFilter.Text = openFileDialogCookedFilter.FileName;
        textCookedFilter.cursorToRight();

        try {
          Image img = Image.FromFile(textCookedFilter.Text);
          pictureCooked.SizeMode = PictureBoxSizeMode.Zoom;
          pictureCooked.Image = img;
        } catch {
          MessageBox.Show("Image can't be loaded");
          return;
        }
        invalidateByImageSimilarity();
      }
    }

    private void SelectorWizard_FormClosing(object sender, FormClosingEventArgs e) {
      persitantData.cookedReferenceDirectory = textSelectDirectoryCookedReference.Text;
      persitantData.hashesCooked = textSelectFileHashCooked.Text;
      persitantData.hashesTFC = textSelectFileHashTFC.Text;
    }

    private void numericNoCandidates_ValueChanged(object sender, EventArgs e) {
      invalidateByImageSimilarity();
    }
  }
}
