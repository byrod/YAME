using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.MOD
{
    class ModObject
    {
        private string modName;
        private int modNumber;
        private bool isDisabled = false;
        private List<string> filesList = new List<string>();  // List of files included by this mod
        private List<string> dependanciesList = new List<string>(); // List of dependance mods
        
        /* temp variables before adding the mod if dependancies accepted by user */
		List<string> createFolderInBackup = new List<string>();
    	List<string> createFolderInGame = new List<string>();
    	List<string> moveFileToBackup = new List<string>();
    	List<string> copyFileToGame = new List<string>();
    	List<string> removeFileFromGame = new List<string>();
    	List<string> alertDependancies = new List<string>();
    	

        public ModObject(string hisname)
        {
            this.ModName = hisname;
        }
        public bool IsDisabled
        {
            get
            {
                return isDisabled;
            }

            set
            {
                isDisabled = value;
            }
        }

        public string ModName
        {
            get
            {
                return modName;
            }

            set
            {
                modName = value;
            }
        }

        public int ModNumber
        {
            get
            {
                return modNumber;
            }

            set
            {
                modNumber = value;
            }
        }
        public List<string> FilesList
        {
            get
            {
                return filesList;
            }

            set
            {
                filesList = value;
            }
        }

        public List<string> DependanciesList
        {
            get
            {
                return dependanciesList;
            }

            set
            {
                dependanciesList = value;
            }
        }
        
        public List<string> CreateFolderInBackup
        {
            get
            {
                return createFolderInBackup;
            }

            set
            {
                createFolderInBackup = value;
            }
        }

        public List<string> CreateFolderInGame
        {
            get
            {
                return createFolderInGame;
            }

            set
            {
                createFolderInGame = value;
            }
        }        
        
        public List<string> MoveFileToBackup
        {
            get
            {
                return moveFileToBackup;
            }

            set
            {
                moveFileToBackup = value;
            }
        }    
        
        public List<string> CopyFileToGame
        {
            get
            {
                return copyFileToGame;
            }

            set
            {
                copyFileToGame = value;
            }
        } 

		public List<string> RemoveFileFromGame
        {
            get
            {
                return removeFileFromGame;
            }

            set
            {
                removeFileFromGame = value;
            }
        }         

        public List<string> AlertDependancies
        {
            get
            {
                return alertDependancies;
            }

            set
            {
                alertDependancies = value;
            }
        } 
        
		public string LogOutput {
			get;
			set;
		} 

        //public String ToString()
        //{
        //    return modName;
        //}

    }
}
