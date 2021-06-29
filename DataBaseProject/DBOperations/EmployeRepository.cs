using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsProject;

namespace DataBaseProject.DBOperations
{
    public class EmployeRepository
    {
        public int AddEmploye(EmployeeModel model)
        {
            using (var context = new EmployeDBEntities())
            {
                Employe emp = new Employe()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Code = model.Code,
                };

                if (model.Address != null)
                {
                    emp.Address = new Address()
                    {
                        Details = model.Address.Details,
                        State = model.Address.State,
                        Country = model.Address.Country

                    };
                }
                context.Employe.Add(emp);
                context.SaveChanges();

                return emp.Id;
            }
        }

       public EmployeeModel GetEmploye(int id)
        {
            using (var context = new EmployeDBEntities())
            {
                var result = context.Employe
                    .Where(x=>x.Id ==id)
                    .Select(x => new EmployeeModel()
                {
                    Id = x.Id,
                    AddressID = x.AddressID,
                    Code = x.Code,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = new AddressModel()
                    {
                        Id = x.Address.Id,
                        Details = x.Address.Details,
                        Country = x.Address.Country,
                        State = x.Address.State
                    }

                }).FirstOrDefault();

                return result;
            }
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            using (var context = new EmployeDBEntities())
            {
                var result = context.Employe.Select(x => new EmployeeModel()
                {
                    Id = x.Id,
                    AddressID = x.AddressID,
                    Code = x.Code,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = new AddressModel()
                    {
                        Id = x.Address.Id,
                        Details = x.Address.Details,
                        Country = x.Address.Country,
                        State = x.Address.State
                    }

                }).ToList();

                return result;
            }
        }


        public bool UpdateEmploye(int id,EmployeeModel employeeModel)
        {
            using (var context = new EmployeDBEntities())
            {
                /*
                 //Before Entity State
                var emp = context.Employe.FirstOrDefault(x => x.Id == id);
                if (emp != null)
                {
                    emp.FirstName = employeeModel.FirstName;
                    emp.LastName = employeeModel.LastName;
                    emp.Email = employeeModel.Email;
                    emp.Code = employeeModel.Code;
                }
                context.SaveChanges();
                return true;

                */
                var emp = new Employe();
              
                if (emp != null)
                {
                    emp.Id = employeeModel.Id;
                    emp.FirstName = employeeModel.FirstName;
                    emp.LastName = employeeModel.LastName;
                    emp.Email = employeeModel.Email;
                    emp.Code = employeeModel.Code;
                    emp.AddressID = employeeModel.AddressID;
                }
                context.Entry(emp).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return true;


            }

        }


        public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeDBEntities())
            {
                /*
                 //Without EntityState Code
                var employ = context.Employe.FirstOrDefault(x => x.Id == id);
                if (employ != null)
                {
                    context.Employe.Remove(employ);
                    context.SaveChanges();
                    return true;
                }
                return false;
                */

                //With EntityState

                var emp = new Employe()
                {
                    Id = id
                };
                context.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        

                return false;
            }
        }
    }

