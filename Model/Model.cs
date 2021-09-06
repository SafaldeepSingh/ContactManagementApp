using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Contact
    {
        private int? id;
        public int? ID { get { return id; } }
        public string Name { get; set; }
        public long? PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Contact(int? id = null)
        {
                this.id = id;
        }
        public override string ToString()
        {
            return string.Format("ID:{4} \nName: {0} \nPhone No. :{1} \nEmail: {2} \nAddress: {3}\n\n"
                ,this.Name,this.PhoneNo,this.Email,this.Address,this.ID);
        }
    }
    public enum Role
    {
        User = 0,
        Administrator = 1
    }
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
