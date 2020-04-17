using BikeDekho_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BikeDekho_CRUD
{
    public class Service : IService
    {
        DatabaseContext Db = new DatabaseContext();

        public int Register(string name, string email, string password)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Db.Database.Connection.ConnectionString;
            User user = new User()
            {
                UserName = name,
                UserEmail = email,
                UserPassword = password
            };
            try
            {
                Db.Users.Add(user);
                Db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int Login(string email, string password)
        {
            try
            {
                User user = Db.Users.FirstOrDefault(x => x.UserEmail == email && x.UserPassword == password);
                if (user == null)
                {
                    return -1;
                }
                return user.UserId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int AddPost(string model, string company, int price, string details, byte[] data, int userId)
        {
            User user = Db.Users.FirstOrDefault(x => x.UserId == userId);
            try
            {
                Bike bike = new Bike()
                {
                    BikeModel = model,
                    BikeCompany = company,
                    BikePrice = price,
                    BikeDetails = details,
                    BikePhoto = data,
                    User = user
                };
                Db.Bikes.Add(bike);
                Db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;
        }

        public int RemovePost(int id)
        {
            try
            {
                Db.Bikes.Remove(Db.Bikes.Find(id));
                Db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;
        }

        public int EditPost(string model, string company, int price, string details, byte[] data, int id)
        {
            var bike = Db.Bikes.Where(x => x.BikeId == id).FirstOrDefault();
            if(bike != null)
            {
                bike.BikeModel = model;
                bike.BikeCompany = company;
                bike.BikePrice = price;
                bike.BikeDetails = details;
                bike.BikePhoto = data;
                Db.Entry(bike).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return -1;
        }

        public List<BikeClass> FetchAllBikes(int userId)
        {
            User user = Db.Users.Include("Bikes").FirstOrDefault(x => x.UserId == userId);
            List<BikeClass> bikeList = new List<BikeClass>();
            foreach (Bike bike in user.Bikes)
            {
                bikeList.Add(new BikeClass()
                {
                    Id = bike.BikeId,
                    Model = bike.BikeModel,
                    Company = bike.BikeCompany,
                    Price = bike.BikePrice,
                    Details = bike.BikeDetails,
                    Data = null
                });
            }
            return (bikeList);
        }

        public List<BikeClass> FetchAllUserBikes()
        {
            var bikes = Db.Bikes.ToList();
            List<BikeClass> bikeList = new List<BikeClass>();
            foreach (Bike bike in bikes)
            {
                bikeList.Add(new BikeClass()
                {
                    Id = bike.BikeId,
                    Model = bike.BikeModel,
                    Company = bike.BikeCompany,
                    Price = bike.BikePrice,
                    Details = bike.BikeDetails,
                    Data = null
                });
            }
            return (bikeList);
        }

        public UserClass GetUser(int userId)
        {
            User user = Db.Users.Find(userId);
            return new UserClass()
            {
                Name = user.UserName,
                Email = user.UserEmail,
                Password = user.UserPassword
            };
        }

        public byte[] GetImage(int id)
        {
            Bike bike = Db.Bikes.Find(id);
            byte[] data = bike.BikePhoto;
            return (data);
        }
    }
}
