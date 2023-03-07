using PeJavaExamMarksProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeJavaExamMarksProject.CMDInteraction
{
    public class InteractionCmd
    {

        public static string GetResultScore(string pathClass, string student, string qNumber, List<string> listInputInTestCase)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(@"cd \");
            cmd.StandardInput.WriteLine(@"cd /d " + pathClass);
            cmd.StandardInput.WriteLine("cd " + student);
            cmd.StandardInput.WriteLine(@"cd " + qNumber);
            cmd.StandardInput.WriteLine(@"cd run");
            cmd.StandardInput.WriteLine("java -jar " + qNumber + "1" + ".jar");
            foreach (string o in listInputInTestCase)
            {
                cmd.StandardInput.WriteLine(o);
            }
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string resultCmd = cmd.StandardOutput.ReadToEnd();
            cmd.Close();
            return resultCmd;
        }
    }
}

