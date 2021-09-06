using DAL;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL
{
    public static class Manager
    {
        public static bool AddContact(Contact newContact)
        {
            Contact[] contacts = GetAllContacts();
            int? ID = (contacts.Length > 0 ? contacts[contacts.Length - 1].ID : 0);
            ID++;
            return Service.AddContact(ID, newContact);
        }
        public static Contact[] GetAllContacts()
        {
            return Service.ReadAllContacts();
        }
        public static bool DeleteContact(int ID)
        {
            return Service.DeleteContact(ID);
        }
        public static bool EditContact(int? ID, Contact newContact)
        {
            return Service.EditContact(ID, newContact);
        }
        public static ArrayList SearchContacts(string toSearch, string type)
        {
            ArrayList contactsList = new ArrayList();
            Contact[] contacts = Service.ReadAllContacts();
            foreach (Contact contact in contacts)
            {
                if (type == "ID" && contact.ID.ToString().ToLower().Contains(toSearch.ToLower()))
                    contactsList.Add(contact);
                else if (type == "Name" && contact.Name.ToLower().Contains(toSearch.ToLower()))
                    contactsList.Add(contact);
                else if (type == "Email" && contact.Email.ToLower().Contains(toSearch.ToLower()))
                    contactsList.Add(contact);
                else if (type == "Address" && contact.Address.ToLower().Contains(toSearch.ToLower()))
                    contactsList.Add(contact);
                else if (type == "Phone No" && contact.PhoneNo.ToString().ToLower().Contains(toSearch.ToLower()))
                    contactsList.Add(contact);
                else if (type == "All"
                    && (
                        contact.ID.ToString().ToLower().Contains(toSearch.ToLower())
                        || contact.Name.ToLower().Contains(toSearch.ToLower())
                        || contact.Email.ToLower().Contains(toSearch.ToLower())
                        || contact.Address.ToLower().Contains(toSearch.ToLower())
                        || contact.PhoneNo.ToString().ToLower().Contains(toSearch.ToLower())
                      )
                    )
                    contactsList.Add(contact);

            }
            return contactsList;
        }
        public static Contact[] SortContacts(Contact[] contacts, string type)
        {
            switch (type)
            {
                case "Name: A-Z":
                    Array.Sort(contacts, (a, b) => a.Name.CompareTo(b.Name));
                    break;
                case "Name: Z-A":
                    Array.Sort(contacts, (a, b) => b.Name.CompareTo(a.Name));
                    break;
                case "Phone No: Ascending":
                    Array.Sort(contacts, (a, b) => a.PhoneNo.ToString().CompareTo(b.PhoneNo.ToString()));
                    break;
                case "Phone No: Descending":
                    Array.Sort(contacts, (a, b) => b.PhoneNo.ToString().CompareTo(a.PhoneNo.ToString()));
                    break;
                case "Email: A-Z":
                    Array.Sort(contacts, (a, b) => a.Email.CompareTo(b.Email));
                    break;
                case "Email: Z-A":
                    Array.Sort(contacts, (a, b) => b.Email.CompareTo(a.Email));
                    break;
                case "Address: A-Z":
                    Array.Sort(contacts, (a, b) => a.Address.CompareTo(b.Address));
                    break;
                case "Address: Z-A":
                    Array.Sort(contacts, (a, b) => b.Address.CompareTo(a.Address));
                    break;
            }
            return contacts;
        }

        public static User GetUser(string name, string password)
        {
            return Service.GetUser(name, password);
        }
    }
}
