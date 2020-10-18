using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem
{
    interface IContact
    {
        string ContactId { get; set; }
        string ContactType { get; set; }
        string ContactNo { get; set; }
        string EmailAddress { get; set; }
    }
}
