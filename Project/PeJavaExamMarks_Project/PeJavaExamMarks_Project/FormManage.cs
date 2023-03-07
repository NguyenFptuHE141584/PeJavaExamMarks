using PeJavaExamMarksProject;
using PeJavaExamMarksProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace PeJavaExamMarks_Project
{
    public partial class FormManage : Form
    {
        private Account currentAccount;
        public FormManage()
        {
            InitializeComponent();
        }

        public FormManage(Account account)
        {
            InitializeComponent();
            currentAccount = account;
        }
        

        private void FormManage_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDGV();
            CheckDGVNotNullOrNull();    
        }

        private void CheckDGVNotNullOrNull()
        {
            if (dataGridView1.DataSource == null)
            {
                cbFilter.Enabled = false;
                txSearch.Enabled = false;
                btAutoMarks.Enabled = false;
                btExport.Enabled = false;
                txPathChooseClass.Enabled = true;
                btGenerate.Enabled = true;
                btGenerate.Text = "Generate";
            }
            else
            {
                cbFilter.Enabled = true;
                txSearch.Enabled = true;
                btAutoMarks.Enabled = true;
                btExport.Enabled = true;
                txPathChooseClass.Enabled = false;
                btGenerate.Enabled = true;
                btGenerate.Text = "Re-Generate";
            }
        }
        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var dialogFolder = new FolderBrowserDialog();
            dialogFolder.ShowDialog();
            txPathChooseClass.Text = dialogFolder.SelectedPath;
        }

        private void btLogOut_Click(object sender, EventArgs e)
        {
            currentAccount = null;
            this.Close();
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }

       

        private void btGenerate_Click(object sender, EventArgs e)
        {
            var context = new PeJavaExamMarksContext();
            if (btGenerate.Text.Equals("Generate"))
            {
                if (txPathChooseClass.Text.Length == 0)
                {
                    MessageBox.Show("Please choose path");
                    return;
                }
                try
                {
                    string result = GetClass(txPathChooseClass.Text, currentAccount.AccountId);
                    if (!result.Equals(""))
                    {
                        SaveInforStudentNeedMark(result.Split("\n"), txPathChooseClass.Text);
                        txPathChooseClass.Enabled = false;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not generate!");
                }
            }
            else
            {
                context.ClassAccounts.RemoveRange(context.ClassAccounts.ToList());
                context.ScoreStudents.RemoveRange(context.ScoreStudents.ToList());
                context.Students.RemoveRange(context.Students.ToList());
                context.Classes.RemoveRange(context.Classes.ToList());
                context.SaveChanges();
                cbFilter.DataSource = null;
            }
            LoadComboBox();
            LoadDGV();
            CheckDGVNotNullOrNull();

        }
        public string GetClass(string textPath, int currentAccountId)
        {
            var context = new PeJavaExamMarksContext();
            string result = "";
            string[] classesName = Directory.GetDirectories(textPath);
            string className;
            foreach (var item in classesName)
            {
                className = item.Substring(item.Length - 6, 6);
                context.Classes.Add(new Class() { ClassName = className });
                context.SaveChanges();
                var classContext = context.Classes.FirstOrDefault(x => x.ClassName.Equals(className));
                context.ClassAccounts.Add(new ClassAccount() { ClassId = classContext.ClassId, AccountId = currentAccountId });
                context.SaveChanges();
                result += className + "\n";
            }
            return result;
        }
        public  void SaveInforStudentNeedMark(string[] listClass, string textPath)
        {
            var context = new PeJavaExamMarksContext();
            for (int i = 0; i < listClass.Length - 1; i++)
            {
                string[] inforStudents = Directory.GetDirectories($"{textPath}\\{listClass[i]}");
                foreach (var item in inforStudents)
                {
                    var x = item.Split("\\")[8];
                    var infor = x.Split("_");
                    context.Students.Add(new Student
                    {
                        RollNumber = infor[0],
                        StudentName = infor[1]
                    });
                    context.SaveChanges();
                    context.ScoreStudents.Add(new ScoreStudent
                    {
                        ClassId = context.Classes.FirstOrDefault(scorestudent=> scorestudent.ClassName.Equals(listClass[i])).ClassId,
                        StudentId = context.Students.FirstOrDefault(scorestudent => scorestudent.RollNumber.Equals(infor[0])).StudentId,
                        ExamCode = infor[2]
                    });
                    context.SaveChanges();
                }
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDGV();
        }



        private void LoadComboBox()
        {
            var context = new PeJavaExamMarksContext();
            cbFilter.DisplayMember = "ClassName";
            cbFilter.ValueMember = "ClassId";
            var classes = context.ClassAccounts.Where(x => x.AccountId == currentAccount.AccountId)
                    .Select(x => new Class { ClassName = x.Class.ClassName, ClassId = x.ClassId }).ToList();
            cbFilter.DataSource = classes;
            LoadDGV();
        }
        public void LoadDGV()
        {
            var context = new PeJavaExamMarksContext();
            var classAccounts = context.ClassAccounts.FirstOrDefault(x => x.AccountId == currentAccount.AccountId);
            if (classAccounts == null)
            {
                dataGridView1.DataSource = null;
            }
            else
            {
                var classId = Convert.ToInt32(cbFilter.SelectedValue.ToString());
                    dataGridView1.DataSource = context.ScoreStudents.Join(context.ClassAccounts,
                         st => st.ClassId,
                         ca => ca.ClassId,
                         (st, ca) => new { st, ca }
                         ).Where(x => x.ca.AccountId == currentAccount.AccountId && x.ca.ClassId == classId).Select(x => new
                         {
                             ClassName = x.st.Class.ClassName,
                             RollNumber = x.st.Student.RollNumber,
                             StudentName = x.st.Student.StudentName,
                             ExamCode = x.st.ExamCode,
                             ScoreDetails = x.st.ScoreDetails,
                             TotalScore = x.st.TotalScore,
                             DateMark = x.st.DateMark
                         }).ToList();
            }
        }

        private void btAutoMarks_Click(object sender, EventArgs e)
        {
            var valueOfCbFilter =Convert.ToInt32(cbFilter.SelectedValue.ToString());
            FormAutoMarks formAutoMarks = new FormAutoMarks(valueOfCbFilter);
            formAutoMarks.ShowDialog();
            LoadDGV();
        }

        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            var context = new PeJavaExamMarksContext();
           
            if (txSearch.Text.Length == 0)
            {
                LoadDGV();
            }
            else
            {
                dataGridView1.DataSource = context.ScoreStudents.Join(context.ClassAccounts,
                        st => st.ClassId,
                        ca => ca.ClassId,
                        (st, ca) => new { st, ca }
                        ).Where(x => x.ca.AccountId == currentAccount.AccountId 
                            &&  x.ca.ClassId == Convert.ToInt32(cbFilter.SelectedValue.ToString())
                            && x.st.Student.RollNumber.Contains(txSearch.Text))
                        .Select(x => new
                        {
                            ClassName = x.st.Class.ClassName,
                            RollNumber = x.st.Student.RollNumber,
                            StudentName = x.st.Student.StudentName,
                            ExamCode = x.st.ExamCode,
                            ScoreDetails = x.st.ScoreDetails,
                            TotalScore = x.st.TotalScore,
                            DateMark = x.st.DateMark
                        }).ToList();
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

    }
}
