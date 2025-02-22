using CSEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var customerCollection = new MongoDbConnection().GetCustomerCollection();

            var document = new BsonDocument
            {
                {"CustomerName", customer.CustomerName},
                {"CustomerSurname", customer.CustomerSurname},
                {"CustomerCity", customer.CustomerCity},
                {"CustomerBalance", customer.CustomerBalance},
                {"CustomerShoppingCount", customer.CustomerShoppingCount}
            };
            customerCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerID = customer["_id"].AsObjectId.ToString(),
                    CustomerName = customer["CustomerName"].AsString,
                    CustomerSurname = customer["CustomerSurname"].AsString,
                    CustomerCity = customer["CustomerCity"].AsString,
                    CustomerBalance = customer["CustomerBalance"].AsDecimal,
                    CustomerShoppingCount = customer["CustomerShoppingCount"].AsInt32
                });
            }
            return customerList;
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = new MongoDbConnection().GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(filter);
        }

        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerID));
            var update = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);
            customerCollection.UpdateOne(filter, update);
        }

        public Customer GetCustomerByID(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var customer = customerCollection.Find(filter).FirstOrDefault();
            return new Customer
            {
                CustomerID = customer["_id"].AsObjectId.ToString(),
                CustomerName = customer["CustomerName"].AsString,
                CustomerSurname = customer["CustomerSurname"].AsString,
                CustomerCity = customer["CustomerCity"].AsString,
                CustomerBalance = customer["CustomerBalance"].AsDecimal,
                CustomerShoppingCount = customer["CustomerShoppingCount"].AsInt32
            };
        }
    }
}
