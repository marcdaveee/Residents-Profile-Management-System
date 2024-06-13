using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Mappers
{
    public static class ToResidentFromCreateVm
    {
        public static Resident toResidentFromCreateResidentVm(this CreateResidentViewModel createdResident)
        {
            return new Resident
            {
                Firstname = createdResident.Firstname,
                Middlename = createdResident.Middlename,
                Lastname = createdResident.Lastname,
                Age = createdResident.Age,
                Gender = createdResident.Gender,
                Status = createdResident.Status,
                Birthday = createdResident.Birthday,
                ContactNo = createdResident.ContactNo,
                Email = createdResident.Email
            };
        }
    }
}
