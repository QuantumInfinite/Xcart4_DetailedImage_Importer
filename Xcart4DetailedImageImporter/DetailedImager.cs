using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Xcart4DetailedImageImporter
{
    class DetailedImager
    {
        StreamWriter detailedImgFile;
        string imagespathRemote = ""; //Folder on your website filesystem
        string imagesPathLocal = Environment.CurrentDirectory.Replace("\\", "/"); //Folder on your system filesystem
        string outputFolder = Environment.CurrentDirectory.Replace("\\", "/");
        public DetailedImager() {
            GetInputs();
            PrepareCsv();
            string[] folders = GetSubFolders();
            foreach (string folder in folders) {
                string[] files = GetFiles(imagesPathLocal + folder);
                foreach (string file in files) {
                    Write(string.Format(@"{1},{0}/{1}{2}", imagespathRemote, folder, file));
                }
            }
            detailedImgFile.Close();
            Console.WriteLine("CSV file created at\n " + outputFolder + @"\DetailedImages.csv");
            Console.WriteLine("Would you like to open the folder? Y/N");
            if (Console.ReadLine().ToUpper() == "Y") {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                    FileName = outputFolder,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        void GetInputs() {
            string input = "";

            Console.WriteLine("Path to local files (Leave Blank for this folder)");
            while (true) {
                input = Console.ReadLine();
                if (Directory.Exists(input)) {
                    imagesPathLocal = (input + @"\").Replace("\\", "/");
                    break;
                }
                else if(input == "") {
                    break;
                }
                Console.WriteLine("Invalid Input. Try again");
            }

            Console.WriteLine("Folder name for images on server (i.e \"files/detailed\"");
            imagespathRemote = Console.ReadLine();

            Console.WriteLine("Path to folder output file (Leave Blank to output to this folder)");
            while (true) {
                input = Console.ReadLine();
                if (Directory.Exists(input)) {
                    outputFolder = (input + @"\").Replace("\\", "/");
                    break;
                }
                else if (input == "") {
                    break;
                }
                Console.WriteLine("Invalid Input. Try again");
            }
        }

        string[] GetSubFolders() {
            string[] folders = Directory.GetDirectories(imagesPathLocal);
            for (int i = 0; i < folders.Length; i++) {
                folders[i] = folders[i].Split('/').LastOrDefault();
            }
            return folders;
        }
        string[] GetFiles(string url) {
            string s = url.Replace("/", @"\");
            string[] files = Directory.GetFiles(s, "*.jpg");
            for (int i = 0; i < files.Length; i++) {
                files[i] = files[i].Substring(url.Length).Replace("\\", @"/"); ;
            }
            return files;

        }

        void PrepareCsv() {

            if (File.Exists(outputFolder + @"\DetailedImages.csv")) {
                File.Delete(outputFolder + @"\DetailedImages.csv");
            }


            detailedImgFile = new StreamWriter(outputFolder + @"\DetailedImages.csv", true);
            detailedImgFile.AutoFlush = true;
            Write("[DETAILED_IMAGES] \n!PRODUCTCODE,!IMAGE");

        }
        public void Write(string s) {
            detailedImgFile.WriteLine(s);
        }
        static void Main(string[] args) {
            DetailedImager a = new DetailedImager();
        }
    }
}