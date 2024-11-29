using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace Day07_1
{
    public class ElfDirectory
    {
        private string name;
        private ElfDirectory? parentDirectory;
        private List<ElfDirectory> childDirectories = new List<ElfDirectory>();
        private List<double> fileSizes = new List<double>();

        public ElfDirectory(ElfDirectory? parentDirectory, string name)
        {
            this.name = name;
            this.parentDirectory = parentDirectory;
        }

        public void AddFile(double fileSize)
        {
            this.fileSizes.Add(fileSize);
        }


        public void AddDirectory(ElfDirectory directory)
        {
            this.childDirectories.Add(directory);
        }

        public ElfDirectory GetParentDirectory()
        {
            return this.parentDirectory ?? this;
        }

        public ElfDirectory GetChildDirectory(string name)
        {
            return this.childDirectories.FirstOrDefault(dir => dir.name == name) ?? this;
        }

        public double GetTotalFileSize()
        {
            double total = this.GetCurrentDirectoryFileSize();

            foreach (ElfDirectory directory in this.childDirectories)
            {
                total += directory.GetTotalFileSize();
            }

            return total;
        }

        private double GetCurrentDirectoryFileSize()
        {
            return this.fileSizes.Sum();
        }

        private bool HasChildDirectories()
        {
            return this.childDirectories.Count > 0;
        }

        public IEnumerable<ElfDirectory> GetAllDescendants()
        {
            foreach (var childDirectory in this.childDirectories)
            {
                yield return childDirectory;
                             
                if (childDirectory.HasChildDirectories())
                {
                    foreach (var grandChildDirectory in childDirectory.GetAllDescendants())
                    {
                        yield return grandChildDirectory;
                    }
                }
            }
        }

        public ElfDirectory GetRootDirectory()
        {
            if( this.parentDirectory == null)
            {
                return this;
            }

            return this.parentDirectory.GetRootDirectory();
        }

    }
}
