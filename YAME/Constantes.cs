using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME
{
    public class Constantes
    {
        private const string appname = "YAME";
        private const string ininame = appname + ".ini";
        private const string backupdir = "!YABACKUP";
        private const string modlogsdir = "!YAMODLOGS";
        private const string defaultmoddir = "MODS";
        private const string infomodext = ".yame";
        private const string infomodfldr = "documentation";
        private const string removetickfile = "-remove";
        private const string snapshotfilename = "YAME Game Snapshot.txt";
        private const string version = "Yet Another Mod Enabler - v";
        private const string newmodfolder = "My New Mod Folder";
        private const string helpurl = "http://yame.byrod.fr/";
        private const string updateurl = "http://yame.byrod.fr/download/";


        public Constantes()
        {

        }

        public string Backupdir
        {
            get 
            {
                return backupdir;
            }

        }

        public string Modlogsdir
        {
            get
            {
                return modlogsdir;
            }

        }

        public string Defaultmoddir
        {
            get
            {
                return defaultmoddir;
            }
        }

        public string Appname
        {
            get
            {
                return appname;
            }
        }

        public string Ininame
        {
            get
            {
                return ininame;
            }
        }

        public string Infomodext
        {
            get
            {
                return infomodext;
            }
        }

        public string Infomodfldr
        {
            get
            {
                return infomodfldr;
            }
        }

        public string Removetickfile
        {
        	get
        	{
        		return removetickfile;
        	}
        	
        }
        public string Version
        {
            get
            {
//                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
//                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
//                return version + fvi.FileVersion;
  				
				Version assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
  				return version + assemblyVersion.Major + "." + assemblyVersion.Minor + "." + assemblyVersion.Build; //change form title                
            }
        }

        public string Newmodfolder
        {
            get
            {
                return newmodfolder;
            }
        }
        
        public string Snapshotfilename
        {
            get
            {
                return snapshotfilename;
            }
        }
        
        public string Helpurl
        {
        	get
        	{
        		return helpurl;
        	}
        }

        public string Updateurl
        {
            get
            {
                return updateurl;
            }
        }
    }
}
