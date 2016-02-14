using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using YAME.MOD;

namespace YAME
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public Constantes _YameConst = new Constantes();	// Find constantes in Constantes class
        
    	public string _StartPath 		= "";	// YAME.exe' Absolute Starting path
        public string _RelModFolder 	= "";	// Relative Mod folder save in YAME.INI
        public string _AbsModFolder     = "";	// Absolute Mod folder save in YAME.INI
        public int _TotalModFound 		= 0;	// Total mods found in _RelModFolder
        public int _TotalModActivated 	= 0;	// Total mods activated
        
        internal List<ModObject> _ActivatedModsList = new List<ModObject>();	// List of ModObject activated 
        
        public INI.Ini AppIniFile;
        public INI.Ini ModIniFile;
        
        public string _SelectedMod;
        public int _SelectedIndexMod;
        
        public string _ContentSnapshot = "";

        public MainForm()
        {
            InitializeComponent();
            listBoxModFound.AllowDrop = true;
			listBoxModFound.DragEnter += new DragEventHandler(listBoxModFound_DragEnter);
			listBoxModFound.DragDrop += new DragEventHandler(listBoxModFound_DragDrop);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Read the Windows Form location from properties
            if ((ModifierKeys & Keys.Shift) == 0)
            {
                string initLocation = Properties.Settings.Default.InitialLocation;
                Point il = new Point(0, 0);
                Size sz = Size;
                if (!string.IsNullOrWhiteSpace(initLocation))
                {
                    string[] parts = initLocation.Split(',');
                    if (parts.Length >= 2)
                    {
                        il = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
                    }
                    if (parts.Length >= 4)
                    {
                        sz = new Size(int.Parse(parts[2]), int.Parse(parts[3]));
                    }
                }
                Size = sz;
                Location = il;
            }

            /* Start Init */
            KeyPreview = true;
            this.labelVersion.Text = _YameConst.Version;
            progressBar1.Minimum = 0;
            progressBar1.Visible = false;
            tbRenameMod.Hide();
            tbCreateMod.Hide();

            // absolute path of start in path in shortcut
            _StartPath = Directory.GetCurrentDirectory();
            if (_StartPath.EndsWith("\\")) _StartPath = _StartPath.Substring(0,_StartPath.Length - 1);

            // load mods folders from ini
            AppIniFile = new INI.Ini(_YameConst.Ininame, true);
            _RelModFolder = AppIniFile["MODS FOLDER", "Name"];

            _AbsModFolder = Path.GetFullPath(_RelModFolder);

            // load activated mods from ini
            loadModsFromIni();

            labelModPath.Text = "[ " + _AbsModFolder + " ]";

            try
            {
                string[] dirs = Directory.GetDirectories(_AbsModFolder);

                foreach (string dir in dirs)
                {
                    // Don't list hidden directory
                    DirectoryInfo directory = new DirectoryInfo(dir);
                    if ((directory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        String[] modname = dir.Split('\\');

                        // if mod not in activatedModsList add to listBoxModfound
                        if (!_ActivatedModsList.Any(mod => mod.ModName == modname[modname.Length - 1]))
                        {
                            listBoxModFound.Items.Add(modname[modname.Length - 1]);
                        }
                    }
                }

                if (_TotalModActivated > 0)  // otherwise add to listBoxModActivated
                    foreach (ModObject mod in _ActivatedModsList)
                    {
                        listBoxModActivated.Items.Add(mod.ModName);
                    }

                modsCalculate(true);
                buttonModPlus.Enabled = false;
                buttonModPlus.BackgroundImage = Properties.Resources.ButtonPlus_d;
                buttonModMoins.Enabled = false;
                buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;
                listBoxModFound.MouseDown += new MouseEventHandler(listBoxModFound_MouseDown);
                listBoxModActivated.MouseDown += new MouseEventHandler(listBoxModActivated_MouseDown);
            }
            catch (Exception)
            {
                MessageBox.Show("No mod folder called " + _AbsModFolder);
            }

        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the Windows Form location into properties
            if ((ModifierKeys & Keys.Shift) == 0)
            {
                Point location = Location;
                Size size = Size;
                if (WindowState != FormWindowState.Normal)
                {
                    location = RestoreBounds.Location;
                    size = RestoreBounds.Size;
                }
                string initLocation = string.Join(",", location.X, location.Y, size.Width, size.Height);
                Properties.Settings.Default.InitialLocation = initLocation;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonModPlus_Click(object sender, EventArgs e)
        {
            performButtonModPlus_Click(sender, e, false);
        }

        private void performButtonModPlus_Click(object sender, EventArgs e, bool stealth /* stealth mode if mods loaded by ymp file, no popup dependancies */)
        {
            Cursor.Current = Cursors.WaitCursor;

            _TotalModActivated++;
            ModObject ModtoAdd = prepareTheMod(listBoxModFound.SelectedItem.ToString());

            if (ModtoAdd == null)
                return;

            string text = "";

            if (ModtoAdd.AlertDependancies.Any() && !stealth)
            {
                progressBar1.Visible = false;
                foreach (string dependance in ModtoAdd.AlertDependancies)
                {
                    string modName;
                    string folderTag;
                    string fileName;

                    var depSplit = dependance.Split('|');
                    if (depSplit.Count() > 2)
                    {
                        modName = depSplit[0];
                        folderTag = "Folder ";
                        fileName = depSplit[2];
                    }
                    else
                    {
                        modName = depSplit[0];
                        folderTag = "";
                        fileName = depSplit[1];
                    }

                    text += folderTag + "\"" + fileName + "\" has already been altered by the " + "\"" + modName + "\" mod.\r\n";
                }

                var result1 = DependanciesForm.Show(text, ModtoAdd.ModName);
                if (result1 == DialogResult.No)
                {
                    _TotalModActivated--;
                    /* Hide ProgressBar and Cursor default */
                    progressBar1.Value = progressBar1.Minimum;
                    progressBar1.Visible = false;
                    Cursor.Current = Cursors.Default;
                    return;
                }
            }
            progressBar1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;

            copyTheMod(ModtoAdd);

            int i = 1;
            foreach (ModObject modobject in _ActivatedModsList)
            {
                ModIniFile["MODS", modobject.ModName] = i.ToString();
                i++;
            }

            listBoxModActivated.Items.Add(listBoxModFound.SelectedItem);
            listBoxModFound.Items.Remove(listBoxModFound.SelectedItem);

            buttonModPlus.Enabled = false;
            buttonModPlus.BackgroundImage = Properties.Resources.ButtonPlus_d;
            buttonModAllMoins.Enabled = true;
            buttonModAllMoins.BackgroundImage = Properties.Resources.ButtonCroix_a;

            modsCalculate(false);

            /* Hide ProgressBar and Cursor default */
            progressBar1.Value = progressBar1.Minimum;
            progressBar1.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void buttonModMoins_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            deletingTheMod(listBoxModActivated.SelectedItem.ToString());

            String modToDel = listBoxModActivated.SelectedItem.ToString();

            _TotalModActivated--;

            listBoxModActivated.Items.Remove(modToDel);
            listBoxModFound.Items.Add(modToDel);

            buttonModMoins.Enabled = false;
            buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;
            if (listBoxModActivated.Items.Count < 1)
            {
                buttonModAllMoins.Enabled = false;
                buttonModAllMoins.BackgroundImage = Properties.Resources.ButtonCroix_d;
            }
                
            modsCalculate(false);

            /* Hide ProgressBar and ursor default */
            progressBar1.Value = progressBar1.Minimum;
            progressBar1.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void buttonModAllMoins_Click(object sender, EventArgs e)
        {
        	performButtonModAllMoins_Click(sender, e, false);
        }

        private void performButtonModAllMoins_Click(object sender, EventArgs e, bool stealth /* stealth mode if mods loaded by ymp file, no popup accpet unload all */)
        {
        	if(!stealth) 
        	{
        		DialogResult result1 = MessageBox.Show("Disable all activated mods ?", "Disable all mods", MessageBoxButtons.YesNo);
            	if (result1 == DialogResult.No)
                	return;	
        	}

            Cursor.Current = Cursors.WaitCursor;

            for (int indexMod = _ActivatedModsList.Count() - 1; indexMod >= 0; indexMod--)
            {
                progressBar1.Value = progressBar1.Minimum;
                string modName = _ActivatedModsList[indexMod].ModName;
                deletingTheMod(_ActivatedModsList[indexMod].ModName);
                listBoxModActivated.Items.Remove(modName);
                listBoxModFound.Items.Add(modName);
            }

            _TotalModActivated = 0;

            buttonModMoins.Enabled = false;
            buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;
            buttonModAllMoins.Enabled = false;
            buttonModAllMoins.BackgroundImage = Properties.Resources.ButtonCroix_d;

            modsCalculate(false);

            /* Hide ProgressBar and Cursor default */
            progressBar1.Value = progressBar1.Minimum;
            progressBar1.Visible = false;
            Cursor.Current = Cursors.Default;
        }
        
        private void toolStripMenuExplore_Click(object sender, EventArgs e)
        {
            string path = PathCombine(_AbsModFolder, _SelectedMod);
            try
            {
                System.Diagnostics.Process.Start(@path);
            }
            catch (Exception)
            {
            }
        }

        private void toolStripMenuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string path = PathCombine(_AbsModFolder, _SelectedMod);

                SENDTOTRASH.SHFILEOP.SendToTrash(path);

                buttonRefresh.PerformClick();
            }
            catch (Exception excp)
            {
                MessageBox.Show("Deleting [" + _SelectedMod + "] is throwing exception!\r\n\r\n" + excp);
            }
        }

        private void toolStripMenuDocument_Click(object sender, EventArgs e)
        {
            string path = PathCombine(_AbsModFolder, _SelectedMod, _YameConst.Infomodfldr);
            try
            {
                System.Diagnostics.Process.Start(@path);
            }
            catch (Exception)
            {
            }
        }
        
        private void toolStripMenuRename_Click(object sender, EventArgs e)
        {
            int posList = listBoxModFound.Top;
            Rectangle r = listBoxModFound.GetItemRectangle(_SelectedIndexMod);

            tbRenameMod.Location = new Point(r.Left + 5, r.Top + posList);

            tbRenameMod.Size = new Size(_SelectedMod.Length * 8, 21);
            tbRenameMod.Text = _SelectedMod;

            Size size = TextRenderer.MeasureText(tbRenameMod.Text, tbRenameMod.Font);
            tbRenameMod.Width = size.Width;
            tbRenameMod.Height = size.Height;

            tbRenameMod.Show();
            tbRenameMod.Focus();
            this.Controls.SetChildIndex(listBoxModFound, 1);
            this.Controls.SetChildIndex(tbRenameMod, 0);

            tbRenameMod.SelectAll();
        }

        private void tbRenameMod_KeyDown(object sender, KeyEventArgs e)
        {
            Size size = TextRenderer.MeasureText(tbRenameMod.Text, tbRenameMod.Font);
            tbRenameMod.Width = size.Width;
            tbRenameMod.Height = size.Height;

            if (e.KeyCode == Keys.Escape)
            {
                tbRenameMod.Hide();
                this.Controls.SetChildIndex(listBoxModFound, 0);
                this.Controls.SetChildIndex(tbRenameMod, 1);
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (tbRenameMod.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
                {
                    if (_SelectedMod != tbRenameMod.Text && !string.IsNullOrEmpty(tbRenameMod.Text))
                    {
                        string oldpath = PathCombine(_AbsModFolder, _SelectedMod);
                        string newpath = PathCombine(_AbsModFolder, tbRenameMod.Text);
                        try
                        {
                            Directory.Move(oldpath, newpath);
                        }
                        catch (Exception excp)
                        {
                            MessageBox.Show("Renaming [" + _SelectedMod + "] is throwing exception!\r\n\r\n" + excp);
                        }

                    }

                    tbRenameMod.Hide();
                    this.Controls.SetChildIndex(listBoxModFound, 0);
                    this.Controls.SetChildIndex(tbRenameMod, 1);
                    buttonRefresh.PerformClick();
                }
            }
        }

        private void tbRenameMod_Leave(object sender, EventArgs e)
        {
            tbRenameMod.Hide();
            this.Controls.SetChildIndex(listBoxModFound, 0);
            this.Controls.SetChildIndex(tbRenameMod, 1);
        }

        private void listBoxModFound_MouseDown(object sender, MouseEventArgs e)
        {
            listBoxModActivated.ClearSelected();
            buttonModMoins.Enabled = false;
            buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;

            contextMenuStrip1.Hide();
            listBoxModFound.ContextMenuStrip = null;

            listBoxModFound.SelectedIndex = listBoxModFound.IndexFromPoint(e.X, e.Y);

            if (listBoxModFound.SelectedIndex < 0)
            {
                buttonModPlus.Enabled = false;
                buttonModPlus.BackgroundImage = Properties.Resources.ButtonPlus_d;
                return;
            }
            
            _SelectedMod = listBoxModFound.SelectedItem.ToString();
            _SelectedIndexMod = listBoxModFound.SelectedIndex;

            if (e.Button == MouseButtons.Right)
            {
            	var itemSeparator = new ToolStripSeparator();
	        	var itemDocumenta = new ToolStripMenuItem()
	        	{
	        		Name = "toolStripMenuDocumentation",
	        		Text = "Documentation"
	        	};
	        	
	        	if(contextMenuStrip1.Items.ContainsKey("toolStripMenuDocumentation"))
	        	{
	        		contextMenuStrip1.Items.RemoveAt(contextMenuStrip1.Items.Count-1);
		        	contextMenuStrip1.Items.RemoveAt(contextMenuStrip1.Items.Count-1);	
	        	}
	        	
                string path = PathCombine(_AbsModFolder, listBoxModFound.SelectedItem.ToString());

                try
                {
                	DirectoryInfo di = new DirectoryInfo(path);
	                string docFolder =
							di.GetDirectories()
						      	.Select(dir => dir.Name)
	                			.FirstOrDefault(name => name.ToLower().Contains(_YameConst.Infomodfldr));
	                
	                if(docFolder!=null)
	                {	                		                	
	                	contextMenuStrip1.Items.Add(itemSeparator);
	                	contextMenuStrip1.Items.Add(itemDocumenta);	         
						itemDocumenta.Click += new System.EventHandler(this.toolStripMenuDocument_Click);
	                }
                    listBoxModFound.ContextMenuStrip = contextMenuStrip1;
                    contextMenuStrip1.Show(MousePosition);
                }
                catch (Exception)
                {
                }
            }

            buttonModPlus.Enabled = true;
            buttonModPlus.BackgroundImage = Properties.Resources.ButtonPlus_a;
        }

        private void listBoxModActivated_MouseDown(object sender, MouseEventArgs e)
        {
            listBoxModFound.ClearSelected();
            buttonModPlus.Enabled = false;
            buttonModPlus.BackgroundImage = Properties.Resources.ButtonPlus_d;

            listBoxModActivated.SelectedIndex = listBoxModActivated.IndexFromPoint(e.X, e.Y);

            if (listBoxModActivated.SelectedIndex < 0)
            {
                buttonModMoins.Enabled = false;
                buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;
            }
            else
            {
                if (_ActivatedModsList.Any(mod => mod.ModName == listBoxModActivated.SelectedItem.ToString() && mod.IsDisabled == true))
                {
                    buttonModMoins.Enabled = false;
                    buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;
                }
                else
                {
                    buttonModMoins.Enabled = true;
                    buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_a;
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            listBoxModFound.Items.Clear();
            listBoxModActivated.ClearSelected();
            buttonModMoins.Enabled = false;
            buttonModPlus.BackgroundImage = Properties.Resources.ButtonPlus_d;
            buttonModPlus.Enabled = false;
            buttonModMoins.BackgroundImage = Properties.Resources.ButtonMoins_d;

            try
            {
                string[] dirs = Directory.GetDirectories(_AbsModFolder);

                foreach (string dir in dirs)
                {
                    // Don't list hidden directory
                    DirectoryInfo directory = new DirectoryInfo(dir);
                    if ((directory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        String[] modname = dir.Split('\\');

                        // if mod not in activatedModsList add to listBoxModfound
                        if (!_ActivatedModsList.Any(mod => mod.ModName == modname[modname.Length - 1]))
                        {
                            listBoxModFound.Items.Add(modname[modname.Length - 1]);
                        }
                    }
                }

                modsCalculate(true);
            }
            catch (Exception)
            {
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modsCalculate(bool init)
        {
            if (init)
            {
                _TotalModFound = listBoxModFound.Items.Count;
                _TotalModFound += listBoxModActivated.Items.Count;
            }
            labelModFindAndActivated.Text = _TotalModFound + " mods found (" + listBoxModActivated.Items.Count + " activated)";
        }
        
        private void loadModsFromIni()
        {
            ModIniFile = new INI.Ini(PathCombine(_AbsModFolder, _YameConst.Ininame), true);

            List<string> ListMods = ModIniFile["MODS", true];

            foreach (string mod in ListMods)
            {
                var num = mod.Split('=');
                string splitModName = num[0];
                int splitModNum = Int32.Parse(num[1]);

                // add objectmod
                string modlogpath = PathCombine(_AbsModFolder, _YameConst.Modlogsdir, splitModName + ".log");

                // add mod to activatedModsList List ModObject 
                ModObject thismod = new ModObject(splitModName);
                thismod.ModNumber = splitModNum;

                // find files in mod.log
                if (File.Exists(modlogpath))
                {
                    var logFile = File.ReadAllLines(modlogpath);
                    thismod.FilesList = new List<string>(logFile);
                }

                ModIniFile = new INI.Ini(PathCombine(_AbsModFolder, _YameConst.Ininame), true);

                string dependancies = ModIniFile["DEPENDANCIES", splitModName];

				if (dependancies != String.Empty) {
					thismod.IsDisabled = true;
					thismod.DependanciesList = dependancies.Split('|').ToList();
					if (thismod.DependanciesList[thismod.DependanciesList.Count - 1] == String.Empty)
						thismod.DependanciesList.RemoveAt(thismod.DependanciesList.Count - 1);
				}

                _ActivatedModsList.Add(thismod);
            }

            _ActivatedModsList = _ActivatedModsList.OrderBy(o => o.ModNumber).ToList();
            _TotalModActivated = _ActivatedModsList.Count();

            if (_TotalModActivated < 1)
            {
                buttonModAllMoins.Enabled = false;
                buttonModAllMoins.BackgroundImage = Properties.Resources.ButtonCroix_d;
            }
        }

        private ModObject prepareTheMod(string mod) 
		{
        	List<string> CreateFolderInBackup = new List<string>();
        	List<string> CreateFolderInGame = new List<string>();
        	List<string> MoveFileToBackup = new List<string>();
        	List<string> CopyFileToGame = new List<string>();
        	List<string> RemoveFileFromGame = new List<string>();        	
        	List<string> AlertDependancies = new List<string>();
        	string LogOutput = "";        	
        	
        	bool isREMFILE = false;
        	
            string backupdir = PathCombine(_AbsModFolder, _YameConst.Backupdir);
            string moddir = PathCombine(_AbsModFolder, mod);

            ModObject addedMod = new ModObject(mod);
            addedMod.ModNumber = _TotalModActivated;

            string absolutePath = PathCombine(_AbsModFolder, addedMod.ModName);

            try
            {
                var listOfFilesInDir = Directory
                                    .EnumerateFiles(moddir, "*", SearchOption.AllDirectories)
                                    .Select(Path.GetFullPath);

                /* Show ProgressBar */
                progressBar1.Maximum = listOfFilesInDir.Count();
                progressBar1.Visible = true;

                foreach (string f in listOfFilesInDir)
                {
                    if (progressBar1.Value < progressBar1.Maximum)
	                	progressBar1.Value++;

                    /* if folder is Documentation do next folder */
                    if (f.Contains(_YameConst.Infomodfldr))
                        continue;

                    /* if ext file is .yame do next file */
                    if (Path.GetExtension(f) == _YameConst.Infomodext)
                        continue;

                    var fileRelativePath = f.Substring(absolutePath.Length);                // \Media\classes\trucks\SID-75-K5-Blazer.xml
                    List<string> listRelativePath = fileRelativePath.Split('\\').ToList();  // {"", Media, classes, trucks, SID-75-K5-Blazer.xml}
                    var fileNameExt = listRelativePath[listRelativePath.Count - 1];         // SID-75-K5-Blazer.xml
                    listRelativePath.RemoveAt(listRelativePath.Count - 1);                  // {"", Media, classes, trucks}
                    listRelativePath.RemoveAt(0);                                           // {Media, classes, trucks}
                    
                    var FolderRelativePath = listRelativePath.Count == 0 ? "" : String.Join("\\", listRelativePath);

                    string Fpath = "";
                    foreach (string folder in listRelativePath)                             // stocker les nouveaux répertoires parents {Media, classes, trucks} ==> Media s'il est nouveau
                    {
                        Fpath += "\\" + folder;

                        if (addedMod.FilesList.Contains("NEWFLDR|" + Fpath + "\\"))         // /!\ Avec cette méthode tous les mods utilisant le repertoire racine seront désactivés !
                            break;

                        if (!Directory.Exists(_StartPath + Fpath))
                        {
                            // write NEWFLD in log
                            LogOutput += "NEWFLDR|" + Fpath + "\\" + "\r\n";
                            // ModObjectList add new folder
                            addedMod.FilesList.Add("NEWFLDR|" + Fpath + "\\");
                            break;
                        }
                    }

                    // Here not PathCombine cause of absoluteGameFolderToModify doesn't exist at the moment
                    var absoluteGameFolderToModify = _StartPath + "\\" + FolderRelativePath;

                    // if not newfolder, and not folder create by other mods, create folder in backup dir
                    if (!Directory.Exists(PathCombine(backupdir, FolderRelativePath)) && Directory.Exists(absoluteGameFolderToModify))
                    {
                        // Modifie le 28/01/2016 - Si Folder créé par Mod il faut creer le backup dans le cas ou dependance. Ecrasement de mod
                        //if ( !( addedMod.FilesList.Any(path => path.Contains(FolderRelativePath)) ) && !_ActivatedModsList.Any(aMod => aMod.FilesList.Any(path => path.Contains(FolderRelativePath)))  )
                        CreateFolderInBackup.Add(PathCombine(backupdir, FolderRelativePath));
                    }
                    	
                    // Test if FolderRelativePath exists in Game directory
                    if (!Directory.Exists(absoluteGameFolderToModify))
                    {
                        // create new folder in game dir
                        CreateFolderInGame.Add(absoluteGameFolderToModify);
                    }

                   	// If file mod ends with "-remove", move original file in backup  
				    if(Path.GetExtension(fileNameExt).Contains(_YameConst.Removetickfile))	/*_YameConst.Removetickfile = "-remove" */
				    {
				    	isREMFILE = true;
				    	string gameFileNameExt = fileNameExt.Replace(_YameConst.Removetickfile,String.Empty);
				    	
				    	if (File.Exists(PathCombine(absoluteGameFolderToModify, gameFileNameExt))) 
				    	{				    		
				    		string fromGame = PathCombine(absoluteGameFolderToModify, gameFileNameExt);
					        string toBackup = PathCombine(backupdir, FolderRelativePath) + "\\" + gameFileNameExt + "." + addedMod.ModName;
					        // move original file to backup
					        RemoveFileFromGame.Add(fromGame + ">" + toBackup);
					        // write file in log
					    	LogOutput += "REMFILE|" + FolderRelativePath + "\\" + gameFileNameExt + "\r\n";
					    	// ModObjectList add new file
					    	addedMod.FilesList.Add("REMFILE|" + FolderRelativePath + "\\" + gameFileNameExt);
				    	}
				    }
				    else
				    {
				    	isREMFILE = false;
				    	// write file in log
                    	LogOutput += FolderRelativePath + "\\" + fileNameExt + "\r\n";
                    	// ModObjectList add new file
                    	addedMod.FilesList.Add(PathCombine(FolderRelativePath, fileNameExt));
                    }
                    	
					/* Find dependancies */
		            foreach (ModObject installedMod in _ActivatedModsList)
		            {
		            	// if file in root game, FolderRelativePath is empty
		            	string relativePathFileName = (FolderRelativePath == String.Empty ? fileNameExt : FolderRelativePath + "\\" + fileNameExt);
		            	
                        if (installedMod.FilesList.Contains(relativePathFileName))
		                {
		                    AlertDependancies.Add(installedMod.ModName + "|" + fileNameExt);
		                }

                        string wPath = "\\";
                        foreach(string folder in listRelativePath)
                        {
                            wPath += folder + "\\";
                            string wString = "NEWFLDR|" + wPath;

                            if (AlertDependancies.Contains(installedMod.ModName + "|FLDR|" + wPath))
                                break;

                            if (installedMod.FilesList.Contains(wString))
                            {
                                AlertDependancies.Add(installedMod.ModName + "|FLDR|" + wPath);
                            }
                        }
		            }

                    // Test if fileNameExt exists in Game directory
                    if (File.Exists(PathCombine(absoluteGameFolderToModify, fileNameExt)))
                    {
                        string fromGame = PathCombine(absoluteGameFolderToModify, fileNameExt);
                        string toBackup = PathCombine(backupdir, FolderRelativePath) + "\\" + fileNameExt + "." + addedMod.ModName;
                        // move original file to backup
                        MoveFileToBackup.Add(fromGame + ">" + toBackup);
                    }

                    // copy mod file to game
                    string fromMod = f;
                    string toGame = absoluteGameFolderToModify + "\\" + fileNameExt;
                    if(!isREMFILE) /* File not ends with -remove, copy file to game */
                    	CopyFileToGame.Add(fromMod + ">" + toGame);
                }
            }
            catch (Exception)
            {
                progressBar1.Value = progressBar1.Minimum;
                progressBar1.Visible = false;
                return null;
            }
            
            addedMod.CreateFolderInBackup = CreateFolderInBackup;
            addedMod.CreateFolderInGame = CreateFolderInGame;
            addedMod.MoveFileToBackup = MoveFileToBackup;
            addedMod.CopyFileToGame = CopyFileToGame;
            addedMod.RemoveFileFromGame = RemoveFileFromGame;
            addedMod.AlertDependancies = AlertDependancies;
            addedMod.LogOutput = LogOutput;

            return addedMod;
		}
        
        private void copyTheMod(ModObject addedMod)
        {
            string logfile = PathCombine(_AbsModFolder, _YameConst.Modlogsdir, addedMod.ModName + ".log");
            try            	            	
            {
            	/* Show ProgressBar */
            	progressBar1.Maximum += addedMod.MoveFileToBackup.Count() + addedMod.CopyFileToGame.Count();
                
            	// AlertDependancies
            	foreach (string alert in addedMod.AlertDependancies)
            	{
            		var alertSplit = alert.Split('|');
            		
            		ModObject dependantMod = _ActivatedModsList.First(mod => mod.ModName == alertSplit[0]);
                    
                    // If already disabled by this mod, break
                    if (dependantMod.DependanciesList.Contains(addedMod.ModName))
                        break;

                    dependantMod.IsDisabled = true;
            		dependantMod.DependanciesList.Add(addedMod.ModName);

            		// update dependances in ini file 
                    ModIniFile["DEPENDANCIES", dependantMod.ModName] = String.Join("|", dependantMod.DependanciesList.ToArray());					
            	}
            	
            	// CreateFolderInBackup
            	foreach (string BackupFolder in addedMod.CreateFolderInBackup)
            	{
            		Directory.CreateDirectory(BackupFolder);
            	}
            	
            	// CreateFolderInGame
            	foreach (string GameFolder in addedMod.CreateFolderInGame)
            	{
            		Directory.CreateDirectory(GameFolder);
            	}
            	
            	// MoveFileToBackup
            	foreach (string mvFile in addedMod.MoveFileToBackup)
            	{
            		if (progressBar1.Value < progressBar1.Maximum)
	            		progressBar1.Value++;

            		var mv = mvFile.Split('>');

                    /* try to make file rw */
                    FileInfo fileInfo = new FileInfo(mv[0]);
                    fileInfo.IsReadOnly = false;

                    MoveFile(mv[0], mv[1]);
            	}
            	
            	// CopyFileToGame
            	foreach (string cpFile in addedMod.CopyFileToGame)
            	{
            		if (progressBar1.Value < progressBar1.Maximum)
            			progressBar1.Value++;

            		var cp = cpFile.Split('>');
                    CopyFile(cp[0], cp[1]);

                    /* try to make file rw */
                    FileInfo fileInfo = new FileInfo(cp[1]);
                    fileInfo.IsReadOnly = false;
            	}
            	
            	// RemoveFileFromGame
            	foreach (string remFile in addedMod.RemoveFileFromGame)
            	{
            		if (progressBar1.Value < progressBar1.Maximum)
            			progressBar1.Value++;

            		var rm = remFile.Split('>');
                    MoveFile(rm[0], rm[1]);
            	}

            }
            catch (Exception)
            {
                progressBar1.Value = progressBar1.Minimum;
                progressBar1.Visible = false;
            }

            _ActivatedModsList.Add(addedMod);
            
            // LogOutput
            File.WriteAllText(logfile, addedMod.LogOutput);	        	
        }                            
        
        public void deletingTheMod(string mod)
        {
            string backupdir = PathCombine(_AbsModFolder, _YameConst.Backupdir);
            string logfile = PathCombine(_AbsModFolder, _YameConst.Modlogsdir, mod + ".log");

            ModObject itemToRemove = null;

            // foreach mod, delete the deactivate one and del the dependancies on the other
            foreach (ModObject modobject in _ActivatedModsList)
            {
                if (modobject.ModName == mod)
                {
                    /* Show ProgressBar */
                    progressBar1.Value = progressBar1.Minimum;
                    progressBar1.Maximum = modobject.FilesList.Count();
                    progressBar1.Visible = true;

                    // suppression des fichiers du mod
                    foreach (string file in modobject.FilesList)
                    {
                        if (progressBar1.Value < progressBar1.Maximum)
                            progressBar1.Value++;

                        if (file.StartsWith("NEWFLDR"))
                        {
                            string folder = file.Split('|')[1]; // ne prendre que le 2nd enreg car le premier est vide
                            string folderPath = PathCombine(_StartPath, folder);

                            // If directory exists in game Delete
                            if (Directory.Exists(folderPath))
                            {
                                try
                                {
                                    Directory.Delete(folderPath, true);       // BUG REPERTOIRE N'EST PAS VIDE ??!
                                }
                                catch (Exception)
                                {
                                }
                            }
                            // If directory exists in backup Delete
                            if (Directory.Exists(PathCombine(_AbsModFolder, _YameConst.Backupdir, folder)))
                            {
                                try
                                {
                                    Directory.Delete(PathCombine(_AbsModFolder, _YameConst.Backupdir, folder), true);      
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        else 
                        {
                        	if (file.StartsWith("REMFILE"))
	                        {
                        		string remFile = file.Split('|')[1]; // ne prendre que le 2nd enreg car le premier est vide
                        		
                        		if (File.Exists(PathCombine(backupdir, remFile + "." + mod)))
                        		{
                        			string gameFilePath = PathCombine(_StartPath, remFile);	// Media\classes\trucks\SID-75-K5-Blazer.xml
                                	string backFilePath = PathCombine(backupdir, remFile);	//!YABACKUP\Media\classes\trucks\SID-75-K5-Blazer.xml
                                	string bkpFpath = Path.GetDirectoryName(backFilePath);	//!YABACKUP\Media\classes\trucks\

                                	MoveFile(PathCombine(backupdir, remFile + "." + mod), gameFilePath);
                                	
                            		DirectoryInfo dir = new DirectoryInfo(bkpFpath);
									DeleteFolderIfEmpty(dir, _YameConst.Backupdir);	                                
                        		}
	                        }
	                        else
	                        {
	                            // if files exists in backup directory delete ingame and move from backup to ingame
	                            if (File.Exists(PathCombine(backupdir, file + "." + mod)))
	                            {
	                                string toFile = PathCombine(_StartPath, file);			// Media\classes\trucks\SID-75-K5-Blazer.xml
	                                string backFilePath = PathCombine(backupdir, file);		//!YABACKUP\Media\classes\trucks\SID-75-K5-Blazer.xml
	                                string bkpFpath = Path.GetDirectoryName(backFilePath);	//!YABACKUP\Media\classes\trucks\
	                                
	                                DeleteFile(toFile);	                                	                               
	                                MoveFile(PathCombine(backupdir, file + "." + mod), toFile);
									
                            		DirectoryInfo dir = new DirectoryInfo(bkpFpath);
									DeleteFolderIfEmpty(dir, _YameConst.Backupdir);	 
									
	                            }
	                            else // else only delete ingame file
	                            {
	                                DeleteFile(PathCombine(_StartPath, file));
	                            }
	                        }
                        }
                    }

                    // delete logfile
                    if (File.Exists(logfile))
                        DeleteFile(logfile);

                    // delete item in activatedModsList
                    itemToRemove = _ActivatedModsList.Single(r => r.ModName == mod);

                    // delete mod in inifile
                    ModIniFile["MODS", mod] = null;
                }
                else
                {
                    // for other activated mods, update dependancies if exists
                    if (modobject.IsDisabled && modobject.DependanciesList.Contains(mod))
                    {
                        modobject.DependanciesList.Remove(mod);
                        // if other dependancies exist
                        if (modobject.DependanciesList.Any()) 
                        {
                            ModIniFile["DEPENDANCIES", modobject.ModName] = String.Join("|", modobject.DependanciesList.ToArray());
                        }
                        else
                        {
                            modobject.IsDisabled = false;
                            ModIniFile["DEPENDANCIES", modobject.ModName] = null;
                        }
                    }
                }
            }

            // itemToRemove is not null, remove mod from activatedModsList
            if (itemToRemove != null)
                _ActivatedModsList.Remove(itemToRemove);
        }

        void listBoxModFound_onKeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxModFound.SelectedIndex < 0)
                return;

            _SelectedMod = listBoxModFound.SelectedItem.ToString();
            _SelectedIndexMod = listBoxModFound.SelectedIndex;

            if (e.KeyCode == Keys.Up)
            {
                _SelectedMod = listBoxModFound.SelectedItem.ToString();
                _SelectedIndexMod = listBoxModFound.SelectedIndex;
            }
            if (e.KeyCode == Keys.Down)
            {
                _SelectedMod = listBoxModFound.SelectedItem.ToString();
                _SelectedIndexMod = listBoxModFound.SelectedIndex;
            }

            if (e.KeyCode == Keys.F2)
            {
                toolStripMenuRename.PerformClick();
            }

            if (e.KeyCode == Keys.Delete)
            {
                toolStripMenuDelete.PerformClick();
            }
        }

        void MainForm_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                buttonRefresh.PerformClick();
            }

            if (e.KeyCode == Keys.F7)
            {
                if(listBoxModFound.Items.Count > 0)
                {
                    listBoxModFound.ClearSelected();
                    int posList = listBoxModFound.Top;
                    int lastItem = listBoxModFound.Items.Count - 1;
                    Rectangle r = listBoxModFound.GetItemRectangle(lastItem);

                    int i = 1;
                    string newfolder = _YameConst.Newmodfolder + "(" + i + ")";
                    while (Directory.Exists(_AbsModFolder + "\\" + newfolder))
                    {
                        i++;
                        newfolder = _YameConst.Newmodfolder + "(" + i + ")";
                    }

                    tbCreateMod.Text = newfolder;

                    Size size = TextRenderer.MeasureText(tbCreateMod.Text, tbCreateMod.Font);
                    tbCreateMod.Width = size.Width;
                    tbCreateMod.Height = size.Height;
                    tbCreateMod.Location = new Point(r.Left + 5, r.Top + posList + size.Height);
                    tbCreateMod.SelectAll();
                    tbCreateMod.Show();
                    tbCreateMod.Focus();
                    this.Controls.SetChildIndex(listBoxModFound, 1);
                    this.Controls.SetChildIndex(tbCreateMod, 0);
                }
                else
                {
                    int posList = listBoxModFound.Top;
                    int left = listBoxModFound.Left;

                    int i = 1;
                    string newfolder = _YameConst.Newmodfolder + "(" + i + ")";
                    while (Directory.Exists(_AbsModFolder + "\\" + newfolder))
                    {
                        i++;
                        newfolder = _YameConst.Newmodfolder + "(" + i + ")";
                    }

                    tbCreateMod.Text = newfolder;

                    Size size = TextRenderer.MeasureText(tbCreateMod.Text, tbCreateMod.Font);
                    tbCreateMod.Width = size.Width;
                    tbCreateMod.Height = size.Height;
                    tbCreateMod.Location = new Point(left + 5, posList);
                    tbCreateMod.SelectAll();
                    tbCreateMod.Show();
                    tbCreateMod.Focus();
                    this.Controls.SetChildIndex(listBoxModFound, 1);
                    this.Controls.SetChildIndex(tbCreateMod, 0);
                }
            }
        }

        void listBoxModFound_MouseHover(object sender, MouseEventArgs e)
        {
            int index = listBoxModFound.IndexFromPoint(e.Location);
            
            if (index < 0)
            {
                toolTip1.SetToolTip(listBoxModFound, "");
                return;
            }
            else
            {
                string mod = listBoxModFound.Items[index].ToString();

                DirectoryInfo di = new DirectoryInfo(PathCombine(_AbsModFolder, mod));

                string firstFileName =
                        di.GetFiles()
                          .Select(fi => fi.Name)
                    .FirstOrDefault(name => name.Contains(_YameConst.Infomodext));

                if (firstFileName == null)
                {
                    toolTip1.SetToolTip(listBoxModFound, "");
                    return;
                }

                string tip = File.ReadAllText(PathCombine(_AbsModFolder, mod, firstFileName));

                if (toolTip1.GetToolTip(listBoxModFound) != tip)
                {
                    toolTip1.InitialDelay = 10;
                    toolTip1.AutoPopDelay = 20000;
                    toolTip1.SetToolTip(listBoxModFound, tip);
                }
            }
        }

        private void listBox_DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            SolidBrush sb = new SolidBrush(e.ForeColor);
            if (((ListBox)sender).Name == "listBoxModActivated")
            {
                if (_ActivatedModsList.Any(mod => mod.ModName == listBoxModActivated.Items[e.Index].ToString() & mod.IsDisabled))
                    sb = new SolidBrush(Color.Gray);
            }            
            try
            {
                Brush bg = new SolidBrush(Color.FromArgb(105, 105, 105));
                e.DrawBackground();
                Graphics g = e.Graphics;
                Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? bg : new SolidBrush(e.BackColor);
                g.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, sb, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
                
            }
            catch (Exception)
            {
            }

            /* Calculate the horizontal scrollbar */
            int maxWidth = 0;

            for (int i = 0; i < ((ListBox)sender).Items.Count; i++)
            {
                int testWidth = TextRenderer.MeasureText(((ListBox)sender).Items[i].ToString(),
                                                         ((ListBox)sender).Font, ((ListBox)sender).ClientSize,
                                                         TextFormatFlags.NoPrefix).Width;
                if (testWidth > maxWidth)
                    maxWidth = testWidth + 5;
            }

            ((ListBox)sender).HorizontalExtent = maxWidth;
        }

        private void tbCreateMod_Leave(object sender, EventArgs e)
        {
            tbCreateMod.Hide();
            this.Controls.SetChildIndex(listBoxModFound, 0);
            this.Controls.SetChildIndex(tbCreateMod, 1);
        }

        private void tbCreateMod_KeyDown(object sender, KeyEventArgs e)
        {
            Size size = TextRenderer.MeasureText(tbCreateMod.Text, tbCreateMod.Font);
            tbCreateMod.Width = size.Width;
            tbCreateMod.Height = size.Height;

            if (e.KeyCode == Keys.Escape)
            {
                tbCreateMod.Hide();
                this.Controls.SetChildIndex(listBoxModFound, 0);
                this.Controls.SetChildIndex(tbCreateMod, 1);
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (tbCreateMod.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
                {
                    if (!listBoxModFound.Items.Contains(tbCreateMod.Text) && !string.IsNullOrEmpty(tbCreateMod.Text))
                    {
                        string createpath = _AbsModFolder + "\\" + tbCreateMod.Text;

                        try
                        {
                            Directory.CreateDirectory(createpath);
                        }
                        catch (Exception excp)
                        {
                            MessageBox.Show("Creating [" + createpath + "] is throwing exception!\r\n\r\n" + excp);
                        }

                    }

                    tbCreateMod.Hide();
                    this.Controls.SetChildIndex(listBoxModFound, 0);
                    this.Controls.SetChildIndex(tbCreateMod, 1);
                    buttonRefresh.PerformClick();
                }
            }
        }
        
		void listBoxModFound_DragEnter(object sender, DragEventArgs e) 
		{
			DragDropEffects effect = DragDropEffects.None;
		    if (e.Data.GetDataPresent(DataFormats.FileDrop))
		    {
				var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
				if (Directory.Exists(path)) {
					// KeyState 8 : CTRL Key (+ 1 for mouse button left)
					if (e.KeyState == 9)
						effect = DragDropEffects.Copy;
					else 
						effect = DragDropEffects.Move;
				}
		    }
		    e.Effect = effect;
		}

		void listBoxModFound_DragDrop(object sender, DragEventArgs e) 
		{
			string[] directories = (string[])e.Data.GetData(DataFormats.FileDrop);
			bool MoveDir = true;
			
			// KeyState 4 : SHIFT Key
			// KeyState 8 : CTRL Key
			if (e.KeyState == 8)
				MoveDir = false;
				
			foreach (string dir in directories)
			{
				// get the file attributes for file or directory
				FileAttributes attr = File.GetAttributes(dir);
				if (!attr.HasFlag(FileAttributes.Directory))
					continue;
    
				string fullPath = Path.GetFullPath(dir).TrimEnd(Path.DirectorySeparatorChar);
				string modName = Path.GetFileName(fullPath);
			
				if(Directory.Exists(_AbsModFolder + "\\" + modName))
					continue;
			
				try 
				{
					if(MoveDir)
					{
						if(Path.GetPathRoot(dir) == Path.GetPathRoot(_AbsModFolder))
							Directory.Move(dir, _AbsModFolder + "\\" + modName);
						else 
						{
							DirectoryCopy(dir, _AbsModFolder + "\\" + modName, true);		
							Directory.Delete(dir, true);
						}
					}						
					else
						DirectoryCopy(dir, _AbsModFolder + "\\" + modName, true);		
				}
				catch (IOException exp)
				{
				    MessageBox.Show(exp.Message);
				}
				
			}
			
			buttonRefresh.PerformClick();
		}
        
        private void buttonLoadSave_Click(object sender, EventArgs e)
        {
        	/* Disable save mod list if empty */
        	contextMenuStrip2.Items[1].Enabled = true;
        	if(listBoxModActivated.Items.Count <= 0)
        	{
        		contextMenuStrip2.Items[1].Enabled = false;
        	}
        	
        	/* Disable compare snapshot if not exists */
        	contextMenuStrip2.Items[4].Enabled = true;
        	if(!File.Exists(_AbsModFolder + "\\" + _YameConst.Snapshotfilename))
        	{
        		contextMenuStrip2.Items[4].Enabled = false;
        	}
        	
            buttonLoadSave.ContextMenuStrip = contextMenuStrip2;
            contextMenuStrip2.Show(MousePosition);
            
        }

        private void toolStripMenuLoad_Click(object sender, EventArgs e)
        {
            int size = -1;
            openFileDialog1.InitialDirectory = _AbsModFolder;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.DefaultExt = "mep";
            openFileDialog1.Filter = "YAME Mod Profiles (*.ymp)|*.ymp";
            openFileDialog1.FileName = "";

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string ympFile = openFileDialog1.FileName;

                if (Path.GetExtension(ympFile) != ".ymp")
                {
                    MessageBox.Show("Not a YAME Mod Profile file!");
                    return;
                }
                    
                string contentYmpFile = "";
                try
                {
                    contentYmpFile = File.ReadAllText(ympFile);
                    size = contentYmpFile.Length;
                }
                catch (IOException)
                {
                }

                if(size>0)
                {
                	// if ok, disable all mod in activated mods before loading those from file
                	performButtonModAllMoins_Click(sender, e, true);
                	
                    string[] stringSeparators = new string[] { "\r\n" };
                    string[] lines = contentYmpFile.Split(stringSeparators, StringSplitOptions.None);
                    foreach (string s in lines)
                    {
                        listBoxModFound.SelectedIndex = -1;
                        if (!String.IsNullOrEmpty(s))
                        {
                            for (int index = 0; index < listBoxModFound.Items.Count; index++)
                            {
                                string item = listBoxModFound.Items[index].ToString();
                                if (s == item)
                                {
                                    listBoxModFound.SelectedIndex = index;
                                    performButtonModPlus_Click(sender, e, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void toolStripMenuSave_Click(object sender, EventArgs e)
        {
            if (listBoxModActivated.Items.Count <= 0)
                return;

            saveFileDialog1.InitialDirectory = _AbsModFolder;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = "mep";
            saveFileDialog1.Filter = "YAME Mod Profiles (*.ymp)|*.ymp";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string contentYmpFile = "";
            foreach (string mod in listBoxModActivated.Items)
            {
                contentYmpFile += mod + "\r\n";
            }

            string ympFile = saveFileDialog1.FileName;
            File.WriteAllText(ympFile, contentYmpFile);
        }

		void toolStripMenuGenerateSnapshot_Click(object sender, EventArgs e)
		{
			if(File.Exists(_AbsModFolder + "\\" + _YameConst.Snapshotfilename))
		   	{
				DialogResult result1 = MessageBox.Show("A snapshot already exists. Are you sure you want to replace it ?", "Generate snapshot", MessageBoxButtons.YesNo);
	            	if (result1 == DialogResult.No)
	                	return;	
		   	}


            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            

            /* Show ProgressBar and Wait Cursor */
            Cursor.Current = Cursors.WaitCursor;

            var listOfFilesInDir = Directory.GetFiles(_StartPath, "*", SearchOption.AllDirectories).Where(s => !s.StartsWith(_AbsModFolder));
            int totalFiles = listOfFilesInDir.Count();

            progressBar1.Value = progressBar1.Minimum;
			progressBar1.Maximum = totalFiles;
            progressBar1.Visible = true;

            _ContentSnapshot = "";
			ProcessGenerateSnapshot(_StartPath);

            progressBar1.Value = progressBar1.Maximum;                        

            File.WriteAllText(_AbsModFolder + "\\" + _YameConst.Snapshotfilename, _ContentSnapshot);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            MessageBox.Show("A snapshot of all game files has been generated. Use the \"Compare game files to snapshot\" task at a later date to determinate any changes." + "\r\n\r\n" + "Elapsed time " + elapsedMs + "ms." );
			
	        /* Hide ProgressBar and Cursor default */
            progressBar1.Value = progressBar1.Minimum;
            progressBar1.Visible = false;
            Cursor.Current = Cursors.Default;
		}
		
		private void ProcessGenerateSnapshot(string dir)
		{
		    try
		    {
		    	foreach (string f in Directory.GetFiles(dir))
		    	{
		    		if (progressBar1.Value < progressBar1.Maximum)
		    			progressBar1.Value++;
		    			
		    		if(Path.GetFileName(f).Contains(_YameConst.Appname))
	        	   		continue;
		    		
		    		// To avoid exception path too long on read file (260 car. max)
		    		if(f.Length < 260)
		    		{
                        /* CHECKSUM.Algorithms. MD5 SHA1 SHA256 SHA384 SHA512 RIPEMD160 */
                        string checksum = CHECKSUM.Algorithms.GetHashFromFile(f, CHECKSUM.Algorithms.SHA1);
                        _ContentSnapshot += f + "\t" + checksum + "\r\n";
		    		}
		    		else 
		    		{
		    			_ContentSnapshot += f + "\t" + "#NO MD5 CAUSE OF PATH TOO LONG!#" + "\r\n";
		    		}
		    	}
		        
		        foreach (string d in Directory.GetDirectories(dir))
		        {
		        	if (progressBar1.Value < progressBar1.Maximum)
		    			progressBar1.Value++;
		        	
		        	if(d == _AbsModFolder)
	        			continue;
		        	
		        	_ContentSnapshot += d + "\\" + "\r\n";
		            ProcessGenerateSnapshot(d);
		        }
		
		    }
		    catch (System.Exception ex)
		    {
		    	throw ex;
		    }		    		    
		}

        public void toolStripMenuCompareSnapshot_Click(object sender, EventArgs e)
        {
            /* Show ProgressBar and Wait Cursor */
                        Cursor.Current = Cursors.WaitCursor;
            progressBar1.Value = progressBar1.Minimum;
            progressBar1.Visible = true;


            Dictionary<string, string> itemsSavedSnapshot = new Dictionary<string, string>();
            string line;

            /* 
                Load in Dictionary the saved Snapshot of the game 
            */

            System.IO.StreamReader file = new System.IO.StreamReader(_AbsModFolder + "\\" + _YameConst.Snapshotfilename);
            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split('\t');

                if (split.Length > 1)
                    itemsSavedSnapshot.Add(split[0], split[1]);
                else
                    itemsSavedSnapshot.Add(split[0], "");
            }

            file.Close();
           


            /* 
               Load in Dictionary the now Snapshot of the game 
            */

            Dictionary<string, string> itemsNowSnapshot = new Dictionary<string, string>();
            _ContentSnapshot = "";
            ProcessGenerateSnapshot(_StartPath);

            string[] stringSeparators = new string[] { "\r\n" };
            string[] listLines = _ContentSnapshot.Split(stringSeparators, StringSplitOptions.None);
            foreach (string listLine in listLines)
            {
                if (String.Empty == listLine)
                    continue;

                string[] split = listLine.Split('\t');

                if (split.Length > 1)
                    itemsNowSnapshot.Add(split[0], split[1]);
                else
                    itemsNowSnapshot.Add(split[0], "");
            }


            /* Show ProgressBar */
            progressBar1.Maximum = itemsSavedSnapshot.Count + Math.Abs(itemsNowSnapshot.Count - itemsSavedSnapshot.Count);


            /* 
                Load in Dictionary the result of difference betwwen saved and now Snaphot of the game 
            */

            int counterNew = 0;
            int counterSame = 0;
            int counterDifferent = 0;
            int counterRemoved = 0;

            SortedDictionary<string, string> itemsCompareSnapshot = new SortedDictionary<string, string>();

            foreach (KeyValuePair<string,string> savedItem in itemsSavedSnapshot)
            {
                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Value++;

                string savedItemFile = savedItem.Key;
                string savedItemChecksum = savedItem.Value;

                string value = "";
                if (itemsNowSnapshot.TryGetValue(savedItemFile, out value)) /* Item is SAME or DIFFERENT */
                {
                    if (value != "" && value != savedItemChecksum)   /* is modified file */
                    {
                        itemsCompareSnapshot.Add(savedItemFile.Substring(_StartPath.Length + 1), "DIFFERENT");
                        itemsNowSnapshot.Remove(savedItemFile);
                        counterDifferent++;
                    }

                    if (value == "" || value == savedItemChecksum)   /* same folder or file */
                    {
                        itemsCompareSnapshot.Add(savedItemFile.Substring(_StartPath.Length + 1), "SAME");
                        itemsNowSnapshot.Remove(savedItemFile);
                        counterSame++;
                    }
                }

                else /* Item not found in now Snapshot, is REMOVED */
                {
                    itemsCompareSnapshot.Add(savedItemFile.Substring(_StartPath.Length + 1), "REMOVED");
                    counterRemoved++;
                }
            }

            foreach (KeyValuePair<string, string> nowItem in itemsNowSnapshot)
            {
                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Value++;

                itemsCompareSnapshot.Add(nowItem.Key.Substring(_StartPath.Length + 1), "NEW");
                counterNew++;
            }

            progressBar1.Value = progressBar1.Maximum;

            var result1 = SnapshotForm.Show(_StartPath, PathCombine(_AbsModFolder, _YameConst.Snapshotfilename), itemsCompareSnapshot, counterSame, counterDifferent, counterNew, counterRemoved);

            /* Hide ProgressBar and Cursor default */
            progressBar1.Value = progressBar1.Minimum;
            progressBar1.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void toolStripMenuUpdate_Click(object sender, EventArgs e)
        {

            string currentVersionUrlTxt = _YameConst.Updateurl + _YameConst.Updatefilename;

            using (var client = new MyClient())
            {
                string updateversion = client.DownloadString(currentVersionUrlTxt);

                if (String.Empty != updateversion)
                {
                    Version assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    string currentVersion = assemblyVersion.Major.ToString() + assemblyVersion.Minor.ToString() + assemblyVersion.Build.ToString();
                    string updateVersionFileUrl = _YameConst.Updateurl + _YameConst.Appname + "_" + updateversion + ".zip";


                    if (updateversion != currentVersion)
                    {
                        string message = "Update " + updateversion + " available !\r\n" + "\r\nVisit website for download ?";
                        DialogResult result = SMARTDIALOG.SmartDialog.Show(message, "V" + currentVersion, "Server found.", "Update found", "NO", "YES");
                        if(result == DialogResult.No) // Inverted button on frame ;)
                            System.Diagnostics.Process.Start(_YameConst.Helpurl + "#download");
                    }
                    else
                    {
                        SMARTDIALOG.SmartDialog.Show("Current version is up to date !", "V"+currentVersion, "Server found.", "No update found", "OK");
                    }

                }
                else
                {
                    MessageBox.Show("Website unreachable!");
                }

            }


        }

        class MyClient : System.Net.WebClient
        {
            public bool HeadOnly { get; set; }
            protected override System.Net.WebRequest GetWebRequest(Uri address)
            {
                System.Net.WebRequest req = base.GetWebRequest(address);
                if (HeadOnly && req.Method == "GET")
                {
                    req.Method = "HEAD";
                }
                return req;
            }
        }


        void toolStripMenuHelp_Click(object sender, EventArgs e)
		{
        	System.Diagnostics.Process.Start(_YameConst.Helpurl);			
		}
        
        void toolStripMenuAbout_Click(object sender, EventArgs e)
		{
			var result1 = AboutForm.Show(_YameConst.Version);
		}













































		
        // ---------------------
        // Utils function
        // ---------------------

        public static string PathCombine(params string[] paths)
        {
            string[] myPaths = new string[paths.Length];
            int i = 0;
            foreach (string path in paths)
            {
                if (path.StartsWith(@"\"))
                    myPaths[i] = path.Remove(0, 1);
                else
                    myPaths[i] = path;

                i++;
            }

            return Path.Combine(myPaths);
        }

        public void DeleteFolderIfEmpty(DirectoryInfo dir, string rootNotDelete)
        {
			if(dir.EnumerateFiles().Any() || dir.EnumerateDirectories().Any())
		        return;
		
			if(dir.Name == rootNotDelete)
				return;
		   
			DirectoryInfo parent = dir.Parent;
			dir.Delete();
		
			// Climb up to the parent
			DeleteFolderIfEmpty(parent, rootNotDelete);
		}
        
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    	{
	        // Get the subdirectories for the specified directory.
	        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
	
	        if (!dir.Exists)
	        {
	            throw new DirectoryNotFoundException(
	                "Source directory does not exist or could not be found: "
	                + sourceDirName);
	        }
	
	        DirectoryInfo[] dirs = dir.GetDirectories();
	        // If the destination directory doesn't exist, create it.
	        if (!Directory.Exists(destDirName))
	        {
	            Directory.CreateDirectory(destDirName);
	        }
	
	        // Get the files in the directory and copy them to the new location.
	        FileInfo[] files = dir.GetFiles();
	        foreach (FileInfo file in files)
	        {
	            string temppath = Path.Combine(destDirName, file.Name);
	            file.CopyTo(temppath, false);
	        }
	
	        // If copying subdirectories, copy them and their contents to new location.
	        if (copySubDirs)
	        {
	            foreach (DirectoryInfo subdir in dirs)
	            {
	                string temppath = Path.Combine(destDirName, subdir.Name);
	                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
	            }
	        }
        }
        
        private bool IsFileLocked(string filename)
        {
            FileInfo file = new FileInfo(filename);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public void DeleteFile(string from)
        {
            try
            {
                FileInfo file = new FileInfo(from);
                // check if the file exists
                if (file.Exists)
                {
                    // check if the file is not locked
                    if (IsFileLocked(from) == false)
                    {
                        // move the file
                        File.Delete(from);
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        public void MoveFile(string from, string to)
        {
            try
            {
                FileInfo file = new FileInfo(from);
                // check if the file exists
                if (file.Exists)
                {
                    // check if the file is not locked
                    if (IsFileLocked(from) == false)
                    {
                        // move the file
                        if (File.Exists(to))
                            File.Delete(to);

                        File.Move(from, to);
                    }
                }
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }

        public void CopyFile(string from, string to)
        {
            try
            {
                FileInfo file = new FileInfo(from);
                // check if the file exists
                if (file.Exists)
                {
                    // copy the file
                    File.Copy(from, to);
                }
            }
            catch (Exception)
            {
                ;
            }
        }

  
    }
}

