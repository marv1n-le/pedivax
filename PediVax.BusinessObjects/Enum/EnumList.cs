using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Enum
{
    public class EnumList
    {
        public enum IsActive
        {
            Active = 1,
            Inactive = 0
        }

        public enum AppointmentStatus
        {
            Pending = 1,
            WaitingForInjection = 2,
            WaitingForResponse = 3,
            Completed = 4,
            Cancelled = 5
        }

        public enum Gender
        {
            Unknown = 0,
            Male = 1,
            Female = 2
        }

        public enum Relationship
        {
            Mother = 1,
            Father = 2,
            Guardian = 3
        }

        public enum PaymentType
        {
            Cash = 1,
            BankTransfer = 2
        }

        public enum PaymentStatus
        {
            AwaitingPayment = 1, 
            Paid = 2,
            Aborted = 3
        }

        public enum Role
        {
            Admin = 1,
            Doctor = 2,
            Staff = 3,
            Customer = 4
        }
    }
}
