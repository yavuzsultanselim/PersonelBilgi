using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelBilgi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PersonelDb db = new PersonelDb();

        public void NewPerson()
        {
            Personel personel = new Personel();
            personel.Name = txtAd.Text;
            personel.LastName = txtSoyad.Text;
            personel.Tc = txtTC.Text;
            personel.BirthDate = dtbirth.Value;
            personel.PhoneNumber = txtTelefon.Text;
            personel.Email = txtEmail.Text;
            personel.Adress = txtAdres.Text;
            personel.StartDate = dtstart.Value;
            personel.Title = txtTitle.Text;

            db.Personeller.Add(personel);
            bool sonuc = db.SaveChanges() > 0;



            MessageBox.Show(sonuc ? $"{personel.Name} {personel.LastName} başarılı şekilde listeye eklendi" : "Bir sorun oluştu");
            KisiListesi();
        }
        public void Delete()
        {

            if (listView1.SelectedItems.Count > 0)
            {
                var id = ((Personel)listView1.SelectedItems[0].Tag).ID;
                if (id > 0)
                {
                    Personel personel = db.Personeller.Find(id);
                    DialogResult dr = MessageBox.Show($"{personel.Name}{personel.LastName} kişiyi silmek istiyor musunuz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        db.Personeller.Remove(personel);
                        db.SaveChanges();
                        KisiListesi();
                    }
                    else
                    {
                        KisiListesi();
                    }
                }
            }
        }
        public void SeePerson()
        {
            guncellenecek = (Personel)listView1.SelectedItems[0].Tag;
            txtAd.Text = guncellenecek.Name;
            txtSoyad.Text = guncellenecek.LastName;
            txtTC.Text = guncellenecek.Tc;
            dtbirth.Value = guncellenecek.BirthDate;
            txtTelefon.Text = guncellenecek.PhoneNumber;
            txtEmail.Text = guncellenecek.Email;
            txtAdres.Text = guncellenecek.Adress;
            dtstart.Value = guncellenecek.StartDate;
            txtTitle.Text = guncellenecek.Title;
        }
        public void UpdatePerson()
        {

            guncellenecek.Name = txtAd.Text;
            guncellenecek.LastName = txtSoyad.Text;
            guncellenecek.Tc = txtTC.Text;
            guncellenecek.BirthDate = dtbirth.Value;
            guncellenecek.PhoneNumber = txtTelefon.Text;
            guncellenecek.Email = txtEmail.Text;
            guncellenecek.Adress = txtAdres.Text;
            guncellenecek.StartDate = dtstart.Value;
            guncellenecek.Title = txtTitle.Text;

            db.SaveChanges();
            KisiListesi();
        }
        Personel guncellenecek;

        public void KisiListesi()
        {
            listView1.Items.Clear();
            foreach (Personel person in db.Personeller)
            {
                ListViewItem listView = new ListViewItem();
                listView.Text = person.ID.ToString();
                listView.SubItems.Add(person.Name);
                listView.SubItems.Add(person.LastName);
                listView.SubItems.Add(person.Tc);
                listView.SubItems.Add(person.BirthDate.ToString());
                listView.SubItems.Add(person.PhoneNumber);
                listView.SubItems.Add(person.Adress);
                listView.SubItems.Add(person.StartDate.ToString());
                listView.SubItems.Add(person.Email);
                listView.SubItems.Add(person.Title);

                listView.Tag = person;
              
                listView1.Items.Add(listView);
            }
        }
        public void KisiListesi(string param)
        {
            listView1.Items.Clear();
            var person = db.Personeller.Where(x => x.Name.StartsWith(param) || x.LastName.StartsWith(param)).ToList();
            foreach (var item in person)
            {
                ListViewItem listView = new ListViewItem();
                listView.Text = item.ID.ToString();
                listView.SubItems.Add(item.Name);
                listView.SubItems.Add(item.LastName);
                listView.SubItems.Add(item.Tc);
                listView.SubItems.Add(item.BirthDate.ToString());
                listView.SubItems.Add(item.PhoneNumber);
                listView.SubItems.Add(item.Adress);
                listView.SubItems.Add(item.StartDate.ToString());
                listView.SubItems.Add(item.Email);
                listView.SubItems.Add(item.Title);

                listView.Tag = item;
                listView1.Items.Add(listView);
            }
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            NewPerson();
        }
       

        private void Form1_Load_1(object sender, EventArgs e)
        {
            KisiListesi();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            KisiListesi(txtAra.Text);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Delete();
        }

       
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            UpdatePerson();
        }

        

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            SeePerson();

        }
    }
}
