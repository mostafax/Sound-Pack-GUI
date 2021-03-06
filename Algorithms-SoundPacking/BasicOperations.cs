﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algorithms_SoundPacking
{
    /// <summary>
    /// The Record and Its Details
    /// </summary>
    public class Folder : IComparable
    {
        public int hours, min, sec;
        public long free_space;
        public int index;
        /// <summary>
        /// used in best fit 
        /// </summary>
        public List<Audios> FolderAudios = new List<Audios>();
        public int CompareTo(object other)
        {
            if (other.GetType() == typeof(Folder))
                return ((Folder)other).free_space.CompareTo(free_space);
            return 0;
        }
        public void ConvertUnits()
        {
            while (sec >= 60)
            {
                min++;
                sec -= 60;
            }
            while (min >= 60)
            {
                hours++;
                min -= 60;
            }
        }
    };

    /// <summary>
    /// The Aduios and Its Details
    /// </summary>
    public class Audios
    {
        public string name;
        public int hours, min, sec, total_in_sec, index;

        /// <summary>
        /// The class that contains the lowest level of code to be utilized by higher classes
        /// </summary>
    }
    class BasicOperations
    {
        static public List<Audios> Audio_files = new List<Audios>();
        static public List<Folder> Audio_Folders = new List<Folder>();
        static public int max_size = 0;
        static public int num_of_rec = 0;

        //public
        /// <summary>
        /// A list containing description of target files
        /// </summary>
        /// <summary>
        /// A list containing description of target folders
        /// </summary>

        /// <summary>
        /// initlaize the two lists (Audio_files,Folders)
        /// </summary>
        /// 
        public static void Initlaize(string FileName1, string FileName2)
        {
            //Reading From AudiosInfo ..
            FileStream fs = new FileStream(FileName1, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            //Reading the first line in the File which contain the Nember of Audios Files..
            string num_of_records = sr.ReadLine();
            //Converting the string value into int by using TypeCasting..
            for (int i = 0; i < num_of_records.Length; i++)
            {
                if (num_of_records[i] != ' ')
                {
                    num_of_rec *= 10;
                    num_of_rec += (int)num_of_records[i] - '0';
                }
                else
                    break;
            }
            //Makeing Array of string to hold the Data from File to split it..
            string[] records = new string[num_of_rec];
            //Makeing a Temp Variable to hold the Data and push it in the List..
            //Loop to Get the Data and split it..
            for (int i = 0; i < num_of_rec; i++)
            {
                Audios aud = new Audios();
                records = sr.ReadLine().Split(' ');
                aud.name = records[0];
                string[] temp = records[1].Split(':');
                aud.hours = int.Parse(temp[0]);
                aud.min = int.Parse(temp[1]);
                aud.sec = int.Parse(temp[2]);
                aud.total_in_sec = aud.hours * 3600 + aud.min * 60 + aud.sec;
                aud.index = i + 1;
                Audio_files.Add(aud);
            }
            //Closing the Folder after Geting the Data..
            sr.Close();
            //Reading from Readme to konw the Max_Size of Folder..
            FileStream f = new FileStream(FileName2, FileMode.Open);
            StreamReader s = new StreamReader(f);
            s.ReadLine();
            string[] Temp = new string[2];
            string[] size = new string[2];
            Temp = s.ReadLine().Split('=');
            size = Temp[1].Split(' ');
            //Closing the File after Reading from it..
            s.Close();
            ////Converting the string value into int by using TypeCasting..
            max_size = int.Parse(size[1]);


        }
        /// <summary>
        /// Sort In Increasing Order 
        /// </summary>
        public static void SortInIncreasing(List<Audios> A)
        {
            A.Sort((x, y) => x.total_in_sec.CompareTo(y.total_in_sec));
        }
        /// <summary>
        /// Sort In Decreasing Order
        /// </summary>
        public static void SortInDecreasing(List<Audios> A)
        {
            A.Sort((x, y) => y.total_in_sec.CompareTo(x.total_in_sec));
        }

    }
}
