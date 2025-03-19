using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Enum
{
    public class EnumList
    {
        public enum IsCompleted
        {
            Yes = 1,
            No = 0,
            Pending = 2
        }
        public enum IsActive
        {
            Active = 1,
            Inactive = 0
        }

        public enum AppointmentStatus
        {
            Pending = 1,
            Checked = 2,
            Paid = 3,
            Injected = 4,
            WaitingForResponse = 5,
            Completed = 6,
            Cancelled = 7
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
