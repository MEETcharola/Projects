using BikeDekho_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BikeDekho_CRUD
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int Register(string name, string email, string password);

        [OperationContract]
        int Login(string email, string password);

        [OperationContract]
        int AddPost(string model, string company, int price, string details, byte[] data, int userId);

        [OperationContract]
        int RemovePost(int id);

        [OperationContract]
        int EditPost(string model, string company, int price, string details, byte[] data, int id);

        [OperationContract]
        List<BikeClass> FetchAllBikes(int userId);

        [OperationContract]
        List<BikeClass> FetchAllUserBikes();

        [OperationContract]
        UserClass GetUser(int userId);

        [OperationContract]
        byte[] GetImage(int id);
    }

    [DataContract]
    public class UserClass
    {
        string name = "";
        string email = "";
        string password = "";

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

    [DataContract]
    public class BikeClass
    {
        int id = -1;
        string model = "";
        string company = "";
        int price = 0;
        string details = "";
        byte[] data;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        [DataMember]
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        [DataMember]
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        [DataMember]
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
