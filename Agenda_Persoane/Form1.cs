using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Agenda_Persoane
{
    public partial class Form1 : Form
    {
        private List<Persoana> persoane = new List<Persoana>();
        private TreeNode[] nodes = new TreeNode[4];
        private string filePath = $"C:\\Users\\Andro\\source\\repos\\Agenda_Persoane\\Agenda_Persoane\\Adaugare.txt";
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            this.Load += new EventHandler(Form2_Load);
            this.Load += new EventHandler(Form3_Load);
            this.Load += new EventHandler(Form4_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode parentNode = new TreeNode();
            parentNode.Name = "parent_1";
            parentNode.Text = "Prieteni";
            treeView1.Nodes.Add(parentNode);
            nodes[0] = parentNode;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            TreeNode parentNode = new TreeNode();
            parentNode.Name = "parent_2";
            parentNode.Text = "Colegi";
            treeView1.Nodes.Add(parentNode);
            nodes[1] = parentNode;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            TreeNode parentNode = new TreeNode();
            parentNode.Name = "parent_3";
            parentNode.Text = "Familie";
            treeView1.Nodes.Add(parentNode);
            nodes[2] = parentNode;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            TreeNode parentNode = new TreeNode();
            parentNode.Name = "parent_4";
            parentNode.Text = "Cunoscuti";
            treeView1.Nodes.Add(parentNode);
            nodes[3] = parentNode;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (nodes.Contains(e.Node))
                return;
            string nume = e.Node.Text;
            Persoana persoana = persoane.Find(p => p.Nume == nume);
            if (persoana != null)
            {
                propertyGrid1.SelectedObject = persoana;
            }
            else
            {
                propertyGrid1.SelectedObject = null;
            }
            LoadDataFromFile(filePath);
        }   

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            string nume = textNume.Text;
            string telefon = textTelefon.Text; 
            DateTime dataNasterii = dateTimePickerNastere.Value;
            string adresa = textAdresa.Text;
            Categorie categorie = (Categorie)comboBoxCategorie.SelectedIndex;

            Persoana persoana = new Persoana(nume,dataNasterii, telefon,adresa, categorie);

            persoane.Add(persoana);

            TreeNode childNode = new TreeNode();
            childNode.Text = nume;
            nodes[(int)categorie].Nodes.Add(childNode);
        }

        public void SaveDataToFile(string file)
        {

            using (StreamWriter sw = new StreamWriter(file))
            {
                foreach (Persoana p in persoane)
                {

                    sw.WriteLine($"Nume : {p.Nume}");
                    sw.WriteLine($"Data Nasterii: {p.DataNasterii}");
                    sw.WriteLine($"Telefon : {p.Telefon}");
                    sw.WriteLine($"Adresa : {p.Adresa}");
                    sw.WriteLine($"Categorie : {p.Categorie}");
                    sw.WriteLine("-------------------------------------------------");
                }
            }

        }

        private void buttonSalvare_Click(object sender, EventArgs e)
        {
            SaveDataToFile(filePath);
            MessageBox.Show("Data saved successfully.", "Save Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        public void LoadDataFromFile(string file)
        {
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    persoane.Clear();
                    treeView1.Nodes.Clear();
                    Form1_Load(null, null);
                    Form2_Load(null, null);
                    Form3_Load(null, null);
                    Form4_Load(null, null);

                    while (!sr.EndOfStream)
                    {
                        {
                            string nume = sr.ReadLine().Split(':')[1].Trim();
                            string dateString= (sr.ReadLine().Split(':')[1].Trim());
                            DateTime dataNasterii;
                            if (!DateTime.TryParse(dateString, out dataNasterii))
                            {
                                dataNasterii = DateTime.MinValue; 
                            }
                            string telefon = sr.ReadLine().Split(':')[1].Trim();
                            string adresa = sr.ReadLine().Split(':')[1].Trim();
                            Categorie categorie = (Categorie)Enum.Parse(typeof(Categorie), sr.ReadLine().Split(':')[1].Trim());
                            sr.ReadLine();

                            Persoana persoana = new Persoana(nume, dataNasterii, telefon, adresa, categorie);
                            persoane.Add(persoana);

                            TreeNode childNode = new TreeNode();
                            childNode.Text = nume;
                            nodes[(int)categorie].Nodes.Add(childNode);
                        }
                    }
                }
            }
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void buttonIncarcaDatele_Click(object sender, EventArgs e)
        {
            LoadDataFromFile(filePath);
            MessageBox.Show("Data loaded successfully.", "Load Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonCautare_Click(object sender, EventArgs e)
        {
            foreach (Persoana p in persoane)
            {
                if (textBoxNumeCautare.Text == p.Nume)
                {
                    propertyGrid1.SelectedObject = p;
                }

            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            Persoana persoanaDeSters = null;
            foreach (Persoana p in persoane)
            {
                if (propertyGrid1.SelectedObject == p)
                {
                    persoanaDeSters = p;
                    break;
                }
            }

            if (persoanaDeSters != null)
            {
                persoane.Remove(persoanaDeSters);
                propertyGrid1.SelectedObject = null;
                UpdateTreeView();
                SaveDataToFile(filePath);
            }
        }

        private void UpdateTreeView()
        {
            treeView1.Nodes.Clear();
            Form1_Load(null, null);
            Form2_Load(null, null);
            Form3_Load(null, null);
            Form4_Load(null, null);

            foreach (Persoana p in persoane)
            {
                TreeNode childNode = new TreeNode();
                childNode.Text = p.Nume;
                nodes[(int)p.Categorie].Nodes.Add(childNode);
            }
        }
    }
}
