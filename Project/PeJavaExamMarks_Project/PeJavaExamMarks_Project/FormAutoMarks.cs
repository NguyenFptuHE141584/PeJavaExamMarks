using PeJavaExamMarks_Project;
using PeJavaExamMarksProject.CMDInteraction;
using PeJavaExamMarksProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PeJavaExamMarksProject
{
    public partial class FormAutoMarks : Form
    {
        public int ValueOfCbFilter { get; set; }
        public FormAutoMarks()
        {
            InitializeComponent();
        }

        public FormAutoMarks(int valueOfCbFilter)
        {
            InitializeComponent();
            ValueOfCbFilter = valueOfCbFilter;
        }



        private void txTestCase_DoubleClick(object sender, EventArgs e)
        {
            var pathTestCase = new FolderBrowserDialog();
            pathTestCase.ShowDialog ();
            txTestCase.Text = pathTestCase.SelectedPath;
        }

        private void txClass_DoubleClick(object sender, EventArgs e)
        {
            var pathClass = new FolderBrowserDialog();
            pathClass.ShowDialog();
            txClass.Text = pathClass.SelectedPath;
        }

        private void btGrading_Click(object sender, EventArgs e)
        {
            var context = new PeJavaExamMarksContext();
            if (!CheckFolder())
            {
                return;
            }
            else
            {
                var getClassNameByCbFilter = context.Classes.FirstOrDefault(x => x.ClassId == ValueOfCbFilter).ClassName;
                var className = txClass.Text.Substring(txClass.Text.Length - 6, 6);
                if (className.Equals(getClassNameByCbFilter))
                {
                    var students = context.ScoreStudents.Where(x => x.Class.ClassName.Equals(className)).Select(x => x.Student.RollNumber + "_" + x.Student.StudentName + "_" + x.ExamCode).ToList();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = students.Count;
                    foreach (string student in students)
                    {
                        string scoreDetail = "";
                        float totalMark = 0;
                        string[] listQ = GetFolderAnswerByStudent(student);
                        for (int i = 0; i < listQ.Length; i++)
                        {
                            string paperNo = student.Substring(student.Length - 1, 1);
                            string[] listTest = GetListTest(paperNo, listQ[i]);
                            float markQ = 0;
                            for (int j = 0; j < listTest.Length; j++)
                            {
                                string[] listContentInTestCase = File.ReadAllLines($"{txTestCase.Text}\\Paper_No{paperNo}\\{listQ[i]}\\{listTest[j]}");
                                List<string> listInputInTestCase = new List<string>();
                                int indexOutput = 0;
                                for (int k = 0; k < listContentInTestCase.Length; k++)
                                {
                                    if (listContentInTestCase[k].Contains("OUTPUT"))
                                    {
                                        indexOutput = k;
                                    }
                                }
                                for (int k = 0; k < indexOutput; k++)
                                {
                                    listInputInTestCase.Add(listContentInTestCase[k]);
                                }
                                string output = "";
                                for (int k = indexOutput + 1; k < listContentInTestCase.Length - 1; k++)
                                {
                                    output += listContentInTestCase[k];
                                }
                                string markTest = listContentInTestCase[listContentInTestCase.Length - 1];
                                float mark = float.Parse(markTest.Substring(markTest.Length - 3, 3));
                                string resultCmd = InteractionCmd.GetResultScore(txClass.Text, student, listQ[i], listInputInTestCase);
                                string[] listop = resultCmd.Split('\n');
                                int a = 0;
                                int b = 0;
                                for (int k = 0; k < listop.Length; k++)
                                {
                                    if (listop[k].Contains("OUTPUT"))
                                    {
                                        a = k;
                                    }
                                    if (listop[k].EndsWith(">"))
                                    {
                                        b = k;
                                    }
                                }
                                string outputOfSv = "";
                                for (int k = a + 1; k < b; k++)
                                {
                                    outputOfSv += listop[k];
                                }
                                string[] listlast = outputOfSv.Split('\r');
                                outputOfSv = "";
                                for (int k = 0; k < listlast.Length; k++)
                                {
                                    outputOfSv += listlast[k];
                                }
                                if (outputOfSv == output)
                                {
                                    markQ += mark;
                                    totalMark += mark;
                                }
                            }
                            scoreDetail += "[" + listQ[i] + ":" + markQ + "];";
                        }
                        if (scoreDetail.Equals(""))
                        {
                            scoreDetail = "Exam file not found";
                        }
                        var z = context.Students.FirstOrDefault(x => x.RollNumber.Equals(student.Substring(0, 8)));
                        var y = context.ScoreStudents.FirstOrDefault(x => x.StudentId == z.StudentId);
                        y.ScoreDetails = scoreDetail;
                        y.TotalScore = totalMark;
                        y.DateMark = DateTime.Now;
                        context.SaveChanges();
                        progressBar1.Value += 1;
                    }
                    MessageBox.Show("Complete grading class " + className);
                    var classId = context.Classes.FirstOrDefault(x => x.ClassName.Equals(className)).ClassId;
                    progressBar1.Value = 0;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The class name in the selected path does not match the class name in the filter");
                }
            }
        }


        private string[] GetListTest(string paperNo, string qNumber)
        {
            string[] fileArray = Directory.GetFiles($"{txTestCase.Text}\\Paper_No{paperNo}\\{qNumber}", "*.txt");
            for (int i = 0; i < fileArray.Length; i++)
            {
                fileArray[i] = fileArray[i].Substring(fileArray[i].Length - 13, 13);
            }
            return fileArray;
        }
        private string[] GetFolderAnswerByStudent(string student)
        {
            string[] fileArray = Directory.GetDirectories($"{txClass.Text}\\{student}");
            for (int i = 0; i < fileArray.Length; i++)
            {
                fileArray[i] = fileArray[i].Substring(fileArray[i].Length - 2, 2);
            }
            return fileArray;
        }
        private bool CheckFolder()
        {
            if (txTestCase.Text.Length == 0 || txClass.Text.Length == 0)
            {
                MessageBox.Show("Path not empty.");
                return false;
            }
            if (!txTestCase.Text.ToUpper().Contains("TEST_CASE"))
            {
                MessageBox.Show("This path invalid");
                return false;
            }
            if (!Regex.IsMatch(txClass.Text.Substring(txClass.Text.Length - 6, 6), @"^SE\d{4}$"))
            {
                MessageBox.Show("This path invalid");
                return false;
            }
            return true;
        }
    }
}
