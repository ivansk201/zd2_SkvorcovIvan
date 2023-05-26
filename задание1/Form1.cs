using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace задание1
{
    public partial class Form1 :Form
    {
        private PhoneBook phoneBook;
        private string fileName = "contacts12.txt";
        public Form1 ()
        {
            InitializeComponent( );
            phoneBook = new PhoneBook( );
        }

        private void Form1_Load (object sender, EventArgs e)
        {
            LoadPhoneBook( );
        }
        

        private void button1_Click (object sender, EventArgs e)
        {
            RefreshContactList( );
        }

        private void button2_Click (object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox3.Visible = true;
            string searchName = textBox3.Text.Trim( );
            if (!string.IsNullOrEmpty(searchName))
            {
                List<Contact> searchResults = phoneBook.SearchContacts(searchName);
                listBox1.Items.Clear( );
                foreach (var contact in searchResults)
                {
                    listBox1.Items.Add(contact.Name + " - " + contact.Phone);
                   
                }
                
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите имя для поиска", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click (object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            string name = textBox1.Text.Trim( );
            string phone = textBox2.Text.Trim( );
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone))
            {
                Contact newContact = new Contact { Name = name, Phone = phone };
                phoneBook.AddContact(newContact);
                RefreshContactList( );
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
              
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите имя и номер телефона", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click (object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string selectedContact = listBox1.SelectedItem.ToString( );
                string [ ] contactDetails = selectedContact.Split('-');
                string contactName = contactDetails [ 0 ].Trim( );
                phoneBook.RemoveContact(contactName);
                RefreshContactList( );
            }
            else
            {
                MessageBox.Show("Выберите контакт для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click (object sender, EventArgs e)
        {
            SavePhoneBook( );
        }

        private void button6_Click (object sender, EventArgs e)
        {
            Close( );
        }

        private void Form1_FormClosing (object sender, FormClosingEventArgs e)
        {
            SavePhoneBook( );
        }
        private void LoadPhoneBook ()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    PhoneBookLoader.Load(phoneBook, fileName);
                    RefreshContactList( );
                }
                else
                {
                    MessageBox.Show("Файл телефонной книги не найден.Создание новой телефонной книги", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке телефонной книги: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SavePhoneBook ()
        {
            try
            {
                PhoneBookLoader.Save(phoneBook, fileName);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка при сохранении телефонной книги: " + ex.Message, "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshContactList ()
        {
            listBox1.Items.Clear( );
            foreach (var contact in phoneBook.GetContacts( ))
            {
                listBox1.Items.Add(contact.Name + " - " + contact.Phone);
            }          
        }
    }
}
