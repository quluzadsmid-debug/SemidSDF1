using SemidSDF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SemidSDF1
{
    public partial class Form1 : Form
    {
        private EmployeeServiceBase service = new EmployeeService();

        private TabControl tabControl1;
        private TabPage tab1, tab2, tab3;

        private TextBox txtCode, txtName, txtSurname, txtAge;
        private TextBox txtPCode, txtPTitle, txtPAmount;
        private TextBox txtSCode;
        private RichTextBox rtbResult;

        public Form1()
        {
            InitializeComponent();
            DizayniQur();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void DizayniQur()
        {
            this.Text = "Maaş İdarəetmə Sistemi";
            this.Width = 620;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10); // Bütün forma üçün ümumi təmiz şrift

            tabControl1 = new TabControl();
            tabControl1.Dock = DockStyle.Fill;

            tab1 = new TabPage("1. İşçi Əlavə Et");
            tab2 = new TabPage("2. Ödəniş Yaz");
            tab3 = new TabPage("3. Statistika");

            Tab1Dizayn();
            Tab2Dizayn();
            Tab3Dizayn();

            tabControl1.TabPages.Add(tab1);
            tabControl1.TabPages.Add(tab2);
            tabControl1.TabPages.Add(tab3);

            this.Controls.Add(tabControl1);
        }

        private void Tab1Dizayn()
        {
            int startX = 40, startY = 40, spacing = 50;

            Label l1 = new Label() { Text = "Kod:", Left = startX, Top = startY, Width = 90 };
            txtCode = new TextBox() { Left = 140, Top = startY - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Label l2 = new Label() { Text = "Ad:", Left = startX, Top = startY + spacing, Width = 90 };
            txtName = new TextBox() { Left = 140, Top = startY + spacing - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Label l3 = new Label() { Text = "Soyad:", Left = startX, Top = startY + (spacing * 2), Width = 90 };
            txtSurname = new TextBox() { Left = 140, Top = startY + (spacing * 2) - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Label l4 = new Label() { Text = "Yaş:", Left = startX, Top = startY + (spacing * 3), Width = 90 };
            txtAge = new TextBox() { Left = 140, Top = startY + (spacing * 3) - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Button btnAddEmp = new Button() { Text = "İşçini Yadda Saxla", Left = 140, Top = startY + (spacing * 4), Width = 220, Height = 38 };
            btnAddEmp.BackColor = Color.LightGreen;
            btnAddEmp.UseVisualStyleBackColor = true;
            btnAddEmp.Click += BtnAddEmp_Click;

            tab1.Controls.AddRange(new Control[] { l1, txtCode, l2, txtName, l3, txtSurname, l4, txtAge, btnAddEmp });
        }

        private void BtnAddEmp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text))
            {
                MessageBox.Show("Bütün xanaları doldurun!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtAge.Text, out int age);

            Employee emp = new Employee { Code = txtCode.Text, FirstName = txtName.Text, LastName = txtSurname.Text, Age = age };
            bool ugurlu = service.AddEmployee(emp);

            if (!ugurlu)
            {
                MessageBox.Show("Bu koda malik işçi artıq mövcuddur!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("İşçi uğurla əlavə olundu!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtCode.Clear(); txtName.Clear(); txtSurname.Clear(); txtAge.Clear();
        }

        private void Tab2Dizayn()
        {
            int startX = 40, startY = 40, spacing = 50;

            Label l1 = new Label() { Text = "İşçi Kodu:", Left = startX, Top = startY, Width = 100 };
            txtPCode = new TextBox() { Left = 150, Top = startY - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Label l2 = new Label() { Text = "Ödəniş Adı:", Left = startX, Top = startY + spacing, Width = 100 };
            txtPTitle = new TextBox() { Left = 150, Top = startY + spacing - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Label l3 = new Label() { Text = "Məbləğ:", Left = startX, Top = startY + (spacing * 2), Width = 100 };
            txtPAmount = new TextBox() { Left = 150, Top = startY + (spacing * 2) - 3, Width = 220, Font = new Font("Segoe UI", 10) };

            Button btnAddPay = new Button() { Text = "Ödəniş Yaz", Left = 150, Top = startY + (spacing * 3), Width = 220, Height = 38 };
            btnAddPay.BackColor = Color.LightBlue;
            btnAddPay.UseVisualStyleBackColor = true;
            btnAddPay.Click += BtnAddPay_Click;

            tab2.Controls.AddRange(new Control[] { l1, txtPCode, l2, txtPTitle, l3, txtPAmount, btnAddPay });
        }

        private void BtnAddPay_Click(object sender, EventArgs e)
        {
            double.TryParse(txtPAmount.Text, out double amount);

            bool ugurlu = service.AddPayment(txtPCode.Text, txtPTitle.Text, amount);

            if (!ugurlu)
            {
                MessageBox.Show("Xəbərdarlıq: Belə kodlu işçi tapılmadı və ya məbləğ yanlışdır!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Ödəniş uğurla əlavə olundu!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtPTitle.Clear(); txtPAmount.Clear();
        }

        private void Tab3Dizayn()
        {
            Label l1 = new Label() { Text = "İşçi Kodu:", Left = 20, Top = 25, Width = 80 };
            txtSCode = new TextBox() { Left = 105, Top = 22, Width = 160, Font = new Font("Segoe UI", 10) };

            Button btnSearch = new Button() { Text = "Axtar", Left = 275, Top = 20, Width = 90, Height = 30 };
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;

            rtbResult = new RichTextBox() { Left = 20, Top = 65, Width = 550, Height = 350, ReadOnly = true };
            rtbResult.Font = new Font("Consolas", 10);

            tab3.Controls.AddRange(new Control[] { l1, txtSCode, btnSearch, rtbResult });
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Employee emp = service.FindEmployee(txtSCode.Text);

            if (emp == null)
            {
                MessageBox.Show("Xəbərdarlıq: Belə kodlu işçi tapılmadı!", "Tapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbResult.Clear();
                return;
            }

            List<Payment> odenisler = service.GetEmployeePayments(emp.Code);

            string netice = "========================================\n";
            netice += " İŞÇİ STATİSTİKASI VƏ MƏLUMATLARI\n";
            netice += "========================================\n";
            netice += " İşçi Kodu : " + emp.Code + "\n";
            netice += " Ad        : " + emp.FirstName + "\n";
            netice += " Soyad     : " + emp.LastName + "\n";
            netice += " Yaş       : " + emp.Age + "\n";
            netice += "----------------------------------------\n";
            netice += " ÖDƏNİŞLƏR SİYAHISI:\n";

            double cem = 0;
            if (odenisler == null || odenisler.Count == 0)
            {
                netice += " (Hələ heç bir ödəniş əlavə edilməyib)\n";
            }
            else
            {
                foreach (var p in odenisler)
                {
                    netice += " • " + p.Title + " — " + p.Amount + " AZN\n";
                    cem += p.Amount;
                }
            }
            netice += "----------------------------------------\n";
            netice += " Ümumi Cəmi: " + cem + " AZN\n";
            netice += "========================================\n";

            rtbResult.Text = netice;
        }
    }
}