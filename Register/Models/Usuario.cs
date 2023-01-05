using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace Register.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String Mobile { get; set; }

        public String Gender { get; set; }

        public String Pwd { get; set; }

        public DateTime MemberSince { get; set; }


    }
}
