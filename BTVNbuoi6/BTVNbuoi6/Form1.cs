using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTVNbuoi6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var context = new Model1();
                var listStudents = context.Students
                    .Select(s => new
                    {
                        s.StudentID,
                        s.FullName,
                        FacultyName = s.Faculty.FacultyName, 
                        s.DiemTrungBinh
                    }).ToList();

                dataGridView1.DataSource = listStudents; 
                var listFaculties = context.Faculties.ToList();
                FillFacultyCombobox(listFaculties); 

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillFacultyCombobox(List<Faculty> listFaculties)
        {
            cbkhoa.DataSource = listFaculties;
            cbkhoa.DisplayMember = "FacultyName";
            cbkhoa.ValueMember = "FacultyID";
        }
        private void cbkhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Model1())
                {
                    var student = new Student
                    {
                        StudentID = int.Parse(txtms.Text),
                        FullName = txtten.Text,
                        DiemTrungBinh = float.Parse(txtDTB.Text),
                        FacultyID = (int)cbkhoa.SelectedValue
                    };

                    context.Students.Add(student); 
                    context.SaveChanges(); 

                    MessageBox.Show("Thêm sinh viên thành công!");
                    LoadData(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void del_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null) 
                {
                    int studentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);

                    using (var context = new Model1())
                    {
                        var student = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                        if (student != null)
                        {
                            context.Students.Remove(student); 
                            context.SaveChanges(); 

                            MessageBox.Show("Xóa sinh viên thành công!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để xóa.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null) 
                {
                    int studentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);

                    using (var context = new Model1())
                    {
                        var student = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                        if (student != null)
                        {
                            student.FullName = txtten.Text;
                            student.DiemTrungBinh = float.Parse(txtDTB.Text);
                            student.FacultyID = (int)cbkhoa.SelectedValue;

                            context.SaveChanges(); 

                            MessageBox.Show("Cập nhật sinh viên thành công!");
                            LoadData(); 
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để sửa.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private void LoadData()
        {
            using (var context = new Model1())
            {
                var listStudents = context.Students
                    .Select(s => new
                    {
                        s.StudentID,
                        s.FullName,
                        FacultyName = s.Faculty.FacultyName,
                        s.DiemTrungBinh
                    }).ToList();

                dataGridView1.DataSource = listStudents;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    txtms.Text = row.Cells["StudentID"].Value?.ToString();
                    txtten.Text = row.Cells["FullName"].Value?.ToString();
                    txtDTB.Text = row.Cells["DiemTrungBinh"].Value?.ToString();

                    string facultyName = row.Cells["FacultyName"].Value?.ToString();
                    cbkhoa.SelectedIndex = cbkhoa.FindStringExact(facultyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }
    }
}
