using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace задание1
{
    public class PhoneBook
    {
        private List<Contact> contacts;

        public PhoneBook ()
        {
            contacts = new List<Contact>( );
        }

        public void AddContact (Contact contact)
        {
            contacts.Add(contact);
        }

        public void RemoveContact (string name)
        {
            contacts.RemoveAll(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Contact> GetContacts ()
        {
            return contacts;
        }

        public List<Contact> SearchContacts (string name)
        {
            return contacts.FindAll(c => c.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase)>=0);
        }
    }
}
