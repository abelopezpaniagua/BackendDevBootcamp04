using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise.BL
{
    public interface IFolderManager
    {
        public List<Folder> GetFolders();
        public bool AddFolder(string name);
        public bool RemoveFolder(Folder folder);
    }
}
