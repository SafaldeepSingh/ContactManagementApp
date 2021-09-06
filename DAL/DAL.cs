using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Service
    {
        static char seperator = '^';
        static string contactsPath = "../../contacts.txt";
        static string usersPath = "../../users.txt";
        public static bool AddContact(int? ID,Contact newContact)
        {
            string line = string.Format("{0}{5}{1}{5}{2}{5}{3}{5}{4}\n"
                , newContact.Name,newContact.PhoneNo,newContact.Email,newContact.Address,ID,seperator);
            try
            {
                File.AppendAllText(contactsPath, line);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static Contact[] ReadAllContacts() {
            string[] lines = File.ReadAllLines(contactsPath);
            Contact[] contacts = new Contact[lines.Length];
            for (int i = 0; i < lines.Length; i++) 
            {
                string line = lines[i];
                string[] data = line.Split(seperator);
                contacts[i] = new Contact(int.Parse(data[4]))
                                { Name = data[0], PhoneNo = long.Parse(data[1]), Email = data[2], Address = data[3] };
            }
            return contacts;

        }
        public static bool DeleteContact(int ID)
        {
            string[] lines = File.ReadAllLines(contactsPath);
            string[] newLines = new string[lines.Length - 1];
            Console.WriteLine(lines.Length);
            int i = 0;
            foreach(string line in lines)
            {
                Console.WriteLine("i="+i);
                string[] data = line.Split(seperator);
                if (int.Parse(data[4]) != ID)
                    newLines[i++] = line;
            }
            try
            {
                File.WriteAllLines(contactsPath,newLines);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool EditContact(int? ID,Contact modifiedContact) {

            string[] lines = File.ReadAllLines(contactsPath);
            string[] newLines = new string[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string[] data = line.Split(seperator);
                if (int.Parse(data[4]) != ID)
                    newLines[i++] = line;
                else
                {
                    string modifiedline = string.Format("{0}{5}{1}{5}{2}{5}{3}{5}{4}"
                        , modifiedContact.Name, modifiedContact.PhoneNo, modifiedContact.Email, modifiedContact.Address
                        , data[4], seperator);
                    newLines[i++] = modifiedline;
                }
            }
            try
            {
                File.WriteAllLines(contactsPath, newLines);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static User GetUser(string name, string password)
        {
            string[] lines = File.ReadAllLines(usersPath);
            foreach(string line in lines)
            {
                string[] data = line.Split(seperator);
                if (data[0].CompareTo(name) == 0 && data[1].CompareTo(password) == 0)
                    return new User() { Name = data[0], Password = data[1],Role = (Role)int.Parse(data[2]) };
            }
            return null;
        }
    }
}
