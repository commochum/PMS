using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem
{
     class Person: IContact
    {
        string firstName;
        string lastName;
        DateTime dob;
        int age;
        string gender;
        string addressId;
        string addressType;
        string addressLine1;
       // string addressLine2;
        string postCode;
        string suburb;
        string state;
        string country;
        string medicareNo;
        DateTime recordCreated;
        DateTime recordUpdated;

        string emergencyContactName;
        string emergencyContactNo;
        string relationship;

        protected string contactId;
        protected string contactType;
        protected string contactNo;
        protected string emailAddress;

        public Person()
        {

        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                this.firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                this.lastName = value;
            }
        }
        public DateTime DOB
        {
            get
            {
                return dob;
            }
            set
            {
                this.dob = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                this.age = value;
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                this.gender = value;
            }
        }
        public string MedicareNo
        {
            get
            {
                return medicareNo;
            }
            set
            {
                this.medicareNo = value;
            }
        }
        string IContact.ContactId
        {
            get
            {
                return contactId;
            }
            set
            {
                this.contactId = value;
            }
        }     
        string IContact.ContactType
        {
            get
            {
                return contactType;
            }
            set
            {
                this.contactType = value;
            }

        }
        string IContact.ContactNo
        {
            get
            {
                return contactNo;
            }
            set
            {
                this.contactNo = value;
            }
        }
        string IContact.EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                this.emailAddress = value;
            }
        }

        public string AddressId
        {
            get
            {
                return addressId;
            }
            set
            {
                this.addressId = value;
            }
        }
        public string AddressType
        {
            get
            {
                return addressType;
            }
            set
            {
                this.addressType = value;
            }
        }
        public string AddressLine1
        {
            get
            {
                return addressLine1;
            }
            set
            {
                this.addressLine1 = value;
            }
        }

        public string PostCode
        {
            get
            {
                return postCode;
            }
            set
            {
                this.postCode = value;
            }
        }
        public string Suburb
        {
            get
            {
                return suburb;
            }
            set
            {
                this.suburb = value;
            }
        }
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                this.state = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                this.country = value;
            }
        }

        public DateTime RecordCreated
        {
            get
            {
                return recordCreated;
            }
            set
            {
                this.recordCreated = value;
            }
        }
        public DateTime RecordUpdated
        {
            get
            {
                return recordUpdated;
            }
            set
            {
                this.recordUpdated = value;
            }
        }

        public string EmergencyContactName
        {
            get
            {
                return emergencyContactName;
            }
            set
            {
                this.emergencyContactName = value;
            }
        }
        public string EmergencyContactNo
        {
            get
            {
                return emergencyContactNo;
            }
            set
            {
                this.emergencyContactNo = value;
            }
        }
        public string Relationship
        {
            get
            {
                return relationship;
            }
            set
            {
                this.relationship = value;
            }
        }
    }
}
