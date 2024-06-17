using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Services
{
    public class DataService
    {
        private readonly AppDbContext _dbContext;

        public DataService(AppDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.Include(r => r.Role).ToList();
        }

        public List<Role> GetRoles()
        {
            return _dbContext.Roles.ToList();
        }

        public List<Privilege> GetPrivileges()
        {
            return _dbContext.Privileges.ToList();
        }

        public List<Delivery> GetDeliveries()
        {
            return _dbContext.Deliveries.ToList();
        }

        public List<DeliveryStatusUpdate> GetDeliveryStatusUpdates()
        {
            return _dbContext.DeliveryStatusUpdates.ToList();
        }

        public bool addUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }

            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return false;
            }

            var role = _dbContext.Roles.FirstOrDefault(e => e.Name == "Default");
            if (role != null)
            {
                user.Role = role;
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return true;
        }

        public bool addDelivery(Delivery delivery)
        {
            try
            {
                _dbContext.Deliveries.Add(delivery);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool removeDelivery(Delivery delivery)
        {
            try
            {
                _dbContext.Deliveries.Remove(delivery);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool addRole(Role role)
        {
            try
            {
                _dbContext.Roles.Add(role);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}